using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;

namespace TheSpender.BLL.Helpers;

/// <summary>
/// Контракт хелпера, который хэширует строку
/// </summary>
internal interface IStringsHasher
{
   /// <summary>
   /// Метод для хэширования строки с солью
   /// </summary>
   /// <param name="input">Строка, которую необходимо захэшировать</param>
   string GetHash(string input);
}

/// <inheritdoc cref="IStringsHasher"/>
internal sealed class StringsHasher(IOptions<SecurityOptions> options) : IStringsHasher
{
   public string GetHash(string input)
   {
      ArgumentNullException.ThrowIfNull(input, nameof(input));

      using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(options.Value.Salt));
      var data = Encoding.UTF8.GetBytes(input);
      return Convert.ToHexString(hmac.ComputeHash(data));
   }
}
