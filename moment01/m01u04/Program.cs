// Isak Wellin, 23TEa, m01u04, 2026-01-17

namespace m01u04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime dt = DateTime.Now;
            int hour = dt.Hour;
            Console.WriteLine(hour);
            
            if(hour > 8 & hour < 16)
            {
                Console.WriteLine("Skoldagen pågår");
            }
            else if (hour > 0 & hour < 7)
            {
                Console.WriteLine("Skoldagen har inte börjat");
            }
            else
            {
                Console.WriteLine("Skoldagen är slut");
            }
        }
    }
}
