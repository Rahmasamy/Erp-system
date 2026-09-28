using System.Text.Json;


namespace Aspire.Data;
public class UserPreference : DatabaseEntity
{
    public long? UserId { get; set; }
    public string? TableName { get; set; }
    public JsonDocument? Preference { get; set; }
}

public class UserPreferenceDTO
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? TableName { get; set; }
    public JsonDocument? Preference { get; set; }
    public long? TenantId { get; set; }
    public long? UserId { get; set; }
}