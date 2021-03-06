using FinanceApp.Server.Data;
using FinanceApp.Server.Models;
using FinanceApp.Server.Responses;
using FinanceApp.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Server.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ApplicationDbContext _context;

    public SubscriptionService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Response<object>> SubscribeAsync(string ticker, SubscribeDTO dto)
    {
        var response = new Response<object>();
        
        var user = await _context.Users.SingleOrDefaultAsync(e => e.Id == dto.IdUser);
        if (user == null)
        {
            response.StatusCode = StatusCodes.Status404NotFound;
            response.Message = "User not found";
            return response;
        }

        var stock = await _context.Stocks.SingleOrDefaultAsync(e => e.Ticker == ticker);
        if (stock == null)
        {
            response.StatusCode = StatusCodes.Status404NotFound;
            response.Message = "Stock not found";
            return response;
        }

        var subscription =
            await _context.Subscriptions.SingleOrDefaultAsync(e => e.IdStock == stock.IdStock && e.IdUser == dto.IdUser);

        if (subscription != null)
        {
            response.StatusCode = StatusCodes.Status400BadRequest;
            response.Message = "Subscription exists";
            return response;
        }
        
        await _context.Subscriptions.AddAsync(new Subscription
        {
            IdUser = user.Id,
            IdStock = stock.IdStock
        });

        await _context.SaveChangesAsync();

        response.StatusCode = StatusCodes.Status200OK;
        
        return response;
    }

    public async Task<Response<object>> UnsubscribeAsync(string ticker, SubscribeDTO dto)
    {
        var response = new Response<object>();

        var user = await _context.Users.SingleOrDefaultAsync(e => e.Id == dto.IdUser);
        if (user == null)
        {
            response.StatusCode = StatusCodes.Status404NotFound;
            response.Message = "User not found";
            return response;
        }

        var stock = await _context.Stocks.SingleOrDefaultAsync(e => e.Ticker.ToLower() == ticker.ToLower());
        if (stock == null)
        {
            response.StatusCode = StatusCodes.Status404NotFound;
            response.Message = "Stock not found";
            return response;
        }

        var subscription =
            await _context.Subscriptions.SingleOrDefaultAsync(e =>
                e.IdUser == dto.IdUser && e.IdStock == stock.IdStock);

        if (subscription == null)
        {
            response.StatusCode = StatusCodes.Status404NotFound;
            response.Message = "Subscription not found";
            return response;
        }

        _context.Subscriptions.Remove(subscription);

        await _context.SaveChangesAsync();

        response.StatusCode = StatusCodes.Status200OK;


        return response;
    }

    public async Task<Response<bool>> HasSubscriptionAsync(string ticker, string userId)
    {
        var response = new Response<bool>();

        var user = await _context.Users.SingleOrDefaultAsync(e => e.Id == userId);
        if (user == null)
        {
            response.StatusCode = StatusCodes.Status404NotFound;
            response.Message = "User not found";
            return response;
        }

        var stock = await _context.Stocks.SingleOrDefaultAsync(e => e.Ticker.ToLower() == ticker.ToLower());
        if (stock == null)
        {
            response.StatusCode = StatusCodes.Status404NotFound;
            response.Message = "Stock not found";
            return response;
        }

        var subscription =
            await _context.Subscriptions.SingleOrDefaultAsync(e =>
                e.IdUser == userId && e.IdStock == stock.IdStock);

        response.StatusCode = StatusCodes.Status200OK;
        response.Result = subscription != null;

        return response;
    }

    public async Task<Response<SubscriptionsDTO>> GetSubscriptionsListAsync(string userId)
    {
        var response = new Response<SubscriptionsDTO>();

        // debug
        Console.WriteLine("DENUG " + userId);
        
        var user = await _context.Users.SingleOrDefaultAsync(e => e.Id == userId);
        if (user == null)
        {
            response.StatusCode = StatusCodes.Status404NotFound;
            response.Message = "User not found";
            return response;
        }

        var results = _context.Stocks.Join(_context.Subscriptions, stock => stock.IdStock, sub => sub.IdStock,
            (stock, subscription) => new SubscriptionDTO
            {
                Name = stock.Name,
                Ticker = stock.Ticker,
                LogoUrl = stock.LogoUrl,
                City = stock.City,
                Currency = stock.CurrencyName
            });
        
        response.StatusCode = StatusCodes.Status200OK;
        response.Result = new SubscriptionsDTO
        {
            Subscriptions = results
        };

        return response;
    }
}