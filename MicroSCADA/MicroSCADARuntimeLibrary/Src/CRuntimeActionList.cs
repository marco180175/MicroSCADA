using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MicroSCADACustomLibrary.Src;
using MicroSCADARuntimeLibrary.Src.Visuals;

namespace MicroSCADARuntimeLibrary.Src
{
    public class CRuntimeActionList : CRuntimeSystem, ICustomActionList
    {
        private delegate object CFunctionDelegate(ArrayList OperandList);//!< Ponteiro para função
        private Dictionary<CCustomActionCode, CFunctionDelegate> m_dictionary;
        private CFunctionDelegate m_function;
        public CRuntimeActionList(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            m_dictionary = new Dictionary<CCustomActionCode, CFunctionDelegate>();
            m_dictionary.Add(CCustomActionCode.ShowScreen, ShowScreen);
            m_dictionary.Add(CCustomActionCode.ShowPopup, ShowPopup);            
            m_dictionary.Add(CCustomActionCode.NextScreen, NextScreen);
            m_dictionary.Add(CCustomActionCode.PrevScreen, PrevScreen);
        }
        /*!
         * 
         */
        public ICustomAction NewAction()
        {
            CRuntimeAction action = new CRuntimeAction(this, project);
            ObjectList.Add(action);
            return action;
        }
        /*!
         * 
         */
        public void Execute(CRuntimeAction Action)
        {
            foreach (CRuntimeCodeLine line in Action.ObjectList)
            {
                m_function = m_dictionary[line.Opcode];
                m_function(line.ParamList);
            }
        }        

        private object ShowScreen(ArrayList OperandList)
        {
            //((CRuntimeScreens)OperandList[0]).ShowNextScreen();
            return 0;
        }

        private object ShowPopup(ArrayList OperandList)
        {
            ((CRuntimePopupScreen)OperandList[0]).Show();
            return 0;
        }

        private object NextScreen(ArrayList OperandList)
        {
            ((CRuntimeScreenList)OperandList[0]).ShowNextScreen();
            return 0;
        }

        private object PrevScreen(ArrayList OperandList)
        {
            ((CRuntimeScreenList)OperandList[0]).ShowPrevScreen();
            return 0;
        }

        private object AssignValueToValue(ArrayList OperandList)
        {
            CRuntimeSystem runtimeSystem;
            string propertyName1, propertyName2, propertyValue;
            PropertyInfo propertyInfo;

            runtimeSystem = (CRuntimeSystem)OperandList[0];
            propertyName1 = (string)OperandList[1];
            propertyInfo = runtimeSystem.GetType().GetProperty(propertyName1);
            propertyValue = (string)propertyInfo.GetValue(runtimeSystem, null);

            runtimeSystem = (CRuntimeSystem)OperandList[2];
            propertyName2 = (string)OperandList[3];
            propertyInfo = runtimeSystem.GetType().GetProperty(propertyName2);
            propertyInfo.SetValue(runtimeSystem, propertyValue, null);

            return 0;
        }

        private object AssignConstToValue(ArrayList OperandList)
        {
            CRuntimeSystem runtimeSystem;
            string propertyName1, propertyValue;
            PropertyInfo propertyInfo;

            runtimeSystem = (CRuntimeSystem)OperandList[0];
            propertyName1 = (string)OperandList[1];
            propertyValue = (string)OperandList[2];
            propertyInfo = runtimeSystem.GetType().GetProperty(propertyName1);
            propertyInfo.SetValue(runtimeSystem, propertyValue, null);

            return 0;
        }

        /*!
         * 
         */
        public void LinkObjects()
        {
            foreach (CRuntimeAction action in ObjectList)                 
                 action.LinkObjects();            
        }
    }
}
