using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms;
using Syncfusion.Drawing;

using Syncfusion.XlsIO;

using p_median;

namespace EVDS
{
    public partial class EVDS : RibbonForm
    {
        datagridForm weighted_distanceDatagridForm = new datagridForm();
        datagridForm demandDatagridForm = new datagridForm();        
        public EVDS()
        {
            InitializeComponent();
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine);
            toolStripComboBox2.Text = (string)toolStripComboBox2.Items[0];
            toolStripComboBox3.Text = (string)toolStripComboBox3.Items[0];
            toolStripComboBox4.Text = (string)toolStripComboBox4.Items[0];
            toolStripComboBox5.Text = (string)toolStripComboBox5.Items[0];
            weighted_distanceDatagridForm.Text = "weighted_distance";
            demandDatagridForm.Text = "demand";
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {    
                if (this.WindowState != FormWindowState.Maximized)
                    this.statusStripEx1.SizingGrip = true;
                else
                    this.statusStripEx1.SizingGrip = false;            
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            new LagrangeForm().Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            new GAForm().Show();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                //Imports Data
                //Step 1 : Instantiate the spreadsheet creation engine.
                ExcelEngine excelEngine = new ExcelEngine();
                //Step 2 : Instantiate the excel application object.
                IApplication application = excelEngine.Excel;
                //Open an existing xls
                IWorkbook workbook = application.Workbooks.Open(openFileDialog1.FileName);
                IWorksheet sheet1 = workbook.Worksheets[0];
                IWorksheet sheet2 = workbook.Worksheets[1];
                //Read data
                DataTable customersTable1 = sheet1.ExportDataTable(sheet1.UsedRange, ExcelExportDataTableOptions.ColumnNames);
                DataTable customersTable2 = sheet2.ExportDataTable(sheet2.UsedRange, ExcelExportDataTableOptions.ColumnNames);
                weighted_distanceDatagridForm.dataGridView1.DataSource = customersTable1;
                demandDatagridForm.dataGridView1.DataSource = customersTable2;
                //Show Datagrid
                weighted_distanceDatagridForm.Show();
                demandDatagridForm.Show();
                //Close the workbook.
                workbook.Close();
                //No exception will be thrown if there are unsaved workbooks.
                excelEngine.ThrowNotSavedOnDestroy = false;
                excelEngine.Dispose();
                if (toolStripComboBox2.Text == (string)toolStripComboBox2.Items[0])
                {
                    Parameter.m = demandDatagridForm.dataGridView1.RowCount - 1;
                    Parameter.n = Parameter.m;
                    for (int i = 0; i < demandDatagridForm.dataGridView1.RowCount - 1; i++)
                    {
                        Parameter.demand[i] = double.Parse(demandDatagridForm.dataGridView1[1, i].Value.ToString());
                        for (int j = 0; j < weighted_distanceDatagridForm.dataGridView1.RowCount - 1; j++)
                        {
                            Parameter.weighted_distance[int.Parse(weighted_distanceDatagridForm.dataGridView1[0, j].Value.ToString()) - 1, int.Parse(weighted_distanceDatagridForm.dataGridView1[1, j].Value.ToString()) - 1] = double.Parse(weighted_distanceDatagridForm.dataGridView1[2, j].Value.ToString());
                            Parameter.weighted_distance[int.Parse(weighted_distanceDatagridForm.dataGridView1[1, j].Value.ToString()) - 1, int.Parse(weighted_distanceDatagridForm.dataGridView1[0, j].Value.ToString()) - 1] = double.Parse(weighted_distanceDatagridForm.dataGridView1[2, j].Value.ToString());
                        }
                    }
                    for (int i = 0; i < Parameter.n; i++)
                    {
                        for (int j = 0; j < Parameter.m; j++)
                        {
                            Parameter.weighted_distance[i, j] = Parameter.weighted_distance[i, j] * Parameter.demand[i];
                        }
                    }
                }
                else if (toolStripComboBox2.Text == (string)toolStripComboBox2.Items[1])
                {
                    Parameter.m = weighted_distanceDatagridForm.dataGridView1.RowCount - 1;
                    Parameter.n = weighted_distanceDatagridForm.dataGridView1.ColumnCount - 1;
                }
                weighted_distanceDatagridForm.Show();
                demandDatagridForm.Show();
            }        
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            new tree_searchForm().Show();
        }
    }
}
