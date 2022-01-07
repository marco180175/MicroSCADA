using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using MicroSCADACustomLibrary;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADACustomLibrary.Src.IOFiles
{
    public class COpenFromXML : CIOFileXML
    {        
        /*!
         * Construtor
         * @param project
         */
        public COpenFromXML(ICustomProject Project)
            : base(Project)
        { 

        }

        public COpenFromXML()
            : this(null)
        {

        }
        /*!
         * Destrutor
         */
        ~COpenFromXML(){}
        /*!
         * Abre arquivo
         * @param FileName
         */
        public void Open(String FileName)
        {
            XmlElement rootNode;
            XmlNode node1;

            CHashObjects.ObjectDictionary.Clear();

            project.SetFileName(FileName);
            xmlDocument.Load(project.FileName);
            project.SetHashMD5(CXMLUtils.GetHashMD5(xmlDocument));
            rootNode = xmlDocument.DocumentElement;

            node1 = rootNode[GUID_ID];
            project.SetGUID(new Guid(node1.InnerText));

            node1 = rootNode["Name"];
            project.Name = node1.InnerText;

            node1 = rootNode["Description"];
            project.Description = node1.InnerText;

            node1 = rootNode["Comment"];
            project.Comment = node1.InnerText;            
            //            
            if (project.Screens != null)
                OpenScreens(project.Screens, rootNode);
            //
            if (project.PopupScreens != null)
                OpenPopupScreens(project.PopupScreens, rootNode);
            //
            if (project.InternalTagList != null)
                OpenInternalTagList(project.InternalTagList, rootNode);
            //
            if (project.PropertyTagList != null)
                OpenPropertyTagList(project.PropertyTagList, rootNode);
            //
            if (project.Network != null)
                OpenNetwork(project.Network, rootNode);
            //
            if (project.AlarmsManager != null)
                OpenAlarmsManager(project.AlarmsManager, rootNode);
            //
            if (project.ActionList != null)
                OpenActionList(project.ActionList, rootNode);
            //
            if (project.BitmapList != null)
                OpenBitmapList(project.BitmapList, rootNode);
            //
            project.LinkObjects();
        }
        /*!
         * 
         */
        private Color OpenColor(XmlNode xmlNode)
        {
            return Color.FromArgb(
                        byte.Parse(xmlNode.Attributes["R"].InnerText),
                        byte.Parse(xmlNode.Attributes["G"].InnerText),
                        byte.Parse(xmlNode.Attributes["B"].InnerText)
                            );
        }
        /*!
         * 
         */
        private Font OpenFont(XmlNode xmlNode)
        {

            return new Font(xmlNode["Name"].InnerText,
                            float.Parse(xmlNode["Size"].InnerText),
                            (FontStyle)Enum.Parse(typeof(FontStyle), xmlNode["Bold"].InnerText) |
                            (FontStyle)Enum.Parse(typeof(FontStyle), xmlNode["Italic"].InnerText) |
                            (FontStyle)Enum.Parse(typeof(FontStyle), xmlNode["Strikeout"].InnerText) |
                            (FontStyle)Enum.Parse(typeof(FontStyle), xmlNode["Underline"].InnerText)
                );

        }
        /*!
         * Le configuraçoes comuns aos objetos
         */
        private void OpenObject(ICustomSystem customObject, XmlNode xmlNode)
        {
            XmlNode node1;
            //
            node1 = xmlNode["Name"];
            if (node1 != null)
            {
                customObject.SetOpenName(node1.InnerText);
            }
            //
            node1 = xmlNode["GUID"];
            if (node1 != null)
            {
                customObject.SetGUID(new Guid(node1.InnerText));
                CHashObjects.ObjectDictionary.Add(customObject.GUID, customObject);
            }
            //   
            node1 = xmlNode["Description"];
            if (node1 != null)
            {
                customObject.Description = node1.InnerText;
            }
        }
        /*!
         * Le configuraçoes comuns aos objetos de tela
         */
        private void OpenScreenObject(ICustomScreenObject screenObject, XmlNode xmlNode)
        {
            XmlNode xmlNode1;
            //
            OpenObject(screenObject, xmlNode);
            //            
            screenObject.Left = int.Parse(xmlNode["Left"].InnerText);
            //            
            screenObject.Top = int.Parse(xmlNode["Top"].InnerText);
            //            
            screenObject.Width = int.Parse(xmlNode["Width"].InnerText);
            //            
            screenObject.Height = int.Parse(xmlNode["Height"].InnerText);
            xmlNode1 = xmlNode["Border"];
            if (xmlNode1 != null)
                screenObject.Border = (CBorder)Enum.Parse(typeof(CBorder), xmlNode1.InnerText);
            xmlNode1 = xmlNode["Frame"];
            if (xmlNode1 != null)
                screenObject.Frame = (CFrame)Enum.Parse(typeof(CFrame), xmlNode1.InnerText);
        }
        /*!
         * Abre telas
         * @param screens
         * @param node
         */
        private void OpenScreens(ICustomDefaultScreenList screens, XmlNode node)
        {
            
            ICustomScreen screen;
            XmlNode xmlNode1, xmlNode2 ;
  
            xmlNode1 = node["Screens"];

            OpenObject(screens, xmlNode1);

            screens.Width = int.Parse(xmlNode1["Width"].InnerText);
            screens.Height = int.Parse(xmlNode1["Height"].InnerText);
            xmlNode2 = xmlNode1["Objects"];
            foreach(XmlNode xmlNode3 in xmlNode2.ChildNodes)
            {                
                screen = (ICustomScreen)screens.NewScreen();
                
                OpenObject(screen, xmlNode3);                
                screen.BackColor = OpenColor(xmlNode3["Color"]);
                //
                try
                {
                    screen.SetGuidBitmapItem(new Guid(xmlNode3["BitmapItem"].InnerText));
                }
                catch
                {
                    screen.SetGuidBitmapItem(new Guid());
                }
                //
                OpenObjectsOfScreen(screen, xmlNode3);                
            }
        }
        /*!
         * Abre lista de telas popup
         * @param screens
         * @param node
         */
        private void OpenPopupScreens(ICustomScreenList screens, XmlNode node)
        {
            XmlNode xmlNode1 = node["PopupScreens"];
            if (xmlNode1 != null)
            {
                OpenObject(screens, xmlNode1);
                XmlNode xmlNode2 = xmlNode1["Objects"];
                foreach (XmlNode child in xmlNode2.ChildNodes)
                {
                    ICustomPopUpScreen screen = (ICustomPopUpScreen)screens.NewScreen();
                    OpenPopupScreen(screen, child);                    
                }
            }
        }
        /*!
         * Abre tela popup
         * @param screen
         * @param node
         */
        private void OpenPopupScreen(ICustomPopUpScreen screen, XmlNode node)
        {
            OpenObject(screen, node);
            screen.Left = int.Parse(ReadProperty("Left", node));
            screen.Top = int.Parse(ReadProperty("Top", node));
            screen.Width = int.Parse(ReadProperty("Width", node));
            screen.Height = int.Parse(ReadProperty("Height", node));
            screen.BackColor = ReadColorProperty("Color", node);
            screen.ShowTitleBar = bool.Parse(ReadProperty("ShowTitleBar", node));
            screen.Title = ReadProperty("Title", node);
            string strguid = ReadProperty("BitmapItem", node);
            if (strguid == "null")
                screen.SetGuidBitmapItem(Guid.NewGuid());
            else
                screen.SetGuidBitmapItem(new Guid(strguid));
            //
            OpenObjectsOfScreen(screen, node);
        }

        private ArrayList OpenObjectsOfScreen(ICustomScreen screen, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode3;
            ArrayList pasteList = new ArrayList();
            xmlNode1 = xmlNode["Objects"];
            ICustomScreenObject screenObject;
            foreach (XmlNode xmlNode2 in xmlNode1.ChildNodes)
            {                
                xmlNode3 = xmlNode2.Attributes["ClassName"];
                if (xmlNode3.InnerText == "CDesignText")
                {
                    screenObject = screen.NewText();
                    if (screenObject != null)
                        OpenText((ICustomText)screenObject, xmlNode2);
                }
                else if (xmlNode3.InnerText == "CDesignPicture")
                {
                    screenObject = screen.NewPicture();
                    if (screenObject != null)
                        OpenPicture((ICustomPicture)screenObject, xmlNode2);
                }
                else if (xmlNode3.InnerText == "CDesignShape")
                {
                    screenObject = screen.NewShape();
                    if (screenObject != null)
                        OpenShape((ICustomShape)screenObject, xmlNode2);
                }
                else if (xmlNode3.InnerText == "CDesignAlphaNumeric")
                {
                    screenObject = screen.NewAlphaNumeric();
                    if (screenObject != null)
                        OpenAlphaNumeric((ICustomAlphaNumeric)screenObject, xmlNode2);
                }
                else if (xmlNode3.InnerText == "CDesignTrendChart")
                {
                    screenObject = screen.NewTrendChart();
                    if (screenObject != null)
                        OpenTrendChart((ICustomTrendChart)screenObject, xmlNode2);
                }
                else if (xmlNode3.InnerText == "CDesignButton")
                {
                    screenObject = screen.NewButton();
                    if (screenObject != null)
                        OpenButton((ICustomButton)screenObject, xmlNode2);
                }
                else if (xmlNode3.InnerText == "CDesignBargraph")
                {
                    screenObject = screen.NewBargraph();
                    if (screenObject != null)
                        OpenBargraph((ICustomBargraph)screenObject, xmlNode2);
                }
                else if (xmlNode3.InnerText == "CDesignDinamicText")
                {
                    screenObject = screen.NewDinamicText();
                    if (screenObject != null)
                        OpenDinamicText((ICustomDinamicText)screenObject, xmlNode2);
                }
                else if (xmlNode3.InnerText == "CDesignAnimation")
                {
                    screenObject = screen.NewAnimation();
                    if (screenObject != null)
                        OpenAnimation((ICustomAnimation)screenObject, xmlNode2);
                }
                else if (xmlNode3.InnerText == "CDesignCheckBox")
                {
                    screenObject = screen.NewCheckBox();
                    if (screenObject != null)
                        OpenCheckBox((ICustomCheckBox)screenObject, xmlNode2);
                }
                else if (xmlNode3.InnerText == "CDesignRadioGroup")
                {
                    screenObject = screen.NewRadioGroup();
                    if (screenObject != null)
                        OpenRadioGroup((ICustomRadioGroup)screenObject, xmlNode2);
                }
                else if (xmlNode3.InnerText == "CDesignMeter")
                {
                    screenObject = screen.NewMeter();
                    if (screenObject != null)
                        OpenMeter((ICustomMeter)screenObject, xmlNode2);
                }
                else
                {
                    screenObject = null;
                    OnIOError(new IOFileErrorEventArgs("Open object :" + xmlNode3.InnerText));
                }
                //
                if (screenObject != null)
                    pasteList.Add(screenObject);
            }
            return pasteList; 
        }
        
        #region Objetos Visuais
        /*!
         * 
         */
        private void OpenText(ICustomText text, XmlNode xmlNode)
        {
            //Propriedades comuns aos objetos da tela
            OpenScreenObject(text, xmlNode);            
            //
            text.StringToText(xmlNode["Text"].InnerText);            
            //            
            text.BackColor = OpenColor(xmlNode["BackColor"]);
            //            
            text.TextFontColor = OpenColor(xmlNode["TextFontColor"]);
            //            
            text.TextFont = OpenFont(xmlNode["TextFont"]);            
            //
            if (xmlNode["Alignment"] != null)
            {
                text.Alignment = (StringAlignment)Enum.Parse(typeof(StringAlignment),
                                    xmlNode["Alignment"].InnerText);
            }
        }
        /*!
         * 
         */
        private void OpenShape(ICustomShape shape, XmlNode xmlNode)
        {
            OpenScreenObject(shape, xmlNode);            
            //
            shape.BackColor = OpenColor(xmlNode["BackColor"]);            
            //            
            shape.BorderColor = OpenColor(xmlNode["BorderColor"]);
            //            
            shape.BorderWidth = Convert.ToInt32(xmlNode["BorderWidth"].InnerText);
            //
            shape.Radius = Convert.ToInt32(xmlNode["Radius"].InnerText);
            //            
            shape.ShapeType = (CShapeType)Enum.Parse(typeof(CShapeType), xmlNode["ShapeType"].InnerText);
        }
        /*!
         * 
         */
        private void OpenPicture(ICustomPicture picture, XmlNode xmlNode)
        {
            //
            OpenScreenObject(picture, xmlNode);            
            //            
            picture.BackColor = OpenColor(xmlNode["BackColor"]);            
            //
            try
            {
                picture.SetGuidBitmapItem(new Guid(xmlNode["BitmapItem"].InnerText));
            }
            catch
            {
                picture.SetGuidBitmapItem(new Guid());
            }
        }
        /*!
         * 
         */
        private void OpenAlphaNumeric(ICustomAlphaNumeric alphaNumeric, XmlNode xmlNode)
        {
            //
            OpenScreenObject(alphaNumeric, xmlNode);
            alphaNumeric.Font = ReadFontProperty("Font", xmlNode);
            alphaNumeric.FontColor = ReadColorProperty("FontColor", xmlNode);            
            alphaNumeric.BackColor = ReadColorProperty("BackColor", xmlNode);            
            alphaNumeric.FieldType = (CFieldType)ReadEnumProperty("FieldType", typeof(CFieldType), xmlNode);            
            alphaNumeric.DecimalCount = int.Parse(ReadProperty("DecimalCount", xmlNode));            
            alphaNumeric.ValueFormat = (CValueFormat)ReadEnumProperty("FormatType", typeof(CValueFormat), xmlNode);
            alphaNumeric.MaxValue = double.Parse(ReadProperty("MaxValue", xmlNode));
            alphaNumeric.MinValue = double.Parse(ReadProperty("MinValue", xmlNode));
            if(xmlNode["TextAlign"]!=null)
                alphaNumeric.TextAlign = (System.Windows.Forms.HorizontalAlignment)ReadEnumProperty("TextAlign", typeof(System.Windows.Forms.HorizontalAlignment), xmlNode);
            //
            if (xmlNode["TagValue"].InnerText != "null")
                alphaNumeric.SetGuidTagValue(new Guid(xmlNode["TagValue"].InnerText));                
        }

        private string ReadProperty(string Name, XmlNode Node)
        {
            XmlNode xmlNode1 = Node[Name];

            if (xmlNode1 != null)
                return xmlNode1.InnerText;
            else
                return "0";            
        }

        private object ReadEnumProperty(string Name,Type EnumType, XmlNode Node)
        {
            if (Node[Name] != null)
                return Enum.Parse(EnumType, Node[Name].InnerText);
            else
                return null;
        }

        private Color ReadColorProperty(string Name, XmlNode Node)
        {
            XmlNode xmlNode1 = Node[Name];

            if (xmlNode1 != null)
                return OpenColor(xmlNode1); 
            else
                return Color.White;
        }

        private Font ReadFontProperty(string Name, XmlNode Node)
        {
            XmlNode xmlNode1 = Node[Name];

            if (xmlNode1 != null)
                return OpenFont(xmlNode1);
            else
                return new Font("Arial", 16, FontStyle.Bold);//default
        }
        /*!
         * 
         */
        private void OpenTrendChart(ICustomTrendChart trendChart, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2;

            OpenScreenObject(trendChart, xmlNode);     
            //            
            trendChart.MaxValueY = float.Parse(ReadProperty("MaxY", xmlNode));
            //
            trendChart.MinValueY = float.Parse(ReadProperty("MinY", xmlNode));
            //
            trendChart.BufferSize = int.Parse(ReadProperty("BufferSize", xmlNode));            
            //
            trendChart.UpdateTime = int.Parse(ReadProperty("UpdateTime", xmlNode));  
            //            
            trendChart.ChartAreaColor = ReadColorProperty("ChartAreaColor", xmlNode);
            //            
            trendChart.PlotAreaColor = ReadColorProperty("PlotAreaColor", xmlNode);            
            //
            xmlNode1 = xmlNode["Pens"];
            foreach (XmlNode childNode in xmlNode1.ChildNodes)
            {
                ICustomPenTrendChart pen;
                XmlNode xmlNode3;
                xmlNode2 = childNode;
                pen = trendChart.AddPen();
                //
                OpenObject(pen, xmlNode2);               
                //
                pen.Label = xmlNode2["Label"].InnerText;
                //
                pen.PenColor = OpenColor(xmlNode2["Color"]);
                //
                pen.Width = int.Parse(xmlNode2["Width"].InnerText);
                //
                xmlNode3 = xmlNode2["TagValue"];
                if (xmlNode3 != null)
                {
                    if (xmlNode3.InnerText != "null")
                        pen.SetGuidTagValue(new Guid(xmlNode3.InnerText));
                }
            }            
        }
        /*!
         * 
         */
        private void OpenBargraph(ICustomBargraph barGraph, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2;
            //
            OpenScreenObject(barGraph, xmlNode);
            //
            barGraph.MaxValue = float.Parse(xmlNode["MaxValue"].InnerText);
            //
            barGraph.MinValue = float.Parse(xmlNode["MinValue"].InnerText);
            //
            barGraph.Orientation = (CBargraphOrientation)Enum.Parse(typeof(CBargraphOrientation), xmlNode["Orientation"].InnerText);
            //
            xmlNode1 = xmlNode["Bars"];
            for (int I = 0; I < xmlNode1.ChildNodes.Count; I++)
            {
                ICustomBargraphElement bar;

                xmlNode2 = xmlNode1.ChildNodes[I];
                bar = (ICustomBargraphElement)barGraph.NewBar();
                //                
                OpenObject(bar, xmlNode2);
                //
                bar.BarColor = OpenColor(xmlNode2["Color"]);
                //
                if (xmlNode2["TagValue"].InnerText != "null")
                    bar.SetGuidTagValue(new Guid(xmlNode2["TagValue"].InnerText));
            }
        }
        /*!
         * 
         */
        private void OpenDinamicText(ICustomDinamicText dinamicText, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2;
            //
            OpenScreenObject(dinamicText, xmlNode);
            //
            dinamicText.SetGUID(new Guid(xmlNode["GUID"].InnerText));
            //
            try
            {
                dinamicText.SetGuidTagValue(new Guid(xmlNode["TagValue"].InnerText));
            }
            catch
            {
                dinamicText.SetGuidTagValue(Guid.NewGuid());
            }
            //
            xmlNode1 = xmlNode["Zones"];
            for (int I = 0; I < xmlNode1.ChildNodes.Count; I++)
            {
                xmlNode2 = xmlNode1.ChildNodes[I];
                ICustomTextZone zone = (ICustomTextZone)dinamicText.AddZone();
                //
                zone.SetGUID(new Guid(xmlNode2["GUID"].InnerText));
                //
                zone.Name = xmlNode2["Name"].InnerText;
                //
                zone.StringToText(xmlNode2["Text"].InnerText);
                //
                zone.TextFont = OpenFont(xmlNode2["TextFont"]);
                //
                zone.TextFontColor = OpenColor(xmlNode2["TextFontColor"]);
                //
                zone.BackColor = OpenColor(xmlNode2["BackColor"]);
                //
                zone.Alignment = (StringAlignment)Enum.Parse(typeof(StringAlignment), xmlNode2["Alignment"].InnerText);
                //
                zone.MaxValue = float.Parse(xmlNode2["MaxValue"].InnerText);
                //
                zone.MinValue = float.Parse(xmlNode2["MinValue"].InnerText);
            }
        }
        /*!
         * 
         */
        private void OpenAnimation(ICustomAnimation animation, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2;
            //
            OpenScreenObject(animation, xmlNode);
            //
            animation.SetGUID(new Guid(xmlNode["GUID"].InnerText));
            //
            try
            {
                animation.SetGuidTagValue(new Guid(xmlNode["TagValue"].InnerText));
            }
            catch
            {
                animation.SetGuidTagValue(Guid.NewGuid());
            }
            //
            xmlNode1 = xmlNode["Zones"];
            for (int I = 0; I < xmlNode1.ChildNodes.Count; I++)
            {
                xmlNode2 = xmlNode1.ChildNodes[I];
                ICustomPictureZone zone = (ICustomPictureZone)animation.AddZone();
                //
                zone.SetGUID(new Guid(xmlNode2["GUID"].InnerText));
                //
                zone.Name = xmlNode2["Name"].InnerText;
                //
                try
                {
                    zone.SetGuidBitmapItem(new Guid(xmlNode2["BitmapItem"].InnerText));
                }
                catch
                {
                    zone.SetGuidBitmapItem(Guid.NewGuid());
                }
            }
        }
        /*!
         * 
         */
        private void OpenButton(ICustomButton button, XmlNode xmlNode)
        {
            OpenScreenObject(button, xmlNode);
            //
            if (xmlNode["ButtonType"] != null)
            {
                button.ButtonType = (CButtonType)Enum.Parse(typeof(CButtonType),
                                    ReadProperty("ButtonType", xmlNode));
            }
            button.Text = ReadProperty("Text", xmlNode);
            button.TextFont = ReadFontProperty("TextFont", xmlNode);
            button.TextFontColor = ReadColorProperty("TextFontColor", xmlNode);
            button.BackColor = ReadColorProperty("BackColor", xmlNode);
            button.ValueOff = int.Parse(ReadProperty("ValueOff", xmlNode));
            button.ValueOn = int.Parse(ReadProperty("ValueOn", xmlNode));
            button.Jog = bool.Parse(ReadProperty("Jog", xmlNode));
            string strGuid = ReadProperty(ID_TAGVALUE, xmlNode);
            if (strGuid != "null")
                button.SetGuidTagValue(new Guid(strGuid));

            strGuid = ReadProperty("Action", xmlNode);
            if (strGuid != "null")
                button.SetGuidAction(new Guid(strGuid));            
        }
        /*!
         * 
         */
        private void OpenCheckBox(ICustomCheckBox checkBox, XmlNode xmlNode)
        {
            OpenScreenObject(checkBox, xmlNode);
            //
            checkBox.Checked = bool.Parse(ReadProperty("Checked", xmlNode));
            checkBox.Caption = ReadProperty("Caption", xmlNode);
            checkBox.Font = ReadFontProperty("Font", xmlNode);
            checkBox.FontColor = ReadColorProperty("FontColor", xmlNode);
            checkBox.BackColor = ReadColorProperty("BackColor", xmlNode);
            string strGuid = ReadProperty(ID_TAGVALUE, xmlNode);
            if (strGuid != "null")
                checkBox.SetGuidTagValue(new Guid(strGuid)); 
        }

        private void OpenRadioGroup(ICustomRadioGroup radioGroup, XmlNode xmlNode)
        {
            OpenScreenObject(radioGroup, xmlNode);
            //            
            radioGroup.Font = ReadFontProperty("Font", xmlNode);
            radioGroup.FontColor = ReadColorProperty("FontColor", xmlNode);
            radioGroup.BackColor = ReadColorProperty("BackColor", xmlNode);
            radioGroup.Count = int.Parse(ReadProperty("Count", xmlNode));
            XmlNode xmlNode1 = xmlNode["Items"];
            for (int i = 0; i < radioGroup.Count; i++)
            {
                //radioGroup.Items[i].Checked = bool.Parse(ReadProperty("Checked", xmlNode1.ChildNodes[i]));
                radioGroup.Items[i].Caption = ReadProperty("Caption", xmlNode1.ChildNodes[i]);
            }
            radioGroup.CheckedButtonIndex = int.Parse(ReadProperty("CheckedButtonIndex", xmlNode));
            string strGuid = ReadProperty(ID_TAGVALUE, xmlNode);
            if (strGuid != "null")
                radioGroup.SetGuidTagValue(new Guid(strGuid));
        }

        private void OpenMeter(ICustomMeter meter, XmlNode xmlNode)
        {
            OpenScreenObject(meter, xmlNode);
            //            
            meter.BackColor = ReadColorProperty("BackColor", xmlNode);
            meter.StartAngle = int.Parse(ReadProperty("StartAngle", xmlNode));
            meter.SweepAngle = int.Parse(ReadProperty("SweepAngle", xmlNode));
            meter.Title = ReadProperty("Title", xmlNode);
            meter.TitleFont = ReadFontProperty("TitleFont", xmlNode);
            meter.TitleFontColor = ReadColorProperty("TitleFontColor", xmlNode);
            meter.TitleEnabled = bool.Parse(ReadProperty("TitleEnabled", xmlNode));
            meter.ScaleFont = ReadFontProperty("ScaleFont", xmlNode);
            meter.ScaleFontColor = ReadColorProperty("ScaleFontColor", xmlNode);
            meter.PointerWidth = int.Parse(ReadProperty("PointerWidth", xmlNode));
            meter.MaxValue = double.Parse(ReadProperty("MaxValue", xmlNode));
            meter.MinValue = double.Parse(ReadProperty("MinValue", xmlNode));
            meter.Direction = (CSweepDirection)ReadEnumProperty("Direction", typeof(CSweepDirection), xmlNode);
            string strGuid = ReadProperty(ID_TAGVALUE, xmlNode);
            if (strGuid != "null")
                meter.SetGuidTagValue(new Guid(strGuid));
        }
        #endregion
        /*!
         * 
         */
        private void OpenActionList(ICustomActionList actionList, XmlNode xmlNode)
        {
            XmlNode xmlNode1 = xmlNode["ActionList"];
            if (xmlNode1 != null)
            {
                OpenObject(actionList, xmlNode1);
                XmlNode xmlNode2 = xmlNode1["Items"];
                foreach (XmlNode node in xmlNode2.ChildNodes)
                {
                    if (node.Name == "Action")
                    {
                        ICustomAction action = actionList.NewAction();
                        OpenAction(action, node);
                    }
                }
            }
        }
        /*!
         * 
         */
        private void OpenAction(ICustomAction action, XmlNode xmlNode)
        {            
            OpenObject(action, xmlNode);
            XmlNode xmlNode1 = xmlNode["Items"];
            foreach (XmlNode node in xmlNode1.ChildNodes)
            {
                if (node.Name == "Line")
                {
                    ICustomCodeLine line = action.NewCodeLine();
                    OpenActionLine(line, node);
                }
            }               
        }
        /*!
         * 
         */
        private void OpenActionLine(ICustomCodeLine line, XmlNode xmlNode)
        {
            XmlNode xmlNode1 = xmlNode["Opcode"];
            line.SetOpcode((CCustomActionCode)Enum.Parse(typeof(CCustomActionCode), xmlNode1.InnerText));
            //
            xmlNode1 = xmlNode["Params"];
            foreach (XmlNode node in xmlNode1.ChildNodes)
            {
                if (node.Name == "GUID")                
                    line.SetGuidParam(new Guid(node.InnerText));                
            }     
        }

        private void OpenInternalTagList(ICustomInternalTagList tagList, XmlNode xmlNode)
        {
            

            XmlNode xmlNode1, xmlNode2, xmlNode4;
           

            xmlNode1 = xmlNode["InternalTags"];

            OpenObject(tagList, xmlNode1);
            
            xmlNode2 = xmlNode1["Objects"];
            foreach (XmlNode xmlNode3 in xmlNode2.ChildNodes)
            {                
                xmlNode4 = xmlNode3.Attributes["ClassName"];
                if (xmlNode4.InnerText == "CDesignValuesTag" || xmlNode4.InnerText == "CDesignDemoTag")
                {
                    OpenDemoTag((ICustomDemoTag)tagList.AddDemoTag(), xmlNode3);                    
                }
                else if (xmlNode4.InnerText == "CDesignMemoryTag" || xmlNode4.InnerText == "CDesignSRAMTag")
                {
                    OpenMemoryTag((ICustomSRAMTag)tagList.AddSRAMTag(), xmlNode3);
                }
                else if (xmlNode4.InnerText == "CDesignTimerTag")
                {
                    OpenTimerTag(tagList.NewTimerTag(), xmlNode3);
                }
                else if (xmlNode4.InnerText == "CDesignGroupOfInternalTags")
                {
                    OpenInternalGroupTag((ICustomInternalTagList)tagList.AddGroup(), xmlNode3);
                }
                else
                {
                    throw new NotImplementedException(xmlNode4.InnerText);
                }              
            }
             
        }
        
        private void OpenInternalGroupTag(ICustomInternalTagList tagList, XmlNode xmlNode)
        {            
            XmlNode xmlNode1, xmlNode2, xmlNode4;
            
            xmlNode1 = xmlNode;

            OpenObject(tagList, xmlNode1);

            xmlNode2 = xmlNode1["Objects"];
            foreach (XmlNode xmlNode3 in xmlNode2.ChildNodes)
            {                
                xmlNode4 = xmlNode3.Attributes["ClassName"];
                if (xmlNode4.InnerText == "CDesignValuesTag" || xmlNode4.InnerText == "CDesignDemoTag")
                {                    
                    OpenDemoTag((ICustomDemoTag)tagList.AddDemoTag(), xmlNode3);
                }
                else if (xmlNode4.InnerText == "CDesignMemoryTag" || xmlNode4.InnerText == "CDesignSRAMTag")
                {                    
                    OpenMemoryTag((ICustomSRAMTag)tagList.AddSRAMTag(), xmlNode3);
                }
                else if (xmlNode4.InnerText == "CDesignTimerTag")
                {
                    OpenTimerTag(tagList.NewTimerTag(), xmlNode3);
                }
                else if (xmlNode4.InnerText == "CDesignGroupOfInternalTags")
                {                    
                    OpenInternalGroupTag((ICustomInternalTagList)tagList.AddGroup(), xmlNode3);
                }
                else
                {
                    throw new NotImplementedException(xmlNode4.InnerText);
                }    
            }
        }

        private void OpenCustomTag(ICustomTag cutomTag, XmlNode xmlNode)
        {
            OpenObject(cutomTag, xmlNode);
            //
            string dt = ReadProperty("DataType", xmlNode);
            cutomTag.DataType = (CCustomDataType)Enum.Parse(typeof(CCustomDataType), dt);
            cutomTag.Value = ReadProperty("Value", xmlNode);            
            OpenAlarm(cutomTag.AlarmHi, xmlNode["AlarmHi"]);            
            OpenAlarm(cutomTag.AlarmLo, xmlNode["AlarmLo"]);
        }
        /*!
         * Abre tag demo.
         * Adiciona GUID na Tabela de referencias
         * @param demoTag Referencia para interface
         * @param xmlNode Referencia para node
         */
        private void OpenDemoTag(ICustomDemoTag valuesTag, XmlNode xmlNode)
        {
            //
            OpenCustomTag(valuesTag, xmlNode);                                 
            //
            valuesTag.Type = (CDemoType)Enum.Parse(typeof(CDemoType), xmlNode["Type"].InnerText);
            //
            valuesTag.MaxValue = float.Parse(xmlNode["MaxValue"].InnerText);
            //
            valuesTag.MinValue = float.Parse(xmlNode["MinValue"].InnerText);            
            //
            valuesTag.Scan = int.Parse(xmlNode["Scan"].InnerText);
            //
            valuesTag.Enabled = Boolean.Parse(xmlNode["Enabled"].InnerText);
            //
            valuesTag.Increment = float.Parse(xmlNode["Increment"].InnerText);            
        }
        /*!
         * Abre tag sram.
         * Adiciona GUID na Tabela de referencias
         * @param demoTag Referencia para interface
         * @param xmlNode Referencia para node
         */
        private void OpenMemoryTag(ICustomSRAMTag memoryTag, XmlNode xmlNode)
        {            
            //
            OpenCustomTag(memoryTag, xmlNode);
        }

        private void OpenTimerTag(ICustomTimerTag timerTag, XmlNode xmlNode)
        {
            //
            OpenCustomTag(timerTag, xmlNode);
            timerTag.MaxValue = TimeSpan.Parse(ReadProperty("MaxValue", xmlNode));
            timerTag.MinValue = TimeSpan.Parse(ReadProperty("MinValue", xmlNode));
        }
        private void OpenPropertyTag(ICustomPropertyTag propertyTag, XmlNode xmlNode)
        {
            //
            OpenCustomTag(propertyTag, xmlNode);
            //
            propertyTag.SetGuidReference(new Guid(xmlNode["Reference"].InnerText));
            //
            propertyTag.PropertyName = xmlNode["PropertyName"].InnerText;            
        }

        private void OpenPropertyTagList(ICustomPropertyTagList tagList, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2;

            xmlNode1 = xmlNode["PropertyTagList"];

            OpenObject(tagList, xmlNode1);

            xmlNode2 = xmlNode1["Objects"];
                        
            foreach (XmlNode xmlNode3 in xmlNode2.ChildNodes)
            {
                OpenPropertyTag(tagList.AddTag(), xmlNode3);
            }
        }

        private void OpenAlarm(ICustomAlarm alarm, XmlNode xmlNode)
        {
            OpenObject(alarm, xmlNode);
            //
            alarm.AlarmMessage = xmlNode["AlarmMessage"].InnerText;            
            //            
            alarm.Enabled = bool.Parse(xmlNode["Enabled"].InnerText);
            //            
            alarm.Value = float.Parse(xmlNode["Value"].InnerText);
        }

        private void OpenBitmapList(ICustomBitmapList bitmapList, XmlNode xmlNode)
        {
            ICustomBitmapItem customBitmap;
            XmlNode xmlNode1, xmlNode2, xmlNode3;
            //
            bitmapList.Open(project.FileName);
            //
            xmlNode1 = xmlNode["BitmapList"];

            bitmapList.Name = xmlNode1["Name"].InnerText;
            xmlNode2 = xmlNode1["Objects"];
            for (int I = 0; I < xmlNode2.ChildNodes.Count; I++)
            {
                customBitmap = (ICustomBitmapItem)bitmapList.AddBitmap();

                xmlNode3 = xmlNode2.ChildNodes[I];
                //
                if (xmlNode3["GUID"] != null)
                {                    
                    try
                    {
                        customBitmap.SetGUID(new Guid(xmlNode3["GUID"].InnerText));
                        CHashObjects.ObjectDictionary.Add(customBitmap.GUID, customBitmap);
                    }
                    catch 
                    {
                        customBitmap.SetGUID(Guid.NewGuid());
                        CHashObjects.ObjectDictionary.Add(customBitmap.GUID, customBitmap);                        
                    }
                }
                customBitmap.Name = xmlNode3["Name"].InnerText;
                customBitmap.Position = int.Parse(xmlNode3["Position"].InnerText);
            }
        }
        private void OpenNetwork(ICustomNetwork network, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2;
            ICustomSlave slave;

            xmlNode1 = xmlNode["Network"];
            //
            network.Name = xmlNode1["Name"].InnerText;
            //
            xmlNode2 = xmlNode1["Slaves"];
            for (int i = 0; i < xmlNode2.ChildNodes.Count; i++)
            {
                slave = (ICustomSlave)network.NewSlave();
                OpenSlave(slave, xmlNode2.ChildNodes[i]);
            }
        }
        private void OpenSlave(ICustomSlave slave, XmlNode xmlNode)
        {
            XmlNode xmlNode1;
            //
            OpenObject(slave, xmlNode);            
            //
            slave.Address = int.Parse(xmlNode["Address"].InnerText);
            //
            slave.Protocol = (CModbusType)Enum.Parse(typeof(CModbusType), xmlNode["Protocol"].InnerText);
            //
            xmlNode1 = xmlNode["SerialPort"];
            slave.COMClientConfig.COM = xmlNode1["PortName"].InnerText;
            slave.COMClientConfig.BaudRate = (CBaudRate)Enum.Parse(typeof(CBaudRate), xmlNode1["BaudRate"].InnerText);
            slave.COMClientConfig.DataBits = (CDataBits)Enum.Parse(typeof(CDataBits), xmlNode1["DataBits"].InnerText);
            slave.COMClientConfig.StopBits = (StopBits)Enum.Parse(typeof(StopBits), xmlNode1["StopBits"].InnerText);
            slave.COMClientConfig.Parity = (Parity)Enum.Parse(typeof(Parity), xmlNode1["Parity"].InnerText);
            //
            xmlNode1 = xmlNode["TCPClient"];
            slave.TCPClientConfig.Address = xmlNode1["IPAddress"].InnerText;
            slave.TCPClientConfig.Port = int.Parse(xmlNode1["Port"].InnerText);
            //
            xmlNode1 = xmlNode["Objects"];
            OpenExternalOrGroup(slave, xmlNode1);            
        }

        private void OpenExternalTag(ICustomExternalTag extTag, XmlNode xmlNode)
        {
            OpenObject(extTag,xmlNode);
            
                                   
            //
            extTag.Address = Convert.ToInt32(xmlNode["Address"].InnerText);
            //
            if (xmlNode["ArraySize"] != null)
                extTag.ArraySize = Convert.ToInt32(xmlNode["ArraySize"].InnerText);
            //
            if (xmlNode["Items"] != null)
            {
                XmlNode xmlNode1 = xmlNode["Items"];
                for (int i = 0; i < xmlNode1.ChildNodes.Count; i++)
                {
                    ICustomExternalTagItem tagItem = (ICustomExternalTagItem)extTag.ObjectList[i];
                    tagItem.SetGUID(new Guid(xmlNode1.ChildNodes[i].Attributes["GUID"].InnerText));
                    CHashObjects.ObjectDictionary.Add(tagItem.GUID, tagItem);
                }
            }
        }

        private void OpenExtTagGroup(ICustomGroupTags group, XmlNode xmlNode)
        {
            XmlNode xmlNode1;

            OpenObject(group, xmlNode);
            xmlNode1 = xmlNode["Objects"];
            OpenExternalOrGroup(group, xmlNode1);            
        }

        private void OpenExternalOrGroup(ICustomGroupTags group, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode3;            

            xmlNode1 = xmlNode;
            foreach (XmlNode xmlNode2 in xmlNode1.ChildNodes)
            {                
                xmlNode3 = xmlNode2.Attributes["ClassName"];
                if (xmlNode3.InnerText == "CDesignExternalTag")
                    OpenExternalTag((ICustomExternalTag)group.AddTag(), xmlNode2);
                else if (xmlNode3.InnerText == "CDesignGroupOfExternalTags")
                    OpenExtTagGroup((ICustomGroupTags)group.AddGroup(), xmlNode2);
            }
        }

        

        private void OpenAlarmsManager(ICustomAlarmsManager alarmsManager, XmlNode xmlNode)
        {
            XmlNode xmlNode1;

            xmlNode1 = xmlNode["AlarmsManager"];
            if (xmlNode1 != null)
            {
                //
                alarmsManager.Name = xmlNode1["Name"].InnerText;
                //
                alarmsManager.ShowPopup = bool.Parse(xmlNode1["ShowPopup"].InnerText);
                //
                alarmsManager.TimeOff = int.Parse(xmlNode1["TimeOff"].InnerText);
                //
                alarmsManager.TimeOn = int.Parse(xmlNode1["TimeOn"].InnerText);
            }
        }
        /*!
         * Paste Ctrl+V
         */
        public ArrayList OpenScreenObjects(ICustomScreen screen,string textXml)
        {
            CHashObjects.ObjectDictionary.Clear();

            xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(textXml);

            if (xmlDocument.DocumentElement.Name == "ScreenObjects")
                return OpenObjectsOfScreen(screen, xmlDocument.DocumentElement);            
            else
                return null;
        }
    }
}
