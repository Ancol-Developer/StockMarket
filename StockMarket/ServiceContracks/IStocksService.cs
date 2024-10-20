using ServiceContracks.DTO;

namespace ServiceContracks;

/// <summary>
/// Represents Stocks service that includes operations like buy order, sell order
/// </summary>
public interface IStocksService
{
    /// <summary>
    /// Creates a buy order
    /// </summary>
    /// <returns>buy order object</returns>
    BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest);

    /// <summary>
    /// Creates a sell order
    /// </summary>
    /// <param name="sellOrderRequest"></param>
    /// <returns>sell order object</returns>
    SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest);

    /// <summary>
    /// Reture all existing buy order
    /// </summary>
    /// <returns>Returns a list of objects of BuyOrder type</returns>
    List<BuyOrderResponse> GetBuyOrders();

    /// <summary>
    /// Returns all existing sell orders
    /// </summary>
    /// <returns>Returns a list of objects of SellOrder type</returns>
    List<SellOrderResponse> GetSellOrders();
}
