using LibraryWebApi.Entities;
using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibraryWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PublishersController : BaseController<Publishers>
{
    public PublishersController(IBaseService<Publishers> publishersService, ILogger<PublishersController> logger) 
        : base(publishersService, logger)
    {
    }
}