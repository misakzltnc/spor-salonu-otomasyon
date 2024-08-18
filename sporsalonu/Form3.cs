using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sporsalonu
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.ControlBox = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            this.Hide();
            form.Show();
        }
        private void kalori()
        {
            int kalori = 0;
            foreach (string item in listBox2.Items)
            {
                string[] parts = item.Split('-');
                if (parts.Length == 2)
                {
                    int calories;

                    if (int.TryParse(parts[1].Trim(), out calories))
                    {
                        kalori += calories;
                    }
                }
            }
            textBox1.Text = kalori.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Tricep pushdown -100");
            listBox1.Items.Add("Overhead Dumbell Extention -50");
            listBox1.Items.Add("Bench Dips -80");
            listBox1.Items.Add("Dumbell Curl -60");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Leg Press -100");
            listBox1.Items.Add("Leg Extention -50");
            listBox1.Items.Add("Barbell Calf Raises -80");
            listBox1.Items.Add("Leg Curl -60");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Seated Cable Row -100");
            listBox1.Items.Add("Dumbell Row -50");
            listBox1.Items.Add("Barbell Calf Raises -80");
            listBox1.Items.Add("Seated Cable Row -60");
        }

        private void button5_Click(object sender, EventArgs e)
        {

            listBox1.Items.Clear();
            listBox1.Items.Add("Barbell Calf Raises -100");
            listBox1.Items.Add("Reverse Crunch -50");
            listBox1.Items.Add("Toe Touches -80");
            listBox1.Items.Add("Plank -60");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1) // Eğer listBox2'de bir öğe seçildiyse
            {
                listBox2.Items.RemoveAt(listBox2.SelectedIndex); // Seçili öğeyi sil
            }
            kalori();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1) // Eğer listBox1'de bir öğe seçildiyse
            {
                string selected = listBox1.SelectedItem.ToString(); // Seçili öğeyi al
                listBox2.Items.Add(selected); // Seçili öğeyi listBox2'ye ekle
            }
            kalori();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}
