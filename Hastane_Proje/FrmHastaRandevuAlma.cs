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
    public partial class FrmHastaRandevuAlma : Form
    {
        public FrmHastaRandevuAlma()
        {
            InitializeComponent();
        }
        public string tc;

        sqlbaglantisi bgln = new sqlbaglantisi();
       

        private void FrmHastaRandevuAlma_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;
            //Ad Soyad Çekme
            SqlCommand komut = new SqlCommand("select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTC=@p1",bgln.baglanti());
            komut.Parameters.AddWithValue("@p1",lblTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] +" "+ dr[1];
            }
            bgln.baglanti().Close();

            //Randevuları Görüntüleme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where HastaTC="+tc,bgln.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource= dt;

            //Bransları çekme

            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Branslar",bgln.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgln.baglanti().Close();
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1",bgln.baglanti());
            komut3.Parameters.AddWithValue("@p1",CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0]+" " + dr3[1]);
            }
            bgln.baglanti().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where RandevuBrans='"+CmbBrans.Text+"'"+" and RandevuDoktor='"+CmbDoktor.Text+"'"+" and RandevuDurum=0",bgln.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        
        }

        private void linkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaBilgiDuzenle fr = new FrmHastaBilgiDuzenle();
            fr.tcno = lblTc.Text;
            fr.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
            CmbBrans.Text = dataGridView2.Rows[secilen].Cells[3].Value.ToString();
            CmbDoktor.Text = dataGridView2.Rows[secilen].Cells[4].Value.ToString();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand kmtrandevual = new SqlCommand("update Tbl_Randevular set RandevuDurum=1,HastaTC=@p1 where RandevuId=@p2",bgln.baglanti());
            kmtrandevual.Parameters.AddWithValue("@p1",lblTc.Text);
            kmtrandevual.Parameters.AddWithValue("@p2",txtId.Text);
            kmtrandevual.ExecuteNonQuery();
            bgln.baglanti().Close();
            MessageBox.Show("Randevu Alındı");
        }
    }
}
