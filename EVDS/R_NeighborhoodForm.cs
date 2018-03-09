﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using r_interdiction;
namespace EVDS
{
    public partial class R_NeighborhoodForm : Form
    {
        public R_NeighborhoodForm()
        {
            InitializeComponent();
            textBox1.Text = Parameter.p.ToString();
            textBox2.Text = Parameter.r.ToString();
            textBox3.Text = Parameter.n.ToString();
            textBox4.Text = Parameter.m.ToString();
            textBox5.Text = Parameter.xj.ToString();
        }
        r_interdiction.r_interdiction ri = new r_interdiction.r_interdiction();
        private void button1_Click(object sender, EventArgs e)
        {
            ri.method = 2;
            ri.greedy_search();
            textBox6.Text = ri.sgood.ToString();
            textBox7.Text = ri.wfi.ToString();
            Parameter.sgood = ri.sgood;
            Parameter.wfi = ri.wfi;
            
        }
    }
}
