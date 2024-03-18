using Microsoft.AspNetCore.Mvc;
using Pix.DTOs;
using Pix.Exceptions;
using Pix.Services;

namespace Pix.Controllers;

[ApiController]
[Route("keys")]
public class KeyController(KeyService keyService) : ControllerBase
{
    private readonly KeyService _keyService = keyService;

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