using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO: поменять название решения, кнопок и т.д
// TODO: смена шаблона (groupBox) по выбору в списке меню
// NOTE: можно добавить цвета для Type, Status

namespace WindowsFormsApp1
{
   
    public partial class Form1 : Form
    {
        int btngridCount = 0;
        private void TextBoxReadOnly(bool flag)
        {
            textBox2.ReadOnly = flag;
            textBox2.BackColor = flag ? SystemColors.Control : SystemColors.Window;
            textBox2.BorderStyle = flag ? BorderStyle.None : BorderStyle.FixedSingle;
        }
        public Form1()
        {
            InitializeComponent();
            DynamicDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.workTableAdapter.Fill(this.taskManagerDBDataSet.Work);
            DynamicDataGridView.DataSource = this.taskManagerDBDataSet.Work;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "taskManagerDBDataSet.Table". При необходимости она может быть перемещена или удалена.
            this.tableTableAdapter.Fill(this.taskManagerDBDataSet.Table);
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = listBox1.SelectedIndex.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (btngridCount < this.taskManagerDBDataSet.Tables.Count && DynamicDataGridView.DataSource != this.taskManagerDBDataSet.Tables[btngridCount])
            {
                DynamicDataGridView.DataSource = this.taskManagerDBDataSet.Tables[btngridCount];
            }
            else
            {
                btngridCount %= this.taskManagerDBDataSet.Tables.Count;
                DynamicDataGridView.DataSource = this.taskManagerDBDataSet.Tables[btngridCount];
            }
            lblTableName.Text = this.taskManagerDBDataSet.Tables[btngridCount].ToString();
            btngridCount++;
            
        }
        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBoxReadOnly(true);
            }
        }

        private void tableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.taskManagerDBDataSet);
        }

        private void tableDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // для юзеров
            // MessageBox.Show("Введите дату в верном формате");
            // для разрабов
            MessageBox.Show(e.Exception.Message);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox2.ReadOnly == false)
            {
                TextBoxReadOnly(true);
            }
        }

        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            TextBoxReadOnly(false);
        }
    }
}
