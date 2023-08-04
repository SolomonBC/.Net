using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ticketandvisitormgmtsys
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }


        private void loginBtn_Click(object sender, EventArgs e)
        {

            this.Hide();
            if (usernameTextbox.Text.Contains("Admin")) 
            {
                Admin admin = new Admin();
                admin.Show();
            }
            else
            {
                Staff staff = new Staff();
                staff.Show();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
