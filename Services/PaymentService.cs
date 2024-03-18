using System.Text.RegularExpressions;
using Pix.DTOs;
using Pix.Exceptions;
using Pix.Models;
using Pix.Repositories;

namespace Pix.Services;

public class PaymentResponseDTO
{
    public required PayDTO OriginalPayDTO { get; set; }
    public int Id { get; set; }
}

public class PaymentService(PaymentRepository paymentRepository, AccountRepository accountRepository,BankRepository bankRepository, KeyRepository keyRepository, MessageService messageService)
{
    private readonly PaymentRepository _paymentRepository = paymentRepository;

    private readonly BankRepository _bankRepository = bankRepository;

    private readonly KeyRepository _keyRepository = keyRepository;

    private readonly AccountRepository _accountRepository = accountRepository;

    private readonly MessageService _messageService = messageService;

    public async Task<PaymentResponseDTO> Pay (PayDTO data, string token)
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

        Payment payment =  await _paymentRepository.Pay(newPayment);

        var paymentDTO = new PaymentDTO
        {
            Id = payment.Id,
            Origin = data.Origin,
            Destiny = data.Destiny,
            Amount = data.Amount,
            Description = data.Description
        };

        _messageService.SendMessage(paymentDTO);

        return new PaymentResponseDTO
        {
            OriginalPayDTO = data,
            Id = payment.Id
        };
    }
}