using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final
{
    public partial class SUPPLIER : Form
    {
        SqlConnection connect = new SqlConnection
            (@"Data Source=HP-PC\SQLEXPRESS;Initial Catalog=ospos;Integrated Security=true");

        public int Id = 0;
        public int Idc = 0;
        public SUPPLIER()
        {
            InitializeComponent();
        }

        private void SUPPLIER_Load(object sender, EventArgs e)
        {
            FillSupplierGrid();
            FillCustomerGrid();
            Clear();
        }

        void Clear()
        {
            txtPerson.Text = txtPhone.Text = txtEmail.Text = txtCompany.Text = "";
            Id = 0;
        }

        public class Supplier
        {
            public int ID { set; get; }
            public string COMPANY { set; get; }
            public string PERSON { set; get; }
            public string PHONE { set; get; }
            public string EMAIL { set; get; }

        }

        void FillSupplierGrid()
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                DynamicParameters param = new DynamicParameters();
                param.Add("@searchText", txtSearch.Text);
                List<Supplier> list = new List<Supplier>();
                list = connect.Query<Supplier>
                    ("GetSuppliers", param, commandType: CommandType.StoredProcedure).ToList();
                gridSupplier2.DataSource = list;

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }


        }

        //supplier
        private void gridSupplier2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridSupplier2.CurrentRow.Index != -1)
                {
                    Id = Convert.ToInt32(gridSupplier2.CurrentRow.Cells[0].Value.ToString());
                    txtCompany.Text = gridSupplier2.CurrentRow.Cells[1].Value.ToString();
                    txtPerson.Text = gridSupplier2.CurrentRow.Cells[2].Value.ToString();
                    txtPhone.Text = gridSupplier2.CurrentRow.Cells[3].Value.ToString();
                    txtEmail.Text = gridSupplier2.CurrentRow.Cells[4].Value.ToString();
                    btnAdd.Text = "EDIT";
                }

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }


        //supplier
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", Id);
                param.Add("@Company", txtCompany.Text);
                param.Add("@Person", txtPerson.Text);
                param.Add("@Phone", txtPhone.Text);
                param.Add("@Email", txtEmail.Text);
                connect.Execute("AddSupplier", param, commandType: CommandType.StoredProcedure);
                connect.Close();
                FillSupplierGrid();
                Clear();
                Id = 0;
                btnAdd.Text = "ADD";

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message); 
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            FillSupplierGrid();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", Id);
                connect.Execute("DeleteSupplier", param, commandType: CommandType.StoredProcedure);
                FillSupplierGrid();

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }
        //supplier
        private void button1_Click(object sender, EventArgs e)
        {
            FillSupplierGrid();
        }

        //supplier
        private void gridSupplier2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridSupplier2.CurrentRow.Index != -1)
                {
                    Id = Convert.ToInt32(gridSupplier2.CurrentRow.Cells[0].Value.ToString());
                    txtCompany.Text = gridSupplier2.CurrentRow.Cells[1].Value.ToString();
                    txtPerson.Text = gridSupplier2.CurrentRow.Cells[2].Value.ToString();
                    txtPhone.Text = gridSupplier2.CurrentRow.Cells[3].Value.ToString();
                    txtEmail.Text = gridSupplier2.CurrentRow.Cells[4].Value.ToString();
                    btnAdd.Text = "EDIT";
                }

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }
        //supplier
        private void gridSupplier2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(gridSupplier2.CurrentRow.Index != -1)
                {
                    Id = Convert.ToInt32(gridSupplier2.CurrentRow.Cells[0].Value.ToString());
                    txtCompany.Text = gridSupplier2.CurrentRow.Cells[1].Value.ToString();
                    txtPerson.Text = gridSupplier2.CurrentRow.Cells[2].Value.ToString();
                    txtPhone.Text = gridSupplier2.CurrentRow.Cells[3].Value.ToString();
                    txtEmail.Text = gridSupplier2.CurrentRow.Cells[4].Value.ToString();
                    btnAdd.Text = "EDIT";
                }
               
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
            
        }
        //supplier
        private void gridSupplier2_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridSupplier2.CurrentRow.Index != -1)
                {
                    Id = Convert.ToInt32(gridSupplier2.CurrentRow.Cells[0].Value.ToString());
                    txtCompany.Text = gridSupplier2.CurrentRow.Cells[1].Value.ToString();
                    txtPerson.Text = gridSupplier2.CurrentRow.Cells[2].Value.ToString();
                    txtPhone.Text = gridSupplier2.CurrentRow.Cells[3].Value.ToString();
                    txtEmail.Text = gridSupplier2.CurrentRow.Cells[4].Value.ToString();
                    btnAdd.Text = "EDIT";
                }

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FillSupplierGrid();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
/*
 * CUSTOMER FUNCTIONS FROM HERE ONWARD
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */

        //customer add
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                DynamicParameters param = new DynamicParameters();

                param.Add("@Id", Idc);
                param.Add("@Name", txtName.Text.Trim());
                param.Add("@Phone", txtPhoneC.Text.Trim());
                param.Add("@Email", txtEmailC.Text.Trim());
                connect.Execute("AddCustomer", param, commandType: CommandType.StoredProcedure);
                Idc = 0;
                Clear2();
                FillCustomerGrid();
                Idc = 0;
                btnAddC.Text = "ADD";
               // MessageBox.Show("Successful");
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }

        }
        //customer
        void FillCustomerGrid()
        {
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }
            DynamicParameters param = new DynamicParameters();
            param.Add("@searchText", txtSearch.Text.Trim());
            List<Customer> list = new List<Customer>();
            list = connect.Query<Customer>
                ("GetCustomers", param, commandType: CommandType.StoredProcedure).ToList();
            gridCustomer.DataSource = list;
        }
        //customer
        class Customer
        {
            public int ID { set; get; }
            public string NAME { set; get; }
            public string PHONE { set; get; }
            public string EMAIL { set; get; }
        }//customer
        void Clear2()
        {
            txtName.Text = txtPhoneC.Text = txtEmailC.Text = "";
            Idc = 0;
            btnAddC.Text = "ADD";
        }
        //customer
        private void button5_Click(object sender, EventArgs e)
        {
            Clear2();
            FillCustomerGrid();
        }
        //customer
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", Idc);
                connect.Execute("DeleteCustomer", param, commandType: CommandType.StoredProcedure);
                FillCustomerGrid();
                Clear();
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
                   
            }
           
        }
        //customer
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            FillCustomerGrid();
        }
       

        

        //customer select
        private void gridCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridCustomer.CurrentRow.Index != -1)
                {
                    Idc = Convert.ToInt16(gridCustomer.CurrentRow.Cells[0].Value.ToString());
                    txtName.Text = gridCustomer.CurrentRow.Cells[1].Value.ToString();
                    txtPhoneC.Text = gridCustomer.CurrentRow.Cells[2].Value.ToString();
                    txtEmailC.Text = gridCustomer.CurrentRow.Cells[3].Value.ToString();
                    btnAddC.Text = "EDIT";
                    FillCustomerGrid();

                }
            }
            catch (Exception eerr)
            {

                MessageBox.Show(eerr.Message);
            }
           

        }
        //customer select 
        private void gridCustomer_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridCustomer.CurrentRow.Index != -1)
                {
                    Idc = Convert.ToInt16(gridCustomer.CurrentRow.Cells[0].Value.ToString());
                    txtName.Text = gridCustomer.CurrentRow.Cells[1].Value.ToString();
                    txtPhoneC.Text = gridCustomer.CurrentRow.Cells[2].Value.ToString();
                    txtEmailC.Text = gridCustomer.CurrentRow.Cells[3].Value.ToString();
                    btnAddC.Text = "EDIT";
                    FillCustomerGrid();

                }
            }
            catch (Exception eerr)
            {

                MessageBox.Show(eerr.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }



        //dashboard to end
        private void button16_Click(object sender, EventArgs e)
        {
            var sale = new SALES(); this.Close();
            sale.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var customers = new CUSTOMERS(); this.Close();
            customers.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var employees = new EMPLOYEES(); this.Close();
            employees.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var gifts = new GIFTS(); this.Close();
            gifts.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var config = new CONFIG(); this.Close();
            config.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
           var supplier = new SUPPLIER();
            this.Close();
            supplier.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var products = new PRODUCTS();
            this.Close();
            products.Show();
        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {

        }
    }
}