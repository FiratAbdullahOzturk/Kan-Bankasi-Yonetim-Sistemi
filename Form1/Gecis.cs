using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form1
{
    public partial class Gecis : Form
    {
        public Gecis()
        {
            InitializeComponent();
        }
        int startpos = 0;


        private void Gecis_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startpos += 1;
            GecisProgressBar.Value = startpos;
            if(GecisProgressBar.Value==100)
            {
                GecisProgressBar.Value = 0;
                timer1.Stop();
                Login login = new Login();
                login.Show();
                this.Hide();
            }
        }
    }
}
