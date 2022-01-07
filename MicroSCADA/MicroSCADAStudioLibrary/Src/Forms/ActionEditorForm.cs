using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADACustomLibrary.Src;
using MicroSCADAStudioLibrary.Src.Visuals;
using MicroSCADAStudioLibrary.Src;

namespace MicroSCADAStudioLibrary.Src.Forms
{
    public partial class ActionEditorForm : Form
    {        
        private CDesignAction m_action;
        private List<CCustomActionCode> m_codeList;
        public ActionEditorForm(CDesignAction Action)
        {
            InitializeComponent();
            this.m_action = Action;
        }

        private void ActionEditorForm_Shown(object sender, EventArgs e)
        {
            CDesignActionList actionList = CDesignActionList.getInstance();
            //btOk.Enabled = (actionList.ObjectList.Count > 0);
            
            foreach(CDesignAction action in actionList.ObjectList)
                listBox1.Items.Add(action);

            lbxActionLines.Items.Clear();
            if (m_action != null)
                lbxActionLines.Items.AddRange(m_action.Lines);
            //
            m_codeList = new List<CCustomActionCode>();
            foreach (CCustomActionCode code in Enum.GetValues(typeof(CCustomActionCode)))
            {
                m_codeList.Add(code);
                lbxCommand.Items.Add(actionList.Dictionary[code]);
            }
        }

        public CDesignAction Action
        {
            get { return this.m_action; }
            //set { this.m_action = value; }
        }
                
        private void btAddAction_Click(object sender, EventArgs e)
        {
            CDesignActionList actionList = CDesignActionList.getInstance();
            m_action = (CDesignAction)actionList.NewActionEx();
            listBox1.SelectedIndex = listBox1.Items.Add(m_action);
        }

        private void btAddLine_Click(object sender, EventArgs e)
        {
            CCustomActionCode code = (CCustomActionCode)m_codeList[lbxCommand.SelectedIndex];
            CDesignCodeLine codeLine = (CDesignCodeLine)m_action.NewCodeLineEx(code);

            if (codeLine.ParamCount > 0)
            {
                ApplicationBrowserForm appBrowser;
                appBrowser = new ApplicationBrowserForm(false);
                if (appBrowser.ShowDialog(this) == DialogResult.OK)
                {                
                    codeLine.AddParam(appBrowser.SelectedObject);                
                }            
            }
            lbxActionLines.Items.Clear();
            lbxActionLines.Items.AddRange(m_action.Lines);            
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            m_action = null;
            DialogResult = DialogResult.Cancel;
        }
        
        private void lbxActionList_SelectedIndexChanged(object sender, EventArgs e)
        {            
            m_action = (CDesignAction)listBox1.SelectedItem;
            //btOk.Enabled = (actionList.ObjectList.Count > 0);
            //btAddLine.Enabled = (m_action != null);
            lbxActionLines.Items.Clear();            
            lbxActionLines.Items.AddRange(m_action.Lines);                        
        }        
    }
}
