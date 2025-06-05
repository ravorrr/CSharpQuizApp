using System.Collections.Generic;
using System.IO;
using CSharpQuizApp.Models;
using Microsoft.Data.Sqlite;
using System;
using CSharpQuizApp.Utils;

namespace CSharpQuizApp.Data;

public class QuizDatabase
{
    public static string ConnectionString = "Data Source=quiz.db";
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
                    ('Które państwo nie graniczy z Polską?', 'Niemcy', 'Litwa', 'Słowacja', 'Rumunia', 3),
                    ('Który pierwiastek ma symbol Na?', 'Sód', 'Wapń', 'Azot', 'Cynk', 0),
                    ('W jakim kraju leży wieża Eiffla?', 'Niemcy', 'Włochy', 'Francja', 'Hiszpania', 2),
                    ('Jakie zwierzę widnieje w herbie Polski?', 'Lew', 'Orzeł', 'Niedźwiedź', 'Wilk', 1),
                    ('Który język jest używany w Brazylii?', 'Hiszpański', 'Angielski', 'Portugalski', 'Francuski', 2),
                    ('W jakim roku człowiek pierwszy raz poleciał w kosmos?', '1961', '1957', '1969', '1972', 0),
                    ('Jaką jednostką mierzymy energię?', 'Wolt', 'Amper', 'Joule', 'Kelwin', 2),
                    ('Stolicą Norwegii jest?', 'Oslo', 'Sztokholm', 'Kopenhaga', 'Helsinki', 0),
                    ('Ile lat ma jedna dekada?', '5', '10', '15', '20', 1),
                    ('Która planeta jest najbliżej Słońca?', 'Merkury', 'Wenus', 'Mars', 'Ziemia', 0),
                    ('Który język programowania jest najczęściej używany?', 'C++', 'Python', 'Java', 'PHP', 1),
                    ('Z ilu kolorów składa się tęcza?', '5', '6', '7', '8', 2),
                    ('Jaki jest symbol chemiczny węgla?', 'W', 'We', 'C', 'Ca', 2),
                    ('Jak brzmi pierwsze prawo Newtona?', 'Akcja = Reakcja', 'Ciało w spoczynku...', 'F = ma', 'P = mv', 1),
                    ('Który kraj słynie z produkcji sushi?', 'Chiny', 'Japonia', 'Korea', 'Wietnam', 1),
                    ('Ile nóg ma rak?', '6', '8', '10', '12', 2),
                    ('Jakiego koloru jest flaga Niemiec?', 'Czarna, czerwona, złota', 'Biała, czerwona, niebieska', 'Czerwona, żółta, zielona', 'Czarna, zielona, czerwona', 0),
                    ('Kto napisał „Zbrodnię i karę”?', 'Tołstoj', 'Dostojewski', 'Puszkin', 'Gogol', 1),
                    ('Jaką liczbą rzymską zapisujemy 1000?', 'L', 'C', 'D', 'M', 3),
                    ('Które zwierzę jest symbolem mądrości?', 'Lis', 'Wilk', 'Sowa', 'Wąż', 2),
                    ('Jakie ciśnienie atmosferyczne uznaje się za normalne?', '760 mmHg', '800 mmHg', '720 mmHg', '700 mmHg', 0),
                    ('Jak nazywa się najmniejsza cząstka pierwiastka?', 'Atom', 'Proton', 'Cząsteczka', 'Neutron', 0),
                    ('Jakiego koloru są liście jesienią?', 'Czerwone', 'Żółte', 'Pomarańczowe', 'Wszystkie powyższe', 3),
                    ('Które państwo nie ma dostępu do morza?', 'Austria', 'Włochy', 'Portugalia', 'Turcja', 0),
                    ('Który instrument należy do strunowych?', 'Flet', 'Skrzypce', 'Bęben', 'Trąbka', 1),
                    ('Jakie jest największe zwierzę lądowe?', 'Nosorożec', 'Słoń afrykański', 'Hipopotam', 'Żubr', 1),
                    ('Jakie jest największe państwo Afryki?', 'Nigeria', 'Egipt', 'Algieria', 'RPA', 2),
                    ('Z ilu kości składa się ludzki szkielet?', '106', '206', '306', '406', 1),
                    ('Jak nazywa się chemiczny symbol wody?', 'HO', 'H2O', 'OH2', 'O2H', 1),
                    ('Co to jest fotosynteza?', 'Oddychanie roślin', 'Proces wzrostu', 'Wytwarzanie tlenu przez rośliny', 'Wchłanianie wody', 2),
                    ('Który wynalazca stworzył telefon?', 'Edison', 'Tesla', 'Bell', 'Newton', 2),
                    ('Które morze jest najcieplejsze?', 'Bałtyckie', 'Czarne', 'Czerwone', 'Śródziemne', 2),
                    ('Jakiego koloru jest złoto?', 'Srebrny', 'Żółty', 'Szary', 'Biały', 1),
                    ('Który ssak potrafi latać?', 'Nietoperz', 'Wiewiórka', 'Orzeł', 'Pelikany', 0),
                    ('Jak nazywa się proces przejścia cieczy w gaz?', 'Kondensacja', 'Parowanie', 'Topnienie', 'Sublimacja', 1),
                    ('Jaka liczba jest największa spośród: 256, 512, 128, 1024?', '512', '128', '256', '1024', 3),
                    ('Które zwierzę żyje najdłużej?', 'Papuga', 'Żółw', 'Koń', 'Słoń', 1),
                    ('Z ilu kontynentów składa się świat?', '5', '6', '7', '8', 2),
                    ('Jak nazywa się najstarszy uniwersytet w Polsce?', 'UJ', 'UW', 'Politechnika Warszawska', 'SGH', 0),
                    ('Który pierwiastek występuje najczęściej w skorupie ziemskiej?', 'Tlen', 'Żelazo', 'Wodór', 'Krzem', 0),
                    ('W jakim państwie wynaleziono proch?', 'Chiny', 'Japonia', 'Rosja', 'Indie', 0),
                    ('Która planeta ma pierścienie?', 'Ziemia', 'Mars', 'Saturn', 'Merkury', 2),
                    ('Który ssak składa jaja?', 'Nietoperz', 'Delfin', 'Kolczatka', 'Słoń', 2),
                    ('Jakiego koloru jest flaga Włoch?', 'Zielony, biały, czerwony', 'Czerwony, biały, niebieski', 'Żółty, niebieski, czerwony', 'Biały, czerwony, zielony', 0),
                    ('Jaka planeta nazywana jest „Czerwoną Planetą”?', 'Ziemia', 'Mars', 'Wenus', 'Jowisz', 1),
                    ('Które państwo leży na dwóch kontynentach?', 'Hiszpania', 'Rosja', 'Egipt', 'Turcja', 3),
                    ('Jakie jest najpopularniejsze hobby na świecie?', 'Wędkarstwo', 'Czytanie', 'Podróżowanie', 'Gotowanie', 0),
                    ('Co to jest haiku?', 'Rodzaj sushi', 'Rodzaj wiersza', 'Styl malarski', 'Taniec', 1),
                    ('Które miasto leży jednocześnie w Europie i Azji?', 'Stambuł', 'Teheran', 'Moskwa', 'Jerozolima', 0)
                    ;
                ";
                tableCmd.ExecuteNonQuery();
            }

            EnsureHistoryTableExists();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex); // <-- logowanie błędów inicjalizacji
        }
    }

    public static void EnsureHistoryTableExists()
    {
        using var connection = new SqliteConnection($"Data Source={FileName}");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
            @"CREATE TABLE IF NOT EXISTS quiz_history (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                player_name TEXT,
                quiz_type TEXT,
                score INTEGER,
                total_questions INTEGER,
                correct_answers INTEGER,
                wrong_answers INTEGER,
                time_in_seconds INTEGER,
                date TEXT
            );";
        command.ExecuteNonQuery();
    }

    public static void SaveQuizHistory(QuizHistoryEntry entry)
    {
        try
        {
            using var connection = new SqliteConnection($"Data Source={FileName}");
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText =
                @"INSERT INTO quiz_history
                  (player_name, quiz_type, score, total_questions, correct_answers, wrong_answers, time_in_seconds, date)
                  VALUES (@name, @type, @score, @total, @correct, @wrong, @time, @date)";

            command.Parameters.AddWithValue("@name", entry.PlayerName);
            command.Parameters.AddWithValue("@type", entry.QuizType);
            command.Parameters.AddWithValue("@score", entry.Score);
            command.Parameters.AddWithValue("@total", entry.TotalQuestions);
            command.Parameters.AddWithValue("@correct", entry.CorrectAnswers);
            command.Parameters.AddWithValue("@wrong", entry.WrongAnswers);
            command.Parameters.AddWithValue("@time", entry.TimeInSeconds);
            command.Parameters.AddWithValue("@date", entry.Date.ToString("yyyy-MM-dd HH:mm:ss"));

            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex);
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
    
    public static List<Question> LoadAllQuestions()
    {
        var questions = new List<Question>();

        try
        {
            using var connection = new SqliteConnection($"Data Source={FileName}");
            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = "SELECT * FROM Questions"; // <-- wszystkie pytania (bez losowania)

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
            Logger.LogError(ex);
            throw;
        }

        return questions;
    }
    
    public static List<QuizHistoryEntry> LoadHistory()
    {
        var history = new List<QuizHistoryEntry>();

        using var connection = new SqliteConnection("Data Source=quiz.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM quiz_history ORDER BY date DESC";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            history.Add(new QuizHistoryEntry
            {
                Id = reader.GetInt32(0),
                PlayerName = reader.GetString(1),
                QuizType = reader.GetString(2),
                Score = reader.GetInt32(3),
                TotalQuestions = reader.GetInt32(4),
                CorrectAnswers = reader.GetInt32(5),
                WrongAnswers = reader.GetInt32(6),
                TimeInSeconds = reader.GetInt32(7),
                Date = DateTime.Parse(reader.GetString(8))
            });
        }

        return history;
    }
    
    public static void ClearHistory()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM quiz_history";
        command.ExecuteNonQuery();
    }
}
