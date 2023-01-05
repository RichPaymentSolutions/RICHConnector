using System;
using System.Collections.Generic;
using System.Text;

namespace RICH_Connector.Clover
{
    public class CloverException : Exception
    {
        private String message;

        public CloverException(string message) : base(message)
        {
            this.message = message;
        }

        public static CloverException InitIsFailed = new CloverException("InitIsFailed");
        public static CloverException NotReadyYet = new CloverException("NotReadyYet");
        public static CloverException PaymentOnlineIsFailed = new CloverException("PaymentOnlineIsFailed");
        public static CloverException PaymentNotSuccess = new CloverException("PaymentNotSuccess");
        public static CloverException SaleRequestIsEmpty = new CloverException("SaleRequestIsEmpty");
        public static CloverException PaymentCancel = new CloverException("UserCancel");
        public static CloverException AmountIsNotValid = new CloverException("AmountIsNotValid");
        public static CloverException PaymentTimeout = new CloverException("PaymentTimeout");
        public static CloverException ProcessingAnotherPayment = new CloverException("ProcessingAnotherPayment");
        public static CloverException RefundPaymentTimeout = new CloverException("RefundPaymentTimeout");
        public static CloverException RefundPaymentNotSuccess = new CloverException("RefundPaymentNotSuccess");
        public static CloverException OrderIsNotExisted = new CloverException("OrderIsNotExisted");
        public static CloverException TotalAmountIsGreaterThanOriginal = new CloverException("TotalAmountIsGreaterThanOriginal");
        public static CloverException PaymentIsNotExisted = new CloverException("PaymentIsNotExisted");



    }
}
