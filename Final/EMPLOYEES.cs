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
    public partial class EMPLOYEES : Form
    {

        public EMPLOYEES()
        {
            InitializeComponent();
        }

       
        SqlConnection con = new SqlConnection(@"Data Source=HP-PC\SQLEXPRESS;Initial Catalog=ospos;Integrated Security=true");
        //private int eId = 0;
        public int EId = 0;


        void LoadComboMode()
        {
            genderCombo.Items.Add("Male");
            genderCombo.Items.Add("Female");
        }
        private void EMPLOYEES_Load(object sender, EventArgs e)
        {
           
             FillEmployeeGrid();
            LoadComboMode();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void name_Click(object sender, EventArgs e)
        {

        }
        void Clear()
        {
            txtName.Text = txtPhone.Text = txtAddress.Text = txtEmail.Text = txtIdNumber.Text = "";
        }
        class Employee
        {
            public int ID { set; get; }
            public string NAME { set; get; }
            public string PHONE { set; get; }
            public string ADDRESS { set; get; }
            public string DOB { set; get; }
            public string GENDER { set; get; }
            public string EMAIL { set; get; }
            public string IDNUMBER{ set; get; }
            

        }
        public void Cancel()
        {
            Clear();
            FillEmployeeGrid();
            
        }

        public void FillEmployeeGrid()
        {
            if (con.State  == ConnectionState.Closed)
            {
                con.Open();    
            }
            DynamicParameters param = new DynamicParameters();
            param.Add("@searchText", txtSearchE.Text);
            List<Employee> list = new List<Employee>();
            list = con.Query<Employee>
               ("GetEmployees", param, commandType: CommandType.StoredProcedure).ToList();
            gridEmployee.DataSource = list;

        }
       
        private void btnReg1_Click(object sender, EventArgs e)
        {
            try
            {
              
               DynamicParameters param = new DynamicParameters();
                param.Add("@Eid", EId );
                param.Add("@Ename", txtName.Text.Trim());
                param.Add("@Eidnumber", txtIdNumber.Text.Trim());
                param.Add("@Ephone", txtPhone.Text.Trim());
                param.Add("@Egender", genderCombo.SelectedItem);
                param.Add("@Eemail", txtEmail.Text.Trim());
                param.Add("@Eaddress", txtAddress.Text.Trim());
                param.Add("@Edob", txtDOB.Value);
             
                con.Execute("RegEmployee", param, commandType:CommandType.StoredProcedure);
                EId = 0;
                btnReg1.Text = "ADD";
               Clear();
                FillEmployeeGrid();

               
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
           
        }

        private void btnDelete1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Are you sure to delete?");
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Eid", Convert.ToInt32(gridEmployee.CurrentRow.Cells[0].Value.ToString()));
                con.Execute("DeleteEmployee", param, commandType: CommandType.StoredProcedure);
                Clear();
                FillEmployeeGrid();
               
            }
            catch (Exception err)
            {

               MessageBox.Show(err.Message);
            }
            
        }

        private void doubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridEmployee.CurrentRow.Index != -1)
            {
                EId = Convert.ToInt32(gridEmployee.CurrentRow.Cells[0].Value.ToString());
                txtName.Text = gridEmployee.CurrentRow.Cells[1].Value.ToString();
               // genderCombo.Text = gridEmployee.CurrentRow.Cells[5].Value.ToString();
                txtPhone.Text = gridEmployee.CurrentRow.Cells[2].Value.ToString();
                txtAddress.Text = gridEmployee.CurrentRow.Cells[3].Value.ToString();
                txtEmail.Text = gridEmployee.CurrentRow.Cells[6].Value.ToString();
                txtIdNumber.Text = gridEmployee.CurrentRow.Cells[7].Value.ToString();
                
                btnReg1.Text = "EDIT";
            }
            else
            {
                MessageBox.Show("Double click cell ");

            }
                
            
        }

        private void click(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridEmployee.CurrentRow.Index != -1)
                {
                    EId = Convert.ToInt32(gridEmployee.CurrentRow.Cells[0].Value.ToString());
                    txtName.Text = gridEmployee.CurrentRow.Cells[1].Value.ToString();
                    //genderCombo.Text = gridEmployee.CurrentRow.Cells[5].Value.ToString();
                    txtPhone.Text = gridEmployee.CurrentRow.Cells[2].Value.ToString();
                    txtAddress.Text = gridEmployee.CurrentRow.Cells[3].Value.ToString();
                    txtEmail.Text = gridEmployee.CurrentRow.Cells[6].Value.ToString();
                    txtIdNumber.Text = gridEmployee.CurrentRow.Cells[7].Value.ToString();

                    btnReg1.Text = "EDIT";
                }
                else
                {
                    MessageBox.Show("Double click cell ");

                }
            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }
            

        }

        private void btnSearch1_Click(object sender, EventArgs e)
        {
            FillEmployeeGrid();
        }

        private void txtSearchE_TextChanged(object sender, EventArgs e)
        {
            FillEmployeeGrid();
        }

        private void btnCancel1_Click(object sender, EventArgs e)
        {
            Clear();
        }
        //DASHBOARD FUNCTIONS



        private void button8_Click(object sender, EventArgs e)
        {
            var receive = new RECEIVINGS();
            this.Close();
            receive.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var customers = new CUSTOMERS();
            this.Close();
            customers.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var gifts = new GIFTS();
            this.Close();
            gifts.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var supplier = new SUPPLIER();
            this.Close();
            supplier.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var config = new CONFIG(); this.Close();
            config.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var sale = new SALES();
            this.Close();
            sale.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var products = new PRODUCTS();
            this.Close();
            products.Show();
        }
    }
}
