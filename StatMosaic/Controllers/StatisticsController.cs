using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StatMosaic.Helpers;
using StatMosaic.Models;
using StatMosaic.Services;

namespace StatMosaic.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize]
public class StatisticsController : Controller
{
    
    private readonly ICosmosDbService _cosmosDbService;
    private readonly ILogger<StatisticsController> _logger;
    
    public StatisticsController(ILogger<StatisticsController> logger, ICosmosDbService cosmosDbService)
    {
        _logger = logger;
        _cosmosDbService = cosmosDbService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetStatisticsAsync([FromHeader] string pollId)
    {

            PollModel? poll = await _cosmosDbService.GetPollAsync(pollId);
            if (poll != null)
            {
              List<AnswersModel> answers = await _cosmosDbService.GetAnswersAsync(pollId);
               
                
                Dictionary<string, Dictionary<string, double>> percentage = await StatisticsHelper.GetPercentageByQuestion(poll, answers);
                Dictionary<string, Dictionary<string, int>> numberOfAnswers = await StatisticsHelper.GetNumberOfAnswersByQuestion(poll, answers);

                return Ok(new
                {
                    PollId = poll.Id,
                    PollTitle = poll.Title,
                    PollDescription = poll.Description,
                    PollStatus = poll.Status,
                    PollQuestions = poll.Questions,
                    PollPercentage = percentage,
                    PollCount = numberOfAnswers
                });
            }

            return Ok();
    }
    
    
    
}