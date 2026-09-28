using System.Globalization;

namespace Aspire.Utilities;

public interface IDateService
{
    (DateTime?, DateTime?) getutcDates(string? value);
    DateTime? getutcDate(string? value);
}

public class DateService(ILogHandlingService logger) : IDateService
{
    public (DateTime?, DateTime?) getutcDates(string? value)
    {
        if (!String.IsNullOrWhiteSpace(value)) value = value.Split('T')[0];
        DateTime? utcDate1 = null;
        DateTime? utcDate2 = null;
        CultureInfo provider = CultureInfo.InvariantCulture;
        
        //string format = "yyyy-MM-dd'T'HH:mm:ssK";
        string format = "yyyy-MM-dd";
        bool isDate = DateTime.TryParseExact(value, format, provider, DateTimeStyles.None, out DateTime parsedDate2);
        if (isDate)
        {
            utcDate1 = DateTime.SpecifyKind(parsedDate2, DateTimeKind.Utc);
            utcDate2 = utcDate1.Value.AddDays(1);
        }
        return (utcDate1, utcDate2);
    }
    public DateTime? getutcDate(string? value)
    {
        if (!String.IsNullOrWhiteSpace(value)) value = value.Split('T')[0];
        DateTime? utcDate = null;
        CultureInfo provider = CultureInfo.InvariantCulture;
        //string format = "yyyy-MM-dd'T'HH:mm:ssK";
        string format = "yyyy-MM-dd";
        bool isDate = DateTime.TryParseExact(value, format, provider, DateTimeStyles.None, out DateTime parsedDate2);
        if (isDate)
        {
            utcDate = DateTime.SpecifyKind(parsedDate2, DateTimeKind.Utc);
        }
        return utcDate;
    }
}
