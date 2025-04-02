using System.Collections.Generic;

namespace CSharpQuizApp.Models;

public class Question
{
    public string Text { get; set; }
    public List<string> Answers { get; set; }
    public int CorrectAnswerIndex { get; set; }

    public Question(string text, List<string> answers, int correctAnswerIndex)
    {
        Text = text;
        Answers = answers;
        CorrectAnswerIndex = correctAnswerIndex;
    }

    public bool IsCorrect(int selectedIndex)
    {
        return selectedIndex == CorrectAnswerIndex;
    }
}