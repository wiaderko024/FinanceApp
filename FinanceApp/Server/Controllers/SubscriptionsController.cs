using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinanceApp.Server.Services;
using FinanceApp.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Server.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _service;

    public SubscriptionsController(ISubscriptionService service)
    {
        _service = service;
    }

    [HttpGet("{ticker}/HasSubscription")]
    public async Task<IActionResult> HasSubscription(string ticker)
    {
        var response = await _service.HasSubscriptionAsync(ticker, User.FindFirstValue(ClaimTypes.NameIdentifier));
        return response.StatusCode switch
        {
            StatusCodes.Status404NotFound => NotFound(response.Message),
            _ => Ok(response.Result)
        };
    }

    [HttpGet]
    public async Task<IActionResult> GetSubscriptionsList()
    {
        var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Console.WriteLine("DBG =>" + user);
        
        var response = await _service.GetSubscriptionsListAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        return response.StatusCode switch
        {
            StatusCodes.Status404NotFound => NotFound(response.Message),
            _ => Ok(response.Result)
        };
    }
}