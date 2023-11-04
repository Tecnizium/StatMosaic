using StatMosaic.Models;

namespace StatMosaic.Helpers;

public abstract class StatisticsHelper
{
    //Get Percentage by Question
    public static async Task<Dictionary<string, Dictionary<string, double>>> GetPercentageByQuestion(PollModel poll, List<AnswersModel> answers)
    {
        var questions = poll.Questions;
        var result = new Dictionary<string, Dictionary<string, double>>();
        foreach (var question in questions)
        {
            var questionId = question.Id;
            var answersByQuestion = new List<AnswerModel>();
                foreach (var answerlist in answers)
                {
                    var answerByQuestion = answerlist.Answers.Where(answer => answer.QuestionId == questionId).ToList();
                    answersByQuestion.AddRange(answerByQuestion);
                }
            var totalAnswers = answersByQuestion.Count;
            var options = question.Options;
            var optionsByQuestion = options.Select(option => option.Text).ToList();
            var resultByQuestion = new Dictionary<string, double>();
            foreach (var option in optionsByQuestion)
            {
                var optionByQuestion = answersByQuestion.Where(answer => answer.Value == option).ToList();
                var totalOption = optionByQuestion.Count;
                var percentage = (double) totalOption / totalAnswers * 100;
                resultByQuestion.Add(option, percentage);
            }
            result.Add(questionId, resultByQuestion);
        }
        return result;
    }
    
    //Get Number of Answers by Question
    public static Task<Dictionary<string, Dictionary<string, int>>> GetNumberOfAnswersByQuestion(PollModel poll, List<AnswersModel> answers)
    {
        var questions = poll.Questions;
        var result = new Dictionary<string, Dictionary<string, int>>();
        
        //Get Number of Answers by poll
        var totalAnswersByPoll = answers.Count;
        result.Add("TotalAnswers", new Dictionary<string, int>(){{"TotalAnswers", totalAnswersByPoll}});
        foreach (var question in questions)
        {
            var questionId = question.Id;
            var answersByQuestion = new List<AnswerModel>();
            foreach (var answerlist in answers)
            {
                var answerByQuestion = answerlist.Answers.Where(answer => answer.QuestionId == questionId).ToList();
                answersByQuestion.AddRange(answerByQuestion);
            }
            var totalAnswers = answersByQuestion.Count;
            var options = question.Options;
            var optionsByQuestion = options.Select(option => option.Text).ToList();
            var resultByQuestion = new Dictionary<string, int>();
            foreach (var option in optionsByQuestion)
            {
                var optionByQuestion = answersByQuestion.Where(answer => answer.Value == option).ToList();
                var totalOption = optionByQuestion.Count;
                resultByQuestion.Add(option, totalOption);
            }
            result.Add(questionId, resultByQuestion);
        }
        return Task.FromResult(result);
    }
    
    
}