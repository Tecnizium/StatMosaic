using StatMosaic.Models;

namespace StatMosaic.Services;

public interface ICosmosDbService
{
    Task<PollModel?> GetPollAsync(string pollId);
    Task<List<AnswersModel>> GetAnswersAsync(string pollId);
}