using LibraryWebApi.Entities;
using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibraryWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class BookletsController : BaseController<Booklets>
{

    public BookletsController(IBaseService<Booklets> bookletsService, ILogger<BookletsController> logger) : base(bookletsService, logger)
    {
    }
}