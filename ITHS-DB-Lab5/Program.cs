using System;

namespace ITHS_DB_Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            Hemsida h = new Hemsida();
            h.Startsida();
        }
        
        public static void LineDivide()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.ResetColor();
        }

        class Hemsida
        {
            public void Startsida()
            {
                Mongoconnect db = new Mongoconnect("Labb5");

                Console.WriteLine("Tryck på 1 om du vill skriva en Bloggpost");
                Console.WriteLine("Tryck på 2 om du vill ta bort en Bloggpost");
                Console.WriteLine("Tryck på 3 om du vill se vilka Bloggposter som finns");
                Console.WriteLine("Tryck på 4 om du vill se vilka kategorier som finns");
                Console.WriteLine("Tryck på 5 för att filtrera blogposter på kategori");
                Console.WriteLine("Tryck på 6 för att filtrera efter en specifik bloggpost");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kryssa för i hörnet för att avsluta programmet");
                Console.ResetColor();

                string svar = Console.ReadLine();
                int input;
                while(!int.TryParse(svar,out input) || input > 6 || input <= 0)
                {
                    Console.WriteLine("Felaktigt svar, skriv in en siffra mellan 1-6");
                    svar = Console.ReadLine();
                }

                if (input == 1)
                {
                    Console.Clear();
                    Blog b = new Blog();
                    Console.WriteLine("Skriv en Rubrik på posten:");
                    b.Rubrik = Console.ReadLine();

                    Console.WriteLine("Skriv texten på posten:");
                    b.Text = Console.ReadLine();

                    Console.WriteLine("Hur många kategorier ska Blogposten ha?");
                    int antal = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Skriv vilken/vilka kategorier posten tillhör:");
                    b.Kategori = new string[antal];

                    for (int i = 0; i < antal; i++)
                    {
                        Console.WriteLine("Kategori: " + (i + 1));
                        b.Kategori[i] = Console.ReadLine().ToUpper();
                    }
                    db.AddBlogg("Bloggar", b);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Bloggposten har publicerats!");
                    Console.ResetColor();
                    Console.WriteLine("Tryck på valfri knapp för att återgå till Startsidan");
                    Console.ReadKey();
                    Console.Clear();
                    Startsida();
                }
                else if (input == 2)
                {
                    Console.Clear();
                    var visa0 = db.VisaBlogg<Blog>("Bloggar");
                    foreach (var g in visa0)
                    {
                        Console.WriteLine("ID:");
                        Console.WriteLine(g.ID);
                        Console.WriteLine("");
                        Console.WriteLine("Rubrik:");
                        Console.WriteLine(g.Rubrik);
                        Console.WriteLine("");
                        Console.WriteLine("Text:");
                        Console.WriteLine(g.Text);
                        Console.WriteLine("");
                        Console.WriteLine("Skapad Datum:");
                        Console.WriteLine(g.SkapadDatum);
                        Console.WriteLine("");
                        Console.WriteLine("Kategori:");
                        for (int i = 0; i < g.Kategori.Length; i++)
                        {
                            Console.WriteLine("- " + g.Kategori[i]);
                        }
                        LineDivide();
                    }
                    Console.WriteLine("Skriv in Bloggpostens Id du vill radera (Tips, Kopiera och klistra in ID.et från ovan)");
                    string IdInput = Console.ReadLine();
                    db.RaderaBlogg<Blog>("Bloggar", IdInput);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Bloggposten Raderad!");
                    Console.ResetColor();
                    Console.WriteLine("Tryck på valfri knapp för att återgå till Startsidan");
                    Console.ReadKey();
                    Console.Clear();
                    Startsida();
                }
                else if (input == 3)
                {
                    Console.Clear();
                    var visa3 = db.VisaBlogg<Blog>("Bloggar");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Antal Bloggar: " + visa3.Count);
                    Console.ResetColor();
                    foreach (var g in visa3)
                    {
                        Console.WriteLine("ID:");
                        Console.WriteLine(g.ID);
                        Console.WriteLine("");
                        Console.WriteLine($"Rubrik:");
                        Console.WriteLine($"{g.Rubrik}");
                        Console.WriteLine("");
                        Console.WriteLine("Skapad Datum:");
                        Console.WriteLine(g.SkapadDatum);
                        Console.WriteLine("");
                        Console.WriteLine("Kategori:");
                        for (int i = 0; i < g.Kategori.Length; i++)
                        {
                            Console.WriteLine("- " + g.Kategori[i]);
                        }
                        LineDivide();
                    }
                    Console.WriteLine("Tryck på valfri knapp för att återgå till Startsidan");
                    Console.ReadKey();
                    Console.Clear();
                    Startsida();
                }
                else if (input == 4)
                {
                    Console.Clear();
                    var visa4 = db.VisaKategori<Blog>("Bloggar");
                    Console.WriteLine("Kategori/Kategorier som finns är:");
                    foreach (var g in visa4)
                    {
                        for (int i = 0; i < g.Kategori.Length; i++)
                        {
                            Console.WriteLine
                            (
                                "- " + g.Kategori[i] + " => " + db.VisaKategoriFilter<Blog>("Bloggar", g.Kategori[i]).Count +
                                " antal Bloggpost/Bloggposter"
                            );
                        }
                    }
                    Console.WriteLine("");
                    Console.WriteLine("Tryck på valfri knapp för att återgå till Startsidan");
                    Console.ReadKey();
                    Console.Clear();
                    Startsida();
                }
                else if (input == 5)
                {
                    Console.Clear();
                    Console.WriteLine("Skriv in den kategorin du vill filtera bloggposterna på");
                    string kategoriInput = Console.ReadLine().ToUpper();
                    var visa5 = db.VisaKategoriFilter<Blog>("Bloggar", kategoriInput);
                    Console.WriteLine("");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(visa5.Count + " Matchning/Matchningar!");
                    Console.ResetColor();

                    foreach (var g in visa5)
                    {
                        Console.WriteLine("ID:");
                        Console.WriteLine(g.ID);
                        Console.WriteLine("");
                        Console.WriteLine("Rubrik:");
                        Console.WriteLine(g.Rubrik);
                        Console.WriteLine("");
                        Console.WriteLine("Text:");
                        Console.WriteLine(g.Text);
                        Console.WriteLine("");
                        Console.WriteLine("Skapad datum:");
                        Console.WriteLine(g.SkapadDatum);
                        Console.WriteLine("");
                        Console.WriteLine("Kategori:");
                        for (int i = 0; i < g.Kategori.Length; i++)
                        {
                            Console.WriteLine("- " + g.Kategori[i]);
                        }
                        LineDivide();
                    }
                    Console.WriteLine("Tryck på valfri knapp för att återgå till Startsidan");
                    Console.ReadKey();
                    Console.Clear();
                    Startsida();
                }
                else if (input == 6)
                {
                    Console.Clear();
                    Console.WriteLine("Skriv in ID på den Bloggposten du specifikt vill söka efter");
                    Console.WriteLine("OBS du kan hämta alla BlogPosters ID i siffra 3 inputet på hemsidan");
                    string IdInput = Console.ReadLine();
                    var visa6 = db.VisaBloggpostFilter<Blog>("Bloggar", IdInput);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(visa6.Count + " Matchning/Matchningar!");
                    Console.ResetColor();

                    foreach (var g in visa6)
                    {
                        Console.WriteLine("ID:");
                        Console.WriteLine(g.ID);
                        Console.WriteLine("");
                        Console.WriteLine("Rubrik:");
                        Console.WriteLine(g.Rubrik);
                        Console.WriteLine("");
                        Console.WriteLine("Text:");
                        Console.WriteLine(g.Text);
                        Console.WriteLine("");
                        Console.WriteLine("Skapad datum:");
                        Console.WriteLine(g.SkapadDatum);
                        Console.WriteLine("");
                        Console.WriteLine("Kategori:");

                        for (int i = 0; i < g.Kategori.Length; i++)
                        {
                            Console.WriteLine("- " + g.Kategori[i]);
                        }
                        LineDivide();
                    }
                    Console.WriteLine("Tryck på valfri knapp för att återgå till Startsidan");
                    Console.ReadKey();
                    Console.Clear();
                    Startsida();
                }
            }
        }
    }
}
