using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using MicroSCADAStudio.Src;
using MicroSCADAStudioLibrary.Src.Visuals;
using MicroSCADACustomLibrary;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADACustomLibrary.Src.IOFiles;
using MicroSCADAStudio.Src.Forms;
using ADOX;
using WeifenLuo.WinFormsUI.Docking;
using MicroSCADAStudio.Src.EnvironmentDesigner;
using MicroSCADAStudioLibrary.Src.TypeConverter;
using MicroSCADAStudio.Src.DockingForms;
using ICSharpCode.SharpZipLib.Zip;
using MicroSCADAStudioLibrary.Src.Tags;
using MicroSCADAStudioLibrary.Src.Forms;
using MicroSCADAStudioLibrary.Src;

namespace MicroSCADAStudio
{
    
    public partial class MainFormDesign : Form
    {
        //private DeserializeDockContent deserializeDockContent;
        private ProjectManagerForm projectManager;
        private ObjectPropertiesForm objectProperties;        
        private WorkSpaceForm workSpace;
        //private ScriptEditorForm scriptEditor;
        private TableInternalTagForm tableInternalTagForm;
        private TableTagForm tableTagForm;
        private TableBitmapForm tableBitmapForm;
        private TableScreenForm tableScreenForm;
        private TableZoneForm tableZoneForm;
        private TableAlarmsForm tableAlarmsForm;
        private CDesignProject project;
        //private CRecentFiles recentFiles;
        private CTreeViewAdapter treeViewAdapter;
        private CTabControlAdapter tabControlAdapter1;
        private CTabControlAdapter tabControlAdapter2;
        private static CDesignProject projectInstace;
        const string PROJECT_FILTER = "Micro SCADA files (*.scd)|*.scd";
        public MainFormDesign()
        {
            InitializeComponent();
            //            
            this.objectProperties = new ObjectPropertiesForm(this.imageList1);
            this.projectManager = new ProjectManagerForm(this.imageList1);
            this.workSpace = new WorkSpaceForm();
            this.objectProperties.TabText = "Object properties";
            this.projectManager.TabText = "Project manager";
            this.workSpace.TabText = "Work space";
            this.projectManager.SelectObject += new SelectedObjectEventHandler(projectManager_SelectObject);            
            this.projectManager.ObjectDoubleClick += new SelectedObjectEventHandler(projectManager_ObjectDoubleClick);
            //this.deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);            
            //
            this.project = CDesignProject.getInstance();            
            this.project.MouseMove = project_MouseMove;
            this.project.DoubleClick = project_DoubleClick;
            this.project.KeyDown = project_KeyDown;
            this.treeViewAdapter = new CTreeViewAdapter(this.projectManager.treeView1, this.project, this.project.ImageIndex);
            this.tabControlAdapter1 = new CTabControlAdapter(this.workSpace.tabControl1, this.project.GetScreensObject());
            this.tabControlAdapter2 = new CTabControlAdapter(this.workSpace.tabControl1, this.project.GetPopupScreensObject());
            projectInstace = this.project;
            //
            //this.recentFiles = new CRecentFiles(this.miOpenRecentFiles);
            //this.recentFiles.SubMenuItemClick = recentFiles_Click;//não usar operador "+="
            //miObjects.Enabled = false;
            miSaveProject.Enabled = false;
            miSaveProjectAs.Enabled = false;
            //            
            this.tableInternalTagForm = new TableInternalTagForm(this.workSpace.tabControl1, (CDesignInternalTagList)project.InternalTagList, imageList1);
            this.tableTagForm = new TableTagForm(this.workSpace.tabControl1,(CDesignNetwork)project.Network);
            this.tableBitmapForm = new TableBitmapForm(this.workSpace.tabControl1);
            this.tableScreenForm = new TableScreenForm(this.workSpace.tabControl1);
            this.tableZoneForm = new TableZoneForm(this.workSpace.tabControl1);
            this.tableAlarmsForm = new TableAlarmsForm(this.workSpace.tabControl1, (CDesignAlarmsManager)project.AlarmsManager, imageList1);
        }
        /*!
         * Evento OnLoad
         * Configura ambiente do software
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Micro SCADA Design";
            this.projectManager.Show(dockPanel1);
            this.objectProperties.Show(dockPanel1);
            this.workSpace.Show(dockPanel1);
            this.projectManager.DockState = DockState.DockLeft;
            this.objectProperties.DockState = DockState.DockLeft;
            //this.recentFiles.Open();            
            this.EnabledEnvironment(false);
            
            //string temp;            
            //temp = CSysUtils.RemoveDirectory(AppDomain.CurrentDomain.BaseDirectory, 3);            
            //temp = temp + "Tests\\project1\\";            
            //this.openFileDialog1.InitialDirectory = temp;
            //this.Text = temp;
            CPreferences.Open();
            CPreferences.RecentFiles.SubMenuItemClick = recentFiles_Click;//não usar operador "+="
            CPreferences.RecentFiles.Open(miOpenRecentFiles);            
            openFileDialog1.InitialDirectory = CPreferences.WorkDirectory;
        }

        private void projectManager_ObjectDoubleClick(object sender, SelectedObjectEventArgs e)
        {

            if (sender is CDesignCustomScreen)
                ((CDesignCustomScreen)sender).Show();
            else if (sender is CDesignInternalTagList)
                tableInternalTagForm.Show();
            else if (sender is CDesignNetwork)
                tableTagForm.Show();
            else if (sender is CDesignBitmapList)
                tableBitmapForm.Show();
            else if (sender is CDesignScreenList)
                tableScreenForm.Show();
            else if (sender is CDesignDinamicText)
                tableZoneForm.Show(sender as IDesignZones);
            else if (sender is CDesignAnimation)
                tableZoneForm.Show(sender as IDesignZones);
            else if (sender is CDesignButton)
                tableZoneForm.Show(sender as IDesignZones);
            else if (sender is CDesignAlarmsManager)
                tableAlarmsForm.Show();
        }

        private void project_DoubleClick(object sender, EventArgs e)
        {
            if (sender is CDesignText)
            {
                TextEditorForm stringCollectionEditor = new TextEditorForm(((CDesignText)sender).Text);                
                if (stringCollectionEditor.ShowDialog(this) == DialogResult.OK)
                    ((CDesignText)sender).StringToText(stringCollectionEditor.Value);
                stringCollectionEditor.Dispose();
            }
            else if (sender is CDesignPicture)
            {
                openFileDialog1.Filter = CDesignPicture.IMAGE_FILTER;
                openFileDialog1.FileName = "";
                if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    ((CDesignPicture)sender).SetImage(openFileDialog1.FileName);
                }                
            }
            else if (sender is CDesignAlphaNumeric)
            {
                ApplicationBrowserForm formSelectTag;
                formSelectTag = new ApplicationBrowserForm(true);
                if (formSelectTag.ShowDialog(this) == DialogResult.OK)
                {
                    ((CDesignAlphaNumeric)sender).TagValue = (CDesignCustomTag)formSelectTag.SelectedObject;                    
                }
                formSelectTag.Dispose();
            }
            else if (sender is CDesignDinamicText)
            {
                ApplicationBrowserForm formSelectTag;
                formSelectTag = new ApplicationBrowserForm(true);
                if (formSelectTag.ShowDialog(this) == DialogResult.OK)
                {
                    ((CDesignDinamicText)sender).TagValue = (CDesignCustomTag)formSelectTag.SelectedObject;
                }
                formSelectTag.Dispose();                
            }
            else if (sender is CDesignAnimation)
            {
                ApplicationBrowserForm formSelectTag;
                formSelectTag = new ApplicationBrowserForm(true);
                if (formSelectTag.ShowDialog(this) == DialogResult.OK)
                {
                    ((CDesignAnimation)sender).TagValue = (CDesignCustomTag)formSelectTag.SelectedObject;
                }
                formSelectTag.Dispose();   
            }
            else if (sender is CDesignMeter)
            {
                ApplicationBrowserForm formSelectTag;
                formSelectTag = new ApplicationBrowserForm(true);
                if (formSelectTag.ShowDialog(this) == DialogResult.OK)
                {
                    ((CDesignMeter)sender).TagValue = (CDesignCustomTag)formSelectTag.SelectedObject;
                }
                formSelectTag.Dispose();
            }
            else if (sender is CDesignBargraph)
            {
                ApplicationBrowserForm formSelectTag;
                formSelectTag = new ApplicationBrowserForm(true);
                if (formSelectTag.ShowDialog(this) == DialogResult.OK)
                {
                    ((CDesignBargraph)sender).AddTagValue((CDesignCustomTag)formSelectTag.SelectedObject);
                }
                formSelectTag.Dispose();
            }
            else if (sender is CDesignTrendChart)
            {
                ApplicationBrowserForm formSelectTag;
                formSelectTag = new ApplicationBrowserForm(true);
                if (formSelectTag.ShowDialog(this) == DialogResult.OK)
                {
                    ((CDesignTrendChart)sender).AddTagValue((CDesignCustomTag)formSelectTag.SelectedObject);
                }
                formSelectTag.Dispose();
            }
        }

        public static CDesignProject getProject()
        {
            return projectInstace;
        }

        public void ioFileXML_FileError(object sender, IOFileErrorEventArgs e)
        {
            MessageBox.Show(this, e.ErrorMessage);
        }

        private void OpenProject(string FileName)
        {
            if (System.IO.File.Exists(FileName))
            {
                COpenFromXML openFromXML;
                openFromXML = new COpenFromXML(project);
                openFromXML.FileError += new IOFileErrorEventHandler(ioFileXML_FileError);
                openFromXML.Open(FileName);
                this.Text = "Micro SCADA Design - " + FileName;
                if (project.Screens.ObjectList.Count > 0)
                    ((CDesignScreen)project.Screens.ObjectList[0]).Show();
                objectProperties.Refresh();
                //
                EnabledEnvironment(true);
                //TODO:remover daqui
                CPreferences.RecentFiles.Add(FileName);
            }
            else
            {
                MessageBox.Show(string.Format("File \"{0}\" do not exist.", FileName));
            }
        }

        private void recentFiles_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            OpenProject(menuItem.Text);            
        }

        private void miOpenProject_Click(object sender, EventArgs e)
        {            
            openFileDialog1.Filter = PROJECT_FILTER;
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {                
                OpenProject(openFileDialog1.FileName);                
            }
        }                     

        private void miNewScreen_Click(object sender, EventArgs e)
        {
            ((CDesignScreenList)this.project.Screens).AddScreenEx();            
        }

        private void miNewPopupScreen_Click(object sender, EventArgs e)
        {
            ((CDesignPopupScreenList)this.project.PopupScreens).NewScreenEx();
        }        

        private void miNewText_Click(object sender, EventArgs e)
        {
            CDesignCustomScreen.setCreateID(CCustomScreen.CREATE_ID_TEXT);
        }

        private void miNewPicture_Click(object sender, EventArgs e)
        {
            CDesignCustomScreen.setCreateID(CCustomScreen.CREATE_ID_PICTURE);
        }

        //private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        //{
             
        //}

        //private void Form1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    MessageBox.Show("Form1_KeyDown"); 
        //}

        //private void treeView1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    MessageBox.Show("treeView1_KeyDown");
        //}

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("tabControl1_KeyDown");
            //CDesignScreen screen = (CDesignScreen)tabControl1.SelectedTab.Tag;
            //screen.form_KeyDown(screen.getForm(), e);
        }
        /*!
         * Cria novo projeto.
         * Uma pasta com o mesmo nome do projeto é criada.
         */
        private void miNewProject_Click(object sender, EventArgs e)
        {            
            saveFileDialog1.InitialDirectory = CPreferences.WorkDirectory;
            saveFileDialog1.Filter = PROJECT_FILTER;
            saveFileDialog1.DefaultExt = "scd";
            saveFileDialog1.FileName = String.Empty;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Cria diretorio com mesmo nome do projeto
                FileInfo fi = new FileInfo(saveFileDialog1.FileName);
                DirectoryInfo dir = new DirectoryInfo(fi.DirectoryName);
                String filenoext = Path.ChangeExtension(fi.Name, "");
                filenoext = filenoext.Remove(filenoext.Length - 1);
                dir.CreateSubdirectory(filenoext);
                filenoext = fi.DirectoryName + "\\" + filenoext + "\\" + fi.Name;
                this.Text = "Micro SCADA Design - "+filenoext;
                //
                project.New(filenoext);
                CSaveToXML saveToXML;
                saveToXML = new CSaveToXML(project);
                saveToXML.Save();
                EnabledEnvironment(true);                
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(e.CloseReason == CloseReason.ApplicationExitCall)
                if(project != null)
                    project.Dispose();
        }

        private void miSaveProject_Click(object sender, EventArgs e)
        {
            CSaveToXML saveToXML;
            saveToXML = new CSaveToXML(project);
            saveToXML.FileError += new IOFileErrorEventHandler(ioFileXML_FileError);
            saveToXML.Save();
            MessageBox.Show(this, "Project saved OK.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Compacta arquivos do projeto
            //TODO:verificar se esta correto
            ZipFile zipFile;
            string zipFileName = Path.ChangeExtension(project.FileName, ".zip");
            if (File.Exists(zipFileName))
                zipFile = new ZipFile(zipFileName);
            else
                zipFile = ZipFile.Create(zipFileName);
            project.BitmapList.Close();
            //
            zipFile.BeginUpdate();
            zipFile.Add(project.FileName);
            zipFile.Add(Path.ChangeExtension(project.FileName, ".tbm"));
            zipFile.CommitUpdate();
            zipFile.Close();
            zipFile = null;
            //
            project.BitmapList.Open();             
        }

        private void miSaveProjectAs_Click(object sender, EventArgs e)
        {
            //saveFileDialog1.InitialDirectory = Application.StartupPath;
            saveFileDialog1.InitialDirectory = CPreferences.WorkDirectory;
            saveFileDialog1.Filter = PROJECT_FILTER;
            saveFileDialog1.DefaultExt = "scd";
            saveFileDialog1.FileName = String.Empty;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {                  
                CSaveToXML saveToXML;
                saveToXML = new CSaveToXML(project);
                saveToXML.FileError += new IOFileErrorEventHandler(ioFileXML_FileError);
                saveToXML.SaveAs(saveFileDialog1.FileName);
                this.Text = "Micro SCADA Design - " + Path.ChangeExtension(project.FileName,"");
            }
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void miNewField_Click(object sender, EventArgs e)
        {
            CDesignCustomScreen.setCreateID(CCustomScreen.CREATE_ID_ALPHANUMERIC_FIELD);
        }       

        private void miNewTrendChart_Click(object sender, EventArgs e)
        {
            CDesignCustomScreen.setCreateID(CCustomScreen.CREATE_ID_TREND_CHART);
        }        

        private void toolStripButton7_Click_2(object sender, EventArgs e)
        {
            String projFileName;
            String bmplFileName;
            FileInfo fileInfo;

            miSaveProject_Click(sender, e);

            fileInfo = new FileInfo(project.FileName);
            projFileName = fileInfo.Directory.FullName + "\\debug.xml";
            fileInfo.CopyTo(projFileName, true);

            fileInfo = new FileInfo(project.BitmapList.FileName);
            bmplFileName = fileInfo.Directory.FullName + "\\debug.tbm";
            fileInfo.CopyTo(bmplFileName, true);

            //CSaveCommunication saveComm = new CSaveCommunication(project);
            //saveComm.Save();
            //TODO:Solução temporaria para resolver o caminho
            #region Solução temporaria para resolver o caminho
            process1.StartInfo.FileName = Application.StartupPath;
            int i = process1.StartInfo.FileName.IndexOf("MicroSCADA\\");
            i += "MicroSCADA\\".Length;
            process1.StartInfo.FileName = process1.StartInfo.FileName.Remove(i, process1.StartInfo.FileName.Length - i);
            process1.StartInfo.FileName += "MicroSCADARuntime\\bin\\Debug\\MicroSCADARuntime.exe";
            #endregion
            process1.StartInfo.Arguments = projFileName;
            process1.StartInfo.CreateNoWindow = true;
            process1.Start();            
        }       
        
        private void miNewDinamicText_Click(object sender, EventArgs e)
        {
            CDesignCustomScreen.setCreateID(CCustomScreen.CREATE_ID_DINAMIC_TEXT);
        }        

        private void miNewShape_Click(object sender, EventArgs e)
        {
            CDesignCustomScreen.setCreateID(CCustomScreen.CREATE_ID_SHAPE);
        }

        private void miNewButton_Click(object sender, EventArgs e)
        {
            CDesignCustomScreen.setCreateID(CCustomScreen.CREATE_ID_BUTTON);
        }
                
        private void miNewBargraph_Click(object sender, EventArgs e)
        {
            CDesignCustomScreen.setCreateID(CCustomScreen.CREATE_ID_BARGRAPH);
        }

        private void miNewAnimation_Click(object sender, EventArgs e)
        {
            CDesignCustomScreen.setCreateID(CCustomScreen.CREATE_ID_ANIMATION);
        }

        private void miNewCheckBox_Click(object sender, EventArgs e)
        {
            CDesignCustomScreen.setCreateID(CCustomScreen.CREATE_ID_CHECKBOX);
        }

        private void miNewRadioGroup_Click(object sender, EventArgs e)
        {
            CDesignCustomScreen.setCreateID(CCustomScreen.CREATE_ID_RADIOGROUP);
        }

        private void miNewMeter_Click(object sender, EventArgs e)
        {
            CDesignCustomScreen.setCreateID(CCustomScreen.CREATE_ID_METER);
        }    

        private void projectManager_SelectObject(object sender, SelectedObjectEventArgs e)
        {                        
            objectProperties.SelectedObject = sender;
        }

        private void project_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = e.Location.ToString();
            //statusStrip1.Items[0].Text = e.Location.ToString();
        }

        private void project_KeyDown(object sender, KeyEventArgs e)
        {            
            statusStrip1.Items[1].Text = e.KeyCode.ToString();
            statusStrip1.Items[2].Text = string.Format("Shift={0}, Ctrl={1}", e.Shift, e.Control);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog(this)== DialogResult.OK)
            {
                COpenFromBIN.OpenProgress += new OpenProgressEventHandler(COpenFromBIN_OpenProgress);
                COpenFromBIN.Open(openFileDialog1.FileName, project);
                MessageBox.Show(this, "Project HMI imported!", Application.ProductName);
            }
        }

        private void COpenFromBIN_OpenProgress(object sender, OpenProgressEventArgs e)
        {
            toolStripStatusLabel1.Text = string.Format("Open {0:0.000}...", e.Progress);
            statusStrip1.Update();
        }

        private void miClose_Click(object sender, EventArgs e)
        {
            CSaveToXML saveToXML = new CSaveToXML(project);
            String hash = saveToXML.GetHashMD5();
            if (project.HashMD5 == hash)
                project.Clear();
            else
            {
                DialogResult dr = MessageBox.Show(this, "Save changes in project ?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (dr)
                {
                    case DialogResult.Yes:
                        saveToXML.Save(hash);
                        project.Clear();
                        break;
                    case DialogResult.No:
                        project.Clear();
                        break;
                    default://Cancel
                        return;
                }
            }
            EnabledEnvironment(false);
        }

        private void EnabledEnvironment(bool Value)
        {
            miSaveProject.Enabled = Value;
            miSaveProjectAs.Enabled = Value;
            miCloseProject.Enabled = Value;
            miEdit.Enabled = Value;
            miObjects.Enabled = Value;
            tsbSave.Enabled = Value;
            tsbCloseProject.Enabled = Value;
            toolStrip1.Enabled = Value;
            toolStrip3.Enabled = Value;
            toolStrip4.Enabled = Value;
        }
        
        private void zoomingToolStrip1_ZoomValueChanged(object sender, EventArgs e)
        {
            CDesignView.setZoomScale(zoomingToolStrip1.ZoomValue);
            workSpace.tabControl1.Invalidate(true);
        }

        private void miImageLibrary_Click(object sender, EventArgs e)
        {
            ImageLibraryForm imageLibraryDialog;
            imageLibraryDialog = new ImageLibraryForm();
            imageLibraryDialog.ShowDialog(this);
        }    

        private void miResourceView_Click(object sender, EventArgs e)
        {
            ResourcesForm resourceDialog;
            resourceDialog = new ResourcesForm();
            resourceDialog.Show();
        }

        private void miPreferences_Click(object sender, EventArgs e)
        {
            PrefecencesForm preferences = new PrefecencesForm();
            preferences.ShowDialog(this);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            
        }

        

                       
    }    
}
