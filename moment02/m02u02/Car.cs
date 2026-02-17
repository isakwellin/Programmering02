using System;
using System.Collections.Generic;
using System.Text;

namespace CarDemoCollection
{
    public class Car
    {
        //Medlemsvariablar

        private String regNr;
        private String make;
        private String model;
        private int year;
        private bool forSale;

        //Properties
        public String RegNr
        {
            get { return regNr; }
            set { regNr = value; }
        }

        public String Make
        {
            get { return make; }
            set { make = value; }
        }

        public String Model
        {
            get { return model; }
            set { model = value; }
        }

        public int Year
        {
            get { return year; }
            set
            {
                if ((value < 1900))
                {
                    year = -1;
                }
                else
                {
                    year = value;
                }
            }
        }

        public bool ForSale
        {
            get { return forSale; }
            set { forSale = value; }
        }

        //Konstruktor
        public Car(String regNr, String make, String model, int year, bool forSale)
        {
            this.RegNr = regNr;
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.ForSale = forSale;
        }


        //Metod som förbereder utskrift av bilinformation
        //Returnerar den formaterade strängen med information
        public override String ToString()
        {
            return String.Format("\nBilinformation\nReg: {0}, {1} {2} [{3}]\n{4}",
                this.RegNr, this.Make, this.Model, this.YearToString(), this.ForSaleToString());
        }

        //Metod som förbereder utskrift av bilinformation i listform
        //Returnerar den formaterade strängen
        public String ToStringList()
        {
            String s = String.Format("\t{0}\t{1}\t{2}\t{3}",
                this.RegNr, this.Make, this.Model, this.YearToString());

            if (this.ForSale)
            {
                s += "\t\tJA";
            }
            else
            {
                s += "\t\tNEJ";
            }
            return s;
        }

        //Metod för att göra om year till en string
        public String YearToString()
        {
            if (this.year == -1)
            {
                return "felaktigt årtal";
            }
            else
            {
                return Convert.ToString(year);
            }
        }

        //Metod för att göra om ForSale till en string
        public String ForSaleToString()
        {
            if (this.ForSale)
            {
                return "Bilen är till salu";
            }
            else
            {
                return "Bilen är inte till salu";
            }
        }

    }
}