namespace Aspire.Enums;

public static class GenericTableFieldType
{
    public const string number = "number";
    public const string text = "text";
    public const string date = "date";
    public const string boolean = "boolean";
}

public enum CurrencyExchangeTypeEnum
{
    From = 0,
    To
}

public static class SeatTypeEnum
{
    public const string Admin = "Admin";
    public const string User = "User";
    public const string Viewer = "Viewer";
    public const string Contractor = "Contractor";
}

public enum ActionRoutingEnum
{
    StayInSameStep = 0,
    ContinueToNextStep = 1,
    BackToPreviousStep = 2,
    ReturnToContractor = 3,
    GoToStep = 4
}

public enum AppUserType
{
    Employee = 1,
    Contractor = 2,
    Customer = 3,
    Supplier = 3
}

public enum AllocationEnum
{
    None = 0,
    Project = 1,
    DisciplineLeader = 2,
    SpecificEmployee = 3,
    RoundRobin = 4,
    LeastBusy = 5,
    MostBusy = 6
}

public static class KPIsCalcEnum
{
    public const string BudgetAtCompletion = "1";
    public const string VarianceIsTypicalForRemainingWork = "2";
    public const string AccelerationOfRemainingWork = "3";
    public const string VarianceIsNotRecurring = "4";
}

public static class BillingTypeEnum
{
    public const string Monthly = "Monthly";
    public const string Quarterly = "Quarterly";
    public const string MonthlyQuarterly = "MonthlyQuarterly";
}

public static class InvoiceARStatusIdEnum
{
    public const string Planned = "1";
    public const string Billed = "2";
    public const string Submitted = "3";
    public const string Collected = "4";
}

