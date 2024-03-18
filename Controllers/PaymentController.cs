using Microsoft.AspNetCore.Mvc;
using Pix.DTOs;
using Pix.Exceptions;
using Pix.Services;

namespace Pix.Controllers;

[ApiController]
[Route("[Controller]")]

public class PaymentController(PaymentService paymentService) : ControllerBase
{
    private readonly PaymentService _paymentService = paymentService;

    [HttpPost]
    public async Task<IActionResult> Pay(PayDTO payDTO)
    {
        string? authorizationHeader = HttpContext.Request.Headers.Authorization;

        if (authorizationHeader == null) throw new InvalidToken("Invalid Token");

        var newPayment = await _paymentService.Pay(payDTO, authorizationHeader);

        return CreatedAtAction(null, null, newPayment);
    }
}