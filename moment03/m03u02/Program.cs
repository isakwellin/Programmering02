//Isak Wellin, 23TEa, m03u02, 2026-02-18

using System.Reflection.Metadata.Ecma335;

namespace VehicleCollection
{
    internal class Program
    {
        //Deklarera klassvariabel som innehåller alla fordon
        public static List<Vehicle> vehicleList = new List<Vehicle>();

        //Metod för att upprepa valet att antingen lägga till eller ta bort en bil
        //int i används för att bestämma om en bil eller lorry ska läggas till eller tas bort
        public static void repeatAction(int i)
        {
            //While-loop för att fortsätta fråga användaren efter upprepning
            while (true)
            {
                //Frågar om användaren vill upprepa valet
                Console.Write("Vill du upprepa valet? (J/N): ");

                //Kollar om svaret är ja (J) annars nej
                char ch = Convert.ToChar(Console.ReadLine());

                //Om svaret är ja (J), då anropar man funktionen addCar, addTrolley eller removeVehicle beroende på valet
                if (Char.ToUpper(ch) == 'J')
                {
                    //Kollar värdet på i och anropar passande metod

                    if (i == 1)
                    {
                        addCar();
                    }

                    else if (i == 2) 
                    {
                        addLorry();
                    }
                    else 
                    {
                        removeVehicle();
                    }
                    continue; //Om svaret är ja, starta om loopen
                }
                else {
                    break;
                }
            }
        }
        //Metod för att tömma listan med fordon
        public static void emptyList()
        {
            vehicleList.Clear();
        }

        //Metod för att ta bort en bil eller en lastbil (lorry)
        public static void removeVehicle()
        {
            Console.WriteLine("Dessa fordon finns i din lista");

            //Skriver ut alla bilar som finns
            printList();

            //Låter användaren välja fordonet att ta bort
            Console.Write("\nVälj ett fordon att ta bort från listan [0 ångrar]: ");
            int removeIndex = Convert.ToInt16(Console.ReadLine());

            //Tar bort fordonet ur listan med hjälp av index siffran
            if (removeIndex != 0)
            {
                vehicleList.RemoveAt(removeIndex - 1);
            }
        }

        public static void addVehicleAtStart()
        {
            vehicleList.Add(new Car("ABC123", "Volvo", "V70", 2012, false));
            vehicleList.Add(new Lorry("DEF456", "BMW", "520", 2011, true, 15000));
            vehicleList.Add(new Car("GHI789", "Saab", "95", 2006, false));
        }

        //Metod för att skriva ut listan med fordon
        public static void printList()
        {
            int i = 1;

            Console.WriteLine("\n\nNr\tRegNr\tMärke\tModel\tÅrsmodell\tTill Salu?");

            foreach (Vehicle v in vehicleList)
            {
                Console.Write(i++);
                Console.WriteLine(v.ToStringList());
            }
        }

        //Metod för att lägga till en ny bil genom användarens värden
        public static void addCar()
        {
            //Frågar efter information om bilen och sparar det i variablar som används senare vid skapelse av nytt objekt

            Console.WriteLine("\n\nAnge information om bilen");

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
            if (Char.ToUpper(ch) == 'J')
            {
                forSale = true;
            }

            vehicleList.Add(new Car(regNr, make, model, year, forSale));
        }

        //Metod för att lägga till en ny lastbil (lorry) baserat på användarens värden
        public static void addLorry()
        {
            //Frågar efter information om lastbilen och sparar det i variablar som används senare vid skapelse av nytt objekt

            Console.WriteLine("\n\nAnge information om lastbilen");

            Console.Write("Registreringsnummer: ");
            String regNr = Console.ReadLine();

            Console.Write("Bilmärke: ");
            String make = Console.ReadLine();

            Console.Write("Modell: ");
            String model = Console.ReadLine();

            Console.Write("Årsmodell: ");
            int year = Convert.ToInt16(Console.ReadLine());

            Console.Write("Lastkapacitet (kg): ");
            int last = Convert.ToInt32(Console.ReadLine());

            Console.Write("Till salu (J/N): ");
            char ch = Convert.ToChar(Console.ReadLine());

            bool forSale = false;
            if (Char.ToUpper(ch) == 'J')
            {
                forSale = true;
            }

            vehicleList.Add(new Lorry(regNr, make, model, year, forSale, last));
        }

        //Metod som skriver ut menyn till programmet
        public static char menu(List<Vehicle> vehicleList)
        {
            //Bygger upp menyn som sträng
            String menu = "\n\n#############################" +
                "\n##                         ##" +
                "\n## Programmeny             ##" +
                "\n## Antal bilar: " + vehicleList.Count + " st.      ##" +
                "\n##                         ##" +
                "\n#############################" +
                "\n1. Skriv ut listan" +
                "\n2. Lägg till bil" +
                "\n3. Lägg till lastbil" +
                "\n4. Ta bort fordon" +
                "\n5. Töm hela listan" +
                "\n0. Avsluta" +
                "\nAnge ditt val: ";

            //Skriver ut menyn
            Console.Write(menu);

            //Returnerar resultatet
            return Console.ReadKey().KeyChar;
        }

        static void Main(string[] args)
        {
            addVehicleAtStart();
            char menuSelection;

            //Loopen där programmet körs, anropar metoder beroende på valet
            do
            {
                switch (menuSelection = menu(vehicleList))
                {
                    case '0':
                        break;

                    case '1':
                        printList();
                        break;

                    case '2':
                        addCar();
                        repeatAction(1);
                        break;

                    case '3':
                        addLorry();
                        repeatAction(2);
                        break;

                    case '4':
                        removeVehicle();
                        repeatAction(3);
                        break;

                    case '5':
                        emptyList();
                        break;

                    default:
                        break;
                }

            } while (menuSelection != '0');

        }
    }
}