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
    public partial class FrmBransPaneli : Form
    {
        public FrmBransPaneli()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgln = new sqlbaglantisi();

        private void FrmBransPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Branslar",bgln.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;


        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand kmtekle = new SqlCommand("insert into Tbl_Branslar (BransAd) values (@p1)", bgln.baglanti());
            kmtekle.Parameters.AddWithValue("@p1", txtBransAd.Text);
            kmtekle.ExecuteNonQuery();
            bgln.baglanti().Close();
            MessageBox.Show("Brans Eklendi","Eklendi");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtBransId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtBransAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand kmtsil = new SqlCommand("delete from Tbl_Branslar where BransId=@a1",bgln.baglanti());
            kmtsil.Parameters.AddWithValue("@a1",txtBransId.Text);
            kmtsil.ExecuteNonQuery();
            bgln.baglanti().Close();
            MessageBox.Show("Brans Silindi");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand kmtguncelle = new SqlCommand("update Tbl_Branslar set BransAd=@p1 where BransId=@p2",bgln.baglanti());
            kmtguncelle.Parameters.AddWithValue("@p1",txtBransAd.Text);
            kmtguncelle.Parameters.AddWithValue("@p2",txtBransId.Text);
            kmtguncelle.ExecuteNonQuery();
            bgln.baglanti().Close();
            MessageBox.Show("Brans Guncellendi");
        }
    }
}
