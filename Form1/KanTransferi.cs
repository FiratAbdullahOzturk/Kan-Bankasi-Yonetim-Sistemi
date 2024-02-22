using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form1
{
    public partial class KanTransferi : Form
    {
        public KanTransferi()
        {
            InitializeComponent();
            fillHastaCb();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Fırat Abdullah ö\Documents\KanBankasiDb.mdf"";Integrated Security=True;Connect Timeout=30");
        
        private void fillHastaCb()
        { 
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select HNum from HastaTbl", baglanti);
            SqlDataReader rdr;
            rdr=komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("HNum",typeof(int));
            dt.Load(rdr);
            HastaIdCb.ValueMember = "HNum";
            HastaIdCb.DataSource = dt;  
            baglanti.Close();
        }
        private void VeriAl()
        {
            baglanti.Open();
            string query = "select * from HastaTbl where HNum=" + HastaIdCb.SelectedValue.ToString() + "";
            SqlCommand komut = new SqlCommand(query, baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                HasAdTb.Text = dr["HAdSoyad"].ToString();
                KanGrupTb.Text = dr["HKGrup"].ToString();
            }
            baglanti.Close();
        }
        int stokk=0;
        private void Stok(string Kgrup)
        {
            baglanti.Open();
            string query = "select * from KanTbl where KGrup='" + Kgrup + "'";
            SqlCommand komut = new SqlCommand(query, baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                stokk = Convert.ToInt32(dr["KStok"].ToString());
            }
            baglanti.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void KanTransferi_Load(object sender, EventArgs e)
        {

        }

        private void HastaIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            VeriAl();
            Stok(KanGrupTb.Text);
            if(stokk>0)
            { 
                TransferBtn.Visible = true;
                UygunLbl.Text = "Stok Uygun";
                UygunLbl.Visible = true;
            }
            else
            {
                UygunLbl.Text = "Stok uygun değil";
                UygunLbl.Visible = true;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Hasta ha=new Hasta();
            ha.Show();
            this.Hide();
        }
        private void Reset()
        {
            HasAdTb.Text = "";
            KanGrupTb.Text = "";
            UygunLbl.Visible = false;
            TransferBtn.Visible = false;
        }
        private void kanGuncelle()
        {
            try
            {
                int yenistok = stokk - 1;
                string query = "update KanTbl set KStok=" + yenistok + " where KGrup='" + KanGrupTb.Text + "';";
                baglanti.Open();
                SqlCommand komut = new SqlCommand(query, baglanti);
                komut.ExecuteNonQuery();
                //MessageBox.Show("Hasta Başarıyla Güncellendi");
                baglanti.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ex.Message" + ex.Message);
            }
        }

        private void TransferBtn_Click(object sender, EventArgs e)
        {
            if (HasAdTb.Text ==  "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                try
                {
                    string query = "insert into TransferTbl values('" +HasAdTb.Text+ "','" +KanGrupTb.Text+ "')";
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand(query, baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Transfer Başarılı");
                    baglanti.Close();
                    Stok(KanGrupTb.Text);
                    kanGuncelle();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ex.Message");
                }


            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            KanStoğu ks = new KanStoğu();
            ks.Show();
            this.Hide();

        }

        private void label9_Click(object sender, EventArgs e)
        {
            Donor dn = new Donor();
            dn.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            KanBagısı dn = new KanBagısı();
            dn.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            DonorListesi dn = new DonorListesi();
            dn.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            HastaListesi dn = new HastaListesi();
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

