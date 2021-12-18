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

        #region FormLoad
        private void Form1_Load(object sender, EventArgs e)
        {
            //BUG: autoincrement и navigator работают плохо
            this.tableTableAdapter.Fill(this.taskManagerDBDataSet.Table);
            // для отображения новых таблиц нужен новый адаптер
            this.workTableAdapter.Fill(this.taskManagerDBDataSet.Work);
            tableDataGridView.DataSource = tableBindingSource;
            //Notification loop
            for (int i = 0; i < tableDataGridView.RowCount; i++)
            {
                for (int j = 0; j < tableDataGridView.ColumnCount; j++)
                {
                    if (DateTime.Now.Date.ToString() == tableDataGridView[j, i].Value.ToString())
                    {
                        try
                        {
                            notifyIcon1.Text = "TaskApp";
                            notifyIcon1.Visible = true;
                            notifyIcon1.BalloonTipTitle = "Today";
                            notifyIcon1.BalloonTipText = !string.IsNullOrEmpty(tableDataGridView[1, i].Value.ToString()) ? tableDataGridView[1, i].Value.ToString() : "Empty name";
                            notifyIcon1.ShowBalloonTip(100);
                        }
                        
                        catch
                        {
                            MessageBox.Show("Enter valid values", "Error");
                        }
                    }
                }
            }
            tableDataGridView.Columns[0].Visible = false;

        }
        #endregion

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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        #region pickDate
        private void tableDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (tableDataGridView.CurrentCell.ColumnIndex == 2)
            {
                var picker = new DateTimePicker();
                Form f = new Form();
                f.StartPosition = FormStartPosition.CenterScreen;
                f.Height = picker.Height +40;
                f.Width = picker.Width + 40;
                f.ShowIcon = false;
                f.MinimizeBox = false;
                f.MaximizeBox = false;
                f.FormBorderStyle = FormBorderStyle.FixedSingle;
                f.Controls.Add(picker);
                if (!String.IsNullOrEmpty(tableDataGridView.CurrentCell.Value.ToString()))
                {
                    picker.Value = DateTime.ParseExact(tableDataGridView.CurrentCell.Value.ToString(), "dd.MM.yyyy H:mm:ss",
                                   System.Globalization.CultureInfo.InvariantCulture);
                }

                f.ShowDialog();
                tableDataGridView.CurrentCell.Value = picker.Value.Date;

            }
        }
        #endregion
        

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tableBindingSource.AddNew();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tableBindingSource.RemoveCurrent();
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.taskManagerDBDataSet);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox1.Text))
            {
                if (tableBindingSource.DataMember == this.taskManagerDBDataSet.Table.TableName)
                {
                    tableBindingSource.Filter = string.Format(@"Name LIKE '%{0}%' OR
                      Status LIKE '%{0}%' OR
                      Subject LIKE '%{0}%' OR
                      Type LIKE '%{0}%'", textBox1.Text);
                }
                else if (tableBindingSource.DataMember == this.taskManagerDBDataSet.Work.TableName)
                {
                   tableBindingSource.Filter = string.Format(
                    @"Name LIKE '%{0}%' OR
                      Description LIKE '%{0}%' OR 
                      Priority LIKE '%{0}%' OR
                      Type LIKE '%{0}%'", textBox1.Text);
                }
            }
            else
            {
                tableBindingSource.Filter = string.Empty;
            }
            
        }

        private void tableDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
