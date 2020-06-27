using System;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace XMLviewer
{
    public partial class Editor : Form
    {
        bool isSaved;
        string tableName;

        public Editor()
        {
            InitializeComponent();
            tableName = "table";
            isSaved = false;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputForm newDialog = new InputForm("Новая таблицы", "Имя таблицы:");
            if (newDialog.ShowDialog(this) == DialogResult.OK)
            {
                dataGridView.Name = newDialog.input;
                dataGridView.DataSource = null;
                dataGridView.Rows.Clear();
            }
            isSaved = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dataGridView.Columns.Clear();

                DataSet dataSet = new DataSet();
                dataSet.ReadXml(openFileDialog.FileName);
                dataGridView.DataSource = dataSet.Tables[0];

                saveFileDialog.FileName = openFileDialog.FileName;
                isSaved = true;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isSaved)
            {
                isSaved = true;
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                saveData();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                saveData();
            }
        }
        private void saveData()
        {
            DataTable dt = new DataTable(tableName);
            foreach (DataGridViewColumn col in dataGridView.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }
            dt.WriteXml(saveFileDialog.FileName);
        }

        private void addColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputForm newDialog = new InputForm("Новая колонка", "Название:");
            if (newDialog.ShowDialog(this) == DialogResult.OK)
            {
                dataGridView.Columns.Add(newDialog.input, newDialog.input);
            }
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView.Columns.Clear();
            isSaved = false;
        }
    }
}
