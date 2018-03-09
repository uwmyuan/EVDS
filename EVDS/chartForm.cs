using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Syncfusion.Windows.Forms;

namespace EVDS
{
    public partial class chartForm : Office2007Form
    {
        public chartForm()
        {
            InitializeComponent();
            this.Hide();
        }

        private void chartForm_Load(object sender, EventArgs e)
        {
            this.chartControl1.Indexed = true;
            this.chartControl1.Titles[0].Text = "结果分析";
        }
    }
}
