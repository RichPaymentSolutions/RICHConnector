using com.clover.remotepay.sdk;
using System;
using System.Collections.Generic;
using System.Text;
using static com.clover.remotepay.sdk.CloverDeviceEvent;

namespace RICH_Connector.Clover
{
    public class CloverListener
 : DefaultCloverConnectorListener
    {

        public Boolean deviceReady { get; set; }
        public Boolean deviceConnected { get; set; }
        public Boolean saleDone { get; set; }
        public Boolean refundDone { get; set; }

        public String externalId { get; set; }
        public String paymentId { get; set; }
        public String orderId { get; set; }

        public Boolean? IsPaymentOnlineSuccess { get; set; }
        private DeviceEventState? lastDeviceEvent;

        public PaymentResponse paymentResponse { get; set; }
        public RefundPaymentResponse refundPaymentResponse { get; set; }
        public VoidPaymentResponse voidPaymentResponse { get; set; }
        public TipAdjustAuthResponse tipAdjustAuthResponse { get; set; }
        public AuthResponse authResponse { get; set; }
        public CloseoutResponse closeoutResponse { get; set; }
        private Action<MerchantInfo> onDeviceReady;
        private Action onDeviceConnected;
        private Action onDeviceDisconnected;
        private Action<ConfirmPaymentRequest> onConfirmPaymentRequest;

        public CloverListener(ICloverConnector cloverConnector) : base(cloverConnector) { }

        public CloverListener(ICloverConnector cloverConnector,
            Action<MerchantInfo> onDeviceReady,
            Action onDeviceConnected, Action onDeviceDisconnected,
            Action<ConfirmPaymentRequest> onConfirmPaymentRequest
        ) : base(cloverConnector)
        {
            this.onDeviceReady = onDeviceReady;
            this.onDeviceConnected = onDeviceConnected;
            this.onDeviceDisconnected = onDeviceDisconnected;
            this.onConfirmPaymentRequest = onConfirmPaymentRequest;
        }

        public override void OnDeviceReady(MerchantInfo merchantInfo)
        {
            base.OnDeviceReady(merchantInfo);
            // this.onDeviceReady(merchantInfo);
            this.deviceReady = true;
            //Connected and available to process requests
        }

        public override void OnDeviceConnected()
        {
            base.OnDeviceConnected();
            // this.onDeviceConnected();
            this.deviceConnected = true;
            // Connected, but not available to process requests

        }

        public override void OnDeviceDisconnected()
        {
            base.OnDeviceDisconnected();
            Console.WriteLine("Disconnected");
            this.deviceConnected = false;
            this.deviceReady = false;   
            //Disconnected
        }

        public override void OnConfirmPaymentRequest(ConfirmPaymentRequest request)
        {
            // this.onConfirmPaymentRequest(request);
        }

        public void OnSaleRequest(SaleRequest saleRequest)
        {
            this.externalId = saleRequest.ExternalId;
        }

        public override void OnSaleResponse(SaleResponse response)
        {
            base.OnSaleResponse(response);
            this.paymentResponse = response;
        }

        public override void OnRefundPaymentResponse(RefundPaymentResponse response)
        {
            base.OnRefundPaymentResponse(response);
            this.refundPaymentResponse = response;
        }

        public override void OnVoidPaymentResponse(VoidPaymentResponse response)
        {
            base.OnVoidPaymentResponse(response);
            this.voidPaymentResponse = response;
        }

        public override void OnRetrieveDeviceStatusResponse(RetrieveDeviceStatusResponse response)
        {
            Console.WriteLine("[INF] Device status: " + response.State);
            base.OnRetrieveDeviceStatusResponse(response);
        }

        public override void OnDeviceActivityStart(CloverDeviceEvent response)
        {
            Console.WriteLine("[INF] Activity start: " + response.EventState + ": " + response.Message);
            if (this.lastDeviceEvent.Equals(DeviceEventState.PROCESSING_GO_ONLINE) && response.EventState.Equals(DeviceEventState.FAILED))
            {
                this.IsPaymentOnlineSuccess = false;
            }

            base.OnDeviceActivityStart(response);
        }

        public override void OnDeviceActivityEnd(CloverDeviceEvent response)
        {
            Console.WriteLine("[INF] Activity end: " + response.EventState + ": " + response.Message);
            base.OnDeviceActivityEnd(response);

            this.lastDeviceEvent = response.EventState;
        }

        public override void OnDeviceError(CloverDeviceErrorEvent response)
        {
            Console.WriteLine("[ERR] Device error: " + response.ErrorType + ": " + response.Message);
            base.OnDeviceError(response);
        }

        public void ResetState()
        {
            this.lastDeviceEvent = null;
            this.IsPaymentOnlineSuccess = null;
        }
        public override void OnTipAdjustAuthResponse(TipAdjustAuthResponse response)
        {
            base.OnTipAdjustAuthResponse(response);
            this.tipAdjustAuthResponse = response;
        }

        public override void OnAuthResponse(AuthResponse response)
        {
            base.OnAuthResponse(response);
            this.authResponse = response;
        }

        public override void OnCloseoutResponse(CloseoutResponse response)
        {
            base.OnCloseoutResponse(response);
            this.closeoutResponse = response;
        }
    }
}
