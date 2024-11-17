using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using ServiceContracks;
using ServiceContracks.DTO;
using StockMarket.Filters;
using StockMarket.Models;
using System.Collections.Generic;

namespace StockMarket.Controllers
{
    public class TradeController : Controller
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinnhubService _finnhubService;
        private readonly IStocksService _stocksService;
        private readonly IConfiguration _configuration;

        // Logger
        private readonly ILogger<TradeController> _logger;

        /// <summary>
        /// Constructor for TradeController that executes when a new object is created for the class
        /// </summary>
        /// <param name="tradingOptions">Injecting TradeOptions config through Options pattern</param>
        /// <param name="stocksService">Injecting StocksService</param>
        /// <param name="finnhubService">Injecting FinnhubService</param>
        /// <param name="configuration">Injecting IConfiguration</param>
        public TradeController(IOptions<TradingOptions> tradingOptions, IFinnhubService finnhubService,IStocksService stocksService ,IConfiguration configuration, ILogger<TradeController> logger)
        {
            _tradingOptions = tradingOptions.Value;
            _finnhubService = finnhubService;
            _stocksService = stocksService;
            _configuration = configuration;
            _logger = logger;
        }

        [Route("~/[controller]/[action]/{stockSymbol}")]
        [Route("~/[controller]/{stockSymbol}")]
        public async Task<IActionResult> Index(string stockSymbol)
        {
            // Logger
            _logger.LogInformation("In TradeController.Index() action method");
            _logger.LogDebug("strockSymbol: {stockSymbol}", stockSymbol);

            // reset Stock symbol if not exists
            if (string.IsNullOrEmpty(stockSymbol))
                stockSymbol = "MSFT";

            //get company profile from API server
            Dictionary<string, object>? companyProfileDictionary = await _finnhubService.GetCompanyProfile(stockSymbol);

            //get stock price quotes from API server
            Dictionary<string, object>? stockQuoteDictionary = await _finnhubService.GetStockPriceQuote(stockSymbol);

            //create model object
            StockTrade stockTrade = new StockTrade() { StockSymbol = stockSymbol };

            //load data from finnHubService into model object
            if (companyProfileDictionary != null && stockQuoteDictionary != null)
            {
                stockTrade = new StockTrade() { StockSymbol = Convert.ToString(companyProfileDictionary["ticker"]), StockName = Convert.ToString(companyProfileDictionary["name"]),Quantity = _tradingOptions.DefaultOrderQuantity ?? 0, Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString()) };
            }

            //Send Finnhub token to view
            ViewBag.FinnhubToken = _configuration["FinnhubToken"];

            return View(stockTrade);
        }

        [Route("[action]")]
        [HttpPost]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            // Update date of model
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            // re-validate the model object after updating the date
            ModelState.Clear();
            TryValidateModel(buyOrderRequest);

            if (!ModelState.IsValid)
            {
                ViewBag.Error = ModelState.Values.SelectMany(x => x.Errors).Select(y => y.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade()
                {
                    StockName = buyOrderRequest.StockName,
                    Quantity = buyOrderRequest.Quantity,
                    StockSymbol = buyOrderRequest.StockSymbol,
                };

                return View("Index", stockTrade);
            }

            //invoke service method
            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);

            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        [HttpPost]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
        {
            // Update date of model
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            // re-validate the model object after updating the date
            ModelState.Clear();
            TryValidateModel(sellOrderRequest);

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade() { StockName = sellOrderRequest.StockName, Quantity = sellOrderRequest.Quantity, StockSymbol = sellOrderRequest.StockSymbol };
                return View("Index", stockTrade);
            }

            //invoke service method
            SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);

            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        public async Task<IActionResult> Orders()
        {
            List<BuyOrderResponse> buyOrderResponses = await _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrderResponses = await _stocksService.GetSellOrders();

            Orders orders = new Orders()
            {
                BuyOrders = buyOrderResponses,
                SellOrders = sellOrderResponses
            };

            ViewBag.TraddingOptions = _tradingOptions;

            return View(orders);
        }

        [Route("OrdersPDF")]
        public async Task<IActionResult> OrdersPDF()
        {
            //Get list of orders
            List<IOrderResponse> orders = new List<IOrderResponse>();
            orders.AddRange(await _stocksService.GetBuyOrders());
            orders.AddRange(await _stocksService.GetSellOrders());
            orders = orders.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToList();

            ViewBag.TradingOptions = _tradingOptions;

            //Return view as pdf
            return new ViewAsPdf("OrdersPDF", orders, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Right = 20, Bottom = 20, Left = 20 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }
    }
}
