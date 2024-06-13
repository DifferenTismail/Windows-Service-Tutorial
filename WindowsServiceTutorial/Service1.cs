using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;

namespace WindowsServiceTutorial
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000; //milisaniye
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stoped at " + DateTime.Now);
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service was ran at " + DateTime.Now);
        }
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".txt";
            if (!File.Exists(filePath))
            {
                //yazı için dosya oluştur
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
