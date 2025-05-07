using UnityEngine;
using System;

namespace MyGameNamespace
{
    [Serializable]
    public class QuestionResponse
    {
        public string questionText;  
        public string correctAnswer; 
        public int difficulty;       
    }
}
