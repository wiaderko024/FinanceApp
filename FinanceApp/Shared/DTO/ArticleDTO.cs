using System.Text.Json.Serialization;

namespace FinanceApp.Shared.DTO;

public class ArticleDTO
{
    public string Title { get; set; }
    public string Author { get; set; }
    [JsonPropertyName("article_url")]
    public string ArticleUrl { get; set; }
    [JsonPropertyName("published_utc")]
    public DateTime PublishedUtc { get; set; }
}