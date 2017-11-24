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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            new WebCrawler(this.textbos.Text);
        }
    }
}