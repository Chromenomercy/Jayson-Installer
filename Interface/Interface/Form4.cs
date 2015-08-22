using System;
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
        static Timer myTimer = new System.Windows.Forms.Timer();
        static bool exitFlag = false;
        Form3 FormPrev;
        string currentFileStatus;
        public Form4(Form3 _FormPrev)
        {
            InitializeComponent();
            this.FormPrev = _FormPrev;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            extractPath = (this.FormPrev.downloadLocation + "bundle-of-joy.git").ToString();
            zipPath = (extractPath + ".zip").ToString();

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
            myTimer.Interval = 50;
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
                destinationFolder.CopyHere(file, 4 | 16);
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
                destinationFolder.CopyHere(file, 4 | 16);
            }
        }

        //regular update method (50 ms intervals)
        private void TimerEventProcessor(Object myObject, EventArgs e)
        {
            progressLabel.Text = currentFileStatus;
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if ((this.FormPrev.downloadLocation[this.FormPrev.downloadLocation.Length - 1]).ToString() != "\\")
                this.FormPrev.downloadLocation += "\\";
            UnZip(zipPath, extractPath);
            if (DeleteFile(zipPath))
                currentFileStatus = "Removing zip folder successful";
            else
                currentFileStatus = "Removing zip folder unsuccessful";
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
