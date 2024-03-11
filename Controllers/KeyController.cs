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
        string? authorizationHeader = HttpContext.Request.Headers.Authorization;

        if (authorizationHeader == null) throw new InvalidToken("Invalid Token");

        var keyInfo = await _keyService.GetKeyInfo(type, value, authorizationHeader) ?? throw new NotFoundException("Key not found!");
        
       return Ok(keyInfo);
    }
}