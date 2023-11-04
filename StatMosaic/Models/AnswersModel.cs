

using Newtonsoft.Json;

namespace StatMosaic.Models;

public class AnswersModel
{
    [JsonProperty("id")] public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string? PollId { get; set; }
    public List<AnswerModel> Answers { get; set; } = new List<AnswerModel>();
}