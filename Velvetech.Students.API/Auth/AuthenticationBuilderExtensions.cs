using System;
using Microsoft.AspNetCore.Authentication;

namespace Velvetech.Students.API.Auth
{
    /// <summary>
    /// Расширения для аутентификации
    /// </summary>
    public static class AuthenticationBuilderExtensions
    {
        /// <summary>
        /// Добавить поддержку аутентификации через API Key
        /// </summary>
        /// <param name="authenticationBuilder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder  authenticationBuilder, Action<ApiKeyAuthenticationOptions> options)
        {
            return authenticationBuilder.AddScheme<ApiKeyAuthenticationOptions,  ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.DefaultScheme, options);
        }
    }
}