using Microsoft.AspNetCore.Authentication;

namespace Velvetech.Students.API.Auth
{
    /// <summary>
    /// Опции аутентификационной схемы для API Key
    /// </summary>
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// Http header для Api Key по умолчанию.
        /// <remarks>Спецификация OAS 3 (OpenAPI 3)</remarks>
        /// </summary>
        public const string DefaultHeaderName = "X-API-KEY";
        /// <summary>
        /// Схема аутентификации по умолчанию
        /// </summary>
        public const string DefaultScheme = "ApiKey";
        /// <summary>
        /// Схема аутентификации
        /// </summary>
        public string Scheme => DefaultScheme;
        /// <summary>
        /// Тип аутентификации
        /// </summary>
        public string AuthenticationType = DefaultScheme;
    }
}