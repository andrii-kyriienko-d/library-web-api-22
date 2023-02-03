using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryWebApi.Services.Interfaces;
using LibraryWebApi.Entities;
using Microsoft.Extensions.Logging;

namespace LibraryWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ReadersController : BaseController<Readers>
{
    private readonly IBookExchange _bookExchangeService;
    public ReadersController(IBaseService<Readers> readersService,
        IBookExchange bookExchangeService, ILogger<ReadersController> logger) : base(readersService, logger)
    {
        _bookExchangeService = bookExchangeService;
    }

    [Authorize]
    [HttpGet("books/{id:int}")]
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 180)]
    public IActionResult GetBooks([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        return Ok(_bookExchangeService.GetBooks(id));
    }

    [Authorize]
    [HttpPost("give/{id:int}")]
    public IActionResult GiveBookToReaderById([FromRoute]int id, [FromBody] Books book)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        _bookExchangeService.GiveBookToReaderById(id, book);

        return NoContent();
    }
}