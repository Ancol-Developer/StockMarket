namespace StockMarket;
/// <summary>
/// Represents Options pattern for "StockPrice" configuration
/// </summary>
public class TradingOptions
{
    public string? Top25PopularStocks { get; set; }
    public uint? DefaultOrderQuantity {  get; set; }
}
