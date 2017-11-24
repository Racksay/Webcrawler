#region License

// --------------------------------------------------
// Copyright © PayEx. All Rights Reserved.
// 
// This software is proprietary information of PayEx.
// USE IS SUBJECT TO LICENSE TERMS.
// --------------------------------------------------

#endregion

using System;
using System.ComponentModel;
using System.Windows;

namespace Webcrawler
{
    public partial class MainWindow : Window
    {
        private readonly BackgroundWorker myWorker = new BackgroundWorker();


        public MainWindow()
        {
            InitializeComponent();

            //this.myWorker.DoWork += new DoWorkEventHandler(myWorker_DoWork);
            //this.myWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(myWorker_RunWorkerCompleted);
            //this.myWorker.ProgressChanged += new ProgressChangedEventHandler(myWorker_ProgressChanged);
            //this.myWorker.WorkerReportsProgress = true;
            //this.myWorker.WorkerSupportsCancellation = true;
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var webcrwaler = new WebCrawler("http://www.google.com");

            //var arrObjects = new object[] { 100 };
            //if (!this.myWorker.IsBusy)
            //{
            //    this.button1.IsEnabled = false;
            //    this.myWorker.RunWorkerAsync(arrObjects);
            //}
        }


        private void myWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //BackgroundWorker sendingWorker = (BackgroundWorker)sender;

            //if (!sendingWorker.CancellationPending)
            //{
            //    var webCrawler = new WebCrawler("http://www.google.com");
            //}
        }


        private void myWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }


        private void myWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}