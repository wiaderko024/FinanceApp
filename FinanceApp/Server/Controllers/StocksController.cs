using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Server.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StocksController : ControllerBase
{
    [HttpGet("SearchStocksInPolygonAPI")]
    public async Task<IActionResult> SearchStockInPolygonAPI(string? search)
    {
        var client = new PolygonApiClient();

        await client.SearchStock(search);

        return Ok();
    }
}