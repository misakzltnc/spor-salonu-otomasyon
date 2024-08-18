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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            this.ControlBox = false;

        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=egitmenler.accdb; Persist Security Info=False;");
        private Button selectedButton;

        private void button11_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            this.Hide();
            form.Show();
        }

        private void ButonlariGuncelle()
        {
            try
            {
                using (OleDbCommand command = new OleDbCommand("SELECT ButonAdi, Durum FROM renk", baglanti))
                {
                    baglanti.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string buttonText = reader["ButonAdi"].ToString();
                            string state = reader["Durum"].ToString();
                            foreach (Control c in this.Controls)
                            {
                                if (c is Button && c.Text.StartsWith("Rezervasyon"))
                                {
                                    Button btn = c as Button;
                                    if (btn.Text == buttonText)
                                    {
                                        btn.BackColor = state == "true" ? Color.Green : Color.Red;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            ButonlariGuncelle();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            selectedButton = sender as Button;
            // Önce seçili butonu bul
            Button btnToCheckout = this.Controls.OfType<Button>()
                .FirstOrDefault(b => b.BackColor == Color.Red && b.Text.StartsWith("Rezervasyon"));

            if (btnToCheckout != null)
            {
                // Butonun arka plan rengini yeşile çevir
                btnToCheckout.BackColor = Color.Green;

                // Veritabanını güncelle
                try
                {
                    using (OleDbCommand command = new OleDbCommand("UPDATE renk SET Durum = 'true' WHERE ButonAdi = ?", baglanti))
                    {
                        command.Parameters.AddWithValue("@ButonAdi", btnToCheckout.Text);
                        baglanti.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    baglanti.Close();
                }
            }


        }
    }
}
