using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.NetworkAnalyst;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms;

namespace EVDS
{
    public partial class exportForm : Form
    {
        private INAContext m_naContext;
        private EVDS mainForm; 
		#region Main Form Constructor and Setup
        int[] xx;
        public exportForm(RibbonForm Form)
		{
			InitializeComponent();
            mainForm = Form as EVDS;
			txtCutOff.Text = "5";
			lbOutput.Items.Clear();
			cbCostAttribute.Items.Clear();
			ckbShowLines.Checked = false;
			ckbUseRestriction.Checked = false;
			mainForm.axMapControl1.ClearLayers();
            //txtWorkspacePath.Text = Application.StartupPath + @"\..\Instance\Philadelphia.gdb";
            txtWorkspacePath.Text = @"C:\Users\Yuanyun\Desktop\Instance\Philadelphia.gdb";
            txtNetworkDataset.Text = "Highway_ND";
			txtFeatureDataset.Text = "Transportation";
			txtInputFacilities.Text = "EV";
			gbServiceAreaSolver.Enabled = false;
		}

		#endregion

		#region Button Clicks

		private void btnSolve_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			lbOutput.Items.Clear();

			ConfigureSolverSettings();

			try
			{
				IGPMessages gpMessages = new GPMessagesClass();
				if (m_naContext.Solver.Solve(m_naContext, gpMessages, null))
					LoadListboxAfterPartialSolve(gpMessages);
				else
					LoadListboxAfterSuccessfulSolve();
			}
			catch (Exception ex)
			{
				lbOutput.Items.Add("Solve Failed: " + ex.Message);
			}

			UpdateMapDisplayAfterSolve();

			this.Cursor = Cursors.Default;
		}

		private void btnLoadMap_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			gbServiceAreaSolver.Enabled = false;
			lbOutput.Items.Clear();


			IWorkspace workspace = OpenWorkspace(txtWorkspacePath.Text);
			if (workspace == null)
			{
				this.Cursor = Cursors.Default;
				return;
			}

			INetworkDataset networkDataset = OpenNetworkDataset(workspace, txtFeatureDataset.Text, txtNetworkDataset.Text);
			IFeatureWorkspace featureWorkspace = workspace as IFeatureWorkspace;

			CreateContextAndSolver(networkDataset);

			if (m_naContext == null)
			{
				this.Cursor = Cursors.Default;
				return;
			}

			LoadCostAttributes(networkDataset);



			if (!LoadLocations(featureWorkspace))
			{
				this.Cursor = Cursors.Default;
				return;
			}
            
			AddNetworkDatasetLayerToMap(networkDataset);
			AddNetworkAnalysisLayerToMap();

			// work around a transparency issue
			IGeoDataset geoDataset = networkDataset as IGeoDataset;
			mainForm.axMapControl1.Extent = mainForm.axMapControl1.FullExtent;
			mainForm.axMapControl1.Extent = geoDataset.Extent;

			if (m_naContext != null) gbServiceAreaSolver.Enabled = true;

			this.Cursor = Cursors.Default;
		}

		#endregion

		#region Set up Context and Solver

		//*********************************************************************************
		// Geodatabase functions
		// ********************************************************************************
		public IWorkspace OpenWorkspace(string strGDBName)
		{
			// As Workspace Factories are Singleton objects, they must be instantiated with the Activator
			var workspaceFactory = System.Activator.CreateInstance(System.Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory")) as ESRI.ArcGIS.Geodatabase.IWorkspaceFactory;

			if (!System.IO.Directory.Exists(txtWorkspacePath.Text))
			{
				MessageBox.Show("The workspace: " + txtWorkspacePath.Text + " does not exist", "Workspace Error");
				return null;
			}

			IWorkspace workspace = null;
			try
			{
				workspace = workspaceFactory.OpenFromFile(txtWorkspacePath.Text, 0);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Opening workspace failed: " + ex.Message, "Workspace Error");
			}

			return workspace;
		}

		public INetworkDataset OpenNetworkDataset(IWorkspace workspace, string featureDatasetName, string strNDSName)
		{
			// Obtain the dataset container from the workspace
			var featureWorkspace = workspace as IFeatureWorkspace;
			ESRI.ArcGIS.Geodatabase.IFeatureDataset featureDataset = featureWorkspace.OpenFeatureDataset(featureDatasetName);
			var featureDatasetExtensionContainer = featureDataset as ESRI.ArcGIS.Geodatabase.IFeatureDatasetExtensionContainer;
			ESRI.ArcGIS.Geodatabase.IFeatureDatasetExtension featureDatasetExtension = featureDatasetExtensionContainer.FindExtension(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTNetworkDataset);
			var datasetContainer3 = featureDatasetExtension as ESRI.ArcGIS.Geodatabase.IDatasetContainer3;

			// Use the container to open the network dataset.
			ESRI.ArcGIS.Geodatabase.IDataset dataset = datasetContainer3.get_DatasetByName(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTNetworkDataset, strNDSName);
			return dataset as ESRI.ArcGIS.Geodatabase.INetworkDataset;
		}

		private void CreateContextAndSolver(INetworkDataset networkDataset)
		{
			if (networkDataset == null) return;

			IDatasetComponent datasetComponent = networkDataset as IDatasetComponent;
			IDENetworkDataset deNetworkDataset = datasetComponent.DataElement as IDENetworkDataset;

			INASolver naSolver = new NAServiceAreaSolverClass();
			m_naContext = naSolver.CreateContext(deNetworkDataset, "ServiceArea");
			INAContextEdit naContextEdit = m_naContext as INAContextEdit;
			naContextEdit.Bind(networkDataset, new GPMessagesClass());
		}

		#endregion

		#region Load Form Controls

        private void LoadSolution(IFeatureWorkspace featureWorkspace,int[] x)
        {
            IFeatureClass featureClass1 = featureWorkspace.OpenFeatureClass("Facility");
            IFeatureClass featureClass2 = featureWorkspace.OpenFeatureClass("EV");
            
            GetFacilityFeature(featureClass1, featureClass2, x);
        }

        private static void GetFacilityFeature(IFeatureClass featureClass1, IFeatureClass featureClass2, int[] oidList)
        {
            foreach (int oid in oidList)
            {
                if (oid == 0) break;

                IFeature pfeature = featureClass1.GetFeature(oid) as IFeature;

                //Create   the   Feature   Buffer 

                IFeatureBuffer featureBuffer = featureClass2.CreateFeatureBuffer();

                //Create   insert   Feature   Cursor   using   buffering   =   true. 

                IFeatureCursor featureCursor = featureClass2.Insert(true);

                object featureOID;

                //With   a   feature   buffer   you   have   the   ability   to   set   the   attribute   for   a   specific   field   to   be 
                //the   same   for   all   features   added   to   the   buffer. 

                //featureBuffer.set_Value(featureBuffer.Fields.FindField("InstalledBy "), "K   Johnston ");

                for (int i = 2; i < featureBuffer.Fields.FieldCount; i++)
                    featureBuffer.set_Value(i, pfeature.get_Value(i));
                featureBuffer.Shape = pfeature.ShapeCopy;
                //int index = featureBuffer.Fields.FindField("ID");

                //featureBuffer.set_Value(index, "4281");
                //Here   you   can   set   the   featurebuffers 's   shape   by   setting   the   featureBuffer.Shape   
                //to   a   geomerty   that   matched   the   featureclasses. 

                //Insert   the   feature   into   the   feature   cursor 
                featureOID = featureCursor.InsertFeature(featureBuffer);

                //Calling   flush   allows   you   to   handle   any   errors   at   a   known   time   rather   then   on   the   cursor   destruction. 
                featureCursor.Flush();

                //Release   the   Cursor 
                System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor); 
            }
        }

		private void LoadCostAttributes(INetworkDataset networkDataset)
		{
			cbCostAttribute.Items.Clear();

			int attrCount = networkDataset.AttributeCount;
			for (int attrIndex = 0; attrIndex < attrCount; attrIndex++)
			{
				INetworkAttribute networkAttribute = networkDataset.get_Attribute(attrIndex);
				if (networkAttribute.UsageType == esriNetworkAttributeUsageType.esriNAUTCost)
					cbCostAttribute.Items.Add(networkAttribute.Name);
			}

			if (cbCostAttribute.Items.Count > 0)
				cbCostAttribute.SelectedIndex = 0;
		}

		private bool LoadLocations(IFeatureWorkspace featureWorkspace)
		{
			IFeatureClass inputFeatureClass = null;
			try
			{
				inputFeatureClass = featureWorkspace.OpenFeatureClass(txtInputFacilities.Text);
			}
			catch (Exception)
			{
				MessageBox.Show("Specified input feature class does not exist");
				return false;
			}

			INamedSet classes = m_naContext.NAClasses;
			INAClass naClass = classes.get_ItemByName("Facilities") as INAClass;

			// delete existing locations, except barriers
			naClass.DeleteAllRows();

			// Create a NAClassLoader and set the snap tolerance (meters unit)
			INAClassLoader naClassLoader = new NAClassLoaderClass();
			naClassLoader.Locator = m_naContext.Locator;
			naClassLoader.Locator.SnapTolerance = 100;
			naClassLoader.NAClass = naClass;

			// Create field map to automatically map fields from input class to NAClass
			INAClassFieldMap naClassFieldMap = new NAClassFieldMapClass();
			naClassFieldMap.CreateMapping(naClass.ClassDefinition, inputFeatureClass.Fields);
			naClassLoader.FieldMap = naClassFieldMap;

			// Avoid loading network locations onto non-traversable portions of elements
			INALocator3 locator = m_naContext.Locator as INALocator3;
			locator.ExcludeRestrictedElements = true;
			locator.CacheRestrictedElements(m_naContext);

			// load network locations
			int rowsIn = 0;
			int rowsLocated = 0;
			naClassLoader.Load(inputFeatureClass.Search(null, true) as ICursor, null, ref rowsIn, ref rowsLocated);

			if (rowsLocated <= 0)
			{
				MessageBox.Show("Facilities were not loaded from input feature class");
                MessageBox.Show("没有导入任何结果！");
				return false;
			}

			// Message all of the network analysis agents that the analysis context has changed
			INAContextEdit naContextEdit = m_naContext as INAContextEdit;
			naContextEdit.ContextChanged();

			return true;
		}

		private void AddNetworkAnalysisLayerToMap()
		{
			ILayer layer = m_naContext.Solver.CreateLayer(m_naContext) as ILayer;
			layer.Name = m_naContext.Solver.DisplayName;
			mainForm.axMapControl1.AddLayer(layer);
		}

		private void AddNetworkDatasetLayerToMap(INetworkDataset networkDataset)
		{
			INetworkLayer networkLayer = new NetworkLayerClass();
			networkLayer.NetworkDataset = networkDataset;
			ILayer layer = networkLayer as ILayer;
			layer.Name = "Network Dataset";
			mainForm.axMapControl1.AddLayer (layer);
		}

		#endregion

		#region Solver Settings

		private void ConfigureSolverSettings()
		{
			ConfigureSettingsSpecificToServiceAreaSolver();

			ConfigureGenericSolverSettings();

			UpdateContextAfterChangingSettings();
		}

		private void ConfigureSettingsSpecificToServiceAreaSolver()
		{
			INAServiceAreaSolver naSASolver = m_naContext.Solver as INAServiceAreaSolver;

			naSASolver.DefaultBreaks = ParseBreaks(txtCutOff.Text);//Breaks服务半径

			naSASolver.MergeSimilarPolygonRanges = true;//true同样服务水平的区域融合
			naSASolver.OutputPolygons = esriNAOutputPolygonType.esriNAOutputPolygonSimplified;
			naSASolver.OverlapLines = true;
			naSASolver.SplitLinesAtBreaks = false;
			naSASolver.TravelDirection = esriNATravelDirection.esriNATravelDirectionFromFacility;

			if (ckbShowLines.Checked)
				naSASolver.OutputLines = esriNAOutputLineType.esriNAOutputLineTrueShape;
			else
				naSASolver.OutputLines = esriNAOutputLineType.esriNAOutputLineNone;
		}

		private void ConfigureGenericSolverSettings()
		{
			INASolverSettings naSolverSettings = m_naContext.Solver as INASolverSettings;
			naSolverSettings.ImpedanceAttributeName = cbCostAttribute.Text;

			// set the oneway restriction, if necessary
			IStringArray restrictions = naSolverSettings.RestrictionAttributeNames;
			restrictions.RemoveAll();
			if (ckbUseRestriction.Checked)
				restrictions.Add("Oneway");            
			naSolverSettings.RestrictionAttributeNames = restrictions;
			//naSolverSettings.RestrictUTurns = esriNetworkForwardStarBacktrack.esriNFSBNoBacktrack;设置是否可以急转弯
		}

		private void UpdateContextAfterChangingSettings()
		{
			IDatasetComponent datasetComponent = m_naContext.NetworkDataset as IDatasetComponent;
			IDENetworkDataset deNetworkDataset = datasetComponent.DataElement as IDENetworkDataset;
			m_naContext.Solver.UpdateContext(m_naContext, deNetworkDataset, new GPMessagesClass());
		}

		private IDoubleArray ParseBreaks(string p)
		{
			String[] breaks = p.Split(' ');
			IDoubleArray pBrks = new DoubleArrayClass();
			int firstIndex = breaks.GetLowerBound(0);
			int lastIndex = breaks.GetUpperBound(0);
			for (int splitIndex = firstIndex; splitIndex <= lastIndex; splitIndex++)
			{
				try
				{
					pBrks.Add(Convert.ToDouble(breaks[splitIndex]));
				}
				catch (FormatException)
				{
					MessageBox.Show("Breaks are not properly formatted.  Use only digits separated by spaces");
					pBrks.RemoveAll();
					return pBrks;
				}
			}

			return pBrks;
		}

		#endregion

		#region Post-Solve

		private void LoadListboxAfterPartialSolve(IGPMessages gpMessages)
		{
			lbOutput.Items.Add("Partial Solve Generated.");
			for (int msgIndex = 0; msgIndex < gpMessages.Messages.Count; msgIndex++)
			{
				string errorText = "";
				switch (gpMessages.GetMessage(msgIndex).Type)
				{
					case esriGPMessageType.esriGPMessageTypeError:
						errorText = "Error " + gpMessages.GetMessage(msgIndex).ErrorCode.ToString() + " " + gpMessages.GetMessage(msgIndex).Description;
						break;
					case esriGPMessageType.esriGPMessageTypeWarning:
						errorText = "Warning " + gpMessages.GetMessage(msgIndex).ErrorCode.ToString() + " " + gpMessages.GetMessage(msgIndex).Description;
						break;
					default:
						errorText = "Information " + gpMessages.GetMessage(msgIndex).Description;
						break;
				}
				lbOutput.Items.Add(errorText);
			}
		}

		private void LoadListboxAfterSuccessfulSolve()
		{
			ITable table = m_naContext.NAClasses.get_ItemByName("SAPolygons") as ITable;
			if (table.RowCount(null) > 0)
			{
				IGPMessage gpMessage = new GPMessageClass();
				lbOutput.Items.Add("FacilityID, FromBreak, ToBreak");
				ICursor cursor = table.Search(null, true);
				IRow row = cursor.NextRow();
				while (row != null)
				{
					int facilityID = (int)row.get_Value(table.FindField("FacilityID"));
					double fromBreak = (double)row.get_Value(table.FindField("FromBreak"));
					double toBreak = (double)row.get_Value(table.FindField("ToBreak"));
					lbOutput.Items.Add(facilityID.ToString() + ", " + fromBreak.ToString("#####0.00") + ", " + toBreak.ToString("#####0.00"));
					row = cursor.NextRow();
				}
			}
		}

		private void UpdateMapDisplayAfterSolve()
		{
			// Zoom to the extent of the service areas
			IGeoDataset geoDataset = m_naContext.NAClasses.get_ItemByName("SAPolygons") as IGeoDataset;
			IEnvelope envelope = geoDataset.Extent;
			if (!envelope.IsEmpty)
			{
				envelope.Expand(1.1, 1.1, true);
				mainForm.axMapControl1.Extent = envelope;

				// Call this to update the renderer for the service area polygons
				// based on the new breaks.
				m_naContext.Solver.UpdateLayer(mainForm.axMapControl1.get_Layer(0) as INALayer);
			}
			mainForm.axMapControl1.Refresh();
		}

		#endregion

        private void button1_Click(object sender, EventArgs e)
        {
            IWorkspace workspace = OpenWorkspace(txtWorkspacePath.Text);
            if (workspace == null)
            {
                this.Cursor = Cursors.Default;
                return;
            }
            IFeatureWorkspace featureWorkspace = workspace as IFeatureWorkspace;
            //Input Deployment result 导入选址信息
            int[] x = xx;
            LoadSolution(featureWorkspace, x);
        }

        private void ResultSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ResultSelectComboBox.SelectedIndex)
            {
                case 0://p-median
                    {
                        int[] y = { 1, 3, 5, 7, 8 };
                        xx=y;
                        //xx=p_median.Parameter.x;
                        //for (int i = 0; i < p_median.Parameter.p; i++) xx[i]++;
                            break;
                    }
                case 1://MCLP
                    {
                        xx = MCLP.Parameter.x;
                        for (int i = 0; i < MCLP.Parameter.p; i++) xx[i]++;
                        break;
                    }
                //case 2://SCLP
                //    {
                //        xx = SCLP.Parameter.x;
                //        break;
                //    }

            }
        }
    }
}
