using Microsoft.AspNetCore.Authentication;

namespace PaymentMicroservice.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Verifica si usuario cuenta con el token del header Authorization
            var result = await context.AuthenticateAsync();

            if (result.Succeeded)
                context.User = result.Principal;

            await _next(context);
        }
    }
}
