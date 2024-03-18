using System.Text.RegularExpressions;
using Pix.DTOs;
using Pix.Exceptions;
using Pix.Models;
using Pix.Repositories;

namespace Pix.Services;

public class PaymentService(PaymentRepository paymentRepository, AccountRepository accountRepository,BankRepository bankRepository, KeyRepository keyRepository)
{
    private readonly PaymentRepository _paymentRepository = paymentRepository;

    private readonly BankRepository _bankRepository = bankRepository;

    private readonly KeyRepository _keyRepository = keyRepository;

    private readonly AccountRepository _accountRepository = accountRepository;

    public async Task<Payment> Pay (PayDTO data, string token)
    {
        Bank? bank = await _bankRepository.GetBankByToken(token) ?? throw new InvalidToken("Invalid token");

        var key = await _keyRepository.GetKeyByValue(data.Destiny.Key.Value) ?? throw new NotFoundException("This key doesn't exist");

        var account = await _accountRepository.GetAccountByNum(data.Origin.Account.Number, bank.Id) ?? throw new NotFoundException("This account doesn't exist");

        Payment newPayment = new()
        {
            Amount = data.Amount,
            Description = data.Description,
            PixKeyId = key.Id,
            PaymentProviderAccountId = account.Id,
        };

        return await _paymentRepository.Pay(newPayment);
    }
}