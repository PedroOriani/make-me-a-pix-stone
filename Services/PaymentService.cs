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

public class PaymentService(PaymentRepository paymentRepository, UserRepository userRepository ,AccountRepository accountRepository,BankRepository bankRepository, KeyRepository keyRepository, MessageService messageService)
{
    private readonly PaymentRepository _paymentRepository = paymentRepository;

    private readonly BankRepository _bankRepository = bankRepository;

    private readonly KeyRepository _keyRepository = keyRepository;

    private readonly AccountRepository _accountRepository = accountRepository;

    private readonly MessageService _messageService = messageService;

    private readonly UserRepository _userRepository = userRepository;

    private readonly int IDEMPOTENCE_SECONDS = 30;

    public async Task<PaymentResponseDTO> Pay (PayDTO data, string token)
    {
        Bank? bank = await _bankRepository.GetBankByToken(token) ?? throw new InvalidToken("Invalid token");

        User user = await _userRepository.GetUserByCpf(data.Origin.User.Cpf) ?? throw new NotFoundException("This CPF doesn't exist");

        Key key = await _keyRepository.GetKeyByValue(data.Destiny.Key.Value) ?? throw new NotFoundException("This key doesn't exist");

        Account? account = await _accountRepository.GetAccountByNum(data.Origin.Account.Number);

        if (account == null) {
            Account newAccount = new(data.Origin.Account.Agency, data.Origin.Account.Number)
            {
                UserId = user.Id,
                BankId = bank.Id,
            };

            await _accountRepository.CreateAccount(newAccount);
            account = newAccount;
        }

        Payment newPayment = new()
        {
            Amount = data.Amount,
            Description = data.Description,
            PixKeyId = key.Id,
            PaymentProviderAccountId = account.Id,
        };

        PaymentIdempotenceKey idempotenceKey = new(newPayment);
        if (await CheckIfDuplicatedByIdempotence(idempotenceKey)) throw new RecentPaymentException($"Can't accept the same payment under {IDEMPOTENCE_SECONDS} seconds");

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

    private async Task<bool> CheckIfDuplicatedByIdempotence(PaymentIdempotenceKey key)
    {
        Payment? payment = await _paymentRepository.GetPaymentByKeyandTime(key, IDEMPOTENCE_SECONDS);
        return payment != null;
    }
}