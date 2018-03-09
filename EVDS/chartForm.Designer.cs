namespace EVDS
{
    partial class chartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Syncfusion.Windows.Forms.Chart.ChartSeries chartSeries1 = new Syncfusion.Windows.Forms.Chart.ChartSeries();
            Syncfusion.Windows.Forms.Chart.ChartSeries chartSeries2 = new Syncfusion.Windows.Forms.Chart.ChartSeries();
            Syncfusion.Windows.Forms.Chart.ChartSeries chartSeries3 = new Syncfusion.Windows.Forms.Chart.ChartSeries();
            this.chartControl1 = new Syncfusion.Windows.Forms.Chart.ChartControl();
            this.SuspendLayout();
            // 
            // chartControl1
            // 
            this.chartControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chartControl1.BackInterior = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Horizontal, new System.Drawing.Color[] {
            System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(229))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(238)))), ((int)(((byte)(230))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(238)))), ((int)(((byte)(230))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(229)))))});
            this.chartControl1.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.Transparent);
            this.chartControl1.ChartInterior = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.Transparent);
            this.chartControl1.CustomPalette = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(43)))), ((int)(((byte)(144))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(78)))), ((int)(((byte)(101))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(95)))), ((int)(((byte)(70))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(110)))), ((int)(((byte)(125))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(178)))), ((int)(((byte)(103))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(94)))), ((int)(((byte)(123)))))};
            this.chartControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(125)))), ((int)(((byte)(59)))));
            // 
            // 
            // 
            this.chartControl1.Legend.Location = new System.Drawing.Point(289, 75);
            this.chartControl1.Location = new System.Drawing.Point(0, 0);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.Palette = Syncfusion.Windows.Forms.Chart.ChartColorPalette.Custom;
            this.chartControl1.PrimaryXAxis.GridLineType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(158)))), ((int)(((byte)(205)))));
            this.chartControl1.PrimaryXAxis.LineType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(158)))), ((int)(((byte)(205)))));
            this.chartControl1.PrimaryXAxis.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(158)))), ((int)(((byte)(205)))));
            this.chartControl1.PrimaryXAxis.TitleColor = System.Drawing.SystemColors.ControlText;
            this.chartControl1.PrimaryYAxis.ForceZero = true;
            this.chartControl1.PrimaryYAxis.GridLineType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(158)))), ((int)(((byte)(205)))));
            this.chartControl1.PrimaryYAxis.LineType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(158)))), ((int)(((byte)(205)))));
            this.chartControl1.PrimaryYAxis.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(158)))), ((int)(((byte)(205)))));
            this.chartControl1.PrimaryYAxis.TitleColor = System.Drawing.SystemColors.ControlText;
            chartSeries1.Name = "Default0";
            chartSeries1.Style.Border.Width = 2F;
            chartSeries1.Style.DisplayShadow = true;
            chartSeries1.Text = "Default0";
            chartSeries1.Type = Syncfusion.Windows.Forms.Chart.ChartSeriesType.Spline;
            chartSeries2.Name = "Default1";
            chartSeries2.Style.Border.Width = 2F;
            chartSeries2.Style.DisplayShadow = true;
            chartSeries2.Text = "Default1";
            chartSeries2.Type = Syncfusion.Windows.Forms.Chart.ChartSeriesType.Spline;
            chartSeries3.Name = "Default2";
            chartSeries3.Style.Border.Width = 2F;
            chartSeries3.Style.DisplayShadow = true;
            chartSeries3.Text = "Default2";
            chartSeries3.Type = Syncfusion.Windows.Forms.Chart.ChartSeriesType.Spline;
            this.chartControl1.Series.Add(chartSeries1);
            this.chartControl1.Series.Add(chartSeries2);
            this.chartControl1.Series.Add(chartSeries3);
            this.chartControl1.Size = new System.Drawing.Size(400, 300);
            this.chartControl1.TabIndex = 0;
            this.chartControl1.Text = "chartControl1";
            // 
            // 
            // 
            this.chartControl1.Title.Name = "Default";
            this.chartControl1.Titles.Add(this.chartControl1.Title);
            // 
            // chartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 262);
            this.Controls.Add(this.chartControl1);
            this.Name = "chartForm";
            this.Text = "chartForm";
            this.Load += new System.EventHandler(this.chartForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public Syncfusion.Windows.Forms.Chart.ChartControl chartControl1;

    }
}