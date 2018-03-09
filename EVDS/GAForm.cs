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

using p_median;
using p_median_genetic_algorithm;

namespace EVDS
{
    public partial class GAForm : Form
    {
        chartForm GAChartForm = new chartForm();
        public GAForm()
        {
            InitializeComponent();
            label10.Text = "";
            comboBox1.Text = (string)comboBox1.Items[0];
            comboBox2.Text = (string)comboBox2.Items[0];
            textBox1.Text = Parameter.m.ToString();
            textBox3.Text = Parameter.n.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //初始化
            int p = int.Parse(this.textBox2.Text);
            int Generations = int.Parse(this.textBox4.Text);//迭代次数
            if (p <= Parameter.m)
            {
                Parameter.p = p;
                GA GAAlgo = new GA();
                System.Collections.ArrayList bestFitness = new System.Collections.ArrayList();
                GAAlgo.MutationRate = double.Parse(this.textBox5.Text);
                GAAlgo.CrossRate = double.Parse(this.textBox6.Text);
                if (comboBox1.Text == (string)comboBox1.Items[1])
                {
                    GAAlgo.Selection = GA.SelectionType.Tournment;
                }
                if (comboBox2.Text == (string)comboBox2.Items[1])
                {
                    GAAlgo.Crosstype = 2;
                }
                //寻优
                GAAlgo.Initialize();//产生第一代群体
                progressBar1.Value = 0;
                GAChromosome GAChr = new GAChromosome();
                bestFitness.Clear();
                while (GAAlgo.GenerationNum < Generations)//产生下一代群体
                {
                    GAAlgo.CreateNextGeneration();
                    GAChr = GAAlgo.GetBestChromosome();
                    bestFitness.Add(GAChr.Fitness);
                }
                progressBar1.Value = 100;
                //显示结果
                label10.Text = (100 / GAChr.Fitness).ToString();
                //图像分析
                GAChartForm.chartControl1.Series.Clear();
                if (checkBox1.Checked)
                {
                    ChartSeries fitnessSeries = new ChartSeries("bestFitness");
                    fitnessSeries.SeriesIndexedModelImpl = new StringIndexedModel(fitnessSeries, (double[])bestFitness.ToArray(typeof(double)));
                    GAChartForm.chartControl1.Series.Add(fitnessSeries);
                }
                if (checkBox2.Checked)
                {
                    ChartSeries total_fitnessSeries = new ChartSeries("totalFitness");
                    total_fitnessSeries.SeriesIndexedModelImpl = new StringIndexedModel(total_fitnessSeries, (double[])GAAlgo.TotalFitness.ToArray(typeof(double)));
                    GAChartForm.chartControl1.Series.Add(total_fitnessSeries);
                }
                for (int i = 0; i < GAChartForm.chartControl1.Series.Count; i++)
                {
                    GAChartForm.chartControl1.Series[i].Type = ChartSeriesType.Line;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (GAChartForm.chartControl1.Series != null)
                GAChartForm.Show();
        }
    }
}
