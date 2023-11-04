using System.Net;
using Microsoft.Azure.Cosmos;
using StatMosaic.Models;

namespace StatMosaic.Services;

public class CosmosDbService : ICosmosDbService
{
    private readonly ILogger<CosmosDbService> _logger;
    private readonly Container _polls;
    private readonly Container _answers;
    
    public CosmosDbService(ILogger<CosmosDbService> logger, IConfiguration config)
    {
        _logger = logger;
        var client = new CosmosClient(Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? config["CosmosDb:ConnectionString"]!);
        var database = client.GetDatabase(config["CosmosDb:DatabaseName"]);
        _polls = database.GetContainer(config["CosmosDb:PollsContainerName"]);
        _answers = database.GetContainer(config["CosmosDb:AnswersContainerName"]);
    }
    
    public async Task<PollModel?> GetPollAsync(string pollId)
    {
        try
        {
            var poll = await _polls.ReadItemAsync<PollModel>(pollId, new PartitionKey(pollId));
            return poll.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }
    
    public Task<List<AnswersModel>> GetAnswersAsync(string pollId)
    {
        var answerQueryList =  _answers.GetItemLinqQueryable<AnswersModel>(true).Where(e => e.PollId == pollId).ToList();

        return Task.FromResult(answerQueryList);
    }
}