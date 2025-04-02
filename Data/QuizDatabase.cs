using System.Collections.Generic;
using System.IO;
using System.Net;
using CSharpQuizApp.Models;
using Microsoft.Data.Sqlite;

namespace CSharpQuizApp.Data;

public class QuizDatabase
{
    private const string FileName = "quiz.db";

    public static void Initialize()
    {
        if (!File.Exists(FileName))
        {
            using var connection = new SqliteConnection($"Data Source={FileName}");
            connection.Open();
            
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                @"
                CREATE TABLE Questions (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Text TEXT NOT NULL,
                    Answer1 TEXT NOT NULL,
                    Answer2 TEXT NOT NULL,
                    Answer3 TEXT NOT NULL,
                    Answer4 TEXT NOT NULL,
                    CorrectIndex INTEGER NOT NULL
                );

                INSERT INTO Questions (Text, Answer1, Answer2, Answer3, Answer4, CorrectIndex)
                VALUES 
                ('Jakie miasto jest stolicą Polski?', 'Kraków', 'Warszawa', 'Poznań', 'Gdańsk', 1),
                ('Który język służy do stylowania stron?', 'HTML', 'Python', 'CSS', 'PHP', 2);
            ";
            tableCmd.ExecuteNonQuery();
        }
    }

    public static List<Question> LoadQuestions()
    {
        var questions = new List<Question>();
        
        using var connection = new SqliteConnection($"Data Source={FileName}");
        connection.Open();
        
        var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = "SELECT * FROM Questions";
        
        using var reader = selectCmd.ExecuteReader();
        while (reader.Read())
        {
            questions.Add(new Question(
                reader.GetString(1),
                new List<string>
                {
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4),
                    reader.GetString(5)
                },
                reader.GetInt32(6)
            ));
        }
        return questions;
    }
}