using System;
namespace MyGameNamespace
{
    [Serializable]
    public class AnswerData
    {
        public int difficulty;       
        public float answerTime;     
        public bool isCorrect;       

        public AnswerData(int difficulty, float answerTime, bool isCorrect)
        {
            this.difficulty = difficulty;
            this.answerTime = answerTime;
            this.isCorrect = isCorrect;
        }
    }
}