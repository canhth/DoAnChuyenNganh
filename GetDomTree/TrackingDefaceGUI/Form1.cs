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

namespace TrackingDefaceGUI
{
    public partial class Form1 : Form
    {
        WebBUS webBUS = new WebBUS();

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

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //webBUS.ListObjectURL();

            ListViewItem itm = new ListViewItem(arr);
            listViewWeb.Items.Add(itm);
        }

  

     

       
      
    }
}
