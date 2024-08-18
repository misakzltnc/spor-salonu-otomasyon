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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            this.ControlBox = false;

        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=egitmenler.accdb; Persist Security Info=False;");


        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            this.Hide();
            form.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["MüşteriID"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["Ad"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["Soyad"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["Telefon"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["Ücret"].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells["AylıkÜcret"].Value.ToString();
        }
        DataTable tablo = new DataTable();
        private void listele()
        {
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * from müsteri", baglanti);
            DataTable tablo = new DataTable();
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(" insert into müsteri(MüsteriID,Ad,Soyad,Telefon,AylıkÜyelik,Ücret) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox6.Text + "'  , '"+textBox5.Text+"') ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Eklendi");
            tablo.Clear();
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            if (!string.IsNullOrEmpty(textBox1.Text)) // Güncellenecek kaydın ID alanı boş değilse devam et
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("UPDATE müsteri SET Ad = @ad, Soyad = @soyad, Telefon = @telefon, AylıkÜyelik = @aylıkÜyelik , Ücret= @ücret WHERE MüsteriID = @MüsteriID", baglanti);
                komut.Parameters.AddWithValue("@ad", textBox2.Text);
                komut.Parameters.AddWithValue("@soyad", textBox3.Text);
                komut.Parameters.AddWithValue("@telefon", textBox4.Text);
                komut.Parameters.AddWithValue("@aylıkÜyelik", textBox6.Text);
                komut.Parameters.AddWithValue("@ücret", textBox5.Text);
                komut.Parameters.AddWithValue("@MüsteriID", textBox1.Text); // Güncellenecek kaydı ID alanına göre belirtiyoruz
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçilen satırın verilerini al
                string müsteriid = dataGridView1.SelectedRows[0].Cells["MüsteriID"].Value.ToString();
                string ad = dataGridView1.SelectedRows[0].Cells["Ad"].Value.ToString();
                string soyad = dataGridView1.SelectedRows[0].Cells["Soyad"].Value.ToString();
                string telefon = dataGridView1.SelectedRows[0].Cells["Telefon"].Value.ToString();
                string ücret = dataGridView1.SelectedRows[0].Cells["Ücret"].Value.ToString();
                string aylıkÜyelik = dataGridView1.SelectedRows[0].Cells["AylıkÜyelik"].Value.ToString();


                // Veritabanından doğrudan silme işlemini gerçekleştir
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("DELETE FROM müsteri WHERE MüsteriID = @MüsteriID AND Ad = @ad AND Soyad = @soyad AND Telefon = @telefon AND Ücret = @ücret ", baglanti);
                komut.Parameters.AddWithValue("@MüsteriID",müsteriid );
                komut.Parameters.AddWithValue("@Ad", ad);
                komut.Parameters.AddWithValue("@Soyad", soyad);
                komut.Parameters.AddWithValue("@Telefon", telefon);
                komut.Parameters.AddWithValue("@Ücret", ücret);
                komut.Parameters.AddWithValue("@aylıkÜyelik", aylıkÜyelik);
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["MüsteriID"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["Ad"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["Soyad"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["Telefon"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["Ücret"].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells["AylıkÜyelik"].Value.ToString();

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}
