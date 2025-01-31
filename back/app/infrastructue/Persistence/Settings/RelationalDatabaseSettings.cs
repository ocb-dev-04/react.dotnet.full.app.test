using System.ComponentModel.DataAnnotations;

namespace Persistence.Settings;

internal sealed class RelationalDatabaseSettings
{
    [Required]
    public string ConnectionString { get; set; }
    public int MaxRetryCount { get; set; } = 3;
    public int CommandTimeout { get; set; } = 5;
}