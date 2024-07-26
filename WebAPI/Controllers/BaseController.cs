using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// Базовый, корневой контроллер
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class BaseController : ControllerBase;