using JetBrains.Annotations;

namespace Velvetech.Students.API.Auth
{
    /// <summary>
    /// Опции аутентификации для API Key
    /// </summary>
    [PublicAPI]
    public class ApiKeyOptions
    {
        /// <summary>
        /// Владелец ключа
        /// </summary>
        public string Owner { get; set; } = "API_KEY";
        /// <summary>
        /// Аутентификационный ключ
        /// </summary>
        public string Key { get; set; }
    }
}