using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Syncfusion.Windows.Forms;
using Syncfusion.Drawing;
using Syncfusion.Windows.Forms.Chart;
using ChartImportData;

using MCLP;
using MCLP_lagrange;

namespace EVDS
{
    public partial class MCLP_LAForm : Form
    {
        chartForm lagrangeChartForm = new chartForm();
        public MCLP_LAForm()
        {
            InitializeComponent();
            textBox1.Text = Parameter.m.ToString();
            textBox3.Text = Parameter.n.ToString();
            label2.Text = "";
            label4.Text = "";
            label10.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Parameter.p = int.Parse(textBox2.Text);
            Parameter.coverdistance = double.Parse(textBox4.Text);
            Lagrange Lag = new Lagrange();
            Lag.findresult();
            //记录结果
            Parameter.opti = Lag.lowbound;
            Parameter.x = Lag.xj1;
            Parameter.y = Lag.yij1;
            Parameter.x_trans();
            //显示结果
            label2.Text = Lag.results;
            label4.Text = Lag.lowbound.ToString();
            //图象分析            
            lagrangeChartForm.chartControl1.Series.Clear();
            if (checkBox1.Checked)
            {
                ChartSeries upboundSeries = new ChartSeries("upbound");
                ChartSeries lowboundSeries = new ChartSeries("lowbound");
                upboundSeries.SeriesIndexedModelImpl = new StringIndexedModel(upboundSeries, (double[])Lag.upbounds.ToArray(typeof(double)));
                lowboundSeries.SeriesIndexedModelImpl = new StringIndexedModel(lowboundSeries, (double[])Lag.lowbounds.ToArray(typeof(double)));
                lagrangeChartForm.chartControl1.Series.Add(upboundSeries);
                lagrangeChartForm.chartControl1.Series.Add(lowboundSeries);
            }
            if (checkBox2.Checked)
            {
                ChartSeries zukSeries = new ChartSeries("zuk");
                ChartSeries zlkSeries = new ChartSeries("zlk");
                zukSeries.SeriesIndexedModelImpl = new StringIndexedModel(zukSeries, (double[])Lag.zuks.ToArray(typeof(double)));
                zlkSeries.SeriesIndexedModelImpl = new StringIndexedModel(zlkSeries, (double[])Lag.zlks.ToArray(typeof(double)));
                lagrangeChartForm.chartControl1.Series.Add(zukSeries);
                lagrangeChartForm.chartControl1.Series.Add(zlkSeries);
            }
            for (int i = 0; i < lagrangeChartForm.chartControl1.Series.Count; i++)
            {
                lagrangeChartForm.chartControl1.Series[i].Type = ChartSeriesType.Line;
            }
            int flag = 0;
            for (int i = 0; i < Parameter.n; i++)
            {
                if (Lag.xj1[i] == 1)
                {
                    Parameter.x[flag] = i;
                    flag++;
                }
            }
            for (int i = 0; i < Parameter.n; i++) label10.Text = label10.Text+Parameter.x[i].ToString()+" ";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            lagrangeChartForm.Show();
        }
    }
}
