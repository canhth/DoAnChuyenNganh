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


namespace TrackingDefaceGUI
{
    public partial class Form1 : Form
    {
        WebBUS webBUS = new WebBUS();
        Web web = new Web();
        TextContentBUS textContentBUS = new TextContentBUS();
        TextContentDAO daoTest = new TextContentDAO();
        ImageContentDAO daoImage = new ImageContentDAO();

        TrackingDeface application = new TrackingDeface();
        BackgroundWorker bg_wk;
        BackgroundWorker bg_wk2;
        Stopwatch timer = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
            bg_wk = new BackgroundWorker();
            bg_wk2 = new BackgroundWorker();
            bg_wk2.DoWork += new DoWorkEventHandler(backgroundWorkerLevel2_DoWork);
            bg_wk.WorkerReportsProgress = true;
            bg_wk.WorkerSupportsCancellation = true;
            
            bg_wk.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            bg_wk.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
            bg_wk.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            
        } 

        private void Form1_Load(object sender, EventArgs e)
        {

            webBUS.LoadDataTable(dataGridViewWeb);
            webBUS.LoadListView(listViewWeb, imageListView);
            timerRunTracking.Start();
            
            //Stopwatch runTime = new Stopwatch();
            //runTime.Start();
            //webBUS.CheckWebSite("http://www.hochiminhcity.gov.vn/Pages/default.aspx");//dataGridViewWeb.CurrentRow.Cells[2].ToString());
            //runTime.Stop();
            //Console.WriteLine(runTime.Elapsed.Seconds);    
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
            //Stopwatch runTime = new Stopwatch();
            //for (int i = 0 ; i < 10000; i++)
            //{
            //    if (i == 9999)
            //    {
            //        runTime.Start();
            //        webBUS.CheckWebSite("http://www.hochiminhcity.gov.vn/Pages/default.aspx");//dataGridViewWeb.CurrentRow.Cells[2].ToString());
            //        runTime.Stop();
            //    } 
            //}
            if (bg_wk2.IsBusy != true)
            {
                bg_wk2.RunWorkerAsync();
                MessageBox.Show("Back Ground 2 dang su dung");
            }
        }

        //public void testTimer ()
        //{
        //    Action work = () => Console.WriteLine(DateTime.Now.ToLongTimeString());

        //    Scheduler.Default.Schedule(
        //        // start in so many seconds
        //        TimeSpan.FromSeconds(60 - DateTime.Now.Second),
        //        // then run every minute
        //        () => Scheduler.Default.SchedulePeriodic(TimeSpan.FromMinutes(1), work));

        //    Console.WriteLine("Press return.");
        //    Console.ReadLine();
        //}

        private void timerRunTracking_Tick(object sender, EventArgs e)
        {
            //application.TrackingDefaceWebSite();
            if (bg_wk.IsBusy != true)
            {
                bg_wk.RunWorkerAsync();
                MessageBox.Show("Back Ground 1 dang chay");
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            application.TrackingDefaceWebSite();
           
            //for (int i = 1; (i <= 10); i++)
            //{
            //    if ((bg_wk.CancellationPending == true))
            //    {
            //        e.Cancel = true;
            //        break;
            //    }
            //    else
            //    {
            //        // Perform a time consuming operation and report progress.
            //        System.Threading.Thread.Sleep(500);
            //        bg_wk.ReportProgress((i * 10));
            //    }
            //}
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            textBoxTestBackground.Text = (e.ProgressPercentage.ToString() + "%");
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Hoan thanh cong viec BG 1");
            if ((e.Cancelled == true))
            {
                this.textBoxTestBackground.Text = "Canceled!";
            }

            else if (!(e.Error == null))
            {
                this.textBoxTestBackground.Text = ("Error: " + e.Error.Message);
            }

            else
            {
                this.textBoxTestBackground.Text = "Done!";
                webBUS.LoadListView(listViewWeb, imageListView);
            }
        }

        private void backgroundWorkerLevel2_DoWork(object sender, DoWorkEventArgs e)
        {
            application.TrackingDefaceWebSite();
        }

        private void backgroundWorkerLevel2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            webBUS.LoadListView(listViewWeb, imageListView);
            MessageBox.Show("Hoan thanh cong viec BG 2");
        }
       

  

     

       
      
    }
}
