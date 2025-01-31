using System.ComponentModel.DataAnnotations;

namespace ElasticSearch.Settings;

public sealed class ElasticSettings
{
    [Required]
    public string Url { get; set; }
    [Required]
    public string DefaultIndex { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
