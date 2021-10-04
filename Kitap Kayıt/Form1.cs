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

namespace Kitap_Kayıt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-6RNSF0J\SQLEXPRESS;Initial Catalog=KitapVt;Integrated Security=True");
        
        void Listele()
        {
            SqlCommand komut = new SqlCommand("Select * From TblKitaplar", baglanti);
            SqlDataAdapter dalist = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            dalist.Fill(dt);           
            dataGridView1.DataSource = dt;
            }

        void Turler()
        {
            SqlCommand komut = new SqlCommand("Select * From TblTurler", baglanti);
            SqlDataAdapter dalist = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            dalist.Fill(dt);
            CmbTur.ValueMember = "TurId";
            CmbTur.DisplayMember = "TurAd";
            CmbTur.DataSource = dt;
        }
        
        private void BtnListele_Click(object sender, EventArgs e)
        {
            Listele();
            Turler();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TxtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komutekle = new SqlCommand(" insert into tblkitaplar(KitapAd,Yazar, Sayfa,Fiyat,YayınEvi,Tur) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);

            komutekle.Parameters.AddWithValue("@p1", TxtAd.Text);
            komutekle.Parameters.AddWithValue("@p2", TxtYazar.Text);
            komutekle.Parameters.AddWithValue("@p3", TxtSayfa.Text);
            komutekle.Parameters.AddWithValue("@p4", TxtFiyat.Text);
            komutekle.Parameters.AddWithValue("@p5", TxtYayın.Text);
            komutekle.Parameters.AddWithValue("@p6", CmbTur.SelectedIndex);
           
            baglanti.Close();

            MessageBox.Show("Kitap Veri Tabanına Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Listele();


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtId.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtId.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtId.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtId.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtId.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komutsil = new SqlCommand("Delete From TblKitaplar where Kitapid=@p1", baglanti);
            komutsil.Parameters.AddWithValue("@p1", TxtId.Text);
            baglanti.Close();

            MessageBox.Show("Kitap Veri Tabanından Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            Listele();
        }
    }
}
