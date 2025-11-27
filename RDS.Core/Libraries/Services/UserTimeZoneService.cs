using TimeZoneConverter;

namespace RDS.Core.Libraries.Services;

public class UserTimeZoneService
{
    public string TimeZoneId { get; set; } = "UTC";

    public DateTimeOffset ConvertToUserTimeZone(DateTimeOffset utcDate)
    {
        try
        {
            // Convertirá IANA al estándar correcto, ya sea Windows o Linux!
            var tzInfo = TZConvert.GetTimeZoneInfo(TimeZoneId);
            return TimeZoneInfo.ConvertTime(utcDate, tzInfo);
        }
        catch
        {
            return utcDate; //o un DateTimeOffset default si prefieres
        }
    }
}