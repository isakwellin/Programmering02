/*  
    Isak Wellin, 23TEa, m01u04, 2026-01-17

    Det var inte så svårt, man känner igen mycket och så länge man har läst
    i moment 1 på kursolle och vet syntaxen är det precis som att bygga en bank
    i ett annat programmeringsspråk. Switch har jag aldrig använt förrut men det
    fungerade verkligen bra här och jag är stolt över slutresultatet.

*/


namespace Bankapplikation
{
    internal class Program
    {
        //Metod som skriver ut massa "-" vilket förbättar user interfacen
        public static void Print()
        {
            for (int i = 0; i < 50; i++)
            {
                Console.Write("-");
            }
        }

        //Metod som loopar igenom listan med transaktioner och beräknar saldot utifrån det
        public static String saldoBeräkning(List<int> list)
        {
            int summa = 0;
            foreach (int i in list)
            {
                summa += i;
            }
            return Convert.ToString(summa);
        }
        static void Main(string[] args)
        {
            //Sätter variabel för att hålla koll om programmet fortfarande ska köras
            bool aktiv = true;
            List<int> transaktioner = new List<int>();
            transaktioner.Add(1000);
            do
            {
                //Beräknar saldot med hjälp av metoden saldoBeräkning()
                int saldo = Convert.ToInt32(saldoBeräkning(transaktioner));

                //Skriver ut menyn med de olika valen
                Console.WriteLine("\n1. Insättning\n2. Uttag\n3. Visa saldo\n4. Avsluta programmet");
                int val = Convert.ToInt32(Console.ReadLine());
                switch (val)
                {
                    //Insättning, frågar användaren sedan sätter in värdet in i listan med transaktioner
                    case 1:
                        Console.WriteLine("Hur mycket vill du sätta in?");
                        transaktioner.Add(Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine("Klart!");
                        Print();
                        break;

                    //Uttag, frågar användaren sedan sätter in värdet fast negativt in i listan med transaktioner
                    case 2:
                        Console.WriteLine("Hur mycket vill du ta ut?");
                        int uttag = Convert.ToInt32(Console.ReadLine());
                        //Kollar om uttaget är större än saldot, i så fall avbryt
                        if(uttag > saldo)
                        {
                            Console.WriteLine("Du kan inte ta ut mer än vad du har!");
                        }
                        //Annars lägg till värdet i listan och fortsätt
                        else
                        {
                            transaktioner.Add(-uttag);
                            Console.WriteLine("Klart!");
                        }
                        Print();
                        break;

                    //Visar saldot som är beräknat sedan tidigare
                    case 3:
                        Console.WriteLine("Saldo: {0}", saldo);
                        Print();
                        break;

                    //Avbryter programmet
                    case 4:
                        aktiv = false;
                        break;
                }
            } while (aktiv);
        }
    }
}
