namespace MarstonX.Domain.Common;

public class DBResponse
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    public long? Id { get; set; }

    /// <summary>
    /// Gets or sets the errors associated with the database response.
    /// </summary>
    public string? Errors { get; set; }

    /// <summary>
    /// Gets or sets the data associated with the database response.
    /// </summary>
    public string? Data { get; set; }
}
