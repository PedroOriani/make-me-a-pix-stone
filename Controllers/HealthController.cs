using Microsoft.AspNetCore.Mvc;

using Pix.Services;

namespace Pix.Controllers;

[ApiController]
[Route("[controller]")] //define qual a rota
public class HealthController : ControllerBase
{

    private HealthService _healthService

    public HealthController(HealthService healthService)
    {
        _healthService = healthService;
    }

    [HttpGet] //define o tipo de requisição
    public IActionResult GetHealth()
    {
        return Ok(_healthService.GetHealthMessage());
    }
} 