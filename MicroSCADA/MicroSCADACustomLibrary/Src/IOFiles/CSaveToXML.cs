using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.Drawing;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADACustomLibrary.Src.IOFiles
{
    public class CSaveToXML : CIOFileXML
    {
        /*!
         * Construtor
         * @param project
         */
        public CSaveToXML(ICustomProject Project)
            : base(Project)
        {

        }
        public CSaveToXML()
            : this(null)
        {

        }
        /*!
         * Destrutor
         */
        ~CSaveToXML() { }      
        /*!
         * Salva projeto para .xml 
         */
        public void Save()
        {
            SaveToXmlDocument();
            project.SetHashMD5(CXMLUtils.GetHashMD5(xmlDocument));
            xmlDocument.Save(project.FileName);
        }
        /*!
         * 
         */
        public void SaveAs(String FileName)
        {
            project.SetFileName(FileName);
            Save();
        }
        /*!
         * 
         */
        public void Save(String NewHashMD5)
        {
            project.SetHashMD5(NewHashMD5);
            xmlDocument.Save(project.FileName);
        }
        /*!
         * Calcula e retorna hash do arquivo
         */
        public String GetHashMD5()
        {
            SaveToXmlDocument();
            return CXMLUtils.GetHashMD5(xmlDocument);
        }
        /*!
         * Salva projeto para .xml sem descarregar no disco
         */
        private void SaveToXmlDocument()
        {
            XmlNode rootNode;
            XmlDeclaration xmldecl;

            xmldecl = xmlDocument.CreateXmlDeclaration(XML_VERSION, XML_ENCODING, null);
            xmlDocument.AppendChild(xmldecl);

            rootNode = xmlDocument.CreateNode(XmlNodeType.Element, "Project", "");
            xmlDocument.AppendChild(rootNode);
            //
            WriteProperty(GUID_ID, project.GUID.ToString(), rootNode);
            WriteProperty("Name", project.Name, rootNode);
            WriteProperty("Description", project.Description, rootNode);
            WritePropertyToCDATA("Comment", project.Comment, rootNode);            
            //
            SaveScreens(project.Screens, rootNode);
            //
            SavePopupScreens(project.PopupScreens, rootNode);
            //
            SaveInternalTagList(project.InternalTagList, rootNode);
            //
            SavePropertyTagList(project.PropertyTagList, rootNode);
            //
            SaveNetwork(project.Network, rootNode);
            //
            SaveAlarmsManager(project.AlarmsManager, rootNode);
            //
            SaveActionList(project.ActionList, rootNode); 
            //
            SaveBitmapList(project.BitmapList, rootNode);                       
        }
        /*!
         * Salva cor em formato RGB
         * @param color cor do objeto
         * @param xmlNode node parent 
         */
        private void SaveColor(Color color,XmlNode xmlNode)
        {
            XmlAttribute xmlAttribute1;

            xmlAttribute1 = xmlDocument.CreateAttribute("R");
            xmlAttribute1.InnerText = color.R.ToString();
            xmlNode.Attributes.Append(xmlAttribute1);
            xmlAttribute1 = xmlDocument.CreateAttribute("G");
            xmlAttribute1.InnerText = color.G.ToString();
            xmlNode.Attributes.Append(xmlAttribute1);
            xmlAttribute1 = xmlDocument.CreateAttribute("B");
            xmlAttribute1.InnerText = color.B.ToString();
            xmlNode.Attributes.Append(xmlAttribute1);
        }
        /*!
         * 
         */
        private void SaveFont(Font font, XmlNode xmlNode)
        {
            XmlNode xmlNode1;

            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Name", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = font.Name;
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Height", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = font.Height.ToString();

            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Size", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = font.Size.ToString();

            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Bold", "");
            xmlNode.AppendChild(xmlNode1);
            if (font.Bold)
                xmlNode1.InnerText = FontStyle.Bold.ToString();
            else
                xmlNode1.InnerText = FontStyle.Regular.ToString();

            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Italic", "");
            xmlNode.AppendChild(xmlNode1);
            if (font.Italic)
                xmlNode1.InnerText = FontStyle.Italic.ToString();
            else
                xmlNode1.InnerText = FontStyle.Regular.ToString();

            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Strikeout", "");
            xmlNode.AppendChild(xmlNode1);
            if (font.Strikeout)
                xmlNode1.InnerText = FontStyle.Strikeout.ToString();
            else
                xmlNode1.InnerText = FontStyle.Regular.ToString();

            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Underline", "");
            xmlNode.AppendChild(xmlNode1);
            if (font.Underline)
                xmlNode1.InnerText = FontStyle.Underline.ToString();
            else
                xmlNode1.InnerText = FontStyle.Regular.ToString();
        }
        /*!
         * 
         */
        private XmlNode WriteProperty(string Name, XmlNode Node)
        {
            XmlNode xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, Name, "");            
            return Node.AppendChild(xmlNode1);            
        }
        /*!
         * 
         */
        private void WriteProperty(string Name, string Value, XmlNode Node)
        {
            XmlNode xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, Name, "");
            xmlNode1.InnerText = Value;
            Node.AppendChild(xmlNode1);            
        }
        /*!
         * 
         */
        private void WriteProperty(string Name, int Value, XmlNode Node)
        {
            XmlNode xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, Name, "");
            xmlNode1.InnerText = Value.ToString();
            Node.AppendChild(xmlNode1);
        }
        /*!
         * 
         */
        private void WriteProperty(string Name, float Value, XmlNode Node)
        {
            XmlNode xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, Name, "");
            xmlNode1.InnerText = Value.ToString();
            Node.AppendChild(xmlNode1);
        }
        /*!
         * 
         */
        private void WriteProperty(string Name, double Value, XmlNode Node)
        {
            XmlNode xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, Name, "");
            xmlNode1.InnerText = Value.ToString();
            Node.AppendChild(xmlNode1);
        }
        /*!
         * 
         */
        private void WriteProperty(string Name, bool Value, XmlNode Node)
        {
            XmlNode xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, Name, "");
            xmlNode1.InnerText = Value.ToString();
            Node.AppendChild(xmlNode1);
        }
        private void WriteProperty(string Name, Type Value, XmlNode Node)
        {
            XmlNode xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, Name, "");
            xmlNode1.InnerText = Value.ToString();
            Node.AppendChild(xmlNode1);
        }
        /*!
         * 
         */
        private void WritePropertyToCDATA(string Name, string Value, XmlNode Node)
        {
            XmlNode xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, Name, "");
            XmlCDataSection xmlCDataSection1 = xmlDocument.CreateCDataSection(Value);
            xmlNode1.AppendChild(xmlCDataSection1);
            Node.AppendChild(xmlNode1);
        }
        /*!
         * 
         */
        private void WriteProperty(string Name, Color Value, XmlNode Node)
        {
            XmlNode xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, Name, "");
            SaveColor(Value, xmlNode1);
            Node.AppendChild(xmlNode1);
        }
        /*!
         * 
         */
        private void WriteProperty(string Name, Font Value, XmlNode Node)
        {
            XmlNode xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, Name, "");
            SaveFont(Value, xmlNode1);
            Node.AppendChild(xmlNode1);
        }
        /*!
         * 
         */
        private void SaveScreens(ICustomDefaultScreenList screens, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2;
            XmlAttribute xmlAttribute1;
            //            
            xmlNode1 = WriteProperty("Screens", xmlNode);            
            SaveObject(screens, xmlNode1);
            WriteProperty("Width", screens.Width, xmlNode1);
            WriteProperty("Height", screens.Height, xmlNode1);            
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Objects", "");
            xmlAttribute1 = xmlDocument.CreateAttribute("Count");
            xmlAttribute1.InnerText = screens.ObjectList.Count.ToString();
            xmlNode2.Attributes.Append(xmlAttribute1);
            xmlNode1.AppendChild(xmlNode2);
            //
            foreach (ICustomScreen screen in screens.ObjectList)
            {                
                SaveScreen(screen, xmlNode2);
            }
        }
        /*!
         * 
         */
        private void SaveScreen(ICustomScreen screen, XmlNode xmlNode)
        {
            XmlNode xmlNode3, xmlNode4;
            
            xmlNode3 = WriteProperty("Screen", xmlNode);            
            SaveObject(screen, xmlNode3);
            
            //
            xmlNode4 = xmlDocument.CreateNode(XmlNodeType.Element, "Color", "");
            xmlNode3.AppendChild(xmlNode4);
            SaveColor(screen.BackColor, xmlNode4);
            //
            ICustomBitmapItem bmpItem = screen.BitmapItem;
            xmlNode4 = xmlDocument.CreateNode(XmlNodeType.Element, "BitmapItem", "");
            xmlNode3.AppendChild(xmlNode4);
            if (bmpItem != null)
                xmlNode4.InnerText = bmpItem.GUID.ToString();
            else
                xmlNode4.InnerText = "null"; 
            
            SaveObjectsOfScreen(screen.ObjectList, xmlNode3);
        }
        /*!
         * 
         */
        private void SavePopupScreens(ICustomScreenList screens, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2;
            XmlAttribute xmlAttribute1;
            //            
            xmlNode1 = WriteProperty("PopupScreens", xmlNode);
            SaveObject(screens, xmlNode1);            
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Objects", "");
            xmlAttribute1 = xmlDocument.CreateAttribute("Count");
            xmlAttribute1.InnerText = screens.ObjectList.Count.ToString();
            xmlNode2.Attributes.Append(xmlAttribute1);
            xmlNode1.AppendChild(xmlNode2);
            //
            foreach (ICustomPopUpScreen screen in screens.ObjectList)
            {
                SavePopupScreen(screen, xmlNode2);
            }
        }
        /*!
         * 
         */
        private void SavePopupScreen(ICustomPopUpScreen screen, XmlNode xmlNode)
        {
            XmlNode xmlNode3, xmlNode4;
            
            xmlNode3 = WriteProperty("PopupScreen", xmlNode);            
            SaveObject(screen, xmlNode3);
            WriteProperty("Left", screen.Left.ToString(), xmlNode3);
            WriteProperty("Top", screen.Top.ToString(), xmlNode3);
            WriteProperty("Width", screen.Width.ToString(), xmlNode3);
            WriteProperty("Height", screen.Height.ToString(), xmlNode3);
            WriteProperty("Color", screen.BackColor, xmlNode3);
            WriteProperty("ShowTitleBar", screen.ShowTitleBar.ToString(), xmlNode3);
            WriteProperty("Title", screen.Title, xmlNode3); 
            xmlNode4 = WriteProperty("BitmapItem", xmlNode3);             
            if (screen.BitmapItem != null)
                xmlNode4.InnerText = screen.BitmapItem.GUID.ToString();
            else
                xmlNode4.InnerText = "null";
            //
            SaveObjectsOfScreen(screen.ObjectList, xmlNode3);
        }
        /*!
         * Salva objectos da tela 
         * 
         */
        private void SaveObjectsOfScreen(ArrayList objectList, XmlNode xmlNode)        
        {
            XmlNode xmlNode1, xmlNode2;
            XmlAttribute xmlAttribute1;

            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Objects", "");
            xmlNode.AppendChild(xmlNode1);
            xmlAttribute1 = xmlDocument.CreateAttribute("Count");
            xmlAttribute1.InnerText = objectList.Count.ToString();
            xmlNode1.Attributes.Append(xmlAttribute1);
            for (int i = 0; i < objectList.Count; i++)
            {
                ICustomScreenObject screenObject;

                screenObject = (ICustomScreenObject)objectList[i];

                xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Object", "");
                xmlNode1.AppendChild(xmlNode2);
                xmlAttribute1 = xmlDocument.CreateAttribute("ClassName");
                xmlAttribute1.InnerText = screenObject.GetType().Name;
                xmlNode2.Attributes.Append(xmlAttribute1);

                if (screenObject is ICustomText)
                    SaveText((ICustomText)screenObject, xmlNode2);
                else if (screenObject is ICustomPicture)
                    SavePicture((ICustomPicture)screenObject, xmlNode2);
                else if (screenObject is ICustomShape)
                    SaveShape((ICustomShape)screenObject, xmlNode2);
                else if (screenObject is ICustomAlphaNumeric)
                    SaveAlphaNumeric((ICustomAlphaNumeric)screenObject, xmlNode2);
                else if (screenObject is ICustomTrendChart)
                    SaveTrendChart((ICustomTrendChart)screenObject, xmlNode2);
                else if (screenObject is ICustomButton)
                    SaveButton((ICustomButton)screenObject, xmlNode2);
                else if (screenObject is ICustomBargraph)
                    SaveBargraph((ICustomBargraph)screenObject, xmlNode2);
                else if (screenObject is ICustomDinamicText)
                    SaveDinamicText((ICustomDinamicText)screenObject, xmlNode2);
                else if (screenObject is ICustomAnimation)
                    SaveAnimation((ICustomAnimation)screenObject, xmlNode2);
                else if (screenObject is ICustomCheckBox)
                    SaveCheckBox((ICustomCheckBox)screenObject, xmlNode2);
                else if (screenObject is ICustomRadioGroup)
                    SaveRadioGroup((ICustomRadioGroup)screenObject, xmlNode2);
                else if (screenObject is ICustomMeter)
                    SaveMeter((ICustomMeter)screenObject, xmlNode2);
                else
                    //    throw new NotImplementedException(screenObject.GetType().Name);
                    OnIOError(new IOFileErrorEventArgs("Save object :"+screenObject.GetType().Name));
            }
        }
        
        /*!
         * Salva configurações comuns dos objetos 
         * @param cutomObject
         * @param xmlNode
         */
        private void SaveObject(ICustomSystem cutomObject, XmlNode xmlNode)
        {       
            WriteProperty("GUID", cutomObject.GUID.ToString(), xmlNode);        
            WriteProperty("Name", cutomObject.Name, xmlNode);        
            WriteProperty("Description", cutomObject.Description, xmlNode);
        }
        /*!
         * Salva configurações comuns dos objetos de tela
         */
        private void SaveScreenObject(ICustomScreenObject screenObject, XmlNode xmlNode)
        {            
            SaveObject(screenObject, xmlNode);
            //
            WriteProperty("Left", screenObject.Left.ToString(), xmlNode);
            WriteProperty("Top", screenObject.Top.ToString(), xmlNode);
            WriteProperty("Width", screenObject.Width.ToString(), xmlNode);
            WriteProperty("Height", screenObject.Height.ToString(), xmlNode);
            WriteProperty("Border", screenObject.Border.ToString(), xmlNode);
            WriteProperty("Frame", screenObject.Frame.ToString(), xmlNode);                      
        }

        private void SaveText(ICustomText text, XmlNode xmlNode)
        {
            //Propriedades comuns aos objetos da tela
            SaveScreenObject(text, xmlNode);            
            //
            WritePropertyToCDATA("Text", text.TextToString(), xmlNode);            
            WriteProperty("BackColor", text.BackColor, xmlNode);
            WriteProperty("TextFontColor", text.TextFontColor, xmlNode);
            WriteProperty("TextFont", text.TextFont, xmlNode);
            WriteProperty("Alignment", text.Alignment.ToString(), xmlNode);            
        }

        private void SaveShape(ICustomShape shape, XmlNode xmlNode)
        {            
            //
            SaveScreenObject(shape, xmlNode);            
            //
            WriteProperty("BackColor", shape.BackColor, xmlNode);
            WriteProperty("BorderColor", shape.BorderColor, xmlNode);
            WriteProperty("BorderWidth", shape.BorderWidth.ToString(), xmlNode);
            WriteProperty("Radius", shape.Radius.ToString(), xmlNode);
            WriteProperty("ShapeType", shape.ShapeType.ToString(), xmlNode);                     
        }
        
        private void SavePicture(ICustomPicture picture, XmlNode xmlNode)
        {
            XmlNode xmlNode1;
            
            SaveScreenObject(picture, xmlNode);            
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "BackColor", "");
            xmlNode.AppendChild(xmlNode1);            
            SaveColor(picture.BackColor, xmlNode1);  
            //
            ICustomBitmapItem bmpItem = picture.BitmapItem;
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "BitmapItem", "");
            xmlNode.AppendChild(xmlNode1);
            if(bmpItem != null)                
                xmlNode1.InnerText = bmpItem.GUID.ToString();
            else
                xmlNode1.InnerText = "null";        
        }
        
        private void SaveAlphaNumeric(ICustomAlphaNumeric alphaNumeric, XmlNode xmlNode)
        {
            XmlNode xmlNode1;
        
            SaveScreenObject(alphaNumeric, xmlNode);                                   
            //
            WriteProperty("Font", alphaNumeric.Font, xmlNode);
            WriteProperty("FontColor", alphaNumeric.FontColor, xmlNode);
            WriteProperty("BackColor", alphaNumeric.BackColor, xmlNode);
            WriteProperty("FieldType", alphaNumeric.FieldType.ToString(), xmlNode);
            WriteProperty("DecimalCount", alphaNumeric.DecimalCount, xmlNode);
            WriteProperty("FormatType", alphaNumeric.ValueFormat.ToString(), xmlNode);
            WriteProperty("MaxValue", alphaNumeric.MaxValue, xmlNode);
            WriteProperty("MinValue", alphaNumeric.MinValue, xmlNode);
            WriteProperty("TextAlign", alphaNumeric.TextAlign.ToString(), xmlNode);
            //tag            
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "TagValue", "");
            xmlNode.AppendChild(xmlNode1);
            if (alphaNumeric.TagValue != null)
                xmlNode1.InnerText = alphaNumeric.TagValue.GUID.ToString();                                
            else
                xmlNode1.InnerText = "null";
        }

        private void SaveMeter(ICustomMeter meter, XmlNode xmlNode)
        {
            SaveScreenObject(meter, xmlNode);
            //
            WriteProperty("BackColor", meter.BackColor, xmlNode);
            WriteProperty("StartAngle", meter.StartAngle, xmlNode);
            WriteProperty("SweepAngle", meter.SweepAngle, xmlNode);
            WriteProperty("Title", meter.Title, xmlNode);
            WriteProperty("TitleFont", meter.TitleFont, xmlNode);
            WriteProperty("TitleFontColor", meter.TitleFontColor, xmlNode);
            WriteProperty("TitleEnabled", meter.TitleEnabled, xmlNode);
            WriteProperty("ScaleFont", meter.ScaleFont, xmlNode);
            WriteProperty("ScaleFontColor", meter.ScaleFontColor, xmlNode);
            WriteProperty("PointerWidth", meter.PointerWidth, xmlNode);
            WriteProperty("MaxValue", meter.MaxValue, xmlNode);
            WriteProperty("MinValue", meter.MinValue, xmlNode);
            WriteProperty("Direction", meter.Direction.ToString(), xmlNode);
            //tag   
            if (meter.TagValue != null)
                WriteProperty("TagValue", meter.TagValue.GUID.ToString(), xmlNode);
            else
                WriteProperty("TagValue", "null", xmlNode); 
        }

        private void SaveTrendChart(ICustomTrendChart trendChart, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2;
            XmlAttribute xmlAttribute1;

            SaveScreenObject(trendChart, xmlNode);  
            //
            WriteProperty("MaxY", trendChart.MaxValueY.ToString(), xmlNode);            
            WriteProperty("MinY", trendChart.MinValueY.ToString(), xmlNode);            
            WriteProperty("BufferSize", trendChart.BufferSize.ToString(), xmlNode);            
            WriteProperty("UpdateTime", trendChart.UpdateTime.ToString(), xmlNode);            
            WriteProperty("ChartAreaColor", trendChart.ChartAreaColor, xmlNode);            
            WriteProperty("PlotAreaColor", trendChart.PlotAreaColor, xmlNode);           
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Pens", "");            
            xmlNode.AppendChild(xmlNode1);
            xmlAttribute1 = xmlDocument.CreateAttribute("Count");
            xmlAttribute1.InnerText = trendChart.ObjectList.Count.ToString();
            xmlNode1.Attributes.Append(xmlAttribute1);
            //
            foreach (ICustomPenTrendChart pen in trendChart.ObjectList)
            {
                xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Pen", "");
                xmlNode1.AppendChild(xmlNode2);
                //
                WriteProperty("GUID", pen.GUID.ToString(), xmlNode2);
                WriteProperty("Name", pen.Name, xmlNode2);
                WriteProperty("Label", pen.Label, xmlNode2);
                WriteProperty("Color", pen.PenColor, xmlNode2);
                WriteProperty("Width", pen.Width.ToString(), xmlNode2);                
                //tag   
                if (pen.TagValue != null)
                    WriteProperty("TagValue", pen.TagValue.GUID.ToString(), xmlNode2);
                else
                    WriteProperty("TagValue", "null", xmlNode2); 
            }            
        }
        
        private void SaveBargraph(ICustomBargraph barGraph, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2, xmlNode3;
            XmlAttribute xmlAttribute1;
            //
            SaveScreenObject(barGraph, xmlNode);
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Orientation", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = barGraph.Orientation.ToString();
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "MaxValue", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = barGraph.MaxValue.ToString();
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "MinValue", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = barGraph.MinValue.ToString();
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Font", "");
            xmlNode.AppendChild(xmlNode1);
            SaveFont(barGraph.ScaleFont, xmlNode1);
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "FontColor", "");
            xmlNode.AppendChild(xmlNode1);
            SaveColor(barGraph.FontColor, xmlNode1);
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Bars", "");
            xmlNode.AppendChild(xmlNode1);
            xmlAttribute1 = xmlDocument.CreateAttribute("Count");
            xmlAttribute1.InnerText = barGraph.ObjectList.Count.ToString();
            xmlNode1.Attributes.Append(xmlAttribute1);
            //
            for (int i = 0; i < barGraph.ObjectList.Count; i++)
            {
                ICustomBargraphElement bar;

                bar = (ICustomBargraphElement)barGraph.ObjectList[i];

                xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Bar", "");
                xmlNode1.AppendChild(xmlNode2);
                //
                SaveObject(bar, xmlNode2);
                //
                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "Color", "");
                xmlNode2.AppendChild(xmlNode3);
                SaveColor(bar.BarColor, xmlNode3);
                //tag
                ICustomTag customTag = bar.TagValue;
                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "TagValue", "");
                xmlNode2.AppendChild(xmlNode3);

                if (customTag != null)
                    xmlNode3.InnerText = customTag.GUID.ToString();
                else
                    xmlNode3.InnerText = "null";
            }

        }

        private void SaveDinamicText(ICustomDinamicText dinamicText, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2, xmlNode3;
            XmlAttribute xmlAttribute1;
            //
            SaveScreenObject(dinamicText, xmlNode);
            //tag
            ICustomTag customTag = dinamicText.TagValue;
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "TagValue", "");
            xmlNode.AppendChild(xmlNode1);
            if (customTag != null)
                xmlNode1.InnerText = customTag.GUID.ToString();
            else
                xmlNode1.InnerText = "null";
            //zonas            
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Zones", "");
            xmlNode.AppendChild(xmlNode1);
            xmlAttribute1 = xmlDocument.CreateAttribute("Count");
            xmlAttribute1.InnerText = dinamicText.ObjectList.Count.ToString();
            xmlNode1.Attributes.Append(xmlAttribute1);
            for (int i = 0; i < dinamicText.ObjectList.Count; i++)
            {
                ICustomTextZone zone = (ICustomTextZone)dinamicText.ObjectList[i];

                xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Zone", "");
                xmlNode1.AppendChild(xmlNode2);
                //
                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "GUID", "");
                xmlNode2.AppendChild(xmlNode3);
                xmlNode3.InnerText = zone.GUID.ToString();
                //
                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "Name", "");
                xmlNode2.AppendChild(xmlNode3);
                xmlNode3.InnerText = zone.Name;
                //
                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "Text", "");
                xmlNode2.AppendChild(xmlNode3);
                XmlCDataSection xmlCDataSection = xmlDocument.CreateCDataSection(zone.TextToString());
                xmlNode3.AppendChild(xmlCDataSection);
                //
                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "TextFont", "");
                xmlNode2.AppendChild(xmlNode3);
                SaveFont(zone.TextFont, xmlNode3);
                //
                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "TextFontColor", "");
                xmlNode2.AppendChild(xmlNode3);
                SaveColor(zone.TextFontColor, xmlNode3);
                //
                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "BackColor", "");
                xmlNode2.AppendChild(xmlNode3);
                SaveColor(zone.BackColor, xmlNode3);
                //
                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "Alignment", "");
                xmlNode2.AppendChild(xmlNode3);
                xmlNode3.InnerText = zone.Alignment.ToString();
                //
                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "MaxValue", "");
                xmlNode2.AppendChild(xmlNode3);
                xmlNode3.InnerText = zone.MaxValue.ToString();
                //
                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "MinValue", "");
                xmlNode2.AppendChild(xmlNode3);
                xmlNode3.InnerText = zone.MinValue.ToString();
            }
        }

        private void SaveAnimation(ICustomAnimation animation, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2, xmlNode3;
            XmlAttribute xmlAttribute1;
            //
            SaveScreenObject(animation, xmlNode);
            //tag
            ICustomTag customTag = animation.TagValue;
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "TagValue", "");
            xmlNode.AppendChild(xmlNode1);
            if (customTag != null)
                xmlNode1.InnerText = customTag.GUID.ToString();
            else
                xmlNode1.InnerText = "null";
            //zonas            
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Zones", "");
            xmlNode.AppendChild(xmlNode1);
            xmlAttribute1 = xmlDocument.CreateAttribute("Count");
            xmlAttribute1.InnerText = animation.ObjectList.Count.ToString();
            xmlNode1.Attributes.Append(xmlAttribute1);
            for (int i = 0; i < animation.ObjectList.Count; i++)
            {
                ICustomPictureZone zone = (ICustomPictureZone)animation.ObjectList[i];

                xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Zone", "");
                xmlNode1.AppendChild(xmlNode2);
                //
                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "GUID", "");
                xmlNode2.AppendChild(xmlNode3);
                xmlNode3.InnerText = zone.GUID.ToString();
                //
                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "Name", "");
                xmlNode2.AppendChild(xmlNode3);
                xmlNode3.InnerText = zone.Name;
                //
                ICustomBitmapItem bmpItem = zone.BitmapItem;
                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "BitmapItem", "");
                xmlNode2.AppendChild(xmlNode3);
                if (bmpItem != null)
                    xmlNode3.InnerText = bmpItem.GUID.ToString();
                else
                    xmlNode3.InnerText = "null";
            }
        }

        private void SaveNetwork(ICustomNetwork network, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2;
            XmlAttribute xmlAttribute1;

            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Network", "");
            xmlNode.AppendChild(xmlNode1);
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Name", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode2.InnerText = network.Name;
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Slaves", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlAttribute1 = xmlDocument.CreateAttribute("Count");
            xmlAttribute1.InnerText = network.ObjectList.Count.ToString();
            xmlNode2.Attributes.Append(xmlAttribute1);
            for (int i = 0; i < network.ObjectList.Count; i++)
                SaveSlave((ICustomSlave)network.ObjectList[i], xmlNode2);
            
        }

        private void SaveSlave(ICustomSlave slave, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2, xmlNode3;
            XmlAttribute xmlAttribute1;

            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Slave", "");
            xmlNode.AppendChild(xmlNode1);            
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "GUID", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode2.InnerText = slave.GUID.ToString();
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Name", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode2.InnerText = slave.Name;
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Description", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode2.InnerText = slave.Description;
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Address", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode2.InnerText = slave.Address.ToString();
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Protocol", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode2.InnerText = slave.Protocol.ToString();
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "SerialPort", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "PortName", "");
            xmlNode2.AppendChild(xmlNode3);
            xmlNode3.InnerText = slave.COMClientConfig.COM;

            xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "BaudRate", "");
            xmlNode2.AppendChild(xmlNode3);
            xmlNode3.InnerText = slave.COMClientConfig.BaudRate.ToString();

            xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "DataBits", "");
            xmlNode2.AppendChild(xmlNode3);
            xmlNode3.InnerText = slave.COMClientConfig.DataBits.ToString();

            xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "StopBits", "");
            xmlNode2.AppendChild(xmlNode3);
            xmlNode3.InnerText = slave.COMClientConfig.StopBits.ToString();

            xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "Parity", "");
            xmlNode2.AppendChild(xmlNode3);
            xmlNode3.InnerText = slave.COMClientConfig.Parity.ToString();

            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "TCPClient", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "IPAddress", "");
            xmlNode2.AppendChild(xmlNode3);
            xmlNode3.InnerText = slave.TCPClientConfig.Address;

            xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "Port", "");
            xmlNode2.AppendChild(xmlNode3);
            xmlNode3.InnerText = slave.TCPClientConfig.Port.ToString();            
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Objects", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlAttribute1 = xmlDocument.CreateAttribute("Count");
            xmlAttribute1.InnerText = slave.ObjectList.Count.ToString();
            xmlNode2.Attributes.Append(xmlAttribute1);
            //
            for (int i = 0; i < slave.ObjectList.Count; i++)
            {
                ICustomObject customObject;

                customObject = (ICustomObject)slave.ObjectList[i];

                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "Object", "");
                xmlNode2.AppendChild(xmlNode3);
                xmlAttribute1 = xmlDocument.CreateAttribute("ClassName");
                xmlAttribute1.InnerText = customObject.GetType().Name;
                xmlNode3.Attributes.Append(xmlAttribute1);

                if (slave.ObjectList[i] is ICustomExternalTag)
                {
                    SaveExternalTag((ICustomExternalTag)slave.ObjectList[i], xmlNode3);
                }
                else if (slave.ObjectList[i] is ICustomGroupTags)
                {
                    SaveExternalTagGroup((ICustomGroupTags)slave.ObjectList[i], xmlNode3);
                }
            }

        }

        private void SaveExternalTag(ICustomExternalTag tag, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2;

            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "GUID", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = tag.GUID.ToString();
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Name", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = tag.Name;
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Description", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = tag.Description;
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Address", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = tag.Address.ToString();
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "ArraySize", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = tag.ArraySize.ToString();
            //
            //xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "AlarmHiHi", "");
            //xmlNode.AppendChild(xmlNode1);
            //SaveAlarm(tag.AlarmHiHi, xmlNode1);
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "AlarmHi", "");
            xmlNode.AppendChild(xmlNode1);
            SaveAlarm(tag.AlarmHi, xmlNode1);
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "AlarmLo", "");
            xmlNode.AppendChild(xmlNode1);
            SaveAlarm(tag.AlarmLo, xmlNode1);
            //
            //xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "AlarmLoLo", "");
            //xmlNode.AppendChild(xmlNode1);
            //SaveAlarm(tag.AlarmHiHi, xmlNode1);
            
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Items", "");
            xmlNode.AppendChild(xmlNode1);
            XmlAttribute xmlAttribute1 = xmlDocument.CreateAttribute("Count");
            xmlAttribute1.InnerText = tag.ObjectList.Count.ToString();
            xmlNode1.Attributes.Append(xmlAttribute1);
            for (int i = 0; i < tag.ObjectList.Count; i++)
            {
                ICustomExternalTagItem tagItem = (ICustomExternalTagItem)tag.ObjectList[i];

                xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "TagItem", "");
                xmlNode1.AppendChild(xmlNode2);
                xmlAttribute1 = xmlDocument.CreateAttribute("GUID");
                xmlAttribute1.InnerText = tagItem.GUID.ToString();
                xmlNode2.Attributes.Append(xmlAttribute1);
                
            }
        }

        private void SaveExternalTagGroup(ICustomGroupTags group, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2;
            XmlAttribute xmlAttribute1;

            
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Name", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = group.Name;
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Objects", "");
            xmlNode.AppendChild(xmlNode1);
            xmlAttribute1 = xmlDocument.CreateAttribute("Count");
            xmlAttribute1.InnerText = group.ObjectList.Count.ToString();
            xmlNode1.Attributes.Append(xmlAttribute1);
            //
            for (int i = 0; i < group.ObjectList.Count; i++)
            {
                ICustomObject customObject;

                customObject = (ICustomObject)group.ObjectList[i];

                xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Object", "");
                xmlNode1.AppendChild(xmlNode2);
                xmlAttribute1 = xmlDocument.CreateAttribute("ClassName");
                xmlAttribute1.InnerText = customObject.GetType().Name;
                xmlNode2.Attributes.Append(xmlAttribute1);

                if (group.ObjectList[i] is ICustomExternalTag)
                {
                    SaveExternalTag((ICustomExternalTag)group.ObjectList[i], xmlNode2);
                }
                else if (group.ObjectList[i] is ICustomGroupTags)
                {
                    SaveExternalTagGroup((ICustomGroupTags)group.ObjectList[i], xmlNode2);
                }
            }


        }
        
        private void SaveInternalTagList(ICustomInternalTagList tagList, XmlNode xmlNode)
        {
            
            XmlNode xmlNode1, xmlNode2, xmlNode3;
            XmlAttribute xmlAttribute1;

            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "InternalTags", "");
            xmlNode.AppendChild(xmlNode1);
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Name", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode2.InnerText = tagList.Name;            
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Objects", "");            
            xmlNode1.AppendChild(xmlNode2);
            xmlAttribute1 = xmlDocument.CreateAttribute("Count");
            xmlAttribute1.InnerText = tagList.ObjectList.Count.ToString();
            xmlNode2.Attributes.Append(xmlAttribute1);
            //
            for (int i = 0; i < tagList.ObjectList.Count; i++)
            {
                ICustomObject internalTag;

                internalTag = (ICustomObject)tagList.ObjectList[i];

                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "Object", "");
                xmlNode2.AppendChild(xmlNode3);
                xmlAttribute1 = xmlDocument.CreateAttribute("ClassName");
                xmlAttribute1.InnerText = internalTag.GetType().Name;
                xmlNode3.Attributes.Append(xmlAttribute1);

                if (internalTag is ICustomDemoTag)
                {
                    SaveDemoTag((ICustomDemoTag)internalTag, xmlNode3);                    
                }
                else if (internalTag is ICustomSRAMTag)
                {
                    SaveSRAMTag((ICustomSRAMTag)internalTag, xmlNode3);
                }
                else if (internalTag is ICustomTimerTag)
                {
                    SaveTimerTag((ICustomTimerTag)internalTag, xmlNode3);
                }
                else
                {
                    SaveInternalTagGroup((ICustomInternalTagList)internalTag, xmlNode3);
                }
            }           
        }
        
        private void SaveInternalTagGroup(ICustomInternalTagList tagList, XmlNode xmlNode)
        {

            XmlNode xmlNode1, xmlNode2;
            XmlAttribute xmlAttribute1;

            
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Name", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = tagList.Name;
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Objects", "");
            xmlNode.AppendChild(xmlNode1);
            xmlAttribute1 = xmlDocument.CreateAttribute("Count");
            xmlAttribute1.InnerText = tagList.ObjectList.Count.ToString();
            xmlNode1.Attributes.Append(xmlAttribute1);
            //
            for (int i = 0; i < tagList.ObjectList.Count; i++)
            {
                ICustomObject internalTag;

                internalTag = (ICustomObject)tagList.ObjectList[i];

                xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Object", "");
                xmlNode1.AppendChild(xmlNode2);
                xmlAttribute1 = xmlDocument.CreateAttribute("ClassName");
                xmlAttribute1.InnerText = internalTag.GetType().Name;
                xmlNode2.Attributes.Append(xmlAttribute1);

                if (internalTag is ICustomDemoTag)                
                    SaveDemoTag((ICustomDemoTag)internalTag, xmlNode2);                
                else if (internalTag is ICustomSRAMTag)                
                    SaveSRAMTag((ICustomSRAMTag)internalTag, xmlNode2);
                else if (internalTag is ICustomTimerTag)           
                    SaveTimerTag((ICustomTimerTag)internalTag, xmlNode2);           
                else                
                    SaveInternalTagGroup((ICustomInternalTagList)internalTag, xmlNode2);                
            }
        }

        private void SaveCustomTag(ICustomTag customTag, XmlNode xmlNode)
        {
            XmlNode xmlNode1;

            SaveObject(customTag, xmlNode);

            WriteProperty("DataType", customTag.DataType.ToString(), xmlNode);
            WriteProperty("Value", customTag.Value, xmlNode);            
            xmlNode1 = WriteProperty("AlarmHi", xmlNode);            
            SaveAlarm(customTag.AlarmHi, xmlNode1);
            xmlNode1 = WriteProperty("AlarmLo", xmlNode);
            SaveAlarm(customTag.AlarmLo, xmlNode1);            
        }

        private void SaveDemoTag(ICustomDemoTag demoTag, XmlNode xmlNode)
        {
            XmlNode xmlNode1;
            SaveCustomTag(demoTag, xmlNode);            
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Type", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = demoTag.Type.ToString();
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "MaxValue", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = demoTag.MaxValue.ToString();
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "MinValue", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = demoTag.MinValue.ToString();            
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Scan", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = demoTag.Scan.ToString();
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Enabled", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = demoTag.Enabled.ToString();
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Increment", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = demoTag.Increment.ToString();            
                        
        }

        private void SaveSRAMTag(ICustomSRAMTag sramTag, XmlNode xmlNode)
        {            
            SaveCustomTag(sramTag, xmlNode);                        
        }

        private void SaveTimerTag(ICustomTimerTag timerTag, XmlNode xmlNode)
        {
            SaveCustomTag(timerTag, xmlNode);
            WriteProperty("MaxValue", timerTag.MaxValue.ToString(), xmlNode);
            WriteProperty("MinValue", timerTag.MinValue.ToString(), xmlNode);
        }

        private void SavePropertyTag(ICustomPropertyTag propertyTag, XmlNode xmlNode)
        {
            XmlNode xmlNode1;

            SaveCustomTag(propertyTag, xmlNode);
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Reference", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = propertyTag.Reference.GUID.ToString();
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "PropertyName", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = propertyTag.PropertyName;
        }

        private void SavePropertyTagList(ICustomPropertyTagList tagList, XmlNode xmlNode)
        {

            XmlNode xmlNode1, xmlNode2, xmlNode3;
            XmlAttribute xmlAttribute1;
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "PropertyTagList", "");
            xmlNode.AppendChild(xmlNode1);
            //
            SaveObject(tagList, xmlNode1);                                    
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Objects", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlAttribute1 = xmlDocument.CreateAttribute("Count");
            xmlAttribute1.InnerText = tagList.ObjectList.Count.ToString();
            xmlNode2.Attributes.Append(xmlAttribute1);
            //
            foreach (ICustomPropertyTag propertyTag in tagList.ObjectList)
            {
                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "Object", "");
                xmlNode2.AppendChild(xmlNode3);
                SavePropertyTag(propertyTag, xmlNode3);               
            }
        }

        private void SaveAlarm(ICustomAlarm alarm, XmlNode xmlNode)
        {
            XmlNode xmlNode1;

            SaveObject(alarm, xmlNode);
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "AlarmMessage", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = alarm.AlarmMessage;
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Enabled", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = alarm.Enabled.ToString();
            //
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "Value", "");
            xmlNode.AppendChild(xmlNode1);
            xmlNode1.InnerText = alarm.Value.ToString();
        }

        private void SaveBitmapList(ICustomBitmapList bitmapList, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2, xmlNode3, xmlNode4;
            XmlAttribute xmlAttribute1;

            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "BitmapList", "");
            xmlNode.AppendChild(xmlNode1);
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Name", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode2.InnerText = bitmapList.Name;            
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Objects", "");            
            xmlNode1.AppendChild(xmlNode2);
            xmlAttribute1 = xmlDocument.CreateAttribute("Count");
            xmlAttribute1.InnerText = bitmapList.ObjectList.Count.ToString();
            xmlNode2.Attributes.Append(xmlAttribute1);
            //
            for (int i = 0; i < bitmapList.ObjectList.Count; i++)
            {
                ICustomBitmapItem customBitmap;

                customBitmap = (ICustomBitmapItem)bitmapList.ObjectList[i];

                xmlNode3 = xmlDocument.CreateNode(XmlNodeType.Element, "BitmapItem", "");
                xmlNode2.AppendChild(xmlNode3);

                xmlNode4 = xmlDocument.CreateNode(XmlNodeType.Element, "GUID", "");
                xmlNode3.AppendChild(xmlNode4);
                xmlNode4.InnerText = customBitmap.GUID.ToString();
                //
                xmlNode4 = xmlDocument.CreateNode(XmlNodeType.Element, "Name", "");
                xmlNode3.AppendChild(xmlNode4);
                xmlNode4.InnerText = customBitmap.Name;
                //
                xmlNode4 = xmlDocument.CreateNode(XmlNodeType.Element, "Position", "");
                xmlNode3.AppendChild(xmlNode4);
                xmlNode4.InnerText = customBitmap.Position.ToString();
            }               
        }

        private void SaveButton(ICustomButton button, XmlNode xmlNode)
        {
            SaveScreenObject(button, xmlNode);
            //
            WriteProperty("ButtonType", button.ButtonType.ToString(), xmlNode);
            WriteProperty("Text", button.Text, xmlNode);
            WriteProperty("TextFont", button.TextFont, xmlNode);
            WriteProperty("TextFontColor", button.TextFontColor, xmlNode);
            WriteProperty("BackColor", button.BackColor, xmlNode);
            WriteProperty("ValueOff", button.ValueOff, xmlNode);
            WriteProperty("ValueOn", button.ValueOn, xmlNode);
            WriteProperty("Jog", button.Jog, xmlNode);
            //
            if (button.ActionClick != null)
                WriteProperty("Action", button.ActionClick.GUID.ToString(), xmlNode);
            else
                WriteProperty("Action", "null", xmlNode);   
            //          
            if (button.TagValue != null)
                WriteProperty(ID_TAGVALUE, button.TagValue.GUID.ToString(), xmlNode);
            else
                WriteProperty(ID_TAGVALUE, "null", xmlNode); 
        }

        private void SaveCheckBox(ICustomCheckBox checkBox, XmlNode xmlNode)
        {
            //
            SaveScreenObject(checkBox, xmlNode);
            //
            WriteProperty("Checked", checkBox.Checked, xmlNode);
            WriteProperty("Caption", checkBox.Caption, xmlNode);
            WriteProperty("Font", checkBox.Font, xmlNode);
            WriteProperty("FontColor", checkBox.FontColor, xmlNode);
            WriteProperty("BackColor", checkBox.BackColor, xmlNode);            
            if (checkBox.TagValue != null)
                WriteProperty(ID_TAGVALUE, checkBox.TagValue.GUID.ToString(), xmlNode);                
            else
                WriteProperty(ID_TAGVALUE, "null", xmlNode);                
        }

        private void SaveRadioGroup(ICustomRadioGroup radioGroup, XmlNode xmlNode)
        {
            //
            SaveScreenObject(radioGroup, xmlNode);
            //            
            WriteProperty("Font", radioGroup.Font, xmlNode);
            WriteProperty("FontColor", radioGroup.FontColor, xmlNode);
            WriteProperty("BackColor", radioGroup.BackColor, xmlNode);
            WriteProperty("Count", radioGroup.Count, xmlNode);
            WriteProperty("CheckedButtonIndex", radioGroup.CheckedButtonIndex, xmlNode);
            XmlNode xmlNode1 = WriteProperty("Items", xmlNode);            
            foreach (ICustomRadioButton item in radioGroup.Items)
            {
                XmlNode xmlNode2 = WriteProperty("Item", xmlNode1);                
                WriteProperty("Checked", item.Checked, xmlNode2);
                WriteProperty("Caption", item.Caption, xmlNode2);
            }
            if (radioGroup.TagValue != null)
                WriteProperty(ID_TAGVALUE, radioGroup.TagValue.GUID.ToString(), xmlNode);
            else
                WriteProperty(ID_TAGVALUE, "null", xmlNode);
        }

        private void SaveActionList(ICustomActionList actionList, XmlNode xmlNode)
        {
            //root actionList
            XmlNode xmlNode1 = WriteProperty("ActionList", xmlNode);
            //
            SaveObject(actionList, xmlNode1);
            XmlNode xmlNode2 = WriteProperty("Items", xmlNode1);
            foreach (ICustomAction action in actionList.ObjectList)                   
                SaveAction(action, xmlNode2);            
        }

        private void SaveAction(ICustomAction action, XmlNode xmlNode)
        {
            //root action
            XmlNode xmlNode1 = WriteProperty("Action", xmlNode);
            //childs
            SaveObject(action, xmlNode1);
            XmlNode xmlNode2 = WriteProperty("Items", xmlNode1);
            foreach (ICustomCodeLine line in action.ObjectList)                     
                SaveActionLine(line, xmlNode2);            
        }

        private void SaveActionLine(ICustomCodeLine line,XmlNode xmlNode)
        {
            //root line
            XmlNode xmlNode1 = WriteProperty("Line", xmlNode);
            //
            WriteProperty("Opcode", line.Opcode.ToString(), xmlNode1);
            XmlNode xmlNode2 = WriteProperty("Params", xmlNode1);
            foreach (ICustomObject param in line.ParamList)
            {                
                if (param != null)
                    WriteProperty("GUID", param.GUID.ToString(), xmlNode2);
                else
                    WriteProperty("GUID", "null", xmlNode2); 
            }       
        }

        private void SaveAlarmsManager(ICustomAlarmsManager alarmsManager, XmlNode xmlNode)
        {
            XmlNode xmlNode1, xmlNode2;
            
            xmlNode1 = xmlDocument.CreateNode(XmlNodeType.Element, "AlarmsManager", "");
            xmlNode.AppendChild(xmlNode1);
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "Name", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode2.InnerText = alarmsManager.Name;
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "ShowPopup", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode2.InnerText = alarmsManager.ShowPopup.ToString();
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "TimeOff", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode2.InnerText = alarmsManager.TimeOff.ToString();
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "TimeOn", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode2.InnerText = alarmsManager.TimeOn.ToString();
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "EnabledLogRegisters", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode2.InnerText = alarmsManager.EnabledLogRegisters.ToString();
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "LogFileName", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode2.InnerText = alarmsManager.LogFileName;
            //
            xmlNode2 = xmlDocument.CreateNode(XmlNodeType.Element, "MaxRegisterCount", "");
            xmlNode1.AppendChild(xmlNode2);
            xmlNode2.InnerText = alarmsManager.MaxRegisterCount.ToString();          
        }
        /*!
         * Copy Ctrl+C
         */
        public string SaveScreenObjects(ArrayList objectList)        
        {            
            XmlNode rootNode;
            XmlDeclaration xmldecl;
            xmlDocument = new XmlDocument();
            xmldecl = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDocument.AppendChild(xmldecl);            
            //
            rootNode = xmlDocument.CreateNode(XmlNodeType.Element, "ScreenObjects", "");
            xmlDocument.AppendChild(rootNode);
            SaveObjectsOfScreen(objectList, rootNode);
            //
            System.IO.StringWriter sw = new System.IO.StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            xmlDocument.Save(xw);
            return sw.ToString();                
        }
    }
}
