using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameNamespace
{
    public class GameController : MonoBehaviour
    {
        private List<AnswerData> answers = new List<AnswerData>();
        private OpenAIChatGPTService openAIService;
      
        public void addAnswer(int _difficulty, float _answerTime, bool _isCorrect)
        {
            answers.Add(new AnswerData(_difficulty, _answerTime, _isCorrect));
            Debug.Log($"added new data:{_difficulty} , {_answerTime}, {_isCorrect}");
        }

        public async Task<QuestionResponse> askQuestion()
        {
            openAIService = gameObject.AddComponent<OpenAIChatGPTService>();
            QuestionResponse newQuestion = await openAIService.SendAnswersAndGetQuestionAsync(answers);
            return newQuestion;
        }
    }
}