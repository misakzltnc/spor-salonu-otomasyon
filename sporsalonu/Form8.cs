using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sporsalonu
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            this.ControlBox = false;
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=egitmenler.accdb; Persist Security Info=False;");

        private void button1_Click(object sender, EventArgs e)
        {
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

                            
                                ucret -= ucret;

                                using (OleDbCommand updateCommand = new OleDbCommand("UPDATE müsteri SET Ücret = ? WHERE MüsteriID = ?", baglanti))
                                {
                                    updateCommand.Parameters.AddWithValue("@Ücret", ucret);
                                    updateCommand.Parameters.AddWithValue("@MüsteriID", musteriID);
                                    updateCommand.ExecuteNonQuery();
                                }

                                MessageBox.Show($"Müşteri {adi} {soyadi}, ödemesi tamamlanmıştır. Yeni ücret: {ucret:C}");
                            
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

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            this.Hide();
            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}
