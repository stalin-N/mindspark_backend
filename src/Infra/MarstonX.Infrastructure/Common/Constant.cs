namespace MarstonX.Infrastructure.Common;

public static class Constant
{
    public const int RetryCount = 2; // This is default Retry Count. If dynamic retry counts are needed, consider using Key Vault.
    public const int CircuitBreakerOpenCount = 2; // This is default CircuitBreakerOpenCount. If dynamic retry counts are needed, consider using Key Vault.
    public const double CircuitBreakDuration = 30; // This is default CircuitBreakDuration. If dynamic retry counts are needed, consider using Key Vault.
    public const double DurationOfBreak = 5; // This is default DurationOfBreak. If dynamic retry counts are needed, consider using Key Vault.
    public const double SleepDurationProvider = 2; // This is default SleepDurationProvider. If dynamic retry counts are needed, consider using Key Vault.
    public const string Authorization = "Authorization";
    public const string Bearer = "bearer";
    public const string JWT = "JWT";
    public const string AppSetting = "FNP:Settings:";
    public const string SQLConnectionString = "AzureSQLConnectionString-engage";
    public const string InvalidDb = "Invalid DB Operation";
    public const string OutOfMemory = "Out of Memory";
    public const string UnAuthorizedAccess = "Unauthorized Access";
    public const string DataNotFound = "Data Not Found";
    public const string AppConfigConn = "AppConfigConn";
    public const string TenantId = "TenantId";
    public const string ClientId = "KeyvaultClientID";
    public const string ClientSecret = "KeyvaultClientSecret";
    public const string LogWorkSpaceId = "LogWorkspaceId";
    public const string LogAuthenticationId = "LogAuthenticationId";
    public const string EngageLog = "EngageLog";
    public const string CircuitError = "Circuit broken! Please try again later.";
    public const string ServiceBusConn = "ServiceBusConn";
    public const string ServiceBusTopic = "appconfigurationtopic";
    public const string ServiceBusSubscription = "AppConfigurationSubscription";
    public const string Healthy = "A healthy result.";
    public const string UnHealthy = "An unhealthy result.";
    public const string ContentType = "application/json";
    public const string ErrorMessage = "Error Occurred";
    public const string InsertSuccessMessage = "Inserted Successfully";
    public const string UpdateSuccessMessage = "Updated Successfully";
    public const string DeleteSuccessMessage = "Deleted Successfully";
    public const string UserlengthValidation = "Length should be below 12";
    public const string CommonValidationError = "One or more validation failures have occurred.";
    public const int NoChanges = 0;
    public const int DefaultUser = 0;
    public const string NoRecordFound = "NO Record Found";
    public const string SuccessMessage = "Success";
    public const string FailureMessage = "Failure";
    public const string InternalError = "Internal Error";
    public const string RetrieveRecords = "retrieved successfully";
    public const string SanitizierValidation = "Input contains malicious details";

    #region AppInsight
    public const string LogAnalyticsWorkSpaceId = $"LogAnalyticsWorkSpaceId";
    public const string LogAnalyticsSharedKey = $"LogAnalyticsSharedKey";
    public const string InstrumentationKey = "appinsightskey";
    public const string LogName = "BackOffice-RestApi";
    #endregion

    #region Polly
    public const string RetryCountSettings = $"RetryCount";
    public const string CircuitBreakerOpenCountSettings = $"CircuitBreakerOpenCount";
    public const string CircuitBreakDurationSettings = $"CircuitBreakDuration";
    public const string BrokenCircuitException = "Error occurred while processing your request.";
    #endregion

    #region ConnectionStrings
    public const string AzureSQLConnectionString = "AzureSQLConnectionString";
    public const string ApplicationInsightsConnectionstring = "AppInsightsConnectionString";
    #endregion
}
