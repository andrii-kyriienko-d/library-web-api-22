using LibraryWebApi.Models.AuthModels;
using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login(UserModel userModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(_loginService.GenerateToken(userModel));
            
    }
}