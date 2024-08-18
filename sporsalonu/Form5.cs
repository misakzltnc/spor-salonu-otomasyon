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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            button1.Click += new EventHandler(button_Click);
            button2.Click += new EventHandler(button_Click);
            button3.Click += new EventHandler(button_Click);
            button4.Click += new EventHandler(button_Click);
            button5.Click += new EventHandler(button_Click);
            button6.Click += new EventHandler(button_Click);
            button7.Click += new EventHandler(button_Click);
            button8.Click += new EventHandler(button_Click);
            button9.Click += new EventHandler(button_Click);
            this.ControlBox = false;

        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=egitmenler.accdb; Persist Security Info=False;");
        private Button selectedButton;

        ///eğer kırmızıysa yani rezerve edilmişse messagebox uyarı vermeli, "lütfen başka bir alan seçiniz" yazabilir
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
        private void Form5_Load(object sender, EventArgs e)
        {
            ButonlariGuncelle();
        }

        private void button_Click(object sender, EventArgs e)
        {
            selectedButton = sender as Button;
            // Müşteri ID kontrolü
            if (string.IsNullOrWhiteSpace(textBox1.Text) || !int.TryParse(textBox1.Text, out int musteriID))
            {
                MessageBox.Show("Lütfen geçerli bir Müşteri ID girin.");
                return;
            }

            try
            {
                // Müşteri bilgilerini kontrol et
                using (OleDbCommand command = new OleDbCommand("SELECT * FROM müsteri WHERE MüsteriID = ?", baglanti))
                {
                    command.Parameters.AddWithValue("@MusteriID", musteriID);
                    baglanti.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool aylikUyelik = Convert.ToBoolean(reader["AylıkÜyelik"]);
                            decimal ucret = Convert.ToDecimal(reader["Ücret"]);
                            string adi = reader.GetString(reader.GetOrdinal("Ad"));
                            string soyadi = reader.GetString(reader.GetOrdinal("Soyad"));

                            // Aylık üyelik kontrolü ve ücret güncelleme
                            if (aylikUyelik)
                            {
                                MessageBox.Show($"Müşteri {adi} {soyadi}, zaten aylık üyeliği var. Ücret kesilmeyecek.");
                            }
                            else
                            {
                                ucret += 140;

                                using (OleDbCommand updateCommand = new OleDbCommand("UPDATE müsteri SET Ücret = ? WHERE MüsteriID = ?", baglanti))
                                {
                                    updateCommand.Parameters.AddWithValue("@Ücret", ucret);
                                    updateCommand.Parameters.AddWithValue("@MüsteriID", musteriID);
                                    updateCommand.ExecuteNonQuery();
                                }

                                MessageBox.Show($"Müşteri {adi} {soyadi}, aylık üyelik yapılmadığı için ücrete 140 birim eklendi. Yeni ücret: {ucret:C}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Belirtilen Müşteri ID bulunamadı.");
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

            // Rezervasyon güncelleme işlemleri
            Button clickedButton = sender as Button;
            if (clickedButton != null && clickedButton.Text.StartsWith("Rezervasyon"))
            {
                // Butonun arka plan rengini değiştirerek rezervasyon durumunu güncelle
                bool isReserved = clickedButton.BackColor == Color.Green;
                clickedButton.BackColor = isReserved ? Color.Red : Color.Green;

                // Veritabanını güncelle
                try
                {
                    using (OleDbCommand command = new OleDbCommand("UPDATE renk SET Durum = ? WHERE ButonAdi = ?", baglanti))
                    {
                        string newState = isReserved ? "false" : "true";
                        command.Parameters.AddWithValue("@Durum", newState);
                        command.Parameters.AddWithValue("@ButonAdi", clickedButton.Text);
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

        private void button10_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            this.Hide();
            form.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
