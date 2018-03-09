namespace EVDS
{
    partial class exportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(exportForm));
            this.btnSolve = new System.Windows.Forms.Button();
            this.ckbShowLines = new System.Windows.Forms.CheckBox();
            this.ckbUseRestriction = new System.Windows.Forms.CheckBox();
            this.cbCostAttribute = new System.Windows.Forms.ComboBox();
            this.lbOutput = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCutOff = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLoadMap = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFeatureDataset = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtInputFacilities = new System.Windows.Forms.TextBox();
            this.txtNetworkDataset = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWorkspacePath = new System.Windows.Forms.TextBox();
            this.gbServiceAreaSolver = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LoadButton = new System.Windows.Forms.Button();
            this.ResultSelectComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.gbServiceAreaSolver.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSolve
            // 
            this.btnSolve.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSolve.Location = new System.Drawing.Point(66, 302);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(295, 34);
            this.btnSolve.TabIndex = 1;
            this.btnSolve.Text = "显示到GIS地图";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // ckbShowLines
            // 
            this.ckbShowLines.AutoSize = true;
            this.ckbShowLines.Location = new System.Drawing.Point(9, 97);
            this.ckbShowLines.Name = "ckbShowLines";
            this.ckbShowLines.Size = new System.Drawing.Size(84, 16);
            this.ckbShowLines.TabIndex = 2;
            this.ckbShowLines.Text = "Show Lines";
            this.ckbShowLines.UseVisualStyleBackColor = true;
            // 
            // ckbUseRestriction
            // 
            this.ckbUseRestriction.AutoSize = true;
            this.ckbUseRestriction.Location = new System.Drawing.Point(9, 75);
            this.ckbUseRestriction.Name = "ckbUseRestriction";
            this.ckbUseRestriction.Size = new System.Drawing.Size(114, 16);
            this.ckbUseRestriction.TabIndex = 3;
            this.ckbUseRestriction.Text = "Use Restriction";
            this.ckbUseRestriction.UseVisualStyleBackColor = true;
            // 
            // cbCostAttribute
            // 
            this.cbCostAttribute.FormattingEnabled = true;
            this.cbCostAttribute.Location = new System.Drawing.Point(164, 28);
            this.cbCostAttribute.Name = "cbCostAttribute";
            this.cbCostAttribute.Size = new System.Drawing.Size(84, 20);
            this.cbCostAttribute.TabIndex = 4;
            // 
            // lbOutput
            // 
            this.lbOutput.FormattingEnabled = true;
            this.lbOutput.ItemHeight = 12;
            this.lbOutput.Location = new System.Drawing.Point(9, 119);
            this.lbOutput.Name = "lbOutput";
            this.lbOutput.Size = new System.Drawing.Size(240, 40);
            this.lbOutput.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Cost Attribute 阻抗";
            // 
            // txtCutOff
            // 
            this.txtCutOff.Location = new System.Drawing.Point(164, 51);
            this.txtCutOff.Name = "txtCutOff";
            this.txtCutOff.Size = new System.Drawing.Size(84, 21);
            this.txtCutOff.TabIndex = 9;
            this.txtCutOff.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "Cut Off 服务半径";
            // 
            // btnLoadMap
            // 
            this.btnLoadMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadMap.Location = new System.Drawing.Point(320, 20);
            this.btnLoadMap.Name = "btnLoadMap";
            this.btnLoadMap.Size = new System.Drawing.Size(105, 84);
            this.btnLoadMap.TabIndex = 11;
            this.btnLoadMap.Text = "Setup \r\n\r\n设置地理数据库";
            this.btnLoadMap.UseVisualStyleBackColor = true;
            this.btnLoadMap.Click += new System.EventHandler(this.btnLoadMap_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFeatureDataset);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtInputFacilities);
            this.groupBox1.Controls.Add(this.btnLoadMap);
            this.groupBox1.Controls.Add(this.txtNetworkDataset);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtWorkspacePath);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 116);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Map Configuration 地图设置";
            // 
            // txtFeatureDataset
            // 
            this.txtFeatureDataset.Location = new System.Drawing.Point(179, 64);
            this.txtFeatureDataset.Name = "txtFeatureDataset";
            this.txtFeatureDataset.Size = new System.Drawing.Size(135, 21);
            this.txtFeatureDataset.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "Feature Dataset 要素数据集";
            // 
            // txtInputFacilities
            // 
            this.txtInputFacilities.Location = new System.Drawing.Point(179, 88);
            this.txtInputFacilities.Name = "txtInputFacilities";
            this.txtInputFacilities.Size = new System.Drawing.Size(135, 21);
            this.txtInputFacilities.TabIndex = 13;
            // 
            // txtNetworkDataset
            // 
            this.txtNetworkDataset.Location = new System.Drawing.Point(179, 42);
            this.txtNetworkDataset.Name = "txtNetworkDataset";
            this.txtNetworkDataset.Size = new System.Drawing.Size(135, 21);
            this.txtNetworkDataset.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "Input Facilities 选择设施点";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Network Dataset 网络数据集";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "Workspace Path 工作空间路径";
            // 
            // txtWorkspacePath
            // 
            this.txtWorkspacePath.Location = new System.Drawing.Point(179, 18);
            this.txtWorkspacePath.Name = "txtWorkspacePath";
            this.txtWorkspacePath.Size = new System.Drawing.Size(135, 21);
            this.txtWorkspacePath.TabIndex = 8;
            // 
            // gbServiceAreaSolver
            // 
            this.gbServiceAreaSolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbServiceAreaSolver.Controls.Add(this.label3);
            this.gbServiceAreaSolver.Controls.Add(this.label2);
            this.gbServiceAreaSolver.Controls.Add(this.txtCutOff);
            this.gbServiceAreaSolver.Controls.Add(this.ckbShowLines);
            this.gbServiceAreaSolver.Controls.Add(this.ckbUseRestriction);
            this.gbServiceAreaSolver.Controls.Add(this.lbOutput);
            this.gbServiceAreaSolver.Controls.Add(this.cbCostAttribute);
            this.gbServiceAreaSolver.Enabled = false;
            this.gbServiceAreaSolver.Location = new System.Drawing.Point(12, 126);
            this.gbServiceAreaSolver.Name = "gbServiceAreaSolver";
            this.gbServiceAreaSolver.Size = new System.Drawing.Size(273, 170);
            this.gbServiceAreaSolver.TabIndex = 13;
            this.gbServiceAreaSolver.TabStop = false;
            this.gbServiceAreaSolver.Text = "Service Area Solver 输出设置";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LoadButton);
            this.groupBox2.Controls.Add(this.ResultSelectComboBox);
            this.groupBox2.Location = new System.Drawing.Point(292, 133);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(154, 163);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Load Result 结果导入";
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(7, 112);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(120, 40);
            this.LoadButton.TabIndex = 1;
            this.LoadButton.Text = "Load 导入";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // ResultSelectComboBox
            // 
            this.ResultSelectComboBox.FormattingEnabled = true;
            this.ResultSelectComboBox.Items.AddRange(new object[] {
            "p-median",
            "MCLP"});
            this.ResultSelectComboBox.Location = new System.Drawing.Point(6, 64);
            this.ResultSelectComboBox.Name = "ResultSelectComboBox";
            this.ResultSelectComboBox.Size = new System.Drawing.Size(121, 20);
            this.ResultSelectComboBox.TabIndex = 0;
            this.ResultSelectComboBox.SelectedIndexChanged += new System.EventHandler(this.ResultSelectComboBox_SelectedIndexChanged);
            // 
            // exportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 348);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.gbServiceAreaSolver);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "exportForm";
            this.Text = "Export Service Area 输出服务区";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbServiceAreaSolver.ResumeLayout(false);
            this.gbServiceAreaSolver.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.CheckBox ckbShowLines;
        private System.Windows.Forms.CheckBox ckbUseRestriction;
        public System.Windows.Forms.ComboBox cbCostAttribute;
        private System.Windows.Forms.ListBox lbOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCutOff;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoadMap;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNetworkDataset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWorkspacePath;
        private System.Windows.Forms.TextBox txtInputFacilities;
        private System.Windows.Forms.GroupBox gbServiceAreaSolver;
        private System.Windows.Forms.TextBox txtFeatureDataset;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.ComboBox ResultSelectComboBox;
    }
}