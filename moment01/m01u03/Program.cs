// Isak Wellin, 23TEa, m01u03, 2026-01-17

namespace m01u03
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Deklarerar penCount och appleWeight men värdet fås senare av användaren
            int penCount;
            int appleCount;
            int pricePerPen = 4;
            double appleWeight;
            int applePricePerKg = 19;

            Console.WriteLine("Hur många pennor vill du köpa? ");
            penCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Hur många äpplen vill du köpa? ");
            appleCount = Convert.ToInt32(Console.ReadLine());

            //Antar att varje äpple väger 0.2kg och använder det senare för att få priset
            appleWeight = (0.2 * appleCount);

            double applePrice = Math.Round((appleWeight * applePricePerKg), 2);
            int penPrice = penCount * pricePerPen;
            double total = applePrice + penPrice;

            //Utskrift med konkatenering
            Console.WriteLine("Jag handlade " + penCount + " pennor för " + penPrice + "kr och " + appleCount + " äpplen för " + applePrice + "kr vilket blev totalt " + total + "kr");

            //Tog bort de andra utskrifterna på grund av behövdes inte i denna uppgift
        }
    }
}