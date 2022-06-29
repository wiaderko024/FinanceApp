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
        var client = new PolygonApiClient();
        await client.GetStockFromPolygon(ticker);
        return Ok();
    }
}