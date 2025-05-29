using System.Collections.Generic;
using System.IO;
using CSharpQuizApp.Models;
using Microsoft.Data.Sqlite;
using System;
using CSharpQuizApp.Utils; // <--- dodane

namespace CSharpQuizApp.Data;

public class QuizDatabase
{
    private const string FileName = "quiz.db";

    public static void Initialize()
    {
        try
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
                    ('Który język służy do stylowania stron WWW?', 'HTML', 'Python', 'CSS', 'PHP', 2),
                    ('Ile kontynentów jest na Ziemi?', '5', '6', '7', '8', 2),
                    ('Największa planeta Układu Słonecznego to?', 'Mars', 'Jowisz', 'Ziemia', 'Wenus', 1),
                    ('Które państwo ma największą powierzchnię?', 'USA', 'Chiny', 'Rosja', 'Brazylia', 2),
                    ('W którym roku rozpoczęła się II wojna światowa?', '1918', '1939', '1945', '1920', 1),
                    ('Co oznacza skrót CPU?', 'Central Processing Unit', 'Computer Personal Unit', 'Central Programming Utility', 'Control Processing Unit', 0),
                    ('Stolicą Włoch jest?', 'Mediolan', 'Rzym', 'Florencja', 'Neapol', 1),
                    ('Pierwiastek o symbolu O to?', 'Ołów', 'Tlen', 'Złoto', 'Srebro', 1),
                    ('Ile wynosi pierwiastek kwadratowy z 16?', '2', '3', '4', '5', 2),
                    ('Jakiego koloru jest chlor?', 'Czerwony', 'Zielony', 'Niebieski', 'Żółty', 1),
                    ('Który ocean jest największy?', 'Atlantycki', 'Spokojny', 'Arktyczny', 'Indyjski', 1),
                    ('Które państwo ma najwięcej ludności?', 'USA', 'Chiny', 'Indie', 'Brazylia', 1),
                    ('Który pierwiastek ma symbol Fe?', 'Fosfor', 'Fluor', 'Żelazo', 'Franc', 2),
                    ('Stolica Niemiec to?', 'Berlin', 'Monachium', 'Hamburg', 'Frankfurt', 0),
                    ('W którym państwie znajduje się Machu Picchu?', 'Meksyk', 'Peru', 'Chile', 'Boliwia', 1),
                    ('Kto wynalazł żarówkę?', 'Edison', 'Tesla', 'Newton', 'Einstein', 0),
                    ('W jakim kraju znajduje się Wielki Mur?', 'Indie', 'Chiny', 'Japonia', 'Rosja', 1),
                    ('Stolicą Hiszpanii jest?', 'Barcelona', 'Madryt', 'Walencja', 'Sewilla', 1),
                    ('Jak nazywa się największe jezioro świata?', 'Bajkał', 'Wiktorii', 'Kaspijskie', 'Tanganika', 2),
                    ('Kiedy Polska wstąpiła do UE?', '2004', '2006', '2000', '2007', 0),
                    ('Kto napisał „Pana Tadeusza”?', 'Słowacki', 'Mickiewicz', 'Kochanowski', 'Norwid', 1),
                    ('Który pierwiastek ma symbol Au?', 'Srebro', 'Złoto', 'Miedź', 'Rtęć', 1),
                    ('Który kontynent ma najwięcej państw?', 'Azja', 'Afryka', 'Europa', 'Ameryka', 1),
                    ('Najdłuższa rzeka świata to?', 'Nil', 'Amazonka', 'Jangcy', 'Missisipi', 1),
                    ('Które zwierzę jest największe na Ziemi?', 'Słoń', 'Płetwal błękitny', 'Rekin', 'Żyrafa', 1),
                    ('Stolicą Australii jest?', 'Sydney', 'Canberra', 'Melbourne', 'Perth', 1),
                    ('Który ocean leży przy Polsce?', 'Atlantycki', 'Spokojny', 'Arktyczny', 'Bałtycki', 3),
                    ('Kto odkrył Amerykę?', 'Cook', 'Kolumb', 'Magellan', 'Vasco da Gama', 1),
                    ('Jaki gaz dominuje w atmosferze?', 'Tlen', 'Azot', 'Dwutlenek węgla', 'Wodór', 1),
                    ('Który kraj jest największy w Europie?', 'Niemcy', 'Francja', 'Ukraina', 'Hiszpania', 2),
                    ('Co oznacza skrót RAM?', 'Random Access Memory', 'Ready And Manage', 'Read A Message', 'Run And Move', 0),
                    ('Najwyższy szczyt na Ziemi to?', 'Mount Everest', 'K2', 'Kilimandżaro', 'Mont Blanc', 0),
                    ('Największe miasto w Polsce to?', 'Kraków', 'Warszawa', 'Wrocław', 'Gdańsk', 1),
                    ('Ile nóg ma pająk?', '6', '8', '10', '12', 1),
                    ('Kto był pierwszym człowiekiem na Księżycu?', 'Buzz Aldrin', 'Neil Armstrong', 'Yuri Gagarin', 'Alan Shepard', 1),
                    ('Jaka jest największa pustynia świata?', 'Sahara', 'Gobi', 'Atakama', 'Kalahari', 0),
                    ('Który ocean jest najgłębszy?', 'Atlantycki', 'Spokojny', 'Arktyczny', 'Indyjski', 1),
                    ('Jaki jest najpopularniejszy język na świecie?', 'Angielski', 'Chiński', 'Hiszpański', 'Arabski', 1),
                    ('Który kraj wynalazł papier?', 'Japonia', 'Chiny', 'Egipt', 'Grecja', 1),
                    ('Które miasto jest stolicą Australii?', 'Sydney', 'Melbourne', 'Canberra', 'Perth', 2),
                    ('Który metal jest najlżejszy?', 'Żelazo', 'Miedź', 'Aluminium', 'Węgiel', 2),
                    ('Jakiego koloru jest szmaragd?', 'Zielony', 'Czerwony', 'Biały', 'Niebieski', 0),
                    ('Najwyższy wodospad na świecie to?', 'Niagara', 'Angel', 'Iguazu', 'Victoria', 1),
                    ('Które państwo graniczy z największą liczbą krajów?', 'Rosja', 'Chiny', 'USA', 'Brazylia', 1),
                    ('Kto napisał „Hamleta”?', 'Dickens', 'Szekspir', 'Hemingway', 'Tolkien', 1),
                    ('Co oznacza skrót WWW?', 'Wide World Web', 'World Wide Web', 'Web Wide World', 'World Wide Website', 1),
                    ('Największy kontynent na świecie to?', 'Afryka', 'Ameryka Północna', 'Azja', 'Europa', 2),
                    ('Jak nazywa się najwyższa góra w Polsce?', 'Babia Góra', 'Giewont', 'Śnieżka', 'Rysy', 3),
                    ('Co to jest H2O?', 'Tlen', 'Woda', 'Wodór', 'Azot', 1),
                    ('Które zwierzę jest ssakiem?', 'Żaba', 'Rekin', 'Nietoperz', 'Kura', 2),
                    ('Które państwo nie graniczy z Polską?', 'Niemcy', 'Litwa', 'Słowacja', 'Rumunia', 3)
                    ;
                ";
                tableCmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex); // <-- logowanie błędów inicjalizacji
        }
    }

    public static List<Question> LoadQuestions()
    {
        var questions = new List<Question>();

        try
        {
            using var connection = new SqliteConnection($"Data Source={FileName}");
            connection.Open();

        var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = "SELECT * FROM Questions ORDER BY RANDOM() LIMIT 10"; // <-- losujemy 10 pytań!

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
        }
        catch (Exception ex)
        {
            Logger.LogError(ex); // <-- logowanie błędów przy ładowaniu pytań
            throw;
        }

        return questions;
    }
}
