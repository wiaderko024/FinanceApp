using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinanceApp.Server.Helpers;
using FinanceApp.Server.Services;
using FinanceApp.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Server.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class StocksController : ControllerBase
{
    private readonly IStockService _service;
    private readonly ISubscriptionService _subscriptionService;

    public StocksController(IStockService service, ISubscriptionService subscriptionService)
    {
        _service = service;
        _subscriptionService = subscriptionService;
    }
    
    [HttpGet]
    public async Task<IActionResult> SearchStocksInPolygonApi(string? search)
    {
        var response = await _service.SearchStocksInPolygonApiAsync(search);
        return response.StatusCode != StatusCodes.Status200OK ? StatusCode(response.StatusCode, response.Message) : Ok(response.Result);
    }

    [HttpGet("{ticker}")]
    public async Task<IActionResult> GetStock(string ticker)
    {
        var response = await _service.GetStockAsync(ticker);
        return response.StatusCode switch
        {
            StatusCodes.Status404NotFound => NotFound(response.Message),
            _ => Ok(response.Result)
        };
    }

    [HttpGet("{ticker}/GetArticles")]
    public async Task<IActionResult> GetArticles(string ticker)
    {
        var response = await _service.GetArticlesAsync(ticker);
        return response.StatusCode switch
        {
            StatusCodes.Status500InternalServerError => StatusCode(response.StatusCode, response.Message),
            _ => Ok(response.Result)
        };
    }

    [HttpGet("{ticker}/GetFinances")]
    public async Task<IActionResult> GetFinances(string ticker)
    {
        var response = await _service.GetFinancesAsync(ticker);
        return response.StatusCode switch
        {
            StatusCodes.Status500InternalServerError => StatusCode(response.StatusCode, response.Message),
            _ => Ok(response.Result)
        };
    }

    [HttpPost("{ticker}/subscribe")]
    public async Task<IActionResult> Subscribe(string ticker)
    {
        var response = await _subscriptionService.SubscribeAsync(ticker, new SubscribeDTO
        {
            IdUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
        });
        
        return response.StatusCode switch
        {
            StatusCodes.Status404NotFound => NotFound(response.Message),
            StatusCodes.Status400BadRequest => BadRequest(response.Message),
            _ => Ok()
        };
    }

    [HttpDelete("{ticker}/unsubscribe")]
    public async Task<IActionResult> Unsubscribe(string ticker)
    {
        var response = await _subscriptionService.UnsubscribeAsync(ticker, new SubscribeDTO
        {
            IdUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
        });
        return response.StatusCode switch
        {
            StatusCodes.Status404NotFound => NotFound(response.Message),
            _ => Ok()
        };
    }
}