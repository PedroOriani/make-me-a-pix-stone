using Pix.Exceptions;
using Pix.Models;
using Pix.Repositories;

namespace Pix.Middlewares
{
    public class AuthenticationHandler(BankRepository bankRepository) : IMiddleware
    {
        private readonly BankRepository _bankRepository = bankRepository;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string? authorizationHeader = context.Request.Headers.Authorization;

            if (string.IsNullOrEmpty(authorizationHeader)) throw new InvalidToken("Token not found");

            string token = authorizationHeader.Split(" ")[1];

            Bank? tokenBank = await _bankRepository.GetBankByToken(token) ?? throw new InvalidToken("Invalid token");

            context.Items["Bank"] = tokenBank;

            await next(context);
        }
    }
}
