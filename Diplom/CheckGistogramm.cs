using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Diplom
{
    public partial class CheckGistogramm : Form
    {
        public CheckGistogramm()
        {
            InitializeComponent();            
        }
        public double[] mymas, mymas1, mymas2;

        private void CheckGistogramm_Load(object sender, EventArgs e)
        {
            /*chart3.Series[0].IsValueShownAsLabel = true;
            chart4.Series[0].IsValueShownAsLabel = true;*/
            

            chart1.Series[0].Points.DataBindY(mymas);
            chart1.Series[1].Points.DataBindY(mymas1);
            chart1.Series[2].Points.DataBindY(mymas2);

            chart2.Series[0].Points.DataBindY(mymas);
            chart3.Series[0].Points.DataBindY(mymas1);
            chart4.Series[0].Points.DataBindY(mymas2);

            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            chart3.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart3.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart3.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart3.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            chart4.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart4.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart4.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart4.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
        }
    }
}
