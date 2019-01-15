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
    public partial class SALES : Form
    {
        SqlConnection connect = new SqlConnection
           (@"Data Source=HP-PC\SQLEXPRESS;Initial Catalog=ospos;Integrated Security=true");
        public int IdProduct = 0;
        public double discount = 0;
        public string shopped = "{0, 0} {1, 20} {2, 30} {3, 40} ";
        public double taxx, total;
        double gross;
        
        DataTable table = new DataTable();

        public SALES()
        {
            InitializeComponent();
        }

        private void SALES_Load(object sender, EventArgs e)
        {
           
         
            FillProducts();
            FillCustomerCombo();
            CreateGrid();
            
        }
        public class Product
        {
            public int ID { set; get; }//0
            public string SERIAL { set; get; }
            public string NAME { set; get; }//2
            public string QUANTITY { set; get; }
            public string TAX { set; get; }//4
            public string BRAND { set; get; }
            public string CATEGORY { set; get; }//6
            public string WHOLESALE { set; get; }
            public string RETAILSALE { set; get; }//8
            public string DESCRIPTION { set; get; }
            public string LOCATION { set; get; }//10
        }

       
        void FillProducts()
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                DynamicParameters param = new DynamicParameters();
                param.Add("@searchText", dataSearch.Text.Trim());
                List<Product> list = new List<Product>();
                list = connect.Query<Product>("GetProducts", param, commandType: CommandType.StoredProcedure).ToList<Product>();
                dataProducts.DataSource = list;

            }
            catch (Exception eer)
            {

                MessageBox.Show(eer.Message);
            }

        }
        void Clear()
        {
            dataName.Text = dataDisc.Text = dataQty.Text = dataPrice.Text = dataPrice.Text = dataBrand.Text = "";
            //FillProducts();
            //FillCustomerCombo();
        }
        class Customer
        {
            public int ID { set; get; }
            public string NAME { set; get; }
            public string PHONE { set; get; }
            public string EMAIL { set; get; }
        }
        void FillCustomerCombo()
        {
            DynamicParameters param = new DynamicParameters();
            List<Customer> list = new List<Customer>();
            param.Add("searchText", customerCombo.Text );
            list = connect.Query<Customer>("GetCustomers", param, commandType:CommandType.StoredProcedure).ToList();
            customerCombo.DataSource = list;
            customerCombo.DisplayMember = "Name";
            customerCombo.ValueMember = "Name";
            
        }

        private void dataSearch_TextChanged(object sender, EventArgs e)
        {
            FillProducts();

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            FillProducts();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dataQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataProducts.CurrentRow.Index != -1)
                {
                   // IdProduct = Convert.ToInt32(dataProducts.CurrentRow.Cells[0].Value.ToString());
                   // dataName.Text = dataProducts.CurrentRow.Cells[2].Value.ToString();
                    //dataQty.Text = 1.ToString();

                   // dataBrand.Text = dataProducts.CurrentRow.Cells[5].Value.ToString();
                   // dataPrice.Text = dataProducts.CurrentRow.Cells[8].Value.ToString();


                    double tax = Convert.ToDouble(dataProducts.CurrentRow.Cells[4].Value.ToString());
                    double price = Convert.ToDouble(dataProducts.CurrentRow.Cells[8].Value.ToString());
                    double qty = Convert.ToDouble(dataQty.Text);
                    double subtotal = price * qty;
                    double taxx = (tax / 100) * subtotal;
                    double total = taxx + subtotal - discount;

                    labTax.Text = taxx.ToString();
                    labTot.Text = total.ToString();
                    labSub.Text = subtotal.ToString();
                   // FillProducts();

                }

            }
            catch (Exception )
            {

                //MessageBox.Show(errr.Message);
            }
        }
        void CreateGrid()
        {
            
            table.Columns.Add("PRODUCT", typeof(string));
            table.Columns.Add("QTY", typeof(string));
            table.Columns.Add("TAX", typeof(string));
            table.Columns.Add("PRICE", typeof(string));

            shoppeditems.DataSource = table;
        }
   
        
        private void dataDisc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                discount = Convert.ToDouble(dataDisc.Text);
                if (dataProducts.CurrentRow.Index != -1)
                {
                    IdProduct = Convert.ToInt32(dataProducts.CurrentRow.Cells[0].Value.ToString());
                    dataName.Text = dataProducts.CurrentRow.Cells[2].Value.ToString();
                    //dataQty.Text = 1.ToString();
                    dataBrand.Text = dataProducts.CurrentRow.Cells[5].Value.ToString();
                    dataPrice.Text = dataProducts.CurrentRow.Cells[8].Value.ToString();


                    double tax = Convert.ToDouble(dataProducts.CurrentRow.Cells[4].Value.ToString());
                    double price = Convert.ToDouble(dataProducts.CurrentRow.Cells[8].Value.ToString());
                    double qty = Convert.ToDouble(dataQty.Text);
                   double subtotal = price * qty;
                     taxx = (tax / 100) * subtotal;
                    if (dataDisc.Text == "") { discount = 0; }
                    double total = taxx + subtotal - discount;

                    labTax.Text = taxx.ToString();
                    labTot.Text = total.ToString();
                    labSub.Text = subtotal.ToString();
                    

                }
            }
            catch ( Exception err)
            {
               // MessageBox.Show("Please Set the discount value!");
                
            }
            
        }

     

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int index = shoppeditems.CurrentCell.RowIndex;
                shoppeditems.Rows.RemoveAt(index);
            }
            catch (Exception)
            {

                MessageBox.Show("Empty selection");
            }
            
        }


       

        private void dataProducts_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dataProducts.CurrentRow.Index != -1)
                {
                   
                    IdProduct = Convert.ToInt32(dataProducts.CurrentRow.Cells[0].Value.ToString());
                    dataName.Text = dataProducts.CurrentRow.Cells[2].Value.ToString();
                    dataQty.Text = 1.ToString();
                    dataBrand.Text = dataProducts.CurrentRow.Cells[5].Value.ToString();
                    dataPrice.Text = dataProducts.CurrentRow.Cells[8].Value.ToString();
                    dataDisc.Text = discount.ToString();


                    double tax = Convert.ToDouble(dataProducts.CurrentRow.Cells[4].Value.ToString());
                    double price = Convert.ToDouble(dataProducts.CurrentRow.Cells[8].Value.ToString());
                    double qty = Convert.ToDouble(dataQty.Text);
                    double subtotal = price * qty;
                    double taxx = (tax / 100) * subtotal;

                    double total = taxx + subtotal - discount;

                    labTax.Text = taxx.ToString();
                    labTot.Text = total.ToString();
                    labSub.Text = subtotal.ToString();
                    FillProducts();
                    discount = 0;
                    //sale data add to variables upto item 14
                    saleWholesalePrice = dataProducts.CurrentRow.Cells[7].Value.ToString();
                    saleSerial = dataProducts.CurrentRow.Cells[1].Value.ToString();

                }

            }
            catch (Exception errr)
            {

                MessageBox.Show(errr.Message);
            }
        }
        double tax;
        double price;
        double qty;
        

        private void dataProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dataProducts.CurrentRow.Index != -1)
                {
                    btnadditem.Enabled = true;
                    IdProduct = Convert.ToInt32(dataProducts.CurrentRow.Cells[0].Value.ToString());
                    dataName.Text = dataProducts.CurrentRow.Cells[2].Value.ToString();
                    dataQty.Text = 1.ToString();
                    dataBrand.Text = dataProducts.CurrentRow.Cells[5].Value.ToString();
                    dataPrice.Text = dataProducts.CurrentRow.Cells[8].Value.ToString();
                    dataDisc.Text = discount.ToString();


                     tax = Convert.ToDouble(dataProducts.CurrentRow.Cells[4].Value.ToString());
                    price = Convert.ToDouble(dataProducts.CurrentRow.Cells[8].Value.ToString());
                     qty = Convert.ToDouble(dataQty.Text);
                    double subtotal = price * qty;
                    double taxx = (tax / 100) * subtotal;

                    double total = taxx + subtotal - discount;

                    labTax.Text = taxx.ToString();
                    labTot.Text = total.ToString();
                    labSub.Text = subtotal.ToString();
                    FillProducts();
                    discount = 0;

                    //sale data add to variables upto item 14
                    saleWholesalePrice = dataProducts.CurrentRow.Cells[7].Value.ToString();
                    saleSerial = dataProducts.CurrentRow.Cells[1].Value.ToString();

                }

            }
            catch (Exception errr)
            {

                MessageBox.Show(errr.Message);
            }
        }
        public string saleProduct;//1
        public string saleSerial;
        public string saleBrand;//3
        public DateTime saleTime;//********
        public string saleQty;//5
        public string saleTax;
        public string saleTotal;//7
        public string saleDisc;
        public string saleCustomer;//9
        public string saleMode;
        public string saleRetailPrice;//11
        public string saleWholesalePrice;
        public string saleEmployee;//13 ********
        public double saleChange;//14 ::14 Items to add to db
        public double saleGross;//15

        private void btncomplete_Click(object sender, EventArgs e)
        {
            //we add change to sale data;upto  item 12
            saleChange = Convert.ToDouble(cashtxt.Text) - Convert.ToDouble(grosslabel.Text);//10
            saleGross = Convert.ToDouble(grosslabel.Text);//11
           saleTime =  DateTime.Now;//12 
    }

        private void btnadditem_Click(object sender, EventArgs e)
        {
            try
            {
                table.Rows.Add(dataName.Text, dataQty.Text, labTax.Text, labTot.Text);
                 gross+= Convert.ToDouble(labTot.Text);
                grosslabel.Text = gross.ToString();
                //sale data detail: upto item 9
                saleProduct = dataName.Text;//1
                saleQty = dataQty.Text;
                saleMode = comboMode.SelectedValue.ToString();//3
                saleBrand = dataBrand.Text;
                saleDisc = dataDisc.Text;//5
                saleTax = labTax.Text;
                saleTotal = labTot.Text;//7
                saleCustomer = customerCombo.SelectedValue.ToString();//8
                saleRetailPrice = dataPrice.Text;//9


                Clear();
            }
            catch (Exception mer)
            {
                MessageBox.Show(mer.Message);
            }
            


        }
    }
}
