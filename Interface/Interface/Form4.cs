﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace Interface
{
    public partial class Form4 : Form
    {
        string extractPath;
        string zipPath;
        CheckedListBox selected_apps;
        static Timer myTimer = new System.Windows.Forms.Timer();
        Form2 FormPrevPrev;
        Form3 FormPrev;
        string currentFileStatus;

        public Form4(Form3 _FormPrev, Form2 _FormPrevPrev)
        {
            InitializeComponent();
            FormPrev = _FormPrev;
            FormPrevPrev = _FormPrevPrev;
            selected_apps = FormPrevPrev.curitems;

            extractPath = (FormPrev.downloadLocation).ToString();
            zipPath = (extractPath + ".zip").ToString();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
        
        private void progressBar1_Click(object sender, EventArgs e)
        {
            progressLabel.Text = currentFileStatus;
        }

        private void DownloadStart(object sender, EventArgs e)
        {
            //Timer events
            myTimer.Tick += new EventHandler(TimerEventProcessor);

            // Sets the timer interval to 5 seconds
            myTimer.Interval = 5;
            myTimer.Start();

            //Called when start button is clicked
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri("https://github.com/Chromenomercy/bundle-of-joy/archive/v1.0.0.zip"), zipPath);
        }

        static bool DeleteFile(string filepath)
        {
            try
            {
                File.Delete(filepath);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        private static void UnZip(string zipFile, string folderPath)
        //flags above used to configure copy options
        //4 – Do not display a progress dialog box.
        //8 – Give the file being operated on a new name in a move, copy, or rename operation if a file with the target name already exists.
        //16 – Respond with “Yes to All” for any dialog box that is displayed.
        //64 – Preserve undo information, if possible.
        //128 – Perform the operation on files only if a wildcard file name(*.*) is specified.
        //256 – Display a progress dialog box but do not show the file names.
        //512 – Do not confirm the creation of a new directory if the operation requires one to be created.
        //1024 – Do not display a user interface if an error occurs.
        //2048 – Version 4.71. Do not copy the security attributes of the file.
        //4096 – Only operate in the local directory.Do not operate recursively into subdirectories.
        //8192 – Version 5.0. Do not copy connected files as a group. Only copy the specified files.
        {
            if (!File.Exists(zipFile))
                throw new FileNotFoundException();

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            Shell32.Shell objShell = new Shell32.Shell();
            Shell32.Folder destinationFolder = objShell.NameSpace(folderPath);
            Shell32.Folder sourceFile = objShell.NameSpace(zipFile);
            foreach (var file in sourceFile.Items())
            {
                destinationFolder.CopyHere(file, 16);
            }
        }

        //Read above for copy instructions
        private static void Copy(string startFile, string folderPath)
        {
            if (!File.Exists(startFile))
                throw new FileNotFoundException();

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            Shell32.Shell objShell = new Shell32.Shell();
            Shell32.Folder destinationFolder = objShell.NameSpace(folderPath);
            Shell32.Folder sourceFile = objShell.NameSpace(startFile);
            foreach (var file in sourceFile.Items())
            {
                destinationFolder.CopyHere(file, 16);
            }
        }

        //regular update method (5 ms intervals)
        private void TimerEventProcessor(Object myObject, EventArgs e)
        {
            progressLabel.Text = currentFileStatus;
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            currentFileStatus = ("Download in progress: " + progressBar.Value + @"% Complete.").ToString();
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if ((FormPrev.downloadLocation[FormPrev.downloadLocation.Length - 1]).ToString() != "\\")
                FormPrev.downloadLocation += "\\";
            currentFileStatus = "Unzipping";
            UnZip(zipPath, extractPath);
            currentFileStatus = "Unzipping successful";
            currentFileStatus = "Removing zip folder";
            if (DeleteFile(zipPath))
                currentFileStatus = "Removing zip folder successful";
            else
                currentFileStatus = "Removing zip folder unsuccessful";

            string bundlePath = (extractPath + @"\bundle-of-joy-1.0.0").ToString();
            string finalLoc = (FormPrev.downloadLocation + @"\JaysonPackage").ToString();

            //need to test if null or crash
            if (selected_apps.CheckedItems != null) {
                foreach (object itemChecked in selected_apps.CheckedItems)
                {
                    if (itemChecked.ToString() == "Souless Escape")
                        Copy((bundlePath + @"\src\chromenomercy\soulless_escape").ToString(), finalLoc);
                }
            }
            currentFileStatus = "Installation complete, please exit";
        }

        private void Start_Click(object sender, EventArgs e)
        {
            DownloadStart(sender, e);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
