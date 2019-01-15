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
    public partial class LOGIN : Form
    {
        public string password;
        public string username;
        public string dbpassword;
        public int idDelete;
        public int idEdit=0;
       SqlConnection connection = new SqlConnection(@"Data Source=HP-PC\SQLEXPRESS;Initial Catalog=trial;Integrated Security=true");

        public LOGIN()
        {
            InitializeComponent();
        }
        class User
        {
            public int Id { set; get; }
            public  string Password{ set; get; }
            public string  Username { set; get; }
            public string Id_number { set; get; }
            public string Phone { set; get; }
  
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // password = txtPwd.Text;
            //username = txtName.Text;
            try
            {
                connection.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@password",password);
                param.Add("@username",username);
                //List<User> list = connection.Query<User>("GetUser", param, commandType:CommandType.StoredProcedure).ToList<User>();
                dbpassword = connection.Query("GetUser", param, commandType:CommandType.StoredProcedure).ToString();
                if (dbpassword == password)
                {
                    //load  dashboard
                    Form1 f2 = new Form1();
                    f2.ShowDialog();
                }
                else
                {
                    //Notify incorrect password
                    LOGIN login = new LOGIN();
                    //instruct.Text = "Credentials Invalid";
                    login.ShowDialog();
                }
            }
            catch (Exception err)
            {

               MessageBox.Show(err.Message);
            }
            

        }

        void FillGrid()
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@searchText", txtSearch.Text.Trim());
            List<User> list = connection.Query<User>
                ("GetUsers", param, commandType: CommandType.StoredProcedure).ToList<User>();
            usersGrid.DataSource = list;
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void register_Click(object sender, EventArgs e)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id",idEdit);
            param.Add("@username", txtUsername.Text.Trim());
            param.Add("@phone", txtPhone.Text.Trim());
            param.Add("@Id_number", txtIdNumber.Text.Trim());
            param.Add("@password", txtPassword.Text.Trim());

            connection.Execute("RegisterUser", param, commandType: CommandType.StoredProcedure);
            Clear();
            FillGrid();
            btnRegister.Text = "REGISTER";
            idEdit = 0;
               
        }

        void Clear()
        {
            txtUsername.Text = txtPassword.Text = txtPhone.Text = txtIdNumber.Text ="";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void itemClick(object sender, DataGridViewCellEventArgs e)
        {
           if(usersGrid.CurrentRow.Index != -1)
            {
                idEdit=idDelete = Convert.ToInt32(usersGrid.CurrentRow.Cells[0].Value.ToString());
                txtUsername.Text = usersGrid.CurrentRow.Cells[1].Value.ToString();
                txtIdNumber.Text = usersGrid.CurrentRow.Cells[2].Value.ToString();
                txtPhone.Text = usersGrid.CurrentRow.Cells[3].Value.ToString();
                txtPassword.Text = usersGrid.CurrentRow.Cells[4].Value.ToString();
                FillGrid();
                btnRegister.Text = "EDIT";
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (idDelete > 0)
            {
                MessageBox.Show("Confirm Delete");
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", idDelete);
                connection.Execute("DeleteUser", param, commandType: CommandType.StoredProcedure);
                FillGrid();
                Clear();
            }
            else
            {
                MessageBox.Show("Select a row by Double click first!");
            }
            
        }

        private void usersGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (usersGrid.CurrentRow.Index != -1)
            {
               idEdit = idDelete = Convert.ToInt32(usersGrid.CurrentRow.Cells[0].Value.ToString());
                txtUsername.Text = usersGrid.CurrentRow.Cells[2].Value.ToString();
                txtIdNumber.Text = usersGrid.CurrentRow.Cells[3].Value.ToString();
                txtPhone.Text = usersGrid.CurrentRow.Cells[4].Value.ToString();
                txtPassword.Text = usersGrid.CurrentRow.Cells[1].Value.ToString();
                FillGrid();
                btnRegister.Text = "EDIT";
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
