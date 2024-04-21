using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthorizationController : ControllerBase
{
    private readonly ILoginService _loginService;

    public AuthorizationController(ILoginService loginService) => _loginService = loginService;

    [HttpPost]
    public async Task<ActionResult<RequestResponse<LoginResponseDTO>>> Login([FromBody] LoginDTO login)
    {
        return this.FromServiceResponse(await _loginService.Login(login with { Password = PasswordUtils.HashPassword(login.Password) }));
    }
}
