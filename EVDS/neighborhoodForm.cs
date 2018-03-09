using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using p_median;
using p_median_neighborhood;

namespace EVDS
{
    public partial class neighborhoodForm : Form
    {
        public neighborhoodForm()
        {
            InitializeComponent();
            label2.Text = "";
            label4.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                Parameter.p = int.Parse(textBox1.Text);
                neighborhood Nei = new neighborhood();
                Nei.neighborhood_search();
                for (int i = 0; i < Parameter.p; i++)
                   label2.Text = label2.Text + Nei.xj[i]+",";
                label4.Text = label4.Text + Nei.zui[Parameter.p-1];
                Parameter.x = Nei.xj;
                Parameter.opti = Nei.zui[Parameter.p - 1];
            }
            
        }
    }
}
