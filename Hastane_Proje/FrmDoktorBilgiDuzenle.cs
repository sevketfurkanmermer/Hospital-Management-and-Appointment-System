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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgln = new sqlbaglantisi();


        private void btnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand kmt = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p4 where DoktorTC=@p5", bgln.baglanti()) ;
            kmt.Parameters.AddWithValue("@p1",TxtAd.Text);
            kmt.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            kmt.Parameters.AddWithValue("@p3", cmbBrans.Text);
            kmt.Parameters.AddWithValue("@p4", TxtSifre.Text);
            kmt.Parameters.AddWithValue("@p5", MtxtTcNo.Text);
            kmt.ExecuteNonQuery();
            bgln.baglanti().Close();
            MessageBox.Show("Doktor Güncellendi","Bilgi");

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MtxtTcNo.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            SqlCommand komutbrans = new SqlCommand("select BransAd from Tbl_Branslar", bgln.baglanti());
            SqlDataReader dr2 = komutbrans.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgln.baglanti().Close();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Doktorlar", bgln.baglanti()) ;
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand kmt = new SqlCommand("Delete from Tbl_Doktorlar where DoktorId=@p1",bgln.baglanti());
            kmt.Parameters.AddWithValue("@p1",txtId.Text);
            kmt.ExecuteNonQuery();
            bgln.baglanti().Close();
            MessageBox.Show("Doktor Silindi");
        }
    }
    
}
