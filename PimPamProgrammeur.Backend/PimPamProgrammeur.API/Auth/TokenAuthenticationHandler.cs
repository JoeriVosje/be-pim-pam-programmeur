
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PimPamProgrammeur.Utils;

namespace PimPamProgrammeur.API.Middleware
{
    public class TokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ITokenProvider _tokenProvider;

        public TokenAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, ITokenProvider tokenProvider) 
            : base(options, logger, encoder, clock)
        {
            _tokenProvider = tokenProvider;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var bearer = Request.Headers["Authorization"].FirstOrDefault();

            if (bearer == null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Token is null"));
            }

            bearer = bearer.Replace("Bearer", "").Trim();
            var (isValid, claims) = _tokenProvider.ReadToken(bearer);
            if (!isValid)
            {
                return Task.FromResult(AuthenticateResult.Fail("Token is null"));
            }

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name)));
        }


    }
}
