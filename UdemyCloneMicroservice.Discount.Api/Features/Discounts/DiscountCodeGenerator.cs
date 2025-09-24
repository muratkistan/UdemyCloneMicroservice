using System.Security.Cryptography;

namespace UdemyCloneMicroservice.Discount.Api.Features.Discounts
{
    public class DiscountCodeGenerator
    {
        public static string Generate(
       int length = 10,
       string allowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));
            if (string.IsNullOrEmpty(allowed)) throw new ArgumentException("Allowed chars cannot be empty.");

            return string.Concat(
                Enumerable.Range(0, length)
                          .Select(_ => allowed[RandomNumberGenerator.GetInt32(allowed.Length)])
            );
        }
    }
}