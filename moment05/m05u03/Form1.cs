using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarGUIdb
{
    public partial class Form1 : Form
    {

        Database dbObject = new Database();

        public Form1()
        {
            InitializeComponent();
            fillListViewFromDataBase();
            checkRemoveButton();
        }

        /// <summary>
        /// Hämta data från databas och lagra dessa i ListView
        /// </summary>
        private void fillListViewFromDataBase()
        {
            string q_select = "SELECT * FROM car;";
            SQLiteCommand dbCommand = new SQLiteCommand(q_select, dbObject.dbConn);
            dbObject.OpenConnection();

            //Hämtar resultatet från databasen
            SQLiteDataReader res = dbCommand.ExecuteReader();

            //Om det finns rader från databasen
            if(res.HasRows)
            {
                while(res.Read())       //Hämta nästa rad (post) från resultatet
                {
                    //Lägg till ett item i ListViewItem
                    ListViewItem item = new ListViewItem(Convert.ToString(res["regNr"])); //Skapar huvuditem
                    item.SubItems.Add(Convert.ToString(res["make"]));
                    item.SubItems.Add(Convert.ToString(res["model"]));
                    item.SubItems.Add(Convert.ToString(res["year"]));

                    //Checkbox / "boolean"
                    if (Convert.ToInt16(res["forSale"])==1)
                    {
                        item.SubItems.Add("Yes");
                    }
                    else
                    {
                        item.SubItems.Add("No");
                    }

                    //Koppla item till vår ListView
                    lsvCars.Items.Add(item);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Kollar om någon textruta är tom
            if (string.IsNullOrEmpty(txtRegNr.Text) || string.IsNullOrEmpty(txtMake.Text) || string.IsNullOrEmpty(txtModel.Text) || string.IsNullOrEmpty(txtYear.Text))
            {
                //msg-box visar meddelande till användaren
                MessageBox.Show("Du måste fylla i alla rutorna", "Felaktig inmatning");
                return;
            }

            //Lägg till ett listItem i ListView
            ListViewItem item = new ListViewItem(txtRegNr.Text);
            item.SubItems.Add(txtMake.Text);
            item.SubItems.Add(txtModel.Text);
            item.SubItems.Add(txtYear.Text);

            //checkbox

            if(cbxForSale.Checked)
            {
                item.SubItems.Add("Yes");
            }
            else
            {
                item.SubItems.Add("No");
            }

            //Koppla item till listView
            lsvCars.Items.Add(item);

            //Lägg till bilen i databasen
            string q_insert = "INSERT INTO car ('regNr', 'make', 'model', 'year', 'forSale') VALUES (@regNr, @make, @model, @year, @forSale);";
            SQLiteCommand dbCommand = new SQLiteCommand(q_insert, dbObject.dbConn);
            dbObject.OpenConnection();

            //Koppla parametrar
            dbCommand.Parameters.AddWithValue("@regNr", txtRegNr.Text);
            dbCommand.Parameters.AddWithValue("@make", txtMake.Text);
            dbCommand.Parameters.AddWithValue("@model", txtModel.Text);
            dbCommand.Parameters.AddWithValue("@year", Convert.ToInt16(txtYear.Text));

            //Checkbox hanteras seperat
            if (cbxForSale.Checked)
            {
                dbCommand.Parameters.AddWithValue("@forSale", 1);
            }
            else
            {
                dbCommand.Parameters.AddWithValue("@forSale", 0);
            }

            int result = dbCommand.ExecuteNonQuery();
            dbObject.CloseConnection();

            //Meddelar att det fungerar
            MessageBox.Show("Du har lagt till " + Convert.ToString(result) + " bil.", "Bil tillagd");


            //Rensa formuläret
            txtRegNr.Clear();
            txtMake.Clear();
            txtModel.Clear();
            txtYear.Clear();
            cbxForSale.Checked = false;

            //Sätt fokus
            txtRegNr.Focus();

            checkRemoveButton();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            int count = lsvCars.Items.Count;
            //Loopa igenom alla items i listView
            foreach(ListViewItem item  in lsvCars.Items)
            {
                lsvCars.Items.Remove(item);
            }

            //Ta bort ifrån databasen
            string q_remove = "DELETE FROM car;";
            SQLiteCommand dbCommand = new SQLiteCommand(q_remove, dbObject.dbConn);
            dbObject.OpenConnection();

            int result = dbCommand.ExecuteNonQuery();       // Returnerar antalet påverkade poster i databasen
            dbObject.CloseConnection();


            MessageBox.Show("Du har rensat listan med " + count + " st bilar.\nDatabasen säger att " + result + " poster togs bort.", "Rensa listan");
            checkRemoveButton();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var index = lsvCars.SelectedItems[0];
            lsvCars.Items.Remove(index);

            //Ta bort ifrån databasen
            string q_remove = "DELETE FROM car WHERE regNr = @regNr;";
            SQLiteCommand dbCommand = new SQLiteCommand(q_remove, dbObject.dbConn);
            dbObject.OpenConnection();
            dbCommand.Parameters.AddWithValue("@regNr", index.Text);

            int result = dbCommand.ExecuteNonQuery();       // Returnerar antalet påverkade poster i databasen
            dbObject.CloseConnection();

            MessageBox.Show("Bilen med regNr: [" + index.Text + "] togs bort.\nDatabasen säger att " + result + " poster togs bort.", "Bil borttagen");
            checkRemoveButton();
        }

        /// <summary>
        /// Gör så att knappen för att ta bort item är oklickbar om ett item inte finns
        /// </summary>
        public void checkRemoveButton()
        {
            if(lsvCars.Items.Count > 0)
            {
                btnRemove.Enabled = true;
                btnClear.Enabled = true;
            }
            else
            {
                btnRemove.Enabled = false;
                btnClear.Enabled = false;
            }
        }
    }
}
