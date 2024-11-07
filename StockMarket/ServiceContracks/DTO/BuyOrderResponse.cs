using StockMarket.Entities;

namespace ServiceContracks.DTO;

public class BuyOrderResponse : IOrderResponse
{
    /// <summary>
    /// The unique ID of the buy order
    /// </summary>
    public Guid BuyOrderID { get; set; }

    /// <summary>
    /// The unique symbol of the stock
    /// </summary>
    public string StockSymbol { get; set; }

    /// <summary>
    /// The company name of the stock
    /// </summary>
    public string StockName { get; set; }

    /// <summary>
    /// Date and time of order, when it is placed by the user
    /// </summary>
    public DateTime DateAndTimeOfOrder { get; set; }

    /// <summary>
    /// The number of stocks (shares) to buy
    /// </summary>
    public uint Quantity { get; set; }

    /// <summary>
    /// The price of each stock (share)
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Number of Trade
    /// </summary>
    public double TradeAmount { get; set; }

    public OrderType TypeOfOrder => OrderType.BuyOrder;

    /// <summary>
    /// Checks if the current object and other (parameter) object values match
    /// </summary>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj is not BuyOrderResponse) return false;

        BuyOrderResponse other = (BuyOrderResponse)obj;
        return BuyOrderID == other.BuyOrderID
            && StockSymbol == other.StockSymbol
            && StockName == other.StockName
            && DateAndTimeOfOrder == other.DateAndTimeOfOrder
            && Quantity == other.Quantity && Price == other.Price;
    }

    /// <summary>
    /// Returns an int value that represents unique stock id of the current object
    /// </summary>
    /// <returns>unique int value</returns>
    public override int GetHashCode()
    {
        return StockSymbol.GetHashCode();
    }

    public override string ToString()
    {
        return $"Buy Order ID: {BuyOrderID}, Stock Symbol: {StockSymbol}, Stock Name: {StockName}, Date and Time of Buy Order: {DateAndTimeOfOrder.ToString("dd MMM yyyy hh:mm ss tt")}, Quantity: {Quantity}, Buy Price: {Price}, Trade Amount: {TradeAmount}";
    }
}

public static class BuyOrderExtensions
{
    public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
    {
        return new BuyOrderResponse() { BuyOrderID = buyOrder.BuyOrderID, StockSymbol = buyOrder.StockSymbol, StockName = buyOrder.StockName, Price = buyOrder.Price, DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder, Quantity = buyOrder.Quantity, TradeAmount = buyOrder.Price * buyOrder.Quantity };
    }
}
