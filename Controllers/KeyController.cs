using Microsoft.AspNetCore.Mvc;
using Pix.DTOs;
using Pix.Exceptions;
using Pix.Models;
using Pix.Repositories;
using Pix.Services;

namespace Pix.Controllers;

[ApiController]
[Route("keys")]
public class KeyController(KeyService keyService, UserRepository userRepository, AccountRepository accountRepository, BankRepository bankRepository) : ControllerBase
{
    private readonly KeyService _keyService = keyService;

    private readonly UserRepository _userRepository = userRepository;

    private readonly AccountRepository _accountRepository = accountRepository;

    private readonly BankRepository _bankRepository = bankRepository;

    [HttpPost]
    public async Task<IActionResult> CreateKey(CreateKeyDTO createKeyDTO)
    {   
        string? authorizationHeader = HttpContext.Request.Headers.Authorization;

        if (authorizationHeader == null) throw new InvalidToken("Invalid Token");

        var newKey = await _keyService.CreateKey(createKeyDTO, authorizationHeader);

        return CreatedAtAction(null, null, newKey);
    }

    [HttpGet("{type}/{value}")]
    public async Task<IActionResult> GetKeyInfo(string type, string value)
    {
        var key = await _keyService.GetKeyInfo(type, value) ?? throw new NotFoundException("Key not found!");
        
        User? user = await _userRepository.GetUserById(key.UserId) ?? throw new NotFoundException("User not found!");

        Account? account = await _accountRepository.GetAccountById(key.AccountId) ?? throw new NotFoundException("Account not found!");

        Bank? bank = await _bankRepository.GetBankById(account.BankId) ?? throw new NotFoundException("Bank not found!");

        KeyInfoDto keyInfo = new()
        {
            Key = new KeyDto
            {
                Value = key.Value,
                Type = key.Type
            },
            User = new UserDto
            {
                Name = user.Name,
                MaskedCpf = MaskCpf(user.Cpf)
            },
            Account = new AccountDto
            {
                Number = account.Number,
                Agency = account.Agency,
                BankName = bank.Name,
                BankId = bank.Id
            }
        };
        
        return Ok(keyInfo);
    }

    private static string MaskCpf(string cpf)
    {
        return cpf[..3] + ".***.***." + cpf[^2..];
    }
}