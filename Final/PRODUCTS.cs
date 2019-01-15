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
    public partial class PRODUCTS : Form
    {
        SqlConnection connect = new SqlConnection
            (@"Data Source=HP-PC\SQLEXPRESS;Initial Catalog=ospos;Integrated Security=true");
        public int Id = 0;
        //private object gridProducts1;

        public PRODUCTS()
        {
            InitializeComponent();
        }

        
        
        void Clear()
        {
            txtName.Text = txtQuantity.Text = txtSerial.Text = txtBrand.Text = txtTax.Text = txtDesc.Text 
                = txtCategory.Text = txtWholesale.Text = txtRetailsale.Text = txtLocation.Text = txtLocation.Text = "";

        }

       

        void FillProductsGrid()
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                DynamicParameters param = new DynamicParameters();
                param.Add("@searchText", searchtxt.Text.Trim());
                List<Product> list = new List<Product>();
                list = connect.Query<Product>("GetProducts", param, commandType: CommandType.StoredProcedure).ToList();
                gridProduct1.DataSource = list;
            }
            catch (Exception eer)
            {

                MessageBox.Show(eer.Message);
            }
        }

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
                param.Add("@Name", txtName.Text.Trim());
                param.Add("@Quantity", txtQuantity.Text.Trim());
                param.Add("@Serial", txtSerial.Text.Trim());
                param.Add("@Brand", txtBrand.Text.Trim());
                param.Add("@Category", txtCategory.Text.Trim());
                param.Add("@Wholesale", txtWholesale.Text.Trim());
                param.Add("@Retailsale", txtRetailsale.Text.Trim());
                param.Add("@Location", txtLocation.Text.Trim());
                param.Add("@Desc", txtDesc.Text.Trim());
                param.Add("@Tax", txtTax.Text.Trim());
                connect.Execute("AddProducts", param, commandType: CommandType.StoredProcedure);
                btnAdd.Text = "ADD";
                Id = 0;
                Clear();
                FillProductsGrid();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Id", Id);
                connect.Execute("DeleteProduct", param, commandType:CommandType.StoredProcedure);
                Clear();
                FillProductsGrid();
            }
            catch (Exception err)
            {

               MessageBox.Show(err.Message);
            }
        }

        private void btnSearch1_Click(object sender, EventArgs e)
        {
            FillProductsGrid();
            Clear();
        }

        class Product
        {
            public int ID { set; get; }
            public string SERIAL { set; get; }
            public string NAME { set; get; }
            public string QUANTITY { set; get; }
            public string TAX { set; get; }
            public string BRAND { set; get; }
            public string CATEGORY { set; get; }
            public string WHOLESALE { set; get; }
            public string RETAILSALE { set; get; }
            public string DESCRIPTION { set; get; }
            public string LOCATION { set; get; }
        }

        private void gridProduct1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if( gridProduct1.CurrentRow.Index != -1)
                {
                    Id = Convert.ToInt32(gridProduct1.CurrentRow.Cells[0].Value.ToString());
                    txtSerial.Text = gridProduct1.CurrentRow.Cells[1].Value.ToString();
                    txtName.Text = gridProduct1.CurrentRow.Cells[2].Value.ToString();
                    txtQuantity.Text = gridProduct1.CurrentRow.Cells[3].Value.ToString();
                    txtTax.Text = gridProduct1.CurrentRow.Cells[4].Value.ToString();
                    txtBrand.Text = gridProduct1.CurrentRow.Cells[5].Value.ToString();
                    txtCategory.Text = gridProduct1.CurrentRow.Cells[6].Value.ToString();
                    txtWholesale.Text = gridProduct1.CurrentRow.Cells[7].Value.ToString();
                    txtRetailsale.Text = gridProduct1.CurrentRow.Cells[8].Value.ToString();
                    txtDesc.Text = gridProduct1.CurrentRow.Cells[9].Value.ToString();
                    txtLocation.Text = gridProduct1.CurrentRow.Cells[10].Value.ToString();
                    FillProductsGrid();
                    btnAdd.Text = "EDIT";
                }

            }
            catch (Exception errr)
            {

                MessageBox.Show(errr.Message);
            }
        }

        private void PRODUCTS_Load(object sender, EventArgs e)
        {
            FillProductsGrid();
            
        }

        private void txtSearch1_Click(object sender, EventArgs e)
        {
            FillProductsGrid();
        }

        private void gridProducts1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridProduct1.CurrentRow.Index != -1)
                {
                    Id = Convert.ToInt32(gridProduct1.CurrentRow.Cells[0].Value.ToString());
                    txtSerial.Text = gridProduct1.CurrentRow.Cells[1].Value.ToString();
                    txtName.Text = gridProduct1.CurrentRow.Cells[2].Value.ToString();
                    txtQuantity.Text = gridProduct1.CurrentRow.Cells[3].Value.ToString();
                    txtTax.Text = gridProduct1.CurrentRow.Cells[4].Value.ToString();
                    txtBrand.Text = gridProduct1.CurrentRow.Cells[5].Value.ToString();
                    txtCategory.Text = gridProduct1.CurrentRow.Cells[6].Value.ToString();
                    txtWholesale.Text = gridProduct1.CurrentRow.Cells[7].Value.ToString();
                    txtRetailsale.Text = gridProduct1.CurrentRow.Cells[8].Value.ToString();
                    txtDesc.Text = gridProduct1.CurrentRow.Cells[9].Value.ToString();
                    txtLocation.Text = gridProduct1.CurrentRow.Cells[10].Value.ToString();
                    FillProductsGrid();
                    btnAdd.Text = "EDIT";
                }

            }
            catch (Exception errr)
            {

                MessageBox.Show(errr.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            FillProductsGrid();
        }

        private void txtSearch1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void searchtxt_TextChanged(object sender, EventArgs e)
        {
            FillProductsGrid();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var sales = new SALES(); this.Close();
            
            sales.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var receiving = new RECEIVINGS(); this.Close();
            receiving.Show();

        }

        private void button14_Click(object sender, EventArgs e)
        {
            var customers = new CUSTOMERS(); this.Close();
            customers.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var employees = new EMPLOYEES(); this.Close();
            employees.Show();

        }

        private void button13_Click(object sender, EventArgs e)
        {
            var gifts = new GIFTS(); this.Close();
            gifts.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var suppliers = new SUPPLIER(); this.Close();
            suppliers.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var config = new CONFIG(); this.Close();
            config.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var customers = new CUSTOMERS(); this.Close();
            customers.Show();
        }

        private void gridProduct1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridProduct1.CurrentRow.Index != -1)
                {
                    Id = Convert.ToInt32(gridProduct1.CurrentRow.Cells[0].Value.ToString());
                    txtSerial.Text = gridProduct1.CurrentRow.Cells[1].Value.ToString();
                    txtName.Text = gridProduct1.CurrentRow.Cells[2].Value.ToString();
                    txtQuantity.Text = gridProduct1.CurrentRow.Cells[3].Value.ToString();
                    txtTax.Text = gridProduct1.CurrentRow.Cells[4].Value.ToString();
                    txtBrand.Text = gridProduct1.CurrentRow.Cells[5].Value.ToString();
                    txtCategory.Text = gridProduct1.CurrentRow.Cells[6].Value.ToString();
                    txtWholesale.Text = gridProduct1.CurrentRow.Cells[7].Value.ToString();
                    txtRetailsale.Text = gridProduct1.CurrentRow.Cells[8].Value.ToString();
                    txtDesc.Text = gridProduct1.CurrentRow.Cells[9].Value.ToString();
                    txtLocation.Text = gridProduct1.CurrentRow.Cells[10].Value.ToString();
                    FillProductsGrid();
                    btnAdd.Text = "EDIT";
                }

            }
            catch (Exception errr)
            {

                MessageBox.Show(errr.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    } 
}

