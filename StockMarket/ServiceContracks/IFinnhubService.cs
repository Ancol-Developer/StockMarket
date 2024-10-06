namespace ServiceContracks;

public interface IFinnhubService
{
    /// <summary>
    /// Return company details such as company country, currency, exchange, IPO date, logo Image, ...
    /// </summary>
    /// <param name="stockSymbol">>Stock symbol to search</param>
    /// <returns>Returns a dictionary that contains details such as company country, currency, exchange, IPO date, logo image, market capitalization, name of the company, phone number etc.</returns>
    Dictionary<string, object>? GetCompanyProfile(string stockSymbol);

    /// <summary>
    /// Returns stock price details such as current price, change in price, percentage change, high price of the day, low price of the day, open price of the day, previous close price
    /// </summary>
    /// <param name="stockSymbol"></param>
    /// <returns></returns>
    Dictionary<string, object>? GetStockPriceQuote(string stockSymbol);
}
