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
using TrackingDefaceDAO;
using TrackingDefaceBUS;
using TrackingDefaceDTO;

namespace TrackingDefaceGUI
{
    public partial class Form1 : Form
    {
        WebBUS webBUS = new WebBUS();
        DataTable dtWeb;
        Web web = new Web();
        string abc = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            //GetDomTree getDom = new GetDomTree();

           // getDom.test();
           //getDom.test();
           //outPutTxt.Text = getDom.tessst(); 
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable DTWEB = webBUS.GetAllRecords();
            dataGridViewWeb.DataSource = DTWEB;
            
            dtWeb = webBUS.GetAllWebEnable(1);
            listViewWeb.LargeImageList = imageListView;
            for (int i = 0; i < dtWeb.Rows.Count; i++)
            {
                DataRow datarow = dtWeb.Rows[i];
                ListViewItem listItem = new ListViewItem(datarow["NameSite"].ToString());
                listItem.ImageIndex = 1;
                listItem.SubItems.Add(datarow["URL"].ToString());   //1
                listItem.SubItems.Add(datarow["WebStatus"].ToString()); //2
                listItem.SubItems.Add(datarow["IPPublic"].ToString()); //3
                listItem.SubItems.Add(datarow["WebPriority"].ToString()); //4
                listItem.SubItems.Add(datarow["Phones"].ToString());// 5
                listItem.SubItems.Add(datarow["Emails"].ToString()); //6
                listItem.SubItems.Add(datarow["isEnable"].ToString()); //7

                listViewWeb.Items.Add(listItem);
            }
        }

        private void listViewWeb_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (listViewWeb.SelectedItems.Count == 0) return;
            //nameLabel.Text = dtWeb.rows

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

        private void button2_Click(object sender, EventArgs e)
        {
            abc = webBUS.listString("http://www.hochiminhcity.gov.vn/");
        }

        private void btnGet_Click_1(object sender, EventArgs e)
        {
            if (abc == null)
            {
                abc = webBUS.listString("http://www.hochiminhcity.gov.vn/");
            }
             
        }



  

     

       
      
    }
}
