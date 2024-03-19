using Microsoft.EntityFrameworkCore;
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

     public async Task<Payment?> GetPaymentByAccountAndKey(PaymentIdempotenceKey key, int seconds)
        {

            DateTime secondsAgo = DateTime.UtcNow.AddSeconds(-seconds);
            Payment? payment = await _context.Payments.Where(p => 
                p.PixKeyId == key.PixKeyId &&
                p.PaymentProviderAccountId == key.PaymentProviderAccountId &&
                p.CreatedAt >= secondsAgo).FirstOrDefaultAsync();
                
            return payment;
        }
        
}