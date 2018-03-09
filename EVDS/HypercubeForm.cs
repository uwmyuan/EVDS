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
    public partial class HypercubeForm : Form
    {
        public HypercubeForm()
        {
            InitializeComponent();
            dataGridView1.Rows.Add(10);
            dataGridView2.Rows.Add(10);
            for(int i=0;i<3;i++)
                for(int j=0;j<10;j++)
                {
                    
                    dataGridView1[i,j].Value=Hyp.ma[j,i];
                }
            for (int k = 0; k < 10; k++)
                dataGridView2[0,k].Value = Hyp.lamodan[k];
        }
        Hypercube.Hypercube Hyp = new Hypercube.Hypercube();
        private void button1_Click(object sender, EventArgs e)
        {

            Hyp.Hypercube_approximate();
            //记录结果
            Parameter.pnu = Hyp.pnu;
            //显示结果
            textBox2.Text = Hyp.pnu[0].ToString();
            textBox3.Text = Hyp.pnu[1].ToString();
            textBox4.Text = Hyp.pnu[2].ToString();
        }
    }
}
