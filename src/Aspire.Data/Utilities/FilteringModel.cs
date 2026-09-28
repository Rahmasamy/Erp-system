namespace Aspire.Utilities;

public class EntityCAT
{
    public string? CATId { get; set; } // Unique Id
    public string? Name { get; set; } // CAT Name
    public string? NameAr { get; set; } // CAT NameAr
}

public class RoleEntityClaims
{
    public string? EntityName { get; set; } // Entity Name
    public string? Claims { get; set; } // Comma Separated for All Claims
    public string? ClaimsAllowed { get; set; } // Comma Separated for Selected Claims
}

public class FilteringModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortBy { get; set; } = "Id";
    public string GroupBy { get; set; } = String.Empty;
    public bool SortDescending { get; set; } = false;
    public Dictionary<string, object>? FilterValues { get; set; } = [];
    public List<FilterField>? FilterFields { get; set; } = [];
    public List<AggregateField>? AggregateFields { get; set; } = [];
    public string FilterOperationType { get; set; } = OperationType.And;    
}


public class FilterField
{
    public string Name { get; set; }
    public string Type { get; set; } = FieldType.Text;
    public bool IsForeign { get; set; } = false;
    public bool IsCurrency { get; set; } = false;
    public List<string> Values { get; set; }
    public int SimilarityType { get; set; } = SimilarityTypeEnum.Exact;
    public int OperationType { get; set; } = FieldValueOperationType.And;

    public FilterField(string FieldName, string FieldType, string OneValue)
    {
        Name = FieldName;
        Type = FieldType;
        Values = new List<string>();
        Values.Add(OneValue);
    }

    public FilterField(string FieldName, string FieldType, string OneValue, int similarityType)
    {
        Name = FieldName;
        Type = FieldType;
        Values = new List<string>();
        Values.Add(OneValue);
        SimilarityType = similarityType;
    }

    public FilterField(string FieldName, string FieldType, List<string> ManyValues)
    {
        Name = FieldName;
        Type = FieldType;
        Values = ManyValues;
    }
}

public class AggregateField
{
    public string Name { get; set; }  // The field name
    public string Function { get; set; }  // The aggregation function: "Sum", "Count", "Min", "Max", "Avg"
}

public static class SimilarityTypeEnum
{
    public const int Exact = 0;
    public const int Like = 1;
    public const int NotEqual = 2;
}

public static class FieldValueOperationType
{
    public const int And = 0;
    public const int Or = 1;
    public const int BetweenInclusive = 2;
    public const int BetweenInclusiveStart = 3;
    public const int BetweenInclusiveEnd = 4;
}
public static class OperationType
{
    public const string And = "and";
    public const string Or = "or";
}

public static class FieldType
{
    public const string Text = "text";
    public const string Number = "number";
    public const string Date = "date";
    public const string Boolean = "bool";
}

public static class AggregateFunction
{
    public const string Sum = "Sum";
    public const string Count = "Count";
    public const string Min = "Min";
    public const string Max = "Max";
    public const string Avg = "Avg";
}