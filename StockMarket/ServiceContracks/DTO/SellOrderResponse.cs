namespace ServiceContracks.DTO;
using StockMarket.Entities;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// DTO class that represents a sell order - that can be used as return type of Stocks service
/// </summary>
public class SellOrderResponse : IOrderResponse
{
    /// <summary>
    /// The unique ID of the sell order
    /// </summary>
    public Guid SellOrderID { get; set; }


    /// <summary>
    /// The unique symbol of the stock
    /// </summary>
    public string StockSymbol { get; set; }


    /// <summary>
    /// The The company name of the stock
    /// </summary>
    [Required(ErrorMessage = "Stock Name can't be null or empty")]
    public string StockName { get; set; }


    /// <summary>
    /// Date and time of order, when it is placed by the user
    /// </summary>
    public DateTime DateAndTimeOfOrder { get; set; }


    /// <summary>
    /// The number of stocks (shares) to sell
    /// </summary>
    public uint Quantity { get; set; }


    /// <summary>
    /// The price of each stock (share)
    /// </summary>
    public double Price { get; set; }

    public double TradeAmount { get; set; }

    public OrderType TypeOfOrder => OrderType.SellOrder;

    /// <summary>
    /// Checks if the current object and other (parameter) object values match
    /// </summary>
    /// <param name="obj">Other object of SellOrderResponse class, to compare</param>
    /// <returns>True or false determines whether current object and other objects match</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj is not SellOrderResponse) return false;

        SellOrderResponse other = (SellOrderResponse)obj;
        return SellOrderID == other.SellOrderID && StockSymbol == other.StockSymbol && StockName == other.StockName && DateAndTimeOfOrder == other.DateAndTimeOfOrder && Quantity == other.Quantity && Price == other.Price;
    }
}
public static class SellOrderExtensions
{
    /// <summary>
    /// An extension method to convert an object of SellOrder class into SellOrderResponse class
    /// </summary>
    /// <param name="sellOrder">The SellOrder object to convert</param>
    /// <returns>Returns the converted SellOrderResponse object</returns>
    public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
    {
        return new SellOrderResponse() { SellOrderID = sellOrder.SellOrderID, StockSymbol = sellOrder.StockSymbol, StockName = sellOrder.StockName, Price = sellOrder.Price, DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder, Quantity = sellOrder.Quantity, TradeAmount = sellOrder.Price * sellOrder.Quantity };
    }
}
