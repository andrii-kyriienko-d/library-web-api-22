using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class LogController : ControllerBase
{
    private readonly ILastLogGetter _lastLogGetter;

    public LogController(ILastLogGetter lastLogGetter)
    {
        _lastLogGetter = lastLogGetter;
    }

    [HttpGet]
    public IActionResult GetLastLog()
    {
        return Ok(_lastLogGetter.GetLastLog());
    }
}