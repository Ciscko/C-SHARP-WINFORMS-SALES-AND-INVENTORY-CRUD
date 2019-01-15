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
    public partial class CUSTOMERS : Form
    {
        private SqlConnection connect = new SqlConnection(@"Data Source=HP-PC\SQLEXPRESS;Initial Catalog=ospos;Integrated Security=true");
        public int Id=0;

        

        public CUSTOMERS()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

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
                param.Add("@Phone", txtPhone.Text.Trim());
                param.Add("@Email", txtEmail.Text.Trim());
                connect.Execute("AddCustomer", param, commandType: CommandType.StoredProcedure);
                Clear();
                FillCustomerGrid();
                Id = 0;
                btnAdd.Text = "ADD";
                MessageBox.Show("Successful");
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }


        }
        class Customer
        {
            public int ID { set; get; }
            public string NAME { set; get; }
            public string PHONE { set; get; }
            public string EMAIL { set; get; }
        }
        void FillCustomerGrid()
        {
            if(connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }
            DynamicParameters param = new DynamicParameters();
            param.Add("@searchText", txtSearch.Text.Trim());
            List<Customer> list = new List<Customer>();
            list = connect.Query<Customer>
                ("GetCustomers", param, commandType:CommandType.StoredProcedure).ToList();
            gridCustomer.DataSource = list;
        }
        void Clear()
        {
            txtName.Text = txtPhone.Text = txtEmail.Text = "";
        }

        private void gridCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridCustomer.CurrentRow.Index != -1)
            {
                Id = Convert.ToInt16(gridCustomer.CurrentRow.Cells[0].Value.ToString());
                txtName.Text = gridCustomer.CurrentRow.Cells[1].Value.ToString();
                txtPhone.Text = gridCustomer.CurrentRow.Cells[2].Value.ToString();
                txtEmail.Text = gridCustomer.CurrentRow.Cells[3].Value.ToString();
                btnAdd.Text = "EDIT";
                FillCustomerGrid();

            }

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

       }

        private void CUSTOMERS_Load(object sender, EventArgs e)
        {
            FillCustomerGrid();
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", Id);
            connect.Execute("DeleteCustomer", param, commandType:CommandType.StoredProcedure);
            FillCustomerGrid();
            Clear();
        }

        private void gridCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridCustomer.CurrentRow.Index != -1)
                {
                    Id = Convert.ToInt32(gridCustomer.CurrentRow.Cells[0].Value.ToString());
                    txtName.Text = gridCustomer.CurrentRow.Cells[1].Value.ToString();
                    txtPhone.Text = gridCustomer.CurrentRow.Cells[2].Value.ToString();
                    txtEmail.Text = gridCustomer.CurrentRow.Cells[3].Value.ToString();
                    btnAdd.Text = "EDIT";

                }
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message) ;
            }
            
        }

        private void gridCustomer_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridCustomer.CurrentRow.Index != -1)
                {
                    Id = Convert.ToInt32(gridCustomer.CurrentRow.Cells[0].Value.ToString());
                    txtName.Text = gridCustomer.CurrentRow.Cells[1].Value.ToString();
                    txtPhone.Text = gridCustomer.CurrentRow.Cells[2].Value.ToString();
                    txtEmail.Text = gridCustomer.CurrentRow.Cells[3].Value.ToString();
                    btnAdd.Text = "EDIT";

                }
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            FillCustomerGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
