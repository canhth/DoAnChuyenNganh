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
using TrackingDefaceBUS.Application;
using System.Xml;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;


namespace TrackingDefaceGUI
{
    public partial class Form1 : Form
    {
        WebBUS webBUS = new WebBUS();
        TrackingDeface application = new TrackingDeface();
        CheckWebResponse checkWebsite = new CheckWebResponse();

        BackgroundWorker bg_wk;
        BackgroundWorker bg_wk2;

        public Form1()
        {
            InitializeComponent();
            bg_wk = new BackgroundWorker();
            bg_wk.WorkerReportsProgress = true;
            bg_wk.WorkerSupportsCancellation = true;
            bg_wk.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            bg_wk.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
            bg_wk.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);

            bg_wk2 = new BackgroundWorker();
            bg_wk2.DoWork += new DoWorkEventHandler(backgroundWorkerLevel2_DoWork);
            bg_wk2.WorkerReportsProgress = true;
            bg_wk2.WorkerSupportsCancellation = true;
            bg_wk2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerLevel2_RunWorkerCompleted);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {           
            webBUS.LoadDataTable(dataGridViewWeb);
            webBUS.LoadListView(listViewWeb, imageListView);
            timerRunTracking.Start();         
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
        public class Object
        {
            public DataTable dtTableWeb { get; set; }
            public int webIndex { get; set; }
            public int count { get; set; }
        }

        /*  Run ThreadPool */
        private void button4_Click(object sender, EventArgs e)
        {
            DataTable dtTableWeb1 = webBUS.GetWebSiteEnable(1);
            int webCount = dtTableWeb1.Rows.Count;

            Object _obj = new Object();
            _obj.dtTableWeb = dtTableWeb1;
            _obj.webIndex = 0;
            _obj.count = webCount/2;
            object obj1 = _obj;

            Object _obj1 = new Object();
            _obj1.dtTableWeb = dtTableWeb1;
            _obj1.webIndex = webCount/2;
            _obj1.count = webCount;
            object obj2 = _obj1;

            ThreadPool.QueueUserWorkItem(RunThread1, obj1);      
            ThreadPool.QueueUserWorkItem(RunThread1, obj2);  
        }
        
        /* Using for ThreadPool */
        public void RunThread1 (object obje1)
        {
            Object obj = (Object)obje1;
            string url, name;
            for (int i = obj.webIndex ; i< obj.count ; i++)
            {
                url = obj.dtTableWeb.Rows[i]["URL"].ToString();
                name = obj.dtTableWeb.Rows[i]["NameSite"].ToString();
                if (checkWebsite.isOnline(url) && checkWebsite.Ping(url))
                {
                    application.TrackingDefaceWebSite(obj.dtTableWeb, i);
                }
                else
                    MessageBox.Show("Website " + name + " da bi chet, kiem tra lai.");             
             }           
            MessageBox.Show("Ket Thuc Thread");
        }
      

        /*  Using for BackGroundWorker */
        private void RunBackGroundWorker (DataTable dtTableWeb, int index, int count)
        {
            string url, name;
            for (int i = index; i < count; i++)
            {
                url = dtTableWeb.Rows[i]["URL"].ToString();
                name = dtTableWeb.Rows[i]["NameSite"].ToString();
                if (checkWebsite.GetPage(url) && checkWebsite.Ping(url))
                {
                    application.TrackingDefaceWebSite(dtTableWeb, i);
                }
                else
                    MessageBox.Show("Website " + name + " da bi chet, kiem tra lai.");
            }    
        }

        // Run when time over
        private void timerRunTracking_Tick(object sender, EventArgs e)
        {
            if (bg_wk.IsBusy != true && bg_wk2.IsBusy != true)
            {             
                bg_wk.RunWorkerAsync();
                MessageBox.Show("Back Ground 1 dang chay");
                bg_wk2.RunWorkerAsync();
                MessageBox.Show("Back Ground 2 dang chay");
            }        
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                DataTable dtTableWeb = webBUS.GetWebSiteEnable(1);
               int webCount = dtTableWeb.Rows.Count;
               RunBackGroundWorker(dtTableWeb, 0, webCount / 2);
            }
            else
                MessageBox.Show("Kiem tra lai ket noi Internet");
                         
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Hoan thanh cong viec BG 1"); 
            webBUS.LoadListView(listViewWeb, imageListView);      
        }

        private void backgroundWorkerLevel2_DoWork(object sender, DoWorkEventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                DataTable dtTableWeb = webBUS.GetWebSiteEnable(1);
                int webCount = dtTableWeb.Rows.Count;
                RunBackGroundWorker(dtTableWeb, webCount /2, webCount);
            }
            else
                MessageBox.Show("Kiem tra lai ket noi Internet");
            
        }

        private void backgroundWorkerLevel2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            webBUS.LoadListView(listViewWeb, imageListView);
            MessageBox.Show("Hoan thanh cong viec BG 2");
            timerRunTracking.Interval = 360000;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timerRunTracking_Tick(sender, e);
        }

        private void dataGridViewWeb_Click(object sender, EventArgs e)
        {
            if (dataGridViewWeb.Rows.Count != 0)
            {
                nameSileTxt.Text = dataGridViewWeb.CurrentRow.Cells[0].Value.ToString();
                urlTxt.Text = dataGridViewWeb.CurrentRow.Cells[1].Value.ToString();
                checkBoxEnable.Checked = (bool)dataGridViewWeb.CurrentRow.Cells[11].Value;
                ipPublicTxt.Text = dataGridViewWeb.CurrentRow.Cells[3].Value.ToString();
                priorityTxt.Text = dataGridViewWeb.CurrentRow.Cells[5].Value.ToString();
                phoneTxt.Text = dataGridViewWeb.CurrentRow.Cells[6].Value.ToString();
                emailTxt.Text = dataGridViewWeb.CurrentRow.Cells[7].Value.ToString();
                findTextTxt.Text = dataGridViewWeb.CurrentRow.Cells[8].Value.ToString();
                banTextTxt.Text = dataGridViewWeb.CurrentRow.Cells[10].Value.ToString();
                
            }
        }
   
       
      
    }
}
