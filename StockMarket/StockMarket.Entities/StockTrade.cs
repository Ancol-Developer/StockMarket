namespace StockMarket.Entities;

/// <summary>
/// Represents the model class to supply trade details (stock id, stock name, price and quantity etc.) to the Trade/Index view
/// </summary>
public class StockTrade
{
    public string? StockSymbol { get; set; }
    public string? StockName { get; set; }
    public double? Price { get; set; }
    public uint Quantity { get; set; } = 0;
}
