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

namespace Form1
{
    public partial class KanBagısı : Form
    {
        public KanBagısı()
        {
            InitializeComponent();
            uyeler();
            KanStok();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Fırat Abdullah ö\Documents\KanBankasiDb.mdf"";Integrated Security=True;Connect Timeout=30");
        private void uyeler()
        {
            baglanti.Open();
            string query = "select *from DonorTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            KBagısıDGV.DataSource = ds.Tables[0];
            baglanti.Close();
        }
        private void KanStok()
        {
            baglanti.Open();
            string query = "select *from KanTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            KStoguDGV.DataSource = ds.Tables[0];
            baglanti.Close();
        }
        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void KanBagısı_Load(object sender, EventArgs e)
        {

        }
        int eskistok;
        private void Stok(string Kgrup)
        {
            baglanti.Open();
            string query = "select * from KanTbl where KGrup='" + Kgrup + "'";
            SqlCommand komut = new SqlCommand(query,baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            sda.Fill(dt);
            foreach(DataRow dr  in dt.Rows)
            {
                eskistok = Convert.ToInt32(dr["KStok"].ToString());
            }
            baglanti.Close();
        }

        private void KBagısıDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DAdSoyadTb.Text = KBagısıDGV.SelectedRows[0].Cells[1].Value.ToString();
            DKGrubuTb.Text = KBagısıDGV.SelectedRows[0].Cells[6].Value.ToString();
            Stok(DKGrubuTb.Text);
        }
        private void Reset()
        {
            DAdSoyadTb.Text = "";
            DKGrubuTb.Text = "";
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (DAdSoyadTb.Text == "")
            {
                MessageBox.Show("Bir Donor Seçiniz");
            }
            else
            {

                try
                {
                    int stok = eskistok + 1;
                    string query = "update KanTbl set KStok='" + stok + "'where KGrup='" + DKGrubuTb.Text + "';";
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand(query, baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Bağış Başarılı");
                    baglanti.Close();
                    Reset();
                    KanStok();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ex.Message" + ex.Message);
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Donor dn = new Donor();
            dn.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            DonorListesi dn = new DonorListesi();
            dn.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            KanBagısı dn = new KanBagısı();
            dn.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Hasta dn = new Hasta();
            dn.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            HastaListesi dn = new HastaListesi();
            dn.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            KanStoğu dn = new KanStoğu();
            dn.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            KanTransferi dn = new KanTransferi();
            dn.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            KontrolPaneli dn = new KontrolPaneli();
            dn.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }
    }
}
