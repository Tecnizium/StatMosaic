using Newtonsoft.Json;

namespace StatMosaic.Models;

public class OptionModel
{
    //OptionModel
    [JsonProperty("id")]
    public string? Text { get; set; }
}