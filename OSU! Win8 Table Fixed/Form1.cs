using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WintabDN;

namespace OSU__Win8_Table_Fixed
{
    public partial class Form1 : Form
    {
        public delegate void DelegateStatueUpdate(string str);

        Thread thread;

        public void StatueUpdate(string str)
        {
            if (this.lab_status.InvokeRequired)//wait invoke 
            {
                DelegateStatueUpdate d = new DelegateStatueUpdate(StatueUpdate);
                this.Invoke(d, new object[] {str});
            }
            else
            {
                this.lab_status.Text = str;
            }
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Do()
        {
            CWintabContext context;

            try
            {
                //Get the table context Process the System Message Only
                context = CWintabInfo.GetDefaultDigitizingContext(ECTXOptionValues.CXO_SYSTEM);
                if (context != null)
                {
                    context.Open();
                    while (true)
                    {
                        context.SetOverlapOrder(true);
                        StatueUpdate("SetOverlapOrder Successful!");
                        Thread.Sleep(2000);
                    }
                }
                else
                {
                    StatueUpdate("Get context Faild!");
                }
            }
            catch (Exception)
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thread = new Thread(Do);
            thread.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
            Environment.Exit(0);
        }
    }
}
