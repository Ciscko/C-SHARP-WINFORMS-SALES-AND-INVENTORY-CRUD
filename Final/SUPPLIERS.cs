using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Final
{
    public partial class Form1 : Form

    {

        public SqlConnection Connect1 { get; set; } = new SqlConnection(@"Data Source=HP-PC\SQLEXPRESS;Initial Catalog=trial;Integrated Security=True");
        public int  Suplier_id=0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        class Supplier
        {
            public int Id { set; get; }
            public string Name { set; get; }
            public string Phone { set; get; }
            public string Address { set; get; }
        }
        
       public void Clear()
        {
            txtPhone.Text=txtAddress.Text = txtName.Text="";
        }

      

        private void button1_Click(object sender, EventArgs e)
        {

            
            try
            {
                Connect1.Close();
                if (Connect1.State == ConnectionState.Closed)
                {
                     Connect1.Open();
                   
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Supplier_id", Suplier_id );
                    param.Add("@Supplier_name", txtName.Text.Trim());
                    param.Add("@Supplier_phone", txtPhone.Text.Trim());
                    param.Add("@Supplier_address", txtAddress.Text.Trim());

                    Connect1.Execute("AddSuppliers", param, commandType: CommandType.StoredProcedure);
                    MessageBox.Show("Successful");
                    btnSaveOrEdit.Text = "SAVE";
                    FillGrid();
                    Clear();
                    

                }
                else
                    MessageBox.Show("Unsuccessful");
                Connect1.Close();


            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                
            }

        }
        void FillGrid()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@searchVal", searchText.Text.Trim());
                List<Supplier> list = Connect1.Query<Supplier>
                    ("ViewSuppliers", param, commandType: CommandType.StoredProcedure).ToList<Supplier>();
                gridSuppliers.DataSource = list;
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
            

        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void Supplier_Load_1(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (gridSuppliers.CurrentRow.Index != -1)
                    Suplier_id = Convert.ToInt32(gridSuppliers.CurrentRow.Cells[0].Value.ToString());
                txtName.Text = gridSuppliers.CurrentRow.Cells[1].Value.ToString();
                txtPhone.Text = gridSuppliers.CurrentRow.Cells[2].Value.ToString();
                txtAddress.Text = gridSuppliers.CurrentRow.Cells[3].Value.ToString();
                btnDelete.Enabled = true;
                btnSaveOrEdit.Text = "EDIT";



            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }

        private void searchText_TextChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void doubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                if (gridSuppliers.CurrentRow.Index != -1)
                  Suplier_id = Convert.ToInt32(gridSuppliers.CurrentRow.Cells[0].Value.ToString());
                txtName.Text = gridSuppliers.CurrentRow.Cells[1].Value.ToString();
                txtPhone.Text = gridSuppliers.CurrentRow.Cells[2].Value.ToString();
                txtAddress.Text = gridSuppliers.CurrentRow.Cells[3].Value.ToString();
                btnDelete.Enabled = true;
                btnSaveOrEdit.Text = "EDIT";



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
               
                if (gridSuppliers.CurrentRow.Index != -1)
                {
                    Suplier_id = Convert.ToInt32(gridSuppliers.CurrentRow.Cells[0].Value.ToString());
                    txtName.Text = gridSuppliers.CurrentRow.Cells[1].Value.ToString();
                    txtPhone.Text = gridSuppliers.CurrentRow.Cells[2].Value.ToString();
                    txtAddress.Text = gridSuppliers.CurrentRow.Cells[3].Value.ToString();
                    param.Add("@Supplier_id", Suplier_id);

                    Connect1.Execute("DeleteSuppliers", param, commandType: CommandType.StoredProcedure);
                    FillGrid();
                    MessageBox.Show("Delete Successful");
                    Clear();
                    
                }
                    
                else 
                    MessageBox.Show("Index of row not valid");


            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }

            
        }

        private void cellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Clear();
            btnSaveOrEdit.Text = "SAVE";
            FillGrid();
        }

        private void gridSuppliers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
