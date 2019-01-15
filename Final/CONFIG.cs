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
    public partial class CONFIG : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=HP-PC\SQLEXPRESS;Initial Catalog=ospos;Integrated Security=true");
        public CONFIG()
        {
            InitializeComponent();
        }

        private void CONFIG_Load(object sender, EventArgs e)
        {
            FillGrid();
            Clear();
        }
        class Config
        {
            public string COMPANY { set; get; }
            public string PHONE { set; get; }
            public string ADDRESS { set; get; }
            public string TAX { set; get; }
            public string POLICY { set; get; }

        }

        void FillGrid()
        {

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DynamicParameters param = new DynamicParameters();
                List<Config> list = new List<Config>();
                list = con.Query<Config>("GetConfig", commandType:CommandType.StoredProcedure).ToList();
                gridConfig.DataSource = list;

            }
            catch (Exception re)
            {

               MessageBox.Show(re.Message);
            }


        }

       void Clear()
        {
            company.Text = phone.Text = tax.Text = address.Text = policy.Text = "";
            FillGrid();
            btnEdit.Text = "ADD";
        }

        private void gridConfig_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridConfig.CurrentRow.Index != -1)
                {
                    Id = Convert.ToInt32(gridConfig.CurrentRow.Cells[0].Value.ToString());
                    company.Text = gridConfig.CurrentRow.Cells[1].Value.ToString();
                    phone.Text = gridConfig.CurrentRow.Cells[2].Value.ToString();
                    address.Text = gridConfig.CurrentRow.Cells[3].Value.ToString();
                    tax.Text = gridConfig.CurrentRow.Cells[4].Value.ToString();
                    policy.Text = gridConfig.CurrentRow.Cells[5].Value.ToString();
                    btnEdit.Text = "EDIT";
                    btnCansel.Enabled = true;

                }
            }
            catch (Exception re)
            {

                MessageBox.Show(re.Message);
            }
        }

        private void btnCansel_Click(object sender, EventArgs e)
        {
            Clear();
        }


        public int  Id = 0;
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", Id);
                param.Add("@Company", company.Text);
                param.Add("@Phone", phone.Text);
                param.Add("@Policy", policy.Text);
                param.Add("@Address", address.Text);
                param.Add("@tax", tax.Text);
                con.Execute("AddConfig", param, commandType:CommandType.StoredProcedure);
                Id = 0;
                FillGrid();
                Clear();

            }
            catch (Exception re)
            {

                MessageBox.Show(re.Message);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", Id);
            con.Execute("DeleteConfig", param, commandType:CommandType.StoredProcedure);
        }

        private void gridConfig_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridConfig.CurrentRow.Index != -1)
                {
                    Id = Convert.ToInt32(gridConfig.CurrentRow.Cells[0].Value.ToString());
                    company.Text = gridConfig.CurrentRow.Cells[1].Value.ToString();
                    phone.Text = gridConfig.CurrentRow.Cells[2].Value.ToString();
                    address.Text = gridConfig.CurrentRow.Cells[3].Value.ToString();
                    tax.Text = gridConfig.CurrentRow.Cells[4].Value.ToString();
                    policy.Text = gridConfig.CurrentRow.Cells[5].Value.ToString();
                    btnEdit.Text = "EDIT";
                    btnCansel.Enabled = true;

                }
            }
            catch (Exception re)
            {

                MessageBox.Show(re.Message);
            }
        }
    }   
}
