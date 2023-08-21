using com.clover.remote.order;
using com.clover.remote.order.operation;
using com.clover.remotepay.sdk;
using com.clover.remotepay.transport;
using com.clover.sdk.v3.payments;
using RICH_Connector.API.Model;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using TipMode = com.clover.remotepay.sdk.TipMode;

namespace RICH_Connector.Clover
{
    public sealed class CloverClient
    {
        private static int WAITING_TIMEOUT = 120;
        private static string MESSAGE_NOT_CONNECTED_CLOVER = "The Clover device is not connected.";
        private CloverClient()
        {

        }

        private static readonly Lazy<CloverClient> lazy = new Lazy<CloverClient>(() => new CloverClient());

        public static CloverClient Instance
        {
            get { return lazy.Value; }
        }

        private ICloverConnector cloverConnector;

        private CloverListener ccl;
        private String DEV_RAID = "4EFVTVHA6NA56.371F5GG0127ZR";
        private String PROD_RAID = "4EFVTVHA6NA56.371F5GG0127ZR";
        private String deviceId = "";
        private String deviceName = "";


        public void Init(String deviceId, String deviceName)
        {
            string RAID = DEV_RAID;
#if !DEBUG
            RAID = PROD_RAID;
#endif

            if (deviceId == null || deviceId == "")
            {
                Console.WriteLine("Init failed. DeviceId is empty");
                throw CloverException.InitIsFailed;
            }

            this.deviceId = deviceId;
            this.deviceName = deviceName;

            if (cloverConnector != null)
            {
                return;
                // TODO: 2 lines below make connector is freezed. 
                // If unplug Clover to PC
                // Then try to call init api several times
                cloverConnector.RemoveCloverConnectorListener(this.ccl);
                cloverConnector.Dispose();
            }

            var usbConfig = new USBCloverDeviceConfiguration(deviceId, RAID, true, 1);
            cloverConnector = CloverConnectorFactory.createICloverConnector(usbConfig);
            this.ccl = new CloverListener(cloverConnector);
            cloverConnector.AddCloverConnectorListener(ccl);
            cloverConnector.InitializeConnection();

            Console.WriteLine("Init Clover Instance");
        }

        public void ShowMessage(String msg)
        {
            if (this.cloverConnector == null)
            {
                throw new Exception(MESSAGE_NOT_CONNECTED_CLOVER);
            }
            this.cloverConnector.ShowMessage(msg);
        }

        public void Reset()
        {
            if (this.deviceId == null || this.deviceId == "")
            {
                Console.WriteLine("Init failed. DeviceId is empty");
                throw new Exception(MESSAGE_NOT_CONNECTED_CLOVER);
            }
            this.cloverConnector.Dispose();
            this.ccl = null;
            this.Init(this.deviceId, this.deviceName);
        }

        public void CheckConnection()
        {
            int count = 0;
            if (ccl == null)
            {                        
                throw CloverException.NotReadyYet;
            }
            while (!ccl.deviceReady && count <= 1)
            {
                Thread.Sleep(100);
                count++;
            }

            if (!ccl.deviceReady)
            {
                throw CloverException.NotReadyYet;
            }
        }

        private bool isClientCancelRequest(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                this.cloverConnector.ResetDevice();
                return true;
            }

            return false;
        }

        public Payment CreateSale(RICH_Connector.API.Model.SaleRequest request, CancellationToken? cancellationToken)
        {
            Console.WriteLine("New sale request");
            ccl.authResponse = null;
            ccl.paymentResponse = null;
            this.ClearCurrentPaymentResponse();
            this.preValidationSaleRequest(request);       
          
            if (request.TipSettings == "onTabletScreen")
            {
                var pendingSale = new com.clover.remotepay.sdk.SaleRequest();
                pendingSale.ExternalId = request.Id;
                pendingSale.Amount = request.Amount;
                pendingSale.TaxAmount = request.Tax;
                pendingSale.TipAmount = request.Tip;
                pendingSale.DisablePrinting = request.DisablePrinting;
                pendingSale.DisableReceiptSelection = request.DisableReceiptSelection;
                pendingSale.TipMode = TipMode.ON_SCREEN_BEFORE_PAYMENT;
                pendingSale.AutoAcceptSignature = true;
                pendingSale.AutoAcceptPaymentConfirmations = true;
                pendingSale.DisableDuplicateChecking = true;
                pendingSale.AllowOfflinePayment = false;
                pendingSale.TippableAmount = request.TippableAmount;
                var retrieveDeviceStt = new RetrieveDeviceStatusRequest();
                cloverConnector.RetrieveDeviceStatus(retrieveDeviceStt);

                cloverConnector.Sale(pendingSale);

                int timeCount = 1;
                while (ccl.paymentResponse == null && timeCount < WAITING_TIMEOUT && this.ccl.IsPaymentOnlineSuccess == null)
                {
                    if (cancellationToken.HasValue && isClientCancelRequest(cancellationToken.Value))
                    {
                        Console.WriteLine("Client cancel!");
                        return null;
                    }
                    Thread.Sleep(1000);
                    timeCount++;
                }
                var payment = ccl.paymentResponse;

                if (payment == null)
                {
                    cloverConnector.ResetDevice();
                    if (this.ccl.IsPaymentOnlineSuccess == false)
                    {
                        this.ccl.ResetState();
                        throw CloverException.PaymentOnlineIsFailed;
                    }
                    throw CloverException.PaymentNotSuccess;
                }

                if (payment.Result == ResponseCode.CANCEL)
                {
                    cloverConnector.ResetDevice();
                    this.ccl.ResetState();
                    throw CloverException.PaymentCancel;
                }

                if (!payment.Success)
                {
                    cloverConnector.ResetDevice();
                    this.ccl.ResetState();
                    throw CloverException.PaymentNotSuccess;
                }

                return payment.Payment;
            }
            else
            {
                var pendingSale = new com.clover.remotepay.sdk.AuthRequest();
                pendingSale.ExternalId = request.Id;
                pendingSale.Amount = request.Amount;
                pendingSale.TaxAmount = request.Tax;             
                pendingSale.DisablePrinting = request.DisablePrinting;
                pendingSale.DisableReceiptSelection = request.DisableReceiptSelection;             
                pendingSale.AutoAcceptSignature = true;
                pendingSale.AutoAcceptPaymentConfirmations = true;
                pendingSale.DisableDuplicateChecking = true;
                pendingSale.AllowOfflinePayment = false;
                pendingSale.TippableAmount = request.TippableAmount;
                var retrieveDeviceStt = new RetrieveDeviceStatusRequest();
                cloverConnector.RetrieveDeviceStatus(retrieveDeviceStt);
                cloverConnector.Auth(pendingSale);

                int timeCount = 1;
                while (ccl.authResponse == null && timeCount < WAITING_TIMEOUT && this.ccl.IsPaymentOnlineSuccess == null)
                {
                    if (cancellationToken.HasValue && isClientCancelRequest(cancellationToken.Value))
                    {
                        Console.WriteLine("Client cancel!");
                        return null;
                    }
                    Thread.Sleep(1000);
                    timeCount++;
                }
                var payment = ccl.authResponse;

                if (payment == null)
                {
                    cloverConnector.ResetDevice();
                    if (this.ccl.IsPaymentOnlineSuccess == false)
                    {
                        this.ccl.ResetState();
                        ccl.authResponse = null;
                        throw CloverException.PaymentOnlineIsFailed;
                    }
                    throw CloverException.PaymentNotSuccess;
                }

                if (payment.Result == ResponseCode.CANCEL)
                {
                    cloverConnector.ResetDevice();
                    ccl.authResponse = null;
                    this.ccl.ResetState();
                    throw CloverException.PaymentCancel;
                }

                if (!payment.Success)
                {
                    cloverConnector.ResetDevice();
                    ccl.authResponse = null;
                    this.ccl.ResetState();
                    throw CloverException.PaymentNotSuccess;
                }

                return payment.Payment;
            }      
        }

        public void ClearCurrentPaymentResponse()
        {
            this.ccl.paymentResponse = null;
        }

        private void preValidationSaleRequest(RICH_Connector.API.Model.SaleRequest saleRequest)
        {
            if (saleRequest == null)
            {
                throw CloverException.SaleRequestIsEmpty;
            }

            if (saleRequest.Amount <= 0)
            {
                throw CloverException.AmountIsNotValid;
            }
        }

        public void Print(string fileName)
        {
            Bitmap bitmap;
            using (Stream bmpStream = System.IO.File.Open(fileName, System.IO.FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);

                bitmap = new Bitmap(image);

            }
            cloverConnector.Print(new PrintRequest(bitmap, Utils.GenerateRandomString(16), null));
        }

        public RefundPaymentResponse TransactionRefund(RefundRequest request)
        {
            var refundRequest = new com.clover.remotepay.sdk.RefundPaymentRequest();
            refundRequest.PaymentId = request.PaymentId;
            refundRequest.OrderId = request.OrderId;
            refundRequest.FullRefund = request.FullRefund;
            refundRequest.DisableReceiptSelection = request.DisableReceiptSelection;
            refundRequest.DisablePrinting = request.DisablePrinting;

            if (!request.FullRefund)
            {
                refundRequest.Amount = request.Amount;

            }
            cloverConnector.RefundPayment(refundRequest);

            int timeCount = 1;
            while (ccl.refundPaymentResponse == null && timeCount < WAITING_TIMEOUT)
            {
                Thread.Sleep(1000);
                timeCount++;
            }
            var refundPayment = ccl.refundPaymentResponse;

            if (refundPayment == null)
            {
                cloverConnector.ResetDevice();
                throw CloverException.RefundPaymentTimeout;
            }

            if (!refundPayment.Success)
            {
                if (refundPayment.Message.Contains("Could not find order for orderId"))
                {
                    throw CloverException.OrderIsNotExisted;
                }
                if (refundPayment.Message.Contains("total refunded amount greater than the original"))
                {
                    throw CloverException.TotalAmountIsGreaterThanOriginal;
                }
                if (refundPayment.Message.Contains("PAYMENT_NOT_FOUND"))
                {
                    throw CloverException.PaymentIsNotExisted;
                }
                throw new CloverException(refundPayment.Message);
            }

            return refundPayment;

        }

        public bool CancelPendingSale()
        {

            //cloverConnector.RetrievePendingPayments();
            //Thread.Sleep(20000);

            cloverConnector.ResetDevice();
            return true;
        }

        public void Disconnect()
        {
            if (this.cloverConnector == null) return;
            this.cloverConnector.Dispose();
            this.cloverConnector = null;
            this.ccl = null;
        }

        public void OpenCashDrawer(string reason)
        {
            if (this.cloverConnector == null) return;
            var request = new OpenCashDrawerRequest(reason);

            this.cloverConnector.OpenCashDrawer(request);

        }

        public void Ping()
        {
            RetrieveDeviceStatusRequest request = new RetrieveDeviceStatusRequest();
            this.cloverConnector.RetrieveDeviceStatus(request);
        }

        public TipAdjustAuthResponse AddTip(AddTipRequest request)
        {
            // create a TipAdjustAuthRequest object with the payment ID and tip amount
            TipAdjustAuthRequest tipAdjustAuthRequest = new TipAdjustAuthRequest();
            tipAdjustAuthRequest.PaymentID = request.PaymentId;
            tipAdjustAuthRequest.OrderID = request.OrderId;
            tipAdjustAuthRequest.TipAmount = request.Amount;
       
            // call the TipAdjustAuth method to charge the tip
            this.cloverConnector.TipAdjustAuth(tipAdjustAuthRequest);
            int timeCount = 1;
            while (ccl.tipAdjustAuthResponse == null && timeCount < WAITING_TIMEOUT)
            {
                Thread.Sleep(1000);
                timeCount++;
            }
            var response = ccl.tipAdjustAuthResponse;

            if (response == null)
            {
                cloverConnector.ResetDevice();
                ccl.tipAdjustAuthResponse = null;
                throw CloverException.RefundPaymentNotSuccess;
            }

            if (!response.Success)
            {
                ccl.tipAdjustAuthResponse = null;
                throw new CloverException(response.Message);
            }

            ccl.tipAdjustAuthResponse = null;
            return response;
        }

        public CloseoutResponse Closeout(CloseoutRequest request)
        {
            this.cloverConnector.Closeout(request);
            int timeCount = 1;
            while (ccl.closeoutResponse == null && timeCount < WAITING_TIMEOUT)
            {
                Thread.Sleep(1000);
                timeCount++;
            }
            var res = ccl.closeoutResponse;

            if (res == null)
            {
                cloverConnector.ResetDevice();
                throw CloverException.CloseoutTimeout;
            }

            if (!res.Success)
            {
                ccl.closeoutResponse = null;
                throw new CloverException(res.Message);
            }

            return res;
        }

        public void OrderUpdate(DisplayOrder order)
        {
            if (this.cloverConnector == null)
            {
                throw new Exception(MESSAGE_NOT_CONNECTED_CLOVER);
            }
            else if(order == null)
            {
                throw new Exception("Order is null");
            }

            this.cloverConnector.ShowDisplayOrder(order);
        }

        public void OrderRemove(DisplayOrder order)
        {
            if (this.cloverConnector == null)
            {
                throw new Exception(MESSAGE_NOT_CONNECTED_CLOVER);
            }
            else if(order == null)
            {
                throw new Exception("Order is null");
            }

            this.cloverConnector.RemoveDisplayOrder(order);
        }

        public void ShowWelcomeScreen()
        {
            if (this.cloverConnector == null)
            {
                throw new Exception(MESSAGE_NOT_CONNECTED_CLOVER);
            }
            this.cloverConnector.ShowWelcomeScreen();
        }

        public void ShowThankyouScreen()
        {
            if (this.cloverConnector == null)
            {
                throw new Exception(MESSAGE_NOT_CONNECTED_CLOVER);
            }
            this.cloverConnector.ShowThankYouScreen();
        }
    }

}
