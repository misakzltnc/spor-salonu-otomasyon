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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.ControlBox = false;

        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=egitmenler.accdb; Persist Security Info=False;");

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["Ad"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["Soyad"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["Telefon"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["Mail"].Value.ToString(); 
            textBox5.Text = dataGridView1.CurrentRow.Cells["Görev"].Value.ToString();

        }
        DataTable tablo = new DataTable();
        private void listele()
        {
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * from egitmenler", baglanti);
            DataTable tablo = new DataTable();
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(" insert into egitmenler(Ad,Soyad,Telefon,Mail,Görev) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "') ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close() ;
            MessageBox.Show("Kayıt Eklendi");
            tablo.Clear();
            listele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            this.Hide();
            form.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçilen satırın verilerini al
                string ad = dataGridView1.SelectedRows[0].Cells["Ad"].Value.ToString();
                string soyad = dataGridView1.SelectedRows[0].Cells["Soyad"].Value.ToString();
                string telefon = dataGridView1.SelectedRows[0].Cells["Telefon"].Value.ToString();
                string eposta = dataGridView1.SelectedRows[0].Cells["Mail"].Value.ToString();
                string gorev = dataGridView1.SelectedRows[0].Cells["Görev"].Value.ToString();

                // Veritabanından doğrudan silme işlemini gerçekleştir
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("DELETE FROM egitmenler WHERE Ad = @ad AND Soyad = @soyad AND Telefon = @telefon AND Mail = @eposta AND Görev = @gorev", baglanti);
                komut.Parameters.AddWithValue("@ad", ad);
                komut.Parameters.AddWithValue("@soyad", soyad);
                komut.Parameters.AddWithValue("@telefon", telefon);
                komut.Parameters.AddWithValue("@eposta", eposta);
                komut.Parameters.AddWithValue("@gorev", gorev);
                komut.ExecuteNonQuery();
                baglanti.Close();

                // DataGridView'i güncelle
                tablo.Clear();
                listele();
                MessageBox.Show("Seçili satır silindi.");
            }
            else
            {
                MessageBox.Show("Lütfen bir satır seçin.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /* baglanti.Open();
             OleDbCommand komut = new OleDbCommand( " update egitmenler set Ad='"+textBox1.Text+"' , Soyad='"+textBox2.Text+"', Telefon = '"+textBox3.Text+"' , Mail = '"+textBox4.Text+"' , Görev = '" +textBox5.Text+"'  "   , baglanti);
             komut.ExecuteNonQuery();
             baglanti.Close();
             MessageBox.Show("Kayıt Güncellendi");
             tablo.Clear();
             listele();*/
            if (!string.IsNullOrEmpty(textBox1.Text)) // Güncellenecek kaydın Ad alanı boş değilse devam et
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("UPDATE egitmenler SET Soyad = @soyad, Telefon = @telefon, Mail = @mail, Görev = @gorev WHERE Ad = @kimlik", baglanti);
                komut.Parameters.AddWithValue("@soyad", textBox2.Text);
                komut.Parameters.AddWithValue("@telefon", textBox3.Text);
                komut.Parameters.AddWithValue("@mail", textBox4.Text);
                komut.Parameters.AddWithValue("@gorev", textBox5.Text);
                komut.Parameters.AddWithValue("@ad", textBox1.Text); // Güncellenecek kaydı Ad alanına göre belirtiyoruz
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Güncellendi");
                tablo.Clear();
                listele();
            }
            else
            {
                MessageBox.Show("Lütfen güncellenecek kaydı seçin.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
