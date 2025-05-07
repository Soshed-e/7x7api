# Adaptive Question Generator (Unity + GPT-4o)

This Unity C# module integrates OpenAI's GPT-4o to generate personalized multiplication questions for educational games.  
It tracks player performance (answer time, correctness, difficulty) and adapts question difficulty in real time using a dynamic prompt sent to the ChatGPT API.

---

## What It Does

- Collects player answers during gameplay.
- Builds a custom prompt based on recent data.
- Sends it to OpenAI ChatGPT (GPT-4o) via REST API.
- Receives a JSON object with a new math question.
- Parses and returns the result as a `QuestionResponse`.

Ideal for:
- Runner games for kids
- Math training games
- Adaptive learning mechanics in Unity

---

## Included Scripts

| File | Purpose |
|------|---------|
| `AnswerData.cs` | Stores a single answer entry (difficulty, time, correctness). |
| `QuestionResponse.cs` | Stores the generated question from the AI. |
| `GameController.cs` | Manages answer collection and question requests. |
| `OpenAIChatGPTService.cs` | Handles API calls and prompt generation. |
| `AnotherClass.cs` | Example class to test the full flow. |

---

## How to Use

1. Clone or copy this module into your Unity project.
2. Replace `YOUR_KEY_HERE` with your actual [OpenAI API key](https://platform.openai.com/).
3. Attach `AnotherClass.cs` or call `GameController.askQuestion()` manually to test.
4. Watch the Unity Console to see generated questions and debug output.

*Requires Unity 2021.3+ and the Newtonsoft.Json package.*

---

## Technologies Used

- Unity (C#)
- OpenAI GPT-4o API
- JSON (via Newtonsoft.Json)
- REST API (UnityWebRequest)
- Async/Await

---

## Author

Developed by Soshed-e.  
Originally created as part of a student project to help children learn multiplication through an adaptive runner game.
