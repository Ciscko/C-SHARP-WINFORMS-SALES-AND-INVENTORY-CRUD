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
    public partial class RECEIVINGS : Form
    {
        public int Id = 0;
        SqlConnection connection = new SqlConnection(@"Data Source=HP-PC\SQLEXPRESS;Initial Catalog=ospos;Integrated Security=true");
        public RECEIVINGS()
        {
            InitializeComponent();
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
             //public string Name => $"{NAME} {CATEGORY} {BRAND}";
        }

        public class Supplier
        {
            public int ID { set; get; }
            public string COMPANY { set; get; }
            public string PERSON { set; get; }
            public string PHONE { set; get; }
            public string EMAIL { set; get; }

        }

        public void  FillComboSupplier()
        {

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DynamicParameters param = new DynamicParameters();
                param.Add("@searchText", 0);
               
                List<Supplier> list = new List<Supplier>();
                list = connection.Query<Supplier>("GetSuppliers", param, commandType: CommandType.StoredProcedure).ToList<Supplier>();
                comboSupplier.DataSource = list;
                comboSupplier.DisplayMember = "Company";
                comboSupplier.ValueMember = "COMPANY";
            }
            catch (Exception errorr)
            {

                MessageBox.Show(errorr.Message);
            }
        }
        void getProducts ()
        {
            try
            {
                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DynamicParameters param = new DynamicParameters();
                param.Add("@searchText", txtSearchP.Text);
                List<Product> list = new List<Product>();
                list = connection.Query<Product>("GetProducts", param, commandType:CommandType.StoredProcedure).ToList<Product>();
                productsgrid.DataSource = list;
              /*  productsgrid.Columns["LOCATION"].Visible = false;
                productsgrid.Columns["DESCRIPTION"].Visible = false;
                productsgrid.Columns["TAX"].Visible = false;
                productsgrid.Columns["ID"].Visible = false;
                productsgrid.Columns["RETAILSALE"].Visible = false;
                productsgrid.Columns["WHOLESALE"].Visible = false;
                */
            }
            catch (Exception errorr)
            {

                MessageBox.Show(errorr.Message);
            }
          
        }

        void Clear()
        {
            btnAdd.Enabled = true;
            btnAdd.Text = "ADD";
            txtProductId.Text = txtProductName.Text =txtDiscount.Text = txtCost.Text = txtQuantity.Text= total.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            getProducts();
            FillComboSupplier();
            FillReceivingsGrid();
            txtQuantity.Enabled = true;
            Id = 0;

        }

        private void RECEIVINGS_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'osposDataSet.Suppliers' table. You can move, or remove it, as needed.
            this.suppliersTableAdapter.Fill(this.osposDataSet.Suppliers);
            getProducts();
            FillComboSupplier();
            FillReceivingsGrid();
        }

      
        //clicking to select product to add
        private void productsgrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(productsgrid.CurrentRow.Index != -1)
                {
                    txtProductId.Text = productsgrid.CurrentRow.Cells[0].Value.ToString();
                    txtProductName.Text = productsgrid.CurrentRow.Cells[2].Value.ToString();

                }
            }
            catch (Exception error)
            {

               MessageBox.Show(error.Message);
            }
        }
        //defines the fields of our grid, since the list is copied here
        class Receiving
        {
            public int ID { set; get; }
            public string PRODUCTID { set; get; }
            public string PRODUCTNAME { set; get; }
            public string MODE { set; get; }
            public string QUANTITY { set; get; }
            public string DATE { set; get; }
            public string DISCOUNT { set; get; }
            public string SUPPLIER { set; get; }
            public string COST { set; get; }

        }

        private void gridReceiving_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridReceiving.CurrentRow.Index != -1)
                {
                    Id = Convert.ToInt32( gridReceiving.CurrentRow.Cells[0].Value.ToString());
                    txtProductId.Text = gridReceiving.CurrentRow.Cells[1].Value.ToString();
                    txtProductName.Text = gridReceiving.CurrentRow.Cells[2].Value.ToString();
                    string qtyStr = txtQuantity.Text = gridReceiving.CurrentRow.Cells[4].Value.ToString();
                    txtQuantity.Enabled = false;
                    txtDate.Text = gridReceiving.CurrentRow.Cells[5].Value.ToString();
                    string discStr = txtDiscount.Text = gridReceiving.CurrentRow.Cells[6].Value.ToString();
                    string costStr = txtCost.Text = gridReceiving.CurrentRow.Cells[8].Value.ToString();

                    double cost = Convert.ToDouble(costStr);
                    double discount = Convert.ToDouble(discStr);
                    double qty = Convert.ToDouble(qtyStr);
                    //total.Text = ((cost * qty)-(discount/100)*(cost*qty).ToString();

                    double totalcost = cost * qty;
                    double discountval = (discount / 100) * totalcost;
                    double netcostval = totalcost - discountval;
                    total.Text = netcostval.ToString();
                    btnAdd.Text = "EDIT";
                }
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }
        void FillReceivingsGrid()
        {
            try
            {
                if(connection.State == ConnectionState.Closed)
                { 
                    connection.Open();
                }
                DynamicParameters param = new DynamicParameters();
                param.Add("@SearchText",searchtxt.Text);
                List<Receiving> list = connection.Query<Receiving>
                    ("GetReceivings", param, commandType:CommandType.StoredProcedure).ToList();
                gridReceiving.DataSource = list;
               
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //ADD RECEIVING
            try
            {
                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", Id);
                param.Add("@Mode", comboMode.SelectedItem);
                param.Add("@Supplier", comboSupplier.SelectedValue.ToString());
                param.Add("@Cost", txtCost.Text);
                param.Add("@Quantity", txtQuantity.Text);
                param.Add("@Discount", txtDiscount.Text);
                param.Add("@ProductName", txtProductName.Text);
                param.Add("@ProductId", txtProductId.Text);
                param.Add("@Date", txtDate.Value);
                connection.Execute("AddReceiving", param, commandType: CommandType.StoredProcedure);
                //UPDATE PRODUCTS
                if(Id == 0)
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    DynamicParameters param2 = new DynamicParameters();
                    param2.Add("@Id", txtProductId.Text);
                    param2.Add("@Wholesale", txtCost.Text);
                    param2.Add("@Mode", comboMode.SelectedItem);
                    param2.Add("@Quantity", Convert.ToInt32(txtQuantity.Text));
                    connection.Execute("UpdateProduct", param2, commandType: CommandType.StoredProcedure);
                }
 
                Clear();
                FillReceivingsGrid();
                Id = 0;
                txtQuantity.Enabled = true;
                btnAdd.Text = "ADD";
                getProducts();
                FillComboSupplier();
                FillReceivingsGrid();
            }
            catch (Exception  err)
            {

                MessageBox.Show(err.Message);
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", Id);
            connection.Execute("DeleteReceiving",param, commandType:CommandType.StoredProcedure);
            Clear();
            Id = 0;
            FillReceivingsGrid();
            btnAdd.Text = "ADD";
            txtQuantity.Enabled = true;
        }

        private void gridReceiving_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridReceiving.CurrentRow.Index != -1)
                {
                    Id = Convert.ToInt32(gridReceiving.CurrentRow.Cells[0].Value.ToString());
                    txtProductId.Text = gridReceiving.CurrentRow.Cells[1].Value.ToString();
                    txtProductName.Text = gridReceiving.CurrentRow.Cells[2].Value.ToString();
                   string qtyStr = txtQuantity.Text = gridReceiving.CurrentRow.Cells[4].Value.ToString();
                    txtQuantity.Enabled = false;
                    txtDate.Text = gridReceiving.CurrentRow.Cells[5].Value.ToString();
                   string discStr =  txtDiscount.Text = gridReceiving.CurrentRow.Cells[6].Value.ToString();
                    string costStr = txtCost.Text = gridReceiving.CurrentRow.Cells[8].Value.ToString();

                    double cost = Convert.ToDouble(costStr);
                    double discount = Convert.ToDouble(discStr);
                    double qty = Convert.ToDouble(qtyStr);
                    //total.Text = ((cost * qty)-(discount/100)*(cost*qty).ToString();

                    double totalcost = cost * qty;
                    double discountval = (discount / 100) * totalcost;
                    double netcostval = totalcost - discountval;
                    total.Text = netcostval.ToString();
                    btnAdd.Text = "EDIT";
                   // btnAdd.Enabled = false;

                }
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }

        private void searchTxt_TextChanged(object sender, EventArgs e)
        {
            FillReceivingsGrid();
        }

        private void txtSearchP_TextChanged(object sender, EventArgs e)
        {
            getProducts();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            getProducts();
        }

        private void productsgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridReceiving_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridReceiving.CurrentRow.Index != -1)
                {
                    Id = Convert.ToInt32(gridReceiving.CurrentRow.Cells[0].Value.ToString());
                    txtProductId.Text = gridReceiving.CurrentRow.Cells[1].Value.ToString();
                    txtProductName.Text = gridReceiving.CurrentRow.Cells[2].Value.ToString();
                    string qtyStr = txtQuantity.Text = gridReceiving.CurrentRow.Cells[4].Value.ToString();
                    txtQuantity.Enabled = false;
                    txtDate.Text = gridReceiving.CurrentRow.Cells[5].Value.ToString();
                    string discStr = txtDiscount.Text = gridReceiving.CurrentRow.Cells[6].Value.ToString();
                    string costStr = txtCost.Text = gridReceiving.CurrentRow.Cells[8].Value.ToString();

                    double cost = Convert.ToDouble(costStr);
                    double discount = Convert.ToDouble(discStr);
                    double qty = Convert.ToDouble(qtyStr);
                    //total.Text = ((cost * qty)-(discount/100)*(cost*qty).ToString();

                    double totalcost = cost * qty;
                    double discountval = (discount / 100) * totalcost;
                    double netcostval = totalcost - discountval;
                    total.Text = netcostval.ToString();
                    btnAdd.Text = "EDIT";
                    //btnAdd.Enabled = false;

                }
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var products = new PRODUCTS();
            this.Close();
            products.Show();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            var customers = new CUSTOMERS();
            this.Close();
            customers.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var modules = new MODULES();
            this.Close();
            modules.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var sales = new SALES();
            this.Close();
            sales.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var products = new PRODUCTS();
            this.Close();
            products.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var customers = new CUSTOMERS();
            this.Close();
            customers.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var employees = new EMPLOYEES();
            this.Close();
            employees.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var gifts = new GIFTS();
            this.Close();
            gifts.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var suppliers = new SUPPLIER();
            this.Close();
            suppliers.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var config = new CONFIG();
            this.Close();
            config.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var products = new PRODUCTS();
            this.Close();
            products.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FillReceivingsGrid();
        }
    }
}
