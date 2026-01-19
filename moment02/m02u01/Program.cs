//Isak Wellin, 23TEa, m02u01, 2026-01-19

namespace CarDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Frågar efter information om bilen och sparar det i variablar som används senare vid skapelse av nytt objekt

            Console.WriteLine("Ange information om bilen");

            Console.Write("Registreringsnummer: ");
            String regNr = Console.ReadLine();

            Console.Write("Bilmärke: ");
            String make = Console.ReadLine();

            Console.Write("Modell: ");
            String model = Console.ReadLine();

            Console.Write("Årsmodell: ");
            int year = Convert.ToInt16(Console.ReadLine());

            Console.Write("Till salu (J/N): ");
            char ch = Convert.ToChar(Console.ReadLine());

            bool forSale = false;
            if(Char.ToUpper(ch) == 'J')
            {
                forSale = true;
            }

            Car c = new Car (regNr, make, model, year, forSale);

            Console.WriteLine("\n" + c.ToString());
            //Skriver ut info om bilarna

        }
    }
}

