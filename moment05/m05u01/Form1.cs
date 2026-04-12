using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            checkRemoveButton();
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

            MessageBox.Show("Du har rensat listan med " + count + " st bilar", "Rensa listan");
            checkRemoveButton();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var index = lsvCars.SelectedItems[0];
            lsvCars.Items.Remove(index);
            MessageBox.Show("Bilen med regNr: [" + index.Text + "] togs bort", "Bil borttagen");
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
