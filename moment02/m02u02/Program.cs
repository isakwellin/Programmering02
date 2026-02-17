//Isak Wellin, 23TEa, m02u02, 2026-01-26

using System.Reflection.Metadata.Ecma335;

namespace CarDemoCollection
{
    internal class Program
    {

        //Metod för att upprepa valet att antingen lägga till eller ta bort en bil
        //int i används för att bestämma om en bil ska läggas till eller tas bort
        public static List<Car> repeatAction(List<Car> carList, int i)
        {
            //While-loop för att fortsätta fråga användaren efter upprepning
            while (true)
            {
                //Frågar om användaren vill upprepa valet
                Console.Write("Vill du upprepa valet? (J/N): ");

                //Kollar om svaret är ja (J) annars nej
                char ch = Convert.ToChar(Console.ReadLine());

                //Om svaret är ja (J), då anropar man funktionen addCar eller removeCar beroende på valet
                if (Char.ToUpper(ch) == 'J')
                {
                    if (i == 0)
                    {
                        addCar(carList);
                    }
                    else
                    {
                        removeCar(carList);
                    }
                }
                else //Annars om svaret inte är ja, returnera carList
                {
                    return carList;
                }
                continue; //Om svaret är ja, starta om loopen
            }
        }
        //Metod för att tömma listan med bilarna
        public static List<Car> emptyList(List<Car> carList)
        {
            carList.Clear();
            return carList;
        }
        public static List<Car> removeCar (List<Car> carList)
        {
            Console.WriteLine("Dessa bilar finns i din lista");
            
            //Skriver ut alla bilar som finns
            printList(carList);

            //Låter användaren välja bilen att ta bort
            Console.Write("\nVälj en bil att ta bort från listan [0 ångrar]: ");
            int removeIndex = Convert.ToInt16(Console.ReadLine());

            //Tar bort bilen ur listan med hjälp av index siffran
            if (removeIndex != 0)
            {
                carList.RemoveAt(removeIndex - 1);
            }

            //Returnerar resultat
            return carList;
        }

        //Metod som lägger till bilar i listan för att hjälpa med testning
        public static List<Car> addCarsAtStart()
        {

            //Gör en lokal lista och lägg sedan till information om tre olika bilar
            List<Car> carList = new List<Car>();
            carList.Add(new Car("ABC123", "Volvo", "V70", 2012, false));
            carList.Add(new Car("DEF456", "BMW", "520", 2011, true));
            carList.Add(new Car("GHI789", "Saab", "95", 2006, false));
            return carList;
        }
        public static void printList(List<Car> carList)
        {
            int i = 1;

            Console.WriteLine("\n\nNr\tRegNr\tMärke\tModel\tÅrsmodell\tTill Salu?");

            foreach (Car car in carList)
            {
                Console.Write(i++);
                Console.WriteLine(car.ToStringList());
            }
        }

        //Metod för att lägga till en ny bil genom användarens värden
        public static List<Car> addCar (List<Car> carList)
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

            carList.Add(new Car(regNr, make, model, year, forSale));

            return carList;
        }

        //Metod som skriver ut menyn till programmet
        public static char menu(List<Car> carList)
        {
            //Bygger upp menyn som sträng
            String menu = "\n\n#############################" +
                "\n##                         ##" +
                "\n## Programmeny             ##" +
                "\n## Antal bilar: " + carList.Count + " st.      ##" +
                "\n##                         ##" +
                "\n#############################" +
                "\n1. Skriv ut listan" +
                "\n2. Lägg till bil" +
                "\n3. Ta bort bil" +
                "\n4. Töm hela listan" +
                "\n0. Avsluta" +
                "\nAnge ditt val: ";

            //Skriver ut menyn
            Console.Write(menu);

            //Returnerar resultatet
            return Console.ReadKey().KeyChar;
        }

        static void Main(string[] args)
        {
            List<Car> carList = addCarsAtStart();
            char menuSelection;


            do
            {
                switch (menuSelection = menu(carList))
                {
                    case '0':
                        break;

                    case '1':
                        printList(carList);
                        break;

                    case '2':
                        carList = addCar(carList);
                        repeatAction(carList, 0);
                        break;

                    case '3':
                        removeCar(carList);
                        repeatAction(carList, 1);
                        break;

                    case '4':
                        emptyList(carList);
                        break;

                    default: 
                        break;
                }

            } while (menuSelection != '0');

        }
    }
}
