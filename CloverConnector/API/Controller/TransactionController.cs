using com.clover.remotepay.sdk;
using Microsoft.AspNetCore.Mvc;
using RICH_Connector.API.Model;
using RICH_Connector.Clover;
using System;
using System.Collections.Generic;
using System.Text;

namespace RICH_Connector.API.Controller
{
    public class TransactionController
    {
        [HttpPost]
        [Route("transactions/refund")]
        public IActionResult TransactionRefund([FromBody] RefundRequest txnRefundRequest)
        {
            CloverClient.Instance.CheckConnection();
            var result = CloverClient.Instance.TransactionRefund(txnRefundRequest);
            return new ObjectResult(new
            {
                status = true,
                data = result
            })
            {
                StatusCode = 200,
            };
        }

        [HttpPost]
        [Route("transactions/add-tip")]
        public IActionResult TransactionAddTip([FromBody] AddTipRequest addTipRequest)
        {
            CloverClient.Instance.CheckConnection();
            var result = CloverClient.Instance.AddTip(addTipRequest);
            return new ObjectResult(new
            {
                status = true,
                data = result
            })
            {
                StatusCode = 200,
            };
        }

        [HttpPost]
        [Route("transactions/closeout")]
        public IActionResult Closeout([FromBody] CloseoutRequest closeoutRequest)
        {
            CloverClient.Instance.CheckConnection();
            var result = CloverClient.Instance.Closeout(closeoutRequest);
            return new ObjectResult(new
            {
                status = true,
                data = result
            })
            {
                StatusCode = 200,
            };
        }
    }
}
