using RepositoryContracts;
using StockMarket.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class StocksRepository : IStockRepository
{
    // Private field
    private readonly StockMarketDbContext _db;

    public StocksRepository(StockMarketDbContext stockMarketDbContext)
    {
        _db = stockMarketDbContext;
    }

    public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
    {
        // add buy order object to buy order Database
        _db.BuyOrders.Add(buyOrder);
        await _db.SaveChangesAsync();

        return buyOrder;
    }

    public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
    {
        // add sell order object to sell orders list
        _db.SellOrders.Add(sellOrder);
        await _db.SaveChangesAsync();

        return sellOrder;
    }

    public async Task<List<BuyOrder>> GetBuyOrders()
    {
        List<BuyOrder> buyOrders = await _db.BuyOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToListAsync();

        return buyOrders;
    }

    public async Task<List<SellOrder>> GetSellOrders()
    {
        List<SellOrder> sellOrders = await _db.SellOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToListAsync();

        return sellOrders;
    }
}
