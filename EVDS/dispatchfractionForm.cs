using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Hypercube;
namespace EVDS
{
    public partial class dispatchfractionForm : Form
    {
        public dispatchfractionForm()
        {
            InitializeComponent();
            dataGridView1.Rows.Add(10);
            dataGridView2.Rows.Add(10);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 10; j++)
                {

                    dataGridView1[i, j].Value = df.ma[j, i];
                }
            for (int k = 0; k < 10; k++)
                dataGridView2[0, k].Value = df.lamodan[k];
        }
        dispatch_fraction df = new dispatch_fraction();

        private void button1_Click(object sender, EventArgs e)
        {
            df.dispatchfraction();
            textBox2.Text = df.fi.ToString();
            //显示结果
            dataGridView3.Rows.Add(3);
            dataGridView4.Rows.Add(3);
            for (int i = 0; i < df.n; i++)
            {
                dataGridView3[0, i].Value = df.fin[i];
                dataGridView4[0, i].Value = df.fii[i];
            }
            //记录结果
            Parameter.fnj = df.fnj;
            Parameter.fi = df.fi;
            Parameter.fin = df.fin;
            Parameter.fii = df.fii;
        }
    }
}
