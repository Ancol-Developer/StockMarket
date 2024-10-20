using ServiceContracks;
using ServiceContracks.DTO;
using Services.Helper;
using StockMarket.Entities;

namespace Services;

public class StocksService : IStocksService
{
    // Private field
    private readonly List<BuyOrder> _buyOrders;
    private readonly List<SellOrder> _sellOrders;

    /// <summary>
    /// Constructor of StocksService class that executes when a new object is created for the class
    /// </summary>
    public StocksService()
    {
        _buyOrders = new List<BuyOrder>();
        _sellOrders = new List<SellOrder>();
    }

    public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
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
        _buyOrders.Add(buyOrder);

        return buyOrder.ToBuyOrderResponse();
    }

    public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
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
        _sellOrders.Add(sellOrder);

        return sellOrder.ToSellOrderResponse();
    }

    public List<BuyOrderResponse> GetBuyOrders()
    {
        // Order by DateTime And Convert
        return _buyOrders
            .OrderByDescending(x => x.DateAndTimeOfOrder)
            .Select(y => y.ToBuyOrderResponse())
            .ToList();
    }

    public List<SellOrderResponse> GetSellOrders()
    {
        // Order by DateTime And Convert
        return _sellOrders
             .OrderByDescending(x => x.DateAndTimeOfOrder)
             .Select(y => y.ToSellOrderResponse())
             .ToList();
    }
}
