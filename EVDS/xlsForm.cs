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
using dispatch;
using r_interdiction;
using MCLP;
using SCLP;
using Hypercube;
namespace EVDS
{
    public partial class xlsForm : Form
    {
        public xlsForm()
        {
            InitializeComponent();
        }

        private void xlsButton_Click(object sender, EventArgs e)
        {
            string[,] excleposition = new string[17, 12] {
                                        {"A1", "B1", "C1", "D1", "E1", "F1", "G1","H1","I1","J1","K1","L1"},
                                        {"A2", "B2", "C2", "D2", "E2", "F2", "G2","H2","I2","J2","K2","L2"},
                                        {"A3", "B3", "C3", "D3", "E3", "F3", "G3","H3","I3","J3","K3","L3"},
                                        {"A4", "B4", "C4", "D4", "E4", "F4", "G4","H4","I4","J4","K4","L4"},
                                        {"A5", "B5", "C5", "D5", "E5", "F5", "G5","H5","I5","J5","K5","L5"},
                                        {"A6", "B6", "C6", "D6", "E6", "F6", "G6","H6","I6","J6","K6","L6"},
                                        {"A7", "B7", "C7", "D7", "E7", "F7", "G7","H7","I7","J7","K7","L7"},
                                        {"A8", "B8", "C8", "D8", "E8", "F8", "G8","H8","I8","J8","K8","L8"},
                                        {"A9", "B9", "C9", "D9", "E9", "F9", "G9","H9","I9","J9","K9","L9"},
                                        {"A10", "B10", "C10", "D10", "E10", "F10", "G10","H10","I10","J10","K10","L10"},
                                        {"A11", "B11", "C11", "D11", "E11", "F11", "G11","H11","I11","J11","K11","L11"},
                                        {"A12", "B12", "C12", "D12", "E12", "F12", "G12","H12","I12","J12","K12","L12"},
                                        {"A13", "B13", "C13", "D13", "E13", "F13", "G13","H13","I13","J13","K13","L13"},
                                        {"A14", "B14", "C14", "D14", "E14", "F14", "G14","H14","I14","J14","K14","L14"},
                                        {"A15", "B15", "C15", "D15", "E15", "F15", "G15","H15","I15","J15","K15","L15"},
                                        {"A16", "B16", "C16", "D16", "E16", "F16", "G16","H16","I16","J16","K16","L16"},
                                        {"A17", "B17", "C17", "D17", "E17", "F17", "G17","H17","I17","J17","K17","L17"}};          
                    //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
                    //The instantiation process consists of two steps.

                    //Step 1 : Instantiate the spreadsheet creation engine.
                    ExcelEngine excelEngine = new ExcelEngine();
                    //Step 2 : Instantiate the excel application object.
                    IApplication application = excelEngine.Excel;

                    //A new workbook is created.[Equivalent to creating a new workbook in MS Excel]
                    //The new workbook will have 5 worksheets
                    IWorkbook workbook = application.Workbooks.Create(1);

                    //The first worksheet object in the worksheets collection is accessed.
                    IWorksheet sheet = workbook.Worksheets[0];

                    //key part undone
                    //Inserting sample text into the first cell of the first worksheet.
                    switch (ResultSelectComboBox.SelectedIndex)
                    {
                        case 0: //p-median
                        { 
                            sheet.Range["A1"].Text="目标函数值";
                            sheet.Range["A2"].Text=p_median.Parameter.opti.ToString();
                            sheet.Range["A3"].Text="选址方案";
                            for(int i=0;i<p_median.Parameter.p;i++)
                            sheet.Range[excleposition[3,i]].Text=p_median.Parameter.x[i].ToString();
                            sheet.Range["A5"].Text="分配方案";
                            for(int i=5;i<(p_median.Parameter.n+5);i++)
                            for(int j=0;j<p_median.Parameter.m;j++)
                            sheet.Range[excleposition[i,j]].Text=p_median.Parameter.y[i-5,j].ToString();
                            break;
                        }
                        case 1://MCLP
                            {
                                sheet.Range["A1"].Text="目标函数值";
                                sheet.Range["A2"].Text=MCLP.Parameter.opti.ToString();
                                sheet.Range["A3"].Text="选址方案";
                                for (int i = 3; i < MCLP.Parameter.n+3; i++)
                                    for (int j = 0; j < MCLP.Parameter.m;j++ )
                                        sheet.Range[excleposition[i,j]].Text = MCLP.Parameter.y[i-3,j].ToString();
                                break;
                            }
                        case 2://SCLP
                            {
                                sheet.Range["A1"].Text="目标函数值";
                                sheet.Range["A2"].Text=SCLP.Parameter.opti.ToString();
                                sheet.Range["A3"].Text="选址方案";
                                for(int i=0;i<SCLP.Parameter.m;i++)
                                sheet.Range[excleposition[3,i]].Text=SCLP.Parameter.x.ToString();
                                sheet.Range["A5"].Text="选址个数";
                                sheet.Range["A6"].Text=SCLP.Parameter.p.ToString();
                                break;
                            }
                        case 3://relocation
                            {
                                sheet.Range["A1"].Text="目标函数值";
                                sheet.Range["A2"].Text=dispatch.Parameter.z.ToString();
                                sheet.Range["A3"].Text="最优解";
                                for(int j=3;j<dispatch.Parameter.n+3;j++)
                                    for(int i=0;i<dispatch.Parameter.p;i++)
                                    sheet.Range[excleposition[j,i]].Text=dispatch.Parameter.x[j-3,i].ToString();
                                break;
                            }
                        case 4://r-interdiction
                            {
                                sheet.Range["A1"].Text="目标函数值";
                                sheet.Range["A2"].Text=r_interdiction.Parameter.wfi.ToString();
                                sheet.Range["A3"].Text="中断点";
                                for(int i=0;i<r_interdiction.Parameter.p;i++)
                                    sheet.Range[excleposition[3,i]].Text=r_interdiction.Parameter.sgood[i].ToString();
                                break;
                            }
                        case 5://busyfraction
                            {
                                sheet.Range["A1"].Text="各服务设施繁忙度";
                                for(int i=0;i<3;i++)
                                    sheet.Range[excleposition[1,i]].Text=Hypercube.Parameter.pnu[i].ToString();
                                break;
                            }

                        case 6://traveltime
                            {
                                sheet.Range["A1"].Text="服务单位n到需求单位j的时间";
                                for(int i=1;i<=3;i++)
                                    for(int j=0;j<10;j++)
                                    sheet.Range[excleposition[i,j]].Text=Hypercube.Parameter.tnj[i-1,j].ToString();
                                sheet.Range["A5"].Text="有派队需求的平均出行时间";
                                sheet.Range["A6"].Text=Hypercube.Parameter.tq.ToString();
                                sheet.Range["A7"].Text="全区域无限制平均出行时间";
                                sheet.Range["A8"].Text=Hypercube.Parameter._t.ToString();
                                sheet.Range["A9"].Text="服务单元n的平均出行时间";
                                for(int i=0;i<3;i++)
                                    sheet.Range[excleposition[9,i]].Text=Hypercube.Parameter.tun[i].ToString();
                                sheet.Range["A11"].Text="到需求单元j的平均出行时间";
                                for(int i=0;i<10;i++)
                                    sheet.Range[excleposition[11,i]].Text=Hypercube.Parameter.tj[i].ToString();
                                sheet.Range["A13"].Text="对响应区域n需求的平均出行时间";
                                for(int i=0;i<3;i++)
                                    sheet.Range[excleposition[13,i]].Text=Hypercube.Parameter.tran[i].ToString();
                                break;
                            }

                        case 7://dispatchfraction
                            {
                                sheet.Range["A1"].Text="服务设施n分配给需求单位j占所有服务的比例";
                                for(int i=1;i<4;i++)
                                    for(int j=0;j<10;j++)
                                    sheet.Range[excleposition[i,j]].Text=Hypercube.Parameter.fnj[i-1,j].ToString();
                                sheet.Range["A5"].Text="所有不负责本身区域的频率";
                                sheet.Range["A6"].Text=Hypercube.Parameter.fi.ToString();
                                sheet.Range["A7"].Text="服务设施n不负责本身响应区域的频率";
                                for(int i=0;i<3;i++)
                                    sheet.Range[excleposition[7,i]].Text=Hypercube.Parameter.fin[i].ToString();
                                sheet.Range["A9"].Text="服务设施n由其他服务设施代替服务的频率";
                                for(int i=0;i<3;i++)
                                    sheet.Range[excleposition[9,i]].Text=Hypercube.Parameter.fii[i].ToString();
                                break;
                            }
                    }
                    string fileName = "";
                    fileName = textBox1.Text;
                    switch(comboBox1.SelectedIndex)
                    {
                        case 0:
                        {
                            fileName = fileName+".xls";
                            workbook.SaveAs(fileName);
                            break;
                        }
                        case 1:
                        {
                            fileName = fileName+".xlsx";
                            workbook.Version = ExcelVersion.Excel2007;
                            workbook.SaveAs(fileName);
                            break;
                        }
                        case 2:
                        {
                            fileName = fileName+".csv";
                            sheet.SaveAs(fileName, ",");
                            break;
                        }
                        case 3:
                        {
                            fileName = fileName+".xml";
                            workbook.SaveAsXml(fileName, ExcelXmlSaveType.MSExcel);
                            break;
                        }
                        default:
                        {
                            fileName = fileName + ".xls";
                            workbook.SaveAs(fileName);
                            break;
                        }
                    }
                    //Close the workbook.
                    workbook.Close();
                    excelEngine.Dispose();

                    //Message box confirmation to view the created document.
                    if (MessageBox.Show("Do you want to view the workbook?", "Workbook has been created",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    == DialogResult.Yes)
                    {
                        try
                        {
                            //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                            System.Diagnostics.Process.Start(fileName);

                            //Exit
                            this.Close();
                        }
                        catch (Win32Exception ex)
                        {
                            MessageBox.Show("Excel 2007 is not installed in this system");
                            Console.WriteLine(ex.ToString());
                        }
                    }
                    else
                        this.Close();
            }
    }
}
