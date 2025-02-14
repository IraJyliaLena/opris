﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace PetShop
{
    public partial class frmMain : Form
    {
        private SqlConnection myConnection;
        private String User_Name;
        public frmMain(SqlConnection con, String user_name, String user_role)
        {
            InitializeComponent();
            myConnection = con;
            this.Text += " (" + user_name + ")";
            getPrivileges(user_role);
            fillTheTable(dgvPets, "DISPLAY_UNSOLD_PETS");
            User_Name = user_name;
        }

        /// <summary>
        /// Отобразить содержимое MainForm в соответствии с ролью
        /// </summary>
        /// <param name="role"></param>
        private void getPrivileges(String role)
        {
            switch (role)
            {
                case "директор":
                    статистикаПоПродажамToolStripMenuItem.Visible = false;
                    продажаToolStripMenuItem.Visible = false;
                    break;
                case "СОП":
                    работаСДаннымиToolStripMenuItem.Visible = false;
                    продажаToolStripMenuItem.Visible = false;
                    break;
                case "продавец":
                    работаСДаннымиToolStripMenuItem.Visible = false;
                    статистикаПоПродажамToolStripMenuItem.Visible = false;
                    break;
                default:
                    break;
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            myConnection.Close();
            this.Owner.Show();
        }


        private void fillTheTable(DataGridView dgv, string commandText)
        {
            string connectionString = @"Data Source=.;Initial Catalog=PetShopO;user id=sa; password=1;";
            myConnection = new SqlConnection(connectionString);
            try
            {
                myConnection.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message); ;
            }
            SqlCommand comm = new SqlCommand(commandText, myConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(dt);
            BindingSource bs = new BindingSource();
            bs.DataSource = dt;
            dgv.DataSource = bs;
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tcMain.SelectedIndex)
            {
                case 0:
                    fillTheTable(dgvPets, "DISPLAY_UNSOLD_PETS");
                    break;
                case 1:
                    fillTheTable(dgvProvider, "DISPLAY_PROVIDER_PUB");
                    break;
                case 2:
                    fillTheTable(dgvEmployee, "DISPLAY_EMPLOYEES_PUB");
                    break;
                default:
                    break;
            }
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myConnection.Close();
            Application.Exit();
        }

        private void выйтиИзПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            myConnection.Close();
            myConnection.Dispose();
            this.Owner.Show();
        }

        private void поставщикиToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmProviders prov = new frmProviders(myConnection, dgvProvider, "Данные о поставщиках");
            this.Hide();
            prov.Show(this);
        }

        private void подобратьЖивотноеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSelect sel = new frmSelect(myConnection, User_Name,dgvPets);
            this.Hide();
            sel.Show(this);
        }

        private void статистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStatistics stats = new frmStatistics(myConnection);
            this.Hide();
            stats.Show(this);
        }

        private void городаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCities city = new frmCities(myConnection);
            this.Hide();
            city.Show(this);
        }

        private void продатьЖивотноеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSale sale = new frmSale(myConnection, User_Name,dgvPets);
            this.Hide();
            sale.Show(this);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void видыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSpecies spec = new frmSpecies(myConnection);
            this.Hide();
            spec.Show(this);

        }

        private void должностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProviders prov = new frmProviders(myConnection, null, "Данные о должностях");
            this.Hide();
            prov.Show(this);
        }

        private void животныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProviders pet = new frmProviders(myConnection, dgvPets, "Данные о животных");
            this.Hide();
            pet.Show(this);
        }

        private void породыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBreeds breed = new frmBreeds(myConnection);
            this.Hide();
            breed.Show(this);

        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProviders prov = new frmProviders(myConnection, dgvEmployee, "Данные о сотрудниках");
            this.Hide();
            prov.Show(this);
        }
    }
}
