using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;




namespace sporsalonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.ControlBox = false;
        }
        OleDbConnection baglanti = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;Data Source = giris.accdb; Persist Security Info = False;");

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string kullaniciAdi = textBox1.Text;
            string sifre = textBox2.Text;
            OleDbCommand komut = new OleDbCommand("Select * from Tablo1 WHERE KullanıcıAdı='" + kullaniciAdi + "' and Şifre = '" + sifre + "'  ", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                Form2 form = new Form2();
                this.Hide();
                form.Show();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı ve şifre hatalı veya eksik!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}
