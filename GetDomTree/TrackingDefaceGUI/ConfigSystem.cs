using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackingDefaceDAO;
using TrackingDefaceBUS;
using TrackingDefaceDTO;
using System.Media;

namespace TrackingDefaceGUI
{
    public partial class ConfigSystem : Form
    {
        EmailBUS emailBUS = new EmailBUS();
        public ConfigSystem()
        {
            InitializeComponent();
        }

       
        private void ConfigSystem_Load(object sender, EventArgs e)
        {           
            emailBUS.LoadDataSource(portTextBox, hostTextBox, timeOutTextBox, emailTextBox, sslCheckBox, passwordTextBox);
            passwordTextBox.PasswordChar = '*';
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            emailBUS.UpdateEmail(portTextBox, hostTextBox, timeOutTextBox, emailTextBox, sslCheckBox, passwordTextBox);
        }
    }
}
