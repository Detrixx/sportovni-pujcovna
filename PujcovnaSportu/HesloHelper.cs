using System.Security.Cryptography;
using System.Text;

public static class HesloHelper
{
    public static string HashHeslo(string heslo)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(heslo));
        return Convert.ToBase64String(bytes);
    }
}
