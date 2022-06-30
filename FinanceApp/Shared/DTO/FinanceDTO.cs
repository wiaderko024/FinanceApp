using System.Text.Json.Serialization;

namespace FinanceApp.Shared.DTO;

public class FinanceDTO
{
    [JsonPropertyName("c")]
    public float ClosePrice { get; set; }
    [JsonPropertyName("h")]
    public float HighestPrice { get; set; }
    [JsonPropertyName("l")]
    public float LowestPrice { get; set; }
    [JsonPropertyName("n")]
    public int NumOfTransactions { get; set; }
    [JsonPropertyName("o")]
    public float OpenPrice { get; set; }
    [JsonPropertyName("t")]
    public long Timestamp { get; set; }
    [JsonPropertyName("v")]
    public float Volume { get; set; }
    [JsonPropertyName("vw")]
    public float VolumeWeighted { get; set; }
}