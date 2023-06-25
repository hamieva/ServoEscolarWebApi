using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace ServoEscolarWebApi.Clases
{

    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public ApiKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var apiKeyHeader = configuration.GetSection("AppSettings:ApiKeyHeader").Value;
            var apiKey = configuration.GetSection("AppSettings:ApiKey").Value;

            if (!Request.Headers.ContainsKey(apiKeyHeader))
                return AuthenticateResult.Fail("Missing Authorization header");

            if (!Request.Headers["X-API-Key"].Equals(apiKey))
                return AuthenticateResult.Fail("Invalid API Key");

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "api-user")};

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

}
