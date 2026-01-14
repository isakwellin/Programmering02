// Isak Wellin, 23TEa, m01u02, 2026-01-14

namespace m02u02
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int penCount = 3;
            int pricePerPen = 4;
            double appleWeight = 0.2;
            int applePricePerKg = 19;

            double applePrice = Math.Round((appleWeight * applePricePerKg), 2);
            int penPrice = penCount * pricePerPen;
            double total = applePrice + penPrice;

            //Utskrift med konkatenering
            Console.WriteLine("Jag handlade " + penCount + " pennor för " + penPrice + "kr och 1 äpple för " + applePrice + "kr vilket blev totalt " + total + "kr");

            //Utskrift genom uppbyggd sträng
            string svar1 = "Jag handlade ";
            string svar2 = " pennor för ";
            string svar3 = "kr och 1 äpple för ";
            string svar4 = "kr vilket blev totalt ";
            string svar5 = "kr";

            Console.WriteLine(svar1 + penCount + svar2 + penPrice + svar3 + applePrice + svar4 + total + svar5);

            //Utskrift med öppen write utan radbrytning
            Console.Write("Jag handlade {0} pennor för {1}kr och 1 äpple för {2}kr vilket blev totalt {3}kr", penCount, penPrice, applePrice, total);

            //Utskrift med $-metoden
            Console.WriteLine($"\nJag handlade {penCount} pennor för {penPrice}kr och 1 äpple för {applePrice}kr vilket blev totalt {total}kr");
        }
    }
}
