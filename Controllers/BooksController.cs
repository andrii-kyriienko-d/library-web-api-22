using LibraryWebApi.Entities;
using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibraryWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class BooksController : BaseController<Books>
{
    private readonly IBookExchange _bookExchangeService;
    private readonly IBookFullInfo _bookFullInfo;

    public BooksController(IBaseService<Books> booksService, 
        IBookExchange bookExchangeService,
        IBookFullInfo bookFullInfo, 
        ILogger<BooksController> logger) : base(booksService, logger)
    {
        _bookExchangeService = bookExchangeService;
        _bookFullInfo = bookFullInfo;
    }


    [Authorize]
    [HttpGet("GetReaders/{id:int}")]
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 180)]
    public IActionResult GetReaders([FromRoute] int id)
    {
        if (ModelState.IsValid)
        {
            return null;
        }

        return Ok(_bookExchangeService.GetReadersForBook(id));
    }

    [Authorize]
    [HttpPut("Give/{id:int}")]
    public IActionResult GiveBookToReader([FromRoute] int id, [FromBody] Readers reader)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        _bookExchangeService.GiveBookToReader(id, reader);

        return NoContent();
    }

    [Authorize]
    [HttpPost("Info/{id:int}")]
    public IActionResult GetInfo([FromRoute]int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(_bookFullInfo.GetFullInfo(id));
    }
}