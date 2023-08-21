using com.clover.remote.order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.PointOfService;
using RICH_Connector.Clover;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;

namespace RICH_Connector.API
{
    public class CloverDisplayOrderController
    {
        [HttpPost]
        [Route("show-display-order")]
        public IActionResult ShowDisplayOrder([FromBody] DisplayOrder order)
        {
            try
            {
                CloverClient.Instance.OrderUpdate(order);

                return new ObjectResult(new
                {
                    status = true,

                })
                {
                    StatusCode = 200,
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new
                {
                    status = false,
                })
                {
                    StatusCode = 500,
                    Value = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("remove-display-order")]
        public IActionResult RemoveDisplayOrder([FromBody] DisplayOrder order)
        {
            try
            {
                CloverClient.Instance.OrderRemove(order);

                return new ObjectResult(new
                {
                    status = true,

                })
                {
                    StatusCode = 200,
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new
                {
                    status = false,
                })
                {
                    StatusCode = 500,
                    Value = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("show-welcome-screen")]
        public IActionResult ShowWelcomeScreen()
        {
            try
            {
                CloverClient.Instance.ShowWelcomeScreen();

                return new ObjectResult(new
                {
                    status = true,

                })
                {
                    StatusCode = 200,
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new
                {
                    status = false,
                })
                {
                    StatusCode = 500,
                    Value = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("reset-connect-clover")]
        public IActionResult ResetConnectClover()
        {
            try
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
            catch (System.Exception ex)
            {
                return new ObjectResult(new
                {
                    status = false,
                })
                {
                    StatusCode = 500,
                    Value = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("show-message-screen")]
        public IActionResult ShowMessageScreen(string message)
        {
            try
            {
                CloverClient.Instance.ShowMessage(message);

                return new ObjectResult(new
                {
                    status = true,

                })
                {
                    StatusCode = 200,
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new
                {
                    status = false,
                })
                {
                    StatusCode = 500,
                    Value = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("show-thank-screen")]
        public IActionResult ShowThankScreen()
        {
            try
            {
                CloverClient.Instance.ShowThankyouScreen();

                return new ObjectResult(new
                {
                    status = true,

                })
                {
                    StatusCode = 200,
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new
                {
                    status = false,
                })
                {
                    StatusCode = 500,
                    Value = ex.Message
                };
            }
        }
    }
}
