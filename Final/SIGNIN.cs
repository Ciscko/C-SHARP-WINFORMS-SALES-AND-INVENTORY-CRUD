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
    public partial class SIGNIN : Form
    {
        public string dbPassword;
        SqlConnection connection = new SqlConnection(@"Data Source=HP-PC\SQLEXPRESS;Initial Catalog=trial;Integrated Security=true");
        public SIGNIN()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        class User
        {
            public string  password {set; get;}
            public string username { set; get; }
        }
        

        void clear()
        {
            txtPassword.Text = txtUsername.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
                connection.Open();
                DynamicParameters param = new DynamicParameters();
               
                param.Add("@password", txtPassword.Text.Trim());
                param.Add("@username", txtUsername.Text.Trim());
                 List<User> list = connection.Query<User>("GetUser", param, commandType:CommandType.StoredProcedure).ToList();

                //dbPassword = connection.Query("GetUser", param, commandType: CommandType.StoredProcedure).ToString();

                if (list.ToString() != "" )
                {
                    //load  dashboard
                    Form1 f2 = new Form1();
                    f2.ShowDialog();
                    connection.Close();
                    clear();
                }
                else
                {
                    //Notify incorrect password
                   
                   
                    MessageBox.Show(dbPassword +" Incorrect");
                    
                    connection.Close();

                }
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
                

            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var modules = new MODULES();
            this.Hide();
            modules.Show();
        }
    }
}
