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

namespace Hastane_Proje
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        public string tc;
        sqlbaglantisi bgln = new sqlbaglantisi();
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;

            //Ad-Soyad
            SqlCommand komut = new SqlCommand("select SekreterAdSoyad from Tbl_Sekreterler where SekreterTC=@p1",bgln.baglanti());
            komut.Parameters.AddWithValue("@p1",lblTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0].ToString();
            }
            bgln.baglanti().Close();
            //Branslar
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select * from Tbl_Branslar",bgln.baglanti());
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;

            //Doktorlar

            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter("select DoktorAd,DoktorSoyad,DoktorBrans from Tbl_Doktorlar",bgln.baglanti());
            da3.Fill(dt3);
            dataGridView2.DataSource= dt3;

            //Combobaxa bransları getirme
            SqlCommand komutbrans = new SqlCommand("select BransAd from Tbl_Branslar", bgln.baglanti()) ;
            SqlDataReader dr2= komutbrans.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
        }

        private void btnRandevuKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values(@p1,@p2,@p3,@p4)",bgln.baglanti());
            komutkaydet.Parameters.AddWithValue("@p1", mskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@p2", mskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@p3", cmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@p4", cmbDoktor.Text);
            komutkaydet.ExecuteNonQuery();
            bgln.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu","Bilgi");
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();

            SqlCommand komutdoktor = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@z1",bgln.baglanti());

            komutdoktor.Parameters.AddWithValue("@z1", cmbBrans.Text);
            SqlDataReader dr3 = komutdoktor.ExecuteReader();
            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0]+ " " + dr3[1]);
            }
            bgln.baglanti().Close();
        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand kmtduyuru = new SqlCommand("insert into Tbl_Duyurular (Duyurular) values (@d1)",bgln.baglanti());
            kmtduyuru.Parameters.AddWithValue("@d1", richTextBox1.Text);
            kmtduyuru.ExecuteNonQuery();
            bgln.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");
        }

        private void btnDoktorPaneli_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli fr = new FrmDoktorPaneli();
            fr.Show();
        }

        private void btnBransPaneli_Click(object sender, EventArgs e)
        {
            FrmBransPaneli fr = new FrmBransPaneli();
            fr.Show();
        }

        private void btnRandevuListesi_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi fr = new FrmRandevuListesi();
            fr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
        }
    }
}
