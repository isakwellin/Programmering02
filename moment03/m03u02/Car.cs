using System;
using System.Collections.Generic;
using System.Text;
using VehicleCollection;

namespace VehicleCollection
{
    public class Car : Vehicle
    {

        //Metod som förbereder utskrift av bilinformation
        //Returnerar den formaterade strängen med information
        public override String ToStringList()
        {
            String s = String.Format("\t{0}\t{1}\t{2}\t[{3}]",
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

        public Car(String regNr, String make, String model, int year, bool forSale)
            : base(regNr, make, model, year, forSale)
        {

        }
    }
}