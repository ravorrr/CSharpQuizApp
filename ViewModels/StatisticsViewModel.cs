using System;
using System.Collections.Generic;
using System.Linq;
using QuizApp.Models;

namespace QuizApp.ViewModels
{
    public class StatisticsViewModel
    {
        public int TotalQuizzes { get; }
        public double AverageScorePercent { get; }
        public int BestScore { get; }
        public int WorstScore { get; }
        public double AverageTimeSeconds { get; }

        public StatisticsViewModel(List<QuizHistoryEntry> history)
        {
            TotalQuizzes = history.Count;
            if (history.Count > 0)
            {
                AverageScorePercent = Math.Round(history.Average(h => (double)h.Score / h.TotalQuestions * 100), 2);
                BestScore = history.Max(h => h.Score);
                WorstScore = history.Min(h => h.Score);
                AverageTimeSeconds = Math.Round(history.Average(h => h.TimeInSeconds), 2);
            }
            else
            {
                AverageScorePercent = 0;
                BestScore = 0;
                WorstScore = 0;
                AverageTimeSeconds = 0;
            }
        }
    }
}