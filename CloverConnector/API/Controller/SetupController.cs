using Microsoft.AspNetCore.Mvc;
using RICH_Connector.API.Model;
using RICH_Connector.Clover;
using System;
using System.Collections.Generic;
using System.Text;

namespace RICH_Connector.API.Controller
{
    [Route("clover")]
    [ApiController]
    public class CloverController
    {
        [HttpPost]
        [Route("init")]
        public IActionResult InitClover([FromBody] InitialCloverRequest initRequest)
        {
            CloverClient.Instance.Init(initRequest.DeviceId, initRequest.DeviceName);

            return new ObjectResult(new
            {
                status = true,
            })
            {
                StatusCode = 200,
            };
        }

        [HttpPost]
        [Route("reset")]
        public IActionResult Reset()
        {
            CloverClient.Instance.Reset();
            return new ObjectResult(new
            {
                status = true,
            })
            {
                StatusCode = 200,
            };
        }

        [HttpPost]
        [Route("test")]
        public IActionResult Test()
        {
            CloverClient.Instance.CheckConnection();
            return new ObjectResult(new
            {
                status = true,
            })
            {
                StatusCode = 200,
            };
        }

        [HttpPost]
        [Route("disconnect")]
        public IActionResult Disconnnect()
        {
            CloverClient.Instance.Disconnect();
            return new ObjectResult(new
            {
                status = true,
            })
            {
                StatusCode = 200,
            };
        }
    }
}
