using Microsoft.AspNetCore.Mvc;
using Pix.DTOs;
using Pix.Exceptions;
using Pix.Models;
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
        if (HttpContext.Items["Bank"] is not Bank bank) throw new NotFoundException("This bank doesn't exist");

        var newPayment = await _paymentService.Pay(payDTO, bank);

        return CreatedAtAction(null, null, newPayment);
    }
}