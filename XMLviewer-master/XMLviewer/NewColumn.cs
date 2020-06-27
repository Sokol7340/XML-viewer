using System;
using System.Windows.Forms;

namespace XMLviewer
{
    public partial class InputForm : Form
    {
        public string input = "";
        public InputForm()
        {
            InitializeComponent();
        }

        public InputForm(string name, string text) : this()
        {
            Text = name;
            label.Text = text;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            input = textBox.Text;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
