// Isak Wellin, 23TEa, m03u01, 2026-02-17

namespace VehicleDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Skapar ny bil c
            Car c = new Car("ABC123", "Volvo", "V70", 2013, true);

            //Skapar ny objekt av klassen lorry l
            Lorry l = new Lorry("DEF456", "Scania", "420", 2008, false, 15000);

            //Skriver ut information om lorry l och car c
            Console.WriteLine("\n" + l.ToString());
            Console.WriteLine("\n" + c.ToString());

            //Samma sak fast i list form
            Console.WriteLine("\n" + c.ToStringList());
            Console.WriteLine("\n" + l.ToStringList());
        }
    }
}
