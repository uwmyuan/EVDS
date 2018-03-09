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
    public partial class traveltimeForm : Form
    {
        public traveltimeForm()
        {
            InitializeComponent();
            dataGridView1.Rows.Add(10);
            dataGridView2.Rows.Add(10);
            dataGridView6.Rows.Add(10);
            dataGridView7.Rows.Add(10);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 10; j++)
                {

                    dataGridView1[i, j].Value = tra.ma[j, i];
                    dataGridView7[i,j].Value=tra.pnj[i,j];
                }
            for (int k = 0; k < 10; k++)
                dataGridView2[0, k].Value = tra.lamodan[k];
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    dataGridView6[i, j].Value = tra.tij[i, j];

        }
        traveltime tra = new traveltime();

        private void button1_Click(object sender, EventArgs e)
        {
            tra.traveltime_calculate();
            //记录结果
            Parameter.tj = tra.tj;
            Parameter.tran = tra.tran;
            Parameter.tun = tra.tun;
            Parameter.tnj = tra.tnj;
            Parameter.tq = tra.tq;
            Parameter._t = tra._t;
            //显示结果
            dataGridView3.Rows.Add(10);
            for (int i = 0; i < tra.na; i++)
                dataGridView3[0, i].Value = tra.tj[i];
            dataGridView4.Rows.Add(1);
            for (int i = 0; i < tra.n; i++)
                dataGridView4[i, 0].Value = tra.tran[i];
            dataGridView5.Rows.Add(1);
            for (int i = 0; i < tra.n; i++)
                dataGridView5[i, 0].Value = tra.tun[i];
        }

    }
}
