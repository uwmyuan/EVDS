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
    public partial class relocationForm : Form
    {
        public relocationForm()
        {
            InitializeComponent();
            label7.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
            {
                relocation.z = 0;
                relocation.M = int.Parse(textBox1.Text);
                relocation.A = int.Parse(textBox2.Text);
                relocation.a = int.Parse(textBox3.Text);
                relocation.b = int.Parse(textBox4.Text);
                relocation.R = int.Parse(textBox5.Text);
                relocation.Pmin = double.Parse(textBox6.Text);
                relocation Rel = new relocation();
                Rel.tree_sourch();
                label7.Text = relocation.z.ToString();
                Parameter.x = relocation.x1;
                Parameter.z = relocation.z;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
            //{
                relocation.z = 0;
                //relocation.M = int.Parse(textBox1.Text);
                //relocation.A = int.Parse(textBox2.Text);
                //relocation.a = int.Parse(textBox3.Text);
                //relocation.b = int.Parse(textBox4.Text);
                //relocation.R = int.Parse(textBox5.Text);
                //relocation.Pmin = double.Parse(textBox6.Text);
                List<double> result_z=new List<double>();
                result_z.Clear();
                for (int i = 0; i < 1000; i++)
                {
                    relocation Rel = new relocation();
                    Rel.tree_sourch();
                    //label7.Text = relocation.z.ToString();
                    //Parameter.x = relocation.x1;
                    result_z.Add(relocation.z);
                    relocation.Pmin += 0.000005;
                }                
            //}
        }

    }
}
