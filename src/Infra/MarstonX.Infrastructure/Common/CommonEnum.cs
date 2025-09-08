namespace MarstonX.Infrastructure.Common;

public static class CommonEnum
{
    /// <summary>
    /// Represents the possible exception codes.
    /// </summary>
    public enum ExceptionCode
    {
        Success = 1,
        DBValidationError = -1,
        DBException = -2,
        NoContent = -3
    }
    /// <summary>
    /// Represents the status of an agent invoice.
    /// </summary>
    public enum InvoiceStatus
    {
        Unpaid = 0,
        Paid = 1,
        PartPaid = 2,
        Unknown = -1,
    }
    public enum JobPriorities
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
    public enum JobStatus
    {
        NotStarted = 1,
        InProgress = 2,
        Completed = 3,
        Enroute = 4,
        Aborted = 5,
        Declined = 6
    }
    public enum RiskRating
    {
        Low = 1,
        Medium = 2,
        High = 3
    }

}
