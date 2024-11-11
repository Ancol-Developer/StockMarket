using StockMarket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts;

public interface IStockRepository
{
    /// <summary>
    /// Creates a buy order
    /// </summary>
    /// <returns>buy order object</returns>
    Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder);

    /// <summary>
    /// Creates a sell order
    /// </summary>
    /// <param name="sellOrderRequest"></param>
    /// <returns>sell order object</returns>
    Task<SellOrder> CreateSellOrder(SellOrder sellOrder);

    /// <summary>
    /// Reture all existing buy order
    /// </summary>
    /// <returns>Returns a list of objects of BuyOrder type</returns>
    Task<List<BuyOrder>> GetBuyOrders();

    /// <summary>
    /// Returns all existing sell orders
    /// </summary>
    /// <returns>Returns a list of objects of SellOrder type</returns>
    Task<List<SellOrder>> GetSellOrders();
}
