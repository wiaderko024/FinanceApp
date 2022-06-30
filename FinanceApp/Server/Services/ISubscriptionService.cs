using FinanceApp.Server.Responses;
using FinanceApp.Shared.DTO;

namespace FinanceApp.Server.Services;

public interface ISubscriptionService
{
    Task<Response<object>> SubscribeAsync(string ticker, SubscribeDTO dto);
}