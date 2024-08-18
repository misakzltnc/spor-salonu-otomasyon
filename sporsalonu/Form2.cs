using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sporsalonu
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.ControlBox = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            this.Hide();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4();
            this.Hide();
            form.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 form = new Form5();
            this.Hide();
            form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form6 form = new Form6();
            this.Hide(); form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form8 form = new Form8();
            this.Hide(); form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form7 form = new Form7();
            this.Hide(); form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //this.Close();
            Application.Exit();
        }
    }
}
