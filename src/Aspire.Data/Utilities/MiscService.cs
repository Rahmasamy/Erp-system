using System.Text;

namespace Aspire.Utilities;

public static class OtpGenerator
{
    public static string GenerateOtp(int length = 6)
    {
        var random = new Random();
        var otp = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            otp.Append(random.Next(0, 10));
        }

        return otp.ToString();
    }
}

public interface IMiscService
{
    string GetFullName(string FirstName, string LastName);
    decimal? NormalizeDecimal(decimal? value);
    double? NormalizeDouble(double? value);
}

public sealed class MiscService : IMiscService
{
    public string GetFullName(string FirstName, string LastName)
    {
        if (String.IsNullOrWhiteSpace(FirstName) && String.IsNullOrWhiteSpace(LastName))
            return String.Empty;
        if (String.IsNullOrWhiteSpace(LastName))
            return FirstName;
        if (String.IsNullOrWhiteSpace(FirstName))
            return LastName;
        return FirstName + ' ' + LastName;
    }
    public decimal? NormalizeDecimal(decimal? value)
    {
        if (value == null) return null;
        return value / 1.000000000000000000000000000000000m;
    }
    public double? NormalizeDouble(double? value)
    {
        if (value == null) return null;
        return value / 1.000000000000000000000000000000000d;
    }
}