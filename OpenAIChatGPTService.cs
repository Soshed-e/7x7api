using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace MyGameNamespace
{
    public class OpenAIChatGPTService : MonoBehaviour
    {
        private const string API_URL = "https://api.openai.com/v1/chat/completions"; // URL ��� ChatGPT API
        private const string API_KEY = "YOUR_KEY_HERE"; 
        public async Task<QuestionResponse> SendAnswersAndGetQuestionAsync(List<AnswerData> answers)
        {
            string prompt = GeneratePromptFromAnswersUser(answers);

            var requestData = new
            {
                model = "gpt-4o",
                messages = new[]
                {
                    new {role = "system", content = ""},
                    new { role = "user", content = prompt }
                },
                max_tokens = 1000
            };

            string jsonData = JsonConvert.SerializeObject(requestData);
            Debug.Log($"Request:\n{jsonData}");


            using (UnityWebRequest request = new UnityWebRequest(API_URL, "POST"))
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", $"Bearer {API_KEY}");

                await request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError($"Request error: {request.error}");
                    return null;
                }
                Debug.Log($"Answer from OpenAI:\n{request.downloadHandler.text}");
                try
                {
                    string responseJson = request.downloadHandler.text;

                    var rawResponse = JsonConvert.DeserializeObject<OpenAIResponse>(responseJson);

                    string messageContent = rawResponse.choices[0].message.content;

                    messageContent = messageContent.Replace("```json\n", "").Replace("\n```", "");

                    var questionResponse = JsonConvert.DeserializeObject<QuestionResponse>(messageContent);

                    return questionResponse;
                }
                catch (JsonSerializationException ex)
                {
                    Debug.LogError($"JSON parsing error: {ex.Message}");
                    return null;
                }
            }
        }

        private string GeneratePromptFromAnswersSystem(List<AnswerData> answers)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Analyze the player's past answers and generate a new multiplication question. Follow these rules STRICTLY:");
            sb.AppendLine();
            sb.AppendLine("1. Use ONLY the last five responses.");
            sb.AppendLine("2. Question format: { \"questionText\": \"A * B\", \"correctAnswer\": <product>, \"difficulty\": <difficulty> }.");
            sb.AppendLine("   - \"A\" and \"B\" are integers between 2 and 10 (inclusive).");
            sb.AppendLine("   - Difficulty = A * B.");
            sb.AppendLine();
            sb.AppendLine("3. Adjust difficulty based on performance:");
            sb.AppendLine("   - If the player answered correctly, increase difficulty.");
            sb.AppendLine("   - If the player answered incorrectly, decrease difficulty.");
            sb.AppendLine("   - The degree of increase or decrease depends on the answer time:");
            sb.AppendLine("     - Over 6 seconds: change difficulty slightly.");
            sb.AppendLine("     - Between 3 and 6 seconds: apply a moderate change.");
            sb.AppendLine("     - Under 3 seconds: apply a significant change.");
            sb.AppendLine();
            sb.AppendLine("4. Trends:");
            sb.AppendLine("   - Consistently correct answers: increase difficulty more aggressively.");
            sb.AppendLine("   - Consistently incorrect answers: decrease difficulty more aggressively.");
            sb.AppendLine();
            sb.AppendLine("5. Avoid extreme changes in difficulty.");
            sb.AppendLine("6. The new question CANNOT be identical to the last question in the player's data.");
            sb.AppendLine("7. Return ONLY a valid JSON object in this format:");
            sb.AppendLine("   { \"questionText\": \"A * B\", \"correctAnswer\": <product>, \"difficulty\": <difficulty> }.");
            sb.AppendLine();
            sb.AppendLine("Player data:");

            for (int i = 0; i < answers.Count; i++)
            {
                var answer = answers[i];
                sb.AppendLine($"- Answer {i + 1} (Difficulty: {answer.difficulty}, Time: {answer.answerTime}s, Correct: {answer.isCorrect})");
            }

            sb.AppendLine();
            sb.AppendLine("Based on this, return ONLY a valid JSON object in the specified format. Do not explain.");
            return sb.ToString();
        }

        private string GeneratePromptFromAnswersUser(List<AnswerData> answers)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Player data:");

            for (int i = 0; i < answers.Count; i++)
            {
                var answer = answers[i];
                sb.AppendLine($"- Answer {i + 1} (Difficulty: {answer.difficulty}, Time: {answer.answerTime}s, Correct: {answer.isCorrect})");
            }

            sb.AppendLine();
            sb.AppendLine("Based on this, return ONLY a valid JSON object in the specified format. Do not explain.");
            return sb.ToString();
        }

        [Serializable]
        public class OpenAIResponse
        {
            public List<Choice> choices;

            [Serializable]
            public class Choice
            {
                public Message message;

                [Serializable]
                public class Message
                {
                    public string role;
                    public string content;
                }
            }
        }
    }
}










