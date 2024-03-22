using System;
using Microsoft.AspNetCore.Mvc;
using Pix.DTOs;
using Pix.Exceptions;
using Pix.Models;
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
        if (HttpContext.Items["Bank"] is not Bank bank) throw new NotFoundException("This bank doesn't exist");

        var newKey = await _keyService.CreateKey(createKeyDTO, bank);

        return CreatedAtAction(null, null, newKey);
    }

    [HttpGet("{type}/{value}")]
    public async Task<IActionResult> GetKeyInfo(string type, string value)
    {
        var keyInfo = await _keyService.GetKeyInfo(type, value) ?? throw new NotFoundException("Key not found!");
        
       return Ok(keyInfo);
    }
}