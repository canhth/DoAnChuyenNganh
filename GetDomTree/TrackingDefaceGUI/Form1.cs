using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackingDefaceDAO;
using TrackingDefaceBUS;
using TrackingDefaceDTO;

namespace TrackingDefaceGUI
{
    public partial class Form1 : Form
    {
        WebBUS webBUS = new WebBUS();
        Web web = new Web();
        string abc = null;
        Stopwatch timer = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            
        } 

        private void Form1_Load(object sender, EventArgs e)
        {
            webBUS.LoadDataTable(dataGridViewWeb);
            webBUS.LoadListView(listViewWeb, imageListView);
            Stopwatch runTime = new Stopwatch();
            for (int i = 0; i < 10000; i++)
            {
                if (i == 9999)
                {
                    runTime.Start();
                    webBUS.CheckWebSite("http://www.hochiminhcity.gov.vn/Pages/default.aspx");//dataGridViewWeb.CurrentRow.Cells[2].ToString());
                    runTime.Stop();
                }
            }
        }

        private void listViewWeb_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (listViewWeb.SelectedItems.Count == 0) return;
            ListViewItem item = listViewWeb.SelectedItems[0];
            nameLabel.Text = item.SubItems[0].Text;
            urlLabel.Text = item.SubItems[1].Text;
            ipLabel.Text = item.SubItems[3].Text;
            priorityLabel.Text = item.SubItems[4].Text;
            phoneLabel.Text = item.SubItems[5].Text;
            emailLabel.Text = item.SubItems[6].Text;
            enableLabel.Text = item.SubItems[7].Text;
            statusLabel.Text = item.SubItems[2].Text;

        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            webBUS.InsertDataWeb(dataGridViewWeb, nameSileTxt, urlTxt, ipPublicTxt, priorityTxt, phoneTxt, emailTxt, findTextTxt, banTextTxt, checkBoxEnable);
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            webBUS.DeleteWeb(dataGridViewWeb);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            webBUS.UpdateWeb(dataGridViewWeb, nameSileTxt, urlTxt, ipPublicTxt, priorityTxt, phoneTxt, emailTxt, findTextTxt, banTextTxt, checkBoxEnable);
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Stopwatch runTime = new Stopwatch();
            for (int i = 0 ; i < 10000; i++)
            {
                if (i == 9999)
                {
                    runTime.Start();
                    webBUS.CheckWebSite("http://www.hochiminhcity.gov.vn/Pages/default.aspx");//dataGridViewWeb.CurrentRow.Cells[2].ToString());
                    runTime.Stop();
                } 
            }
        }

       

  

     

       
      
    }
}
