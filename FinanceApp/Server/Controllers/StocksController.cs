using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Server.Helpers;
using FinanceApp.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StocksController : ControllerBase
{
    private readonly IStockService _service;

    public StocksController(IStockService service)
    {
        _service = service;
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
}