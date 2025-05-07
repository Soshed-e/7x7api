using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyGameNamespace
{
    public class AnotherClass : MonoBehaviour
    {
        private GameController gameController;
        private int lastDiff;

        private async void Start()
        {
            gameController = gameObject.AddComponent<GameController>();

            if (gameController != null)
            {
                gameController.addAnswer(50, 5.2f, true);
                gameController.addAnswer(70, 3.7f, false);

                await AskNewQuestion();
                gameController.addAnswer(lastDiff, 10.1f, true);
                await AskNewQuestion();
                gameController.addAnswer(lastDiff, 1.0f, true);
                await AskNewQuestion();
                gameController.addAnswer(lastDiff, 2.9f, false);
                await AskNewQuestion();
                gameController.addAnswer(lastDiff, 0.8f, true);
                await AskNewQuestion();
                gameController.addAnswer(lastDiff, 6.7f, true);
                await AskNewQuestion();
                gameController.addAnswer(lastDiff, 4.6f, false);
                await AskNewQuestion();
                gameController.addAnswer(lastDiff, 0.5f, true);
                await AskNewQuestion();
            }
            else
            {
                Debug.LogError("GameController is null!");
            }
        }

        private async Task AskNewQuestion()
        {
            QuestionResponse newQuestion = await gameController.askQuestion();

            if (newQuestion != null)
            {
                Debug.Log($"New question: {newQuestion.questionText}, Answer: {newQuestion.correctAnswer}, Difficulty: {newQuestion.difficulty}");
                lastDiff = newQuestion.difficulty;
            }
            else
            {
                Debug.LogError("Failed to get a new question.");
            }
        }
    }
}
