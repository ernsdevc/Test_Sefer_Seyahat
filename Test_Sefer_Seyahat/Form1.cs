using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Test_Sefer_Seyahat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=MACHINEX\MSSQLSERVER01;Initial Catalog=TestYolcuBilet;Integrated Security=True");

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLYolcuBilgi (Ad,Soyad,Telefon,TC,Cinsiyet,Mail) VALUES(@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTelefon.Text);
            komut.Parameters.AddWithValue("@p4", mskTC.Text);
            komut.Parameters.AddWithValue("@p5", rdbErkek.Checked ? "Erkek" : "Kadın");
            komut.Parameters.AddWithValue("@p6", txtMail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Yolcu Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnKaptan_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLKaptan (KaptanNo,AdSoyad,Telefon) VALUES(@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", mskKaptanNo.Text);
            komut.Parameters.AddWithValue("@p2", txtKaptanAdSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskKaptanTelefon.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kaptan Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSeferOlustur_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLSeferBilgi (Kalkis,Varis,Tarih,Saat,Kaptan,Fiyat) VALUES(@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtKalkis.Text);
            komut.Parameters.AddWithValue("@p2", txtVaris.Text);
            komut.Parameters.AddWithValue("@p3", mskTarih.Text);
            komut.Parameters.AddWithValue("@p4", mskSaat.Text);
            komut.Parameters.AddWithValue("@p5", mskKaptan.Text);
            komut.Parameters.AddWithValue("@p6", txtFiyat.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Sefer Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void SeferListesi(string tarih, string saat)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLSeferBilgi WHERE (@Tarih='' OR Tarih=@Tarih) AND (@Saat='' OR Saat=@Saat)", baglanti);
            da.SelectCommand.Parameters.AddWithValue("@Tarih", tarih);
            da.SelectCommand.Parameters.AddWithValue("@Saat", saat);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtSaat.CustomFormat = "HH:mm";
            dtSaat.ShowUpDown = true;
            SeferListesi(string.Empty, string.Empty);
        }

        void ButonTemizle()
        {
            for (int i = 1; i < 17; i++)
            {
                string buttonName = "btn" + i.ToString();
                Button button = Controls.Find(buttonName, true).FirstOrDefault() as Button;
                button.Enabled = true;
                button.BackColor = Color.Silver;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ButonTemizle();
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtSeferNumara.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();

            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT sd.Koltuk,yb.Cinsiyet FROM TBLSeferDetay AS sd INNER JOIN TBLYolcuBilgi AS yb ON yb.TC = sd.YolcuTC WHERE SeferNo = @SeferNo", baglanti);
            komut.Parameters.AddWithValue("@SeferNo", txtSeferNumara.Text);
            SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                int koltuk = Convert.ToInt32(dr["Koltuk"]);
                string cinsiyet = dr["Cinsiyet"].ToString();

                string buttonName = "btn" + koltuk.ToString();
                Button button = Controls.Find(buttonName, true).FirstOrDefault() as Button;
                button.Enabled = false;

                if (cinsiyet == "Erkek")
                {
                    button.BackColor = Color.DeepSkyBlue;
                }
                else
                {
                    button.BackColor = Color.Pink;
                }
            }

            baglanti.Close();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "9";
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "10";
        }

        private void btn11_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "11";
        }

        private void btn12_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "12";
        }

        private void btn13_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "13";
        }

        private void btn14_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "14";
        }

        private void btn15_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "15";
        }

        private void btn16_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "16";
        }

        private void btnRezervasyon_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLSeferDetay (SeferNo,YolcuTC,Koltuk) VALUES(@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtSeferNumara.Text);
            komut.Parameters.AddWithValue("@p2", mskYolcuTC.Text);
            komut.Parameters.AddWithValue("@p3", txtKoltukNo.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Rezervasyon Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSeferBul_Click(object sender, EventArgs e)
        {
            string tarih = dtTarih.Value.ToString("dd.MM.yyyy");
            string saat = dtSaat.Value.ToString("HH:mm");
            SeferListesi(tarih,saat);
        }
    }
}
