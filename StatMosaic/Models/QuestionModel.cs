using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace StatMosaic.Models;

public class QuestionModel
{
    //QuestionModel
    [JsonProperty("id")]
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string? Text { get; set; }
    public List<OptionModel>? Options { get; set; }
    public QuestionType Type { get; set; }
    
}

//Enum Question Type
public enum QuestionType
{
    SingleChoice,
    MultipleChoice,
    FreeText
}
