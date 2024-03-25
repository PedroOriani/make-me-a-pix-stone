using Microsoft.AspNetCore.Mvc;
using Pix.DTOs;
using Pix.Exceptions;
using Pix.Models;
using Pix.Services;

namespace Pix.Controllers;

[ApiController]
[Route("[Controller]")]
public class ConcilliationController(ConcilliationService concilliationService) : ControllerBase
{
    private readonly ConcilliationService _concilliationService = concilliationService;

    [HttpPost]
    public async Task<IActionResult> Create (ConcilliationDTO dto)
    {
        if (HttpContext.Items["Bank"] is not Bank bank) throw new NotFoundException("This bank doesn't exist");

        await _concilliationService.Verify(dto, bank.Id);

        return Ok();
    }
}