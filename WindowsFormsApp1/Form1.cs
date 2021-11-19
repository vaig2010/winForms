using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = listBox1.SelectedIndex.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextBox tbox = new TextBox();
            tbox.Location = new System.Drawing.Point(259, 135);
            this.Controls.Add(tbox);
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            textBox2.Text = label1.Text;

            //tbox.Location = new System.Drawing.Point(259, 135);
            //this.Controls.Add(textBox2);
            textBox2.Visible = true;
            label1.Visible = false;
        }
        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Visible = false;
                label1.Text = textBox2.Text;
                label1.Visible = true;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
