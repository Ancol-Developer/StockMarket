using Microsoft.EntityFrameworkCore;
using ServiceContracks;
using ServiceContracks.DTO;
using Services.Helper;
using StockMarket.Entities;

namespace Services;

public class StocksService : IStocksService
{
    // Private field
    private readonly StockMarketDbContext _dbContext;

    /// <summary>
    /// Constructor of StocksService class that executes when a new object is created for the class
    /// </summary>
    public StocksService(StockMarketDbContext dbContext)
    {
        _dbContext = dbContext;
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
        _dbContext.BuyOrders.Add(buyOrder);
        await _dbContext.SaveChangesAsync();

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
        _dbContext.SellOrders.Add(sellOrder);
        await _dbContext.SaveChangesAsync();

        return sellOrder.ToSellOrderResponse();
    }

    public async Task<List<BuyOrderResponse>> GetBuyOrders()
    {
        List<BuyOrder> buyOrders = await _dbContext.BuyOrders
            .OrderByDescending(x => x.DateAndTimeOfOrder).ToListAsync();

        // Order by DateTime And Convert
        return buyOrders.Select(y => y.ToBuyOrderResponse()).ToList();
    }

    public async Task<List<SellOrderResponse>> GetSellOrders()
    {
        List<SellOrder> sellOrders = await _dbContext.SellOrders
            .OrderByDescending(x => x.DateAndTimeOfOrder).ToListAsync();

        // Order by DateTime And Convert
        return sellOrders.Select(y => y.ToSellOrderResponse()).ToList();
    }
}
