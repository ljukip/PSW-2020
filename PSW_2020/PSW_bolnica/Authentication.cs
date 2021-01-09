
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace PSW_bolnica
{
    public class Authentication : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private DBContext dbcontext;

        public Authentication(IOptionsMonitor<AuthenticationSchemeOptions> options,
                                          ILoggerFactory logger,
                                          UrlEncoder encoder,
                                          ISystemClock clock, DBContext context) : base(options, logger, encoder, clock) => dbcontext = context;

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.Fail("Authorization header was not found!"));

            AuthenticationTicket ticket;
            User user = GetUser();

            if (user == null)
            {
                ticket = null;
                return Task.FromResult(AuthenticateResult.Fail("Authorization failed!"));
            }
            else
            {
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(ClaimTypes.Role, user.role)
                });
                var principal = new ClaimsPrincipal(identity);
                ticket = new AuthenticationTicket(principal, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }

        }
        private User GetUser()
        {
            var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
            string[] credentials = Encoding.UTF8.GetString(bytes).Split(":");
            string username = credentials[0];

            User myUser = dbcontext.user.Where(user => user.username == username).FirstOrDefault();

            return myUser;
        }
    }
}
