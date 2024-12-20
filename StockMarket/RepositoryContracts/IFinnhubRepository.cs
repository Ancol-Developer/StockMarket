﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts;

public interface IFinnhubRepository
{
    /// <summary>
    /// Return company detail such as company, country, currency, exchange, IPO date, logo image, market capitalization, name of the company, phone number, etc.
    /// </summary>
    /// <param name="stockSymbol">Stock symbol to search</param>
    /// <returns>Returns a dictionary that contains details such as company country, currency, exchange, IPO date, logo image, market capitalization, name of the company, phone number etc.</returns>
    Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);

    /// <summary>
    /// Return stock price detail such as current price, change in price, percentage change, high price of the day, low price of the day, open price of the day, previous close price
    /// </summary>
    /// <param name="stockSymbol">Stock symbol to search</param>
    /// <returns>Returns a dictionary that contains details such as current price, change i
    /// n price, percentage change, high price of the day, low price of the day, open price of the day, previous close price</returns>
    Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);

    /// <summary>
    /// Returns list all of stocks supported by an exchange (default: US)
    /// </summary>
    /// <returns>List of stocks</returns>
    Task<List<Dictionary<string, string>>?> GetStocks();

    /// <summary>
    /// Returns list of matching stocks based on the given stock symbol
    /// </summary>
    /// <param name="stockSymbolToSearch">Stock symbol to search</param>
    /// <returns>List of matching stocks</returns>
    Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
}
