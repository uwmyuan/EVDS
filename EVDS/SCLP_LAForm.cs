using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Syncfusion.Windows.Forms;
using Syncfusion.Drawing;
using Syncfusion.Windows.Forms.Chart;
using ChartImportData;

using SCLP;
using Lagrange_SCLP;

namespace EVDS
{
    public partial class SCLP_LAForm : Form
    {
        chartForm lagrangeChartForm = new chartForm();
        public SCLP_LAForm()
        {
            InitializeComponent();
            textBox3.Text = Parameter.n.ToString();
            label2.Text = "";
            label4.Text = ""; 
        }
         private void button1_Click(object sender, EventArgs e)
        {
            Parameter.coverdistance = double.Parse(textBox4.Text);
            Lagrange Lag = new Lagrange();
            Lag.findresult();
            //显示结果
            int sum=0;
            for (int i = 0; i < Parameter.n; i++) 
            {
                sum += Lag.xj1[i];
            }
            label2.Text = sum.ToString();
            label4.Text = Lag.totalcost.ToString();
            Parameter.p = sum;
            Parameter.opti = Lag.totalcost;
            Parameter.x_trans();
        }
    }
}
