using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Interface
{ 
    public partial class Form3 : Form
    {
        Form2 FormPrev;
        Form4 FormNext;
        public string downloadLocation = (@"C:\Users\" + Environment.UserName + @"\AppData\Roaming").ToString();

        public Form3(Form2 _FormPrev)
        {
            InitializeComponent();
            this.FormPrev = _FormPrev;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Finish_Click(object sender, EventArgs e)
        {
            
        }

        private void Back_Click(object sender, EventArgs e)
        {
            
        }

        private void Finish_Click_1(object sender, EventArgs e)
        {
            if (FormNext == null)
                FormNext = new Form4(this, FormPrev);
            FormNext.Show();
            Hide();
        }

        private void Back_Click_1(object sender, EventArgs e)
        {
            FormPrev.Show();
            Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            downloadLocation = e.ToString();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void Browse_Click(object sender, EventArgs e)
        {
            //If user pressed ok in dialoge window, set download location to the location and update the text box
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                downloadLocation = folderBrowserDialog1.SelectedPath;
                textBox1.Text = downloadLocation;
            }
        }
    }
}
