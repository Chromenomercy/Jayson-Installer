using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interface
{
    public partial class Form2 : Form
    {
        Form1 FormPrev;
        Form3 FormNext;
        string curitems;

        public Form2(Form1 _FormPrev)
        {
            InitializeComponent();
            this.FormPrev = _FormPrev;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Next_Click(object sender, EventArgs e)
        {
            
        }

        private void Back_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            curitems = checkedListBox1.SelectedItems.ToString();
        }

        private void Next_Click_1(object sender, EventArgs e)
        {
            if (FormNext == null)
                FormNext = new Form3(this);
            FormNext.Show();
            this.Hide();
        }

        private void Back_Click_1(object sender, EventArgs e)
        {
            this.FormPrev.Show();
            this.Hide();
        }
    }
}
