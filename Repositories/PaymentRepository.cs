using Pix.Data;
using Pix.Models;

namespace Pix.Repositories;

public class PaymentRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Payment> Pay (Payment payment)
    {
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
        return payment;
    }
}