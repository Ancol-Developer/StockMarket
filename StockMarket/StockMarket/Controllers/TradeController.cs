using Microsoft.AspNetCore.Mvc;

namespace StockMarket.Controllers
{
    public class TradeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
