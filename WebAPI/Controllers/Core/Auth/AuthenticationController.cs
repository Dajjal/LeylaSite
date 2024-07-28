using Core.Domain.Models.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Core.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(IConfiguration configuration)
    : ControllerBase
{
    // private readonly UserManager<UserModel> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;


    // [HttpPost]
    // public async Task<IActionResult> Register()
    // {
    //     if (ModelState.IsValid)
    //     {
    //         var user = await _userManager.FindByEmailAsync("test@test.test");
    //     }
    //     return Ok();
    // }
}