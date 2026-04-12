using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Drawing.Text;

namespace CarAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            createTable();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Om längden på regNr är större än 0
            if(txbRegNr.Text.Length > 0)
            {
                //Hämta regNr
                string regNr = txbRegNr.Text.ToUpper();

                //Tömma textboxen
                txbRegNr.Text = "";

                //Anropa metoden
                printData(regNr);

            }
            else
            {
                MessageBox.Show("Du måste ange ett registreringsnummer!", "Inmatning saknas",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void printData(string regNr)
        {
            //Variabler
            string token = "jtBt2fWF8vS0v1B5EcZT9111j9YExcUfiLHq5-4xSWY";
            string call = $"https://data.biluppgifter.se/api/v1/lookup/vehicle/regno/{regNr}";

            try
            {
                //Skapar ett objekt med API-anropet
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(call);
                request.Method = "GET";
                request.Headers["Authorization"] = $"Bearer {token}";

                //Skapa ett svarsobjekt
                WebResponse response = request.GetResponse();

                //Läser av strömmen
                StreamReader reader = new StreamReader(response.GetResponseStream());

                //Gör om till sträng
                string car_JSON = reader.ReadToEnd();

                //Gör om strängen till ett JSON-objekt
                JObject jo = JObject.Parse(car_JSON);

                //Skriver ut objektet i rutan
                txtCarInfo.Text = jo.ToString();

                // Hämtar data från JSON-objektet och formaterar det till en sträng, om den är tom så sätts den till en tom sträng
                string type = jo.SelectToken("vehicle.type")?.ToString() ?? "";
                string make = jo.SelectToken("vehicle.make")?.ToString() ?? "";
                string model = jo.SelectToken("vehicle.model")?.ToString() ?? "";
                string vehicleYear = jo.SelectToken("vehicle.vehicle_year")?.ToString() ?? "";
                string color = jo.SelectToken("vehicle.color")?.ToString() ?? "";

                //Fyll tabellen med data
                // Skapar labels som skall läggas i tabellen
                Label l1 = new Label();
                l1.Text = regNr;
                Label l2 = new Label();
                l2.Text = type;
                Label l3 = new Label();
                l3.Text = make;
                Label l4 = new Label();
                l4.Text = model;
                Label l5 = new Label();
                l5.Text = vehicleYear;
                Label l6 = new Label();
                l6.Text = color;

                //Rensar tabellens innehåll och lägger till rubrikerna med hjälp av metoden createTable()
                tlpCarInfo.Controls.Clear();
                createTable();

                // Lägg in labels i tabellen
                int row = 0;
                tlpCarInfo.Controls.Add(l1, 1, row++);
                tlpCarInfo.Controls.Add(l2, 1, row++);
                tlpCarInfo.Controls.Add(l3, 1, row++);
                tlpCarInfo.Controls.Add(l4, 1, row++);
                tlpCarInfo.Controls.Add(l5, 1, row++);
                tlpCarInfo.Controls.Add(l6, 1, row++);

            }
            catch (Exception e)
            {
                //Töm textrutan
                txtCarInfo.Text = "";

                //Skriver ut ett meddelande
                MessageBox.Show($"Registreringsnummer  [{regNr}] hittades inte i databasen.\nMeddelande: {e.Message}",
                    "Felaktigt registreringsnummer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Metod som skapar tabellens innehåll på nytt, användbart då man söker på ett nytt registreringsnummer
        private void createTable()
        {
            // Skapar labels som skall läggas i tabellen
            Label l1 = new Label();
            l1.Text = "RegNr";
            Label l2 = new Label();
            l2.Text = "Typ";
            Label l3 = new Label();
            l3.Text = "Märke";
            Label l4 = new Label();
            l4.Text = "Modell";
            Label l5 = new Label();
            l5.Text = "Årsmodell";
            Label l6 = new Label();
            l6.Text = "Färg";

            // Fetmarkera
            l1.Font = new Font(l1.Font, FontStyle.Bold);
            l2.Font = new Font(l2.Font, FontStyle.Bold);
            l3.Font = new Font(l3.Font, FontStyle.Bold);
            l4.Font = new Font(l4.Font, FontStyle.Bold);
            l5.Font = new Font(l5.Font, FontStyle.Bold);
            l6.Font = new Font(l6.Font, FontStyle.Bold);

            // Lägg in labels i tabellen
            int row = 0;
            tlpCarInfo.Controls.Add(l1, 0, row++);
            tlpCarInfo.Controls.Add(l2, 0, row++);
            tlpCarInfo.Controls.Add(l3, 0, row++);
            tlpCarInfo.Controls.Add(l4, 0, row++);
            tlpCarInfo.Controls.Add(l5, 0, row++);
            tlpCarInfo.Controls.Add(l6, 0, row++);
        }
    }
}
