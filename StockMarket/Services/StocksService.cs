using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracks;
using ServiceContracks.DTO;
using Services.Helper;
using StockMarket.Entities;

namespace Services;

public class StocksService : IStocksService
{
    // Private field
    private readonly IStockRepository _stockRepository;

    /// <summary>
    /// Constructor of StocksService class that executes when a new object is created for the class
    /// </summary>
    public StocksService(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
    {
        // Validation 
        if(buyOrderRequest is null)
            throw new ArgumentNullException(nameof(buyOrderRequest));

        // Model Validation
        ValidationHelper.ModelValidation(buyOrderRequest);

        // Convert buyOrderRequest to BuyOrder
        BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

        // Generate BuyOrderId
        buyOrder.BuyOrderID = Guid.NewGuid();

        // add buy order object to buy order list
        BuyOrder buyOrderFromRepo = await _stockRepository.CreateBuyOrder(buyOrder);

        return buyOrder.ToBuyOrderResponse();
    }

    public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
    {
        // Validate SellOrderRequest 
        if (sellOrderRequest is null)
            throw new ArgumentNullException(nameof(sellOrderRequest));

        // Model Validation
        ValidationHelper.ModelValidation(sellOrderRequest);

        //convert sellOrderRequest into SellOrder type
        SellOrder sellOrder = sellOrderRequest.ToSellOrder();

        // Generate SellOrderId
        sellOrder.SellOrderID = Guid.NewGuid();

        // add sell order object to sell order list
        SellOrder SellOrderFromRepo = await _stockRepository.CreateSellOrder(sellOrder);

        return sellOrder.ToSellOrderResponse();
    }

    public async Task<List<BuyOrderResponse>> GetBuyOrders()
    {
        //Convert all SellOrder objects into SellOrderResponse objects
        List<BuyOrder> buyOrders = await _stockRepository.GetBuyOrders();

        // Order by DateTime And Convert
        return buyOrders.Select(y => y.ToBuyOrderResponse()).ToList();
    }

    public async Task<List<SellOrderResponse>> GetSellOrders()
    {
        //Convert all SellOrder objects into SellOrderResponse objects
        List<SellOrder> sellOrders = await _stockRepository.GetSellOrders();

        // Order by DateTime And Convert
        return sellOrders.Select(y => y.ToSellOrderResponse()).ToList();
    }
}
