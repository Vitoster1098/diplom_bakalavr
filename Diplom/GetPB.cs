using System;
using System.Windows.Forms;

namespace Diplom
{
    public partial class GetPB : Form
    {
        public GetPB()
        {
            InitializeComponent();
        }
        public int number = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            number = 0;
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            number = 1;
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            number = 2;
            this.Hide();
        }
    }
}
