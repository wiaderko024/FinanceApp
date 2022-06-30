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

        await _context.Subscriptions.AddAsync(new Subscription
        {
            IdUser = user.Id,
            IdStock = stock.IdStock
        });

        await _context.SaveChangesAsync();

        response.StatusCode = StatusCodes.Status200OK;
        
        return response;
    }
}