using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO: добавить возможность вставки столбца
// TODO: поменять название решения, кнопок и т.д
// TODO: смена шаблона (groupBox) по выбору в списке меню
// NOTE: можно добавить цвета для Type, Status

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
            //BUG: autoincrement и navigator работают плохо
            this.tableTableAdapter.Fill(this.taskManagerDBDataSet.Table);
            // для отображения новых таблиц нужен новый адаптер
            this.workTableAdapter.Fill(this.taskManagerDBDataSet.Work);
            tableDataGridView.DataSource = tableBindingSource;

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
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

        private void bindingNavigatorSave_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.taskManagerDBDataSet);
        }

        private void menuTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (menuTemplates.SelectedIndex != -1)
                tableBindingSource.DataMember = this.taskManagerDBDataSet.Tables[menuTemplates.SelectedIndex].TableName;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
