using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Velvetech.Students.API.Auth
{
    /// <summary>
    /// Обработчик аутентификации API Key
    /// </summary>
    public class ApiKeyAuthenticationHandler :  AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private const string ApiKeyHeaderName =  ApiKeyAuthenticationOptions.DefaultHeaderName;
        private readonly ApiKeyOptions _apiKeyOptions;
        /// <summary>
        /// ctor
        /// </summary>
        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IOptionsMonitor<ApiKeyOptions> apiKeyOptionsMonitor
        )
            : base(options, logger, encoder, clock)
        {
            _apiKeyOptions = apiKeyOptionsMonitor.CurrentValue;
        }
        /// <inheritdoc />
        protected override async Task<AuthenticateResult>  HandleAuthenticateAsync()
        {
            var authorizationKey = GetAuthorization(Request);
            if (string.IsNullOrEmpty(authorizationKey))
            {
                return AuthenticateResult.NoResult();
            }
            if (!ValidateApiKey(authorizationKey))
            {
                return AuthenticateResult.Fail("Invalid API Key provided.");
            }
            return AuthenticateSuccessfully();
        }
        /// <summary>
        /// Успешная аутентификация
        /// </summary>
        /// <returns></returns>
        private AuthenticateResult AuthenticateSuccessfully()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _apiKeyOptions.Owner)
            };
            var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
            var identities = new List<ClaimsIdentity> { identity };
            var principal = new ClaimsPrincipal(identities);
            var ticket = new AuthenticationTicket(principal, Options.Scheme);
            return AuthenticateResult.Success(ticket);
        }
        /// <summary>
        /// Получить авторизационный заголовок
        /// </summary>
        /// <param name="request">Http запрос</param>
        /// <returns></returns>
        private static string GetAuthorization(HttpRequest request)
        {
            return request.Headers[ApiKeyHeaderName].ToString();
        }
        /// <summary>
        /// Проверить Api ключ
        /// </summary>
        /// <param name="authorizationKey">Авторизационный ключ</param>
        /// <returns></returns>
        private bool ValidateApiKey(string authorizationKey)
        {
            return authorizationKey == _apiKeyOptions.Key;
        }
    }
}