﻿using System;
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
    public partial class FrmRandevuListesi : Form
    {
        public FrmRandevuListesi()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgln = new sqlbaglantisi();

        private void FrmRandevuListesi_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular",bgln.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource= dt;
        }
    }
}
