using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Models;

namespace StockMarket.Controllers
{
    public class HomeController : Controller
    {
        [Route("~/[controller]/Error")]
        public IActionResult Error()
        {
            IExceptionHandlerPathFeature? exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature != null && exceptionHandlerPathFeature.Error != null)
            {
                Error error = new Error() { ErrorMessage = exceptionHandlerPathFeature.Error.Message };
                return View(error);
            }
            else
            {
                Error error = new Error() { ErrorMessage = "Error encountered" };
                return View(error);
            }
        }
    }
}
