using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final
{
    public partial class MODULES : Form
    {
        public MODULES()
        {
            InitializeComponent();
        }

        private void MODULES_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sale = new SALES();
            sale.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var customers = new CUSTOMERS();
            customers.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var employees = new EMPLOYEES();
            employees.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var products = new PRODUCTS();
            products.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var gifts = new GIFTS();
            gifts.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var supplier = new SUPPLIER();
            supplier.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var config = new CONFIG();
            config.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var receive = new RECEIVINGS();
            receive.Show();
        }
    }
}
