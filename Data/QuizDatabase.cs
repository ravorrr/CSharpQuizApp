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
                    CorrectIndex INTEGER NOT NULL,
                    Category TEXT NOT NULL DEFAULT 'Ogólne'
                );

                INSERT INTO Questions (Text, Answer1, Answer2, Answer3, Answer4, CorrectIndex, Category)
                VALUES
                ('Jakie miasto jest stolicą Polski?', 'Kraków', 'Warszawa', 'Poznań', 'Gdańsk', 1, 'Geografia'),
                ('Który ocean jest największy?', 'Atlantycki', 'Spokojny', 'Arktyczny', 'Indyjski', 1, 'Geografia'),
                ('Które państwo ma najwięcej ludności?', 'USA', 'Chiny', 'Indie', 'Brazylia', 1, 'Geografia'),
                ('Stolicą Włoch jest?', 'Mediolan', 'Rzym', 'Florencja', 'Neapol', 1, 'Geografia'),
                ('Stolicą Australii jest?', 'Sydney', 'Canberra', 'Melbourne', 'Perth', 1, 'Geografia'),
                ('Który ocean leży przy Polsce?', 'Atlantycki', 'Spokojny', 'Arktyczny', 'Bałtycki', 3, 'Geografia'),
                ('Najwyższy szczyt na Ziemi to?', 'Mount Everest', 'K2', 'Kilimandżaro', 'Mont Blanc', 0, 'Geografia'),
                ('Jak nazywa się największe jezioro świata?', 'Bajkał', 'Wiktorii', 'Kaspijskie', 'Tanganika', 2, 'Geografia'),
                ('Stolica Niemiec to?', 'Berlin', 'Monachium', 'Hamburg', 'Frankfurt', 0, 'Geografia'),
                ('Które państwo leży na dwóch kontynentach?', 'Hiszpania', 'Rosja', 'Egipt', 'Turcja', 3, 'Geografia'),
                ('Który język służy do stylowania stron WWW?', 'HTML', 'Python', 'CSS', 'PHP', 2, 'Informatyka'),
                ('Co oznacza skrót CPU?', 'Central Processing Unit', 'Computer Personal Unit', 'Central Programming Utility', 'Control Processing Unit', 0, 'Informatyka'),
                ('Co oznacza skrót RAM?', 'Random Access Memory', 'Ready And Manage', 'Read A Message', 'Run And Move', 0, 'Informatyka'),
                ('Który język programowania jest najczęściej używany?', 'C++', 'Python', 'Java', 'PHP', 1, 'Informatyka'),
                ('Co oznacza skrót HTML?', 'HighText Machine Language', 'HyperText Markup Language', 'Home Tool Markup Language', 'HyperText Machine Language', 1, 'Informatyka'),
                ('Co oznacza skrót SMS?', 'Short Message Service', 'Smart Media System', 'Super Mail Service', 'Simple Message Service', 0, 'Informatyka'),
                ('Co oznacza skrót USB?', 'Universal Serial Bus', 'Universal Service Box', 'United System Board', 'Universal Socket Bridge', 0, 'Informatyka'),
                ('Jak nazywa się największa wyszukiwarka internetowa?', 'Yahoo', 'Google', 'Bing', 'DuckDuckGo', 1, 'Informatyka'),
                ('Jak nazywa się podstawowy sprzęt wyjścia?', 'Monitor', 'Klawiatura', 'Projektor', 'Drukarka', 0, 'Informatyka'),
                ('Która firma stworzyła system Android?', 'Microsoft', 'Apple', 'Google', 'Nokia', 2, 'Informatyka'),
                ('Największa planeta Układu Słonecznego to?', 'Mars', 'Jowisz', 'Ziemia', 'Wenus', 1, 'Astronomia'),
                ('Który pierwiastek ma symbol Fe?', 'Fosfor', 'Fluor', 'Żelazo', 'Franc', 2, 'Astronomia'),
                ('Kto był pierwszym człowiekiem na Księżycu?', 'Buzz Aldrin', 'Neil Armstrong', 'Yuri Gagarin', 'Alan Shepard', 1, 'Astronomia'),
                ('Która planeta jest najbliżej Słońca?', 'Merkury', 'Wenus', 'Mars', 'Ziemia', 0, 'Astronomia'),
                ('W jakim roku człowiek pierwszy raz poleciał w kosmos?', '1961', '1957', '1969', '1972', 0, 'Astronomia'),
                ('Która planeta ma pierścienie?', 'Ziemia', 'Mars', 'Saturn', 'Merkury', 2, 'Astronomia'),
                ('Jaka planeta nazywana jest „Czerwoną Planetą”?', 'Ziemia', 'Mars', 'Wenus', 'Jowisz', 1, 'Astronomia'),
                ('Jak nazywa się najjaśniejsza gwiazda na niebie?', 'Słońce', 'Sirius', 'Wega', 'Polaris', 1, 'Astronomia'),
                ('Jak nazywa się najbliższa Ziemi gwiazda (po Słońcu)?', 'Proxima Centauri', 'Betelgeza', 'Sirius', 'Aldebaran', 0, 'Astronomia'),
                ('W jakiej galaktyce znajduje się Układ Słoneczny?', 'Galaktyka Andromedy', 'Wielki Obłok Magellana', 'Droga Mleczna', 'Mały Obłok Magellana', 2, 'Astronomia'),
                ('W którym roku rozpoczęła się II wojna światowa?', '1918', '1939', '1945', '1920', 1, 'Historia'),
                ('Kto wynalazł żarówkę?', 'Edison', 'Tesla', 'Newton', 'Einstein', 0, 'Historia'),
                ('Kiedy Polska wstąpiła do UE?', '2004', '2006', '2000', '2007', 0, 'Historia'),
                ('Kto odkrył Amerykę?', 'Cook', 'Kolumb', 'Magellan', 'Vasco da Gama', 1, 'Historia'),
                ('Jak nazywa się najstarszy uniwersytet w Polsce?', 'UJ', 'UW', 'Politechnika Warszawska', 'SGH', 0, 'Historia'),
                ('W jakim państwie wynaleziono proch?', 'Chiny', 'Japonia', 'Rosja', 'Indie', 0, 'Historia'),
                ('Który kraj wynalazł papier?', 'Japonia', 'Chiny', 'Egipt', 'Grecja', 1, 'Historia'),
                ('Jakie zwierzę widnieje w herbie Polski?', 'Lew', 'Orzeł', 'Niedźwiedź', 'Wilk', 1, 'Historia'),
                ('W jakim roku odbyła się bitwa pod Grunwaldem?', '1410', '966', '1795', '1944', 0, 'Historia'),
                ('Kto był pierwszym królem Polski?', 'Bolesław Chrobry', 'Mieszko I', 'Kazimierz Wielki', 'Władysław Jagiełło', 0, 'Historia'),
                ('Pierwiastek o symbolu O to?', 'Ołów', 'Tlen', 'Złoto', 'Srebro', 1, 'Chemia'),
                ('Jakiego koloru jest chlor?', 'Czerwony', 'Zielony', 'Niebieski', 'Żółty', 1, 'Chemia'),
                ('Który pierwiastek ma symbol Au?', 'Srebro', 'Złoto', 'Miedź', 'Rtęć', 1, 'Chemia'),
                ('Który pierwiastek ma symbol Na?', 'Sód', 'Wapń', 'Azot', 'Cynk', 0, 'Chemia'),
                ('Co to jest H2O?', 'Tlen', 'Woda', 'Wodór', 'Azot', 1, 'Chemia'),
                ('Jak nazywa się najmniejsza cząstka pierwiastka?', 'Atom', 'Proton', 'Cząsteczka', 'Neutron', 0, 'Chemia'),
                ('Jak nazywa się chemiczny symbol wody?', 'HO', 'H2O', 'OH2', 'O2H', 1, 'Chemia'),
                ('Który pierwiastek występuje najczęściej w skorupie ziemskiej?', 'Tlen', 'Żelazo', 'Wodór', 'Krzem', 0, 'Chemia'),
                ('Jaki jest symbol chemiczny węgla?', 'W', 'We', 'C', 'Ca', 2, 'Chemia'),
                ('Jakiego koloru jest złoto?', 'Srebrny', 'Żółty', 'Szary', 'Biały', 1, 'Chemia'),
                ('Ile wynosi pierwiastek kwadratowy z 16?', '2', '3', '4', '5', 2, 'Matematyka'),
                ('Jaką liczbą rzymską zapisujemy 1000?', 'L', 'C', 'D', 'M', 3, 'Matematyka'),
                ('Jaka liczba jest największa spośród: 256, 512, 128, 1024?', '512', '128', '256', '1024', 3, 'Matematyka'),
                ('Ile to jest 7 x 8?', '54', '56', '64', '48', 1, 'Matematyka'),
                ('Ile to jest 12^2?', '122', '144', '132', '124', 1, 'Matematyka'),
                ('Ile boków ma pięciokąt?', '4', '5', '6', '8', 1, 'Matematyka'),
                ('Jaki jest wynik działania 9 + 10?', '17', '18', '19', '20', 2, 'Matematyka'),
                ('Ile wynosi 3! (silnia)?', '3', '6', '9', '12', 1, 'Matematyka'),
                ('Jak się nazywa kąt o mierze 90°?', 'Ostry', 'Rozwarty', 'Prosty', 'Wklęsły', 2, 'Matematyka'),
                ('Ile krawędzi ma sześcian?', '6', '8', '12', '14', 2, 'Matematyka'),
                ('Które zwierzę jest największe na Ziemi?', 'Słoń', 'Płetwal błękitny', 'Rekin', 'Żyrafa', 1, 'Biologia'),
                ('Ile nóg ma pająk?', '6', '8', '10', '12', 1, 'Biologia'),
                ('Które zwierzę jest ssakiem?', 'Żaba', 'Rekin', 'Nietoperz', 'Kura', 2, 'Biologia'),
                ('Ile nóg ma rak?', '6', '8', '10', '12', 2, 'Biologia'),
                ('Jakie jest największe zwierzę lądowe?', 'Nosorożec', 'Słoń afrykański', 'Hipopotam', 'Żubr', 1, 'Biologia'),
                ('Z ilu kości składa się ludzki szkielet?', '106', '206', '306', '406', 1, 'Biologia'),
                ('Co to jest fotosynteza?', 'Oddychanie roślin', 'Proces wzrostu', 'Wytwarzanie tlenu przez rośliny', 'Wchłanianie wody', 2, 'Biologia'),
                ('Który ssak potrafi latać?', 'Nietoperz', 'Wiewiórka', 'Orzeł', 'Pelikany', 0, 'Biologia'),
                ('Który ssak składa jaja?', 'Nietoperz', 'Delfin', 'Kolczatka', 'Słoń', 2, 'Biologia'),
                ('Które zwierzę żyje najdłużej?', 'Papuga', 'Żółw', 'Koń', 'Słoń', 1, 'Biologia'),
                ('Kto napisał „Pana Tadeusza”?', 'Słowacki', 'Mickiewicz', 'Kochanowski', 'Norwid', 1, 'Literatura'),
                ('Kto napisał „Hamleta”?', 'Dickens', 'Szekspir', 'Hemingway', 'Tolkien', 1, 'Literatura'),
                ('Kto napisał „Zbrodnię i karę”?', 'Tołstoj', 'Dostojewski', 'Puszkin', 'Gogol', 1, 'Literatura'),
                ('Kto napisał ""Małego Księcia""?', 'A. Milne', 'A. de Saint-Exupéry', 'J.K. Rowling', 'H. Ch. Andersen', 1, 'Literatura'),
                ('Kto jest autorem „Lalki”?', 'Prus', 'Żeromski', 'Mickiewicz', 'Sienkiewicz', 0, 'Literatura'),
                ('Kto napisał „Opowieść wigilijną”?', 'Dickens', 'Twain', 'Szekspir', 'Austen', 0, 'Literatura'),
                ('Kto napisał „Harry’ego Pottera”?', 'Tolkien', 'Rowling', 'Lewis', 'Pratchett', 1, 'Literatura'),
                ('Kto napisał „Krzyżaków”?', 'Sienkiewicz', 'Prus', 'Żeromski', 'Reymont', 0, 'Literatura'),
                ('Kto jest autorem „Romea i Julii”?', 'Szekspir', 'Dumas', 'Hugo', 'Dickens', 0, 'Literatura'),
                ('Kto napisał „W pustyni i w puszczy”?', 'Prus', 'Sienkiewicz', 'Mickiewicz', 'Reymont', 1, 'Literatura'),
                ('Jaką jednostką mierzymy energię?', 'Wolt', 'Amper', 'Joule', 'Kelwin', 2, 'Fizyka'),
                ('Z ilu kolorów składa się tęcza?', '5', '6', '7', '8', 2, 'Fizyka'),
                ('Jak brzmi pierwsze prawo Newtona?', 'Akcja = Reakcja', 'Ciało w spoczynku...', 'F = ma', 'P = mv', 1, 'Fizyka'),
                ('Jakie ciśnienie atmosferyczne uznaje się za normalne?', '760 mmHg', '800 mmHg', '720 mmHg', '700 mmHg', 0, 'Fizyka'),
                ('Jak nazywa się proces przejścia cieczy w gaz?', 'Kondensacja', 'Parowanie', 'Topnienie', 'Sublimacja', 1, 'Fizyka'),
                ('W jakiej jednostce mierzy się napięcie elektryczne?', 'Wolt', 'Amper', 'Omb', 'Dżul', 0, 'Fizyka'),
                ('Jaka jest prędkość światła w próżni?', '300 000 km/s', '150 000 km/s', '1 000 000 km/s', '100 000 km/s', 0, 'Fizyka'),
                ('Co to jest grawitacja?', 'Siła tarcia', 'Siła przyciągająca ciała', 'Prąd elektryczny', 'Ciepło', 1, 'Fizyka'),
                ('Co jest nośnikiem prądu w metalach?', 'Protony', 'Elektrony', 'Neutrony', 'Jony', 1, 'Fizyka'),
                ('Jak nazywa się zjawisko odbicia światła od powierzchni?', 'Dyfrakcja', 'Refrakcja', 'Odbicie', 'Załamanie', 2, 'Fizyka'),
                ('Jaki jest najpopularniejszy język na świecie?', 'Angielski', 'Chiński', 'Hiszpański', 'Arabski', 1, 'Języki'),
                ('Który język jest używany w Brazylii?', 'Hiszpański', 'Angielski', 'Portugalski', 'Francuski', 2, 'Języki'),
                ('Jaki jest język urzędowy w Polsce?', 'Polski', 'Niemiecki', 'Angielski', 'Rosyjski', 0, 'Języki'),
                ('Jakiego języka używa się w Austrii?', 'Niemiecki', 'Francuski', 'Hiszpański', 'Włoski', 0, 'Języki'),
                ('Który język jest urzędowym w Kanadzie?', 'Angielski', 'Francuski', 'Oba powyższe', 'Hiszpański', 2, 'Języki'),
                ('Jakiego języka używa się w Meksyku?', 'Hiszpański', 'Portugalski', 'Angielski', 'Francuski', 0, 'Języki'),
                ('Jaki język jest urzędowy w Egipcie?', 'Arabski', 'Francuski', 'Angielski', 'Włoski', 0, 'Języki'),
                ('Jakiego języka używa się w Argentynie?', 'Portugalski', 'Hiszpański', 'Włoski', 'Francuski', 1, 'Języki'),
                ('Jakiego języka używa się w Indiach (urzędowy)?', 'Angielski', 'Hindi', 'Oba powyższe', 'Tamilski', 2, 'Języki'),
                ('Jaki język urzędowy ma Szwajcaria?', 'Niemiecki', 'Francuski', 'Włoski', 'Wszystkie powyższe', 3, 'Języki'),
                ('Który kraj słynie z produkcji sushi?', 'Chiny', 'Japonia', 'Korea', 'Wietnam', 1, 'Kultura'),
                ('Które zwierzę jest symbolem mądrości?', 'Lis', 'Wilk', 'Sowa', 'Wąż', 2, 'Kultura'),
                ('Co to jest haiku?', 'Rodzaj sushi', 'Rodzaj wiersza', 'Styl malarski', 'Taniec', 1, 'Kultura'),
                ('Jak nazywa się tradycyjna polska wycinanka?', 'Origami', 'Wycinanka łowicka', 'Kalejdoskop', 'Mandala', 1, 'Kultura'),
                ('Jakie jest najpopularniejsze hobby na świecie?', 'Wędkarstwo', 'Czytanie', 'Podróżowanie', 'Gotowanie', 0, 'Kultura'),
                ('Jak nazywa się popularny polski zespół ludowy?', 'Mazowsze', 'Śląsk', 'Podhale', 'Kujawiak', 0, 'Kultura'),
                ('Jak nazywa się tradycyjna japońska sztuka składania papieru?', 'Origami', 'Ikebana', 'Sumo', 'Haiku', 0, 'Kultura'),
                ('Który kraj jest znany z flamenco?', 'Włochy', 'Hiszpania', 'Francja', 'Grecja', 1, 'Kultura'),
                ('Jakie święto obchodzimy 24 grudnia?', 'Wielkanoc', 'Wigilia', 'Boże Ciało', 'Andrzejki', 1, 'Kultura'),
                ('Jak nazywa się słynny festiwal filmowy we Francji?', 'Berlin', 'Cannes', 'Wenecja', 'Toronto', 1, 'Kultura'),
                ('Który instrument należy do strunowych?', 'Flet', 'Skrzypce', 'Bęben', 'Trąbka', 1, 'Muzyka'),
                ('Kto był kompozytorem „Dla Elizy”?', 'Mozart', 'Beethoven', 'Chopin', 'Bach', 1, 'Muzyka'),
                ('Jak nazywa się instrument klawiszowy?', 'Gitara', 'Fortepian', 'Trąbka', 'Skrzypce', 1, 'Muzyka'),
                ('Który zespół nagrał „Yesterday”?', 'Queen', 'The Beatles', 'ABBA', 'The Rolling Stones', 1, 'Muzyka'),
                ('Kto jest nazywany „królem popu”?', 'Elvis Presley', 'Michael Jackson', 'Freddie Mercury', 'David Bowie', 1, 'Muzyka'),
                ('Jak nazywa się polski hymn narodowy?', 'Rota', 'Bogurodzica', 'Mazurek Dąbrowskiego', 'Gaude Mater Polonia', 2, 'Muzyka'),
                ('Który kompozytor napisał „Symfonię Z Nowego Świata”?', 'Dvořák', 'Mozart', 'Haydn', 'Beethoven', 0, 'Muzyka'),
                ('Jak nazywa się znana muzyczna nagroda filmowa?', 'Oscar', 'Grammy', 'Nobel', 'Pulitzer', 1, 'Muzyka'),
                ('Jak nazywa się tradycyjny instrument góralski?', 'Dudy', 'Cymbały', 'Lira korbowa', 'Fletnia Pana', 0, 'Muzyka'),
                ('Który z tych muzyków jest polskim pianistą?', 'Beethoven', 'Chopin', 'Bach', 'Verdi', 1, 'Muzyka'),
                ('Jakiego kraju producentem jest marka Toyota?', 'Japonia', 'Korea', 'Chiny', 'USA', 0, 'Motoryzacja'),
                ('Jak nazywa się najpopularniejszy samochód elektryczny na świecie?', 'Nissan Leaf', 'Tesla Model 3', 'BMW i3', 'Renault Zoe', 1, 'Motoryzacja'),
                ('Co oznacza skrót ABS w samochodzie?', 'Automatyczny Blok Silnika', 'Antyblokada Systemu', 'Automatyczny Bezpiecznik', 'Antyblokujący System Hamulcowy', 3, 'Motoryzacja'),
                ('Która marka produkuje model Golf?', 'Audi', 'Opel', 'Volkswagen', 'Ford', 2, 'Motoryzacja'),
                ('Jak nazywa się największe targi motoryzacyjne w Europie?', 'Geneva Motor Show', 'Paris Motor Show', 'Frankfurt Motor Show', 'Brussels Auto Show', 2, 'Motoryzacja'),
                ('Co to jest SUV?', 'Samochód Używany', 'Sportowy Uniwersalny Van', 'Samochód Uliczny', 'Sport Utility Vehicle', 3, 'Motoryzacja'),
                ('Jaka marka stworzyła model Mustang?', 'Chevrolet', 'Dodge', 'Ford', 'Toyota', 2, 'Motoryzacja'),
                ('Który kraj słynie z produkcji supersamochodów marki Ferrari?', 'Niemcy', 'Włochy', 'Francja', 'Hiszpania', 1, 'Motoryzacja'),
                ('Co oznacza skrót LPG?', 'Lekka Paliwa Gazowe', 'Liquefied Petroleum Gas', 'Lotnicze Paliwo Gazowe', 'Lokalne Paliwo Gazowe', 1, 'Motoryzacja'),
                ('Który samochód jest uznawany za najszybszy seryjnie produkowany na świecie (2024)?', 'Bugatti Chiron', 'Koenigsegg Jesko Absolut', 'Hennessey Venom F5', 'SSC Tuatara', 3, 'Motoryzacja'),
                ('W którym roku odbyły się pierwsze nowożytne igrzyska olimpijskie?', '1896', '1900', '1912', '1924', 0, 'Sport'),
                ('Kto zdobył najwięcej Złotych Piłek?', 'Messi', 'Ronaldo', 'Pele', 'Maradona', 0, 'Sport'),
                ('Jak nazywa się najbardziej znany wyścig kolarski na świecie?', 'Tour de France', 'Giro d’Italia', 'Vuelta a España', 'Paris-Roubaix', 0, 'Sport'),
                ('W jakim mieście odbyła się olimpiada w 2008 roku?', 'Ateny', 'Pekin', 'Sydney', 'Londyn', 1, 'Sport'),
                ('Który sport uprawia Iga Świątek?', 'Koszykówka', 'Tenis', 'Siatkówka', 'Szermierka', 1, 'Sport'),
                ('Który kraj jest kolebką sumo?', 'Chiny', 'Korea', 'Japonia', 'Tajlandia', 2, 'Sport'),
                ('Kto jest rekordzistą świata w biegu na 100 m?', 'Usain Bolt', 'Carl Lewis', 'Justin Gatlin', 'Asafa Powell', 0, 'Sport'),
                ('W którym roku Polska zdobyła złoty medal MŚ w siatkówce mężczyzn?', '2014', '2006', '2010', '2022', 0, 'Sport'),
                ('Jak nazywa się polski kierowca Formuły 1?', 'Michał Sołowow', 'Kuba Przygoński', 'Robert Kubica', 'Karol Basz', 2, 'Sport'),
                ('Ile trwa mecz piłki ręcznej (bez dogrywki)?', '40 min', '60 min', '80 min', '90 min', 1, 'Sport'),
                ('Który system operacyjny stworzył Bill Gates?', 'Linux', 'macOS', 'Windows', 'Android', 2, 'Technologia'),
                ('Jakie jest najpopularniejsze narzędzie do wideokonferencji w 2024 roku?', 'Skype', 'Zoom', 'Messenger', 'Telegram', 1, 'Technologia'),
                ('Który język programowania jest najstarszy?', 'Java', 'Python', 'Fortran', 'JavaScript', 2, 'Technologia'),
                ('Jak nazywa się największa wyszukiwarka internetowa?', 'Yahoo', 'Google', 'Bing', 'DuckDuckGo', 1, 'Technologia'),
                ('Jakie jest oficjalne logo Apple?', 'Gruszka', 'Jabłko', 'Winogrono', 'Pomarańcza', 1, 'Technologia'),
                ('W którym roku powstał Facebook?', '2001', '2004', '2010', '1999', 1, 'Technologia'),
                ('Który producent stworzył smartfon Galaxy?', 'Apple', 'Samsung', 'Huawei', 'Xiaomi', 1, 'Technologia'),
                ('Jakie jest podstawowe urządzenie wejścia?', 'Monitor', 'Klawiatura', 'Projektor', 'Drukarka', 1, 'Technologia'),
                ('Czym mierzy się pojemność dysku twardego?', 'Litr', 'Watt', 'Bajt', 'Volt', 2, 'Technologia'),
                ('Jaki jest skrót sztucznej inteligencji?', 'AI', 'SI', 'IQ', 'AG', 0, 'Technologia')
                ;
                ";
            tableCmd.ExecuteNonQuery();
        }

        EnsureHistoryTableExists();
    }
    catch (Exception ex)
    {
        Logger.LogError(ex);
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
    
    public static List<string> LoadCategories()
    {
        var categories = new List<string>();

        try
        {
            using var connection = new SqliteConnection($"Data Source={FileName}");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT DISTINCT Category FROM Questions ORDER BY Category";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(reader.GetString(0));
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex);
            throw;
        }

        return categories;
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
    
    public static List<Question> LoadQuestionsByCategory(string category, int limit = 10)
    {
        var questions = new List<Question>();

        try
        {
            using var connection = new SqliteConnection($"Data Source={FileName}");
            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = @"
            SELECT * FROM Questions
            WHERE Category = @category
            ORDER BY RANDOM()
            LIMIT @limit
        ";
            selectCmd.Parameters.AddWithValue("@category", category);
            selectCmd.Parameters.AddWithValue("@limit", limit);

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
}
