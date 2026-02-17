using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleDemo
{
    public class Lorry : Vehicle
    {

        //Metod som förbereder utskrift av bilinformation
        //Returnerar den formaterade strängen med information
        public override String ToStringList()
        {
            String fs;
            if (this.ForSale)
            {
                fs = "\t\tJA";
            }
            else
            {
                fs = "\t\tNEJ";
            }

                return String.Format("\t{0}\t{1}\t{2}\t[{3}] {4}\tMaxlast: {5}kg.",
                    this.RegNr, this.Make, this.Model, this.YearToString(), fs, this.Load);
        }

        public new String ToString()
        {
            String s = base.ToString();
            s += String.Format("\nMaxlast: {0}kg", this.Load);
            return s;
        }

        //Medlemsvariablar

        //Maxlast, kg

        int load;
        public int Load
        {
            get { return load; }
            set { load = value; }
        }
       

        public Lorry(String regNr, String make, String model, int year, bool forSale, int load)
            : base(regNr, make, model, year, forSale)
        {
            this.Load = load;
        }
    }
}
