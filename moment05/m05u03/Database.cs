using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarGUIdb
{
    internal class Database
    {
        public SQLiteConnection dbConn;         //connectionvariabeln
        string databaseFilename = "./car.db";   //Databasens namn och path "./" innebär root

        public Database()
        {
            //Skapa objektet dbConn
            dbConn = new SQLiteConnection("Data Source=" + databaseFilename);

            //Skapa databasfilen om den inte finns
            if (!File.Exists(databaseFilename))
            {
                SQLiteConnection.CreateFile(databaseFilename);
            }
        }

        /// <summary>
        /// Om dbConn inte är öppen så öppna den
        /// </summary>
        public void OpenConnection()
        {
            if(dbConn.State != System.Data.ConnectionState.Open)
            {
                dbConn.Open();
            }
        }

        /// <summary>
        /// Om dbConn inte är stängd så stäng den
        /// </summary>
        public void CloseConnection()
        {
            if (dbConn.State != System.Data.ConnectionState.Closed)
            {
                dbConn.Close();
            }
        }

    }
}
