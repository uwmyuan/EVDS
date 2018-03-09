using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using dispatch;

namespace EVDS
{
    public partial class tree_searchForm : Form
    {
        public tree_searchForm()
        {
            InitializeComponent();
        }
        relocation Relocation = new relocation();
        private void button1_Click_1(object sender, EventArgs e)
        {
            Relocation.tree_sourch();
            textBox1.Text = relocation.z.ToString();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            relocation.stopflag = true;
        }
    }
}
