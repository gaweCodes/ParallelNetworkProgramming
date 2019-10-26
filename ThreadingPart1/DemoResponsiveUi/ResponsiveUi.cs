using System;
using System.Threading;
using System.Windows.Forms;

namespace DemoResponsiveUi
{
    public partial class ResponsiveUi : Form
    {
        public ResponsiveUi()
        {
            InitializeComponent();
        }
        private void DoLongOperation()
        {
            Thread.Sleep(10000);
            MessageBox.Show("Finished");
        }

        private void BtnWithoutThread_Click(object sender, EventArgs e) => DoLongOperation();

        private void BtnThreaded_Click(object sender, EventArgs e) => new Thread(DoLongOperation).Start();
    }
}
