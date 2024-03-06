using Microsoft.AspNetCore.Mvc;

namespace Pix.Controllers;

[ApiController]
[Route("[controller]")] //define qual a rota
public class HealthController : ControllerBase
{
    [HttpGet] //define o tipo de requisição
    public IActionResult GetHealth()
    {
        return Ok("I'm alive!");
    }
}