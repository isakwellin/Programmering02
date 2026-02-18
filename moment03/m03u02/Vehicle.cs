using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleCollection
{


    public abstract class Vehicle
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
        public Vehicle(String regNr, String make, String model, int year, bool forSale)
        {
            this.RegNr = regNr;
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.ForSale = forSale;
        }

        public abstract String ToStringList();

        //Metod som förbereder utskrift av bilinformation
        //Returnerar den formaterade strängen med information
        public override String ToString()
        {
            return String.Format("\nBilinformation\nReg: {0}, {1} {2} [{3}]\n{4}",
                this.RegNr, this.Make, this.Model, this.YearToString(), this.ForSaleToString());
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