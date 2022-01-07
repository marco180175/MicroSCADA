using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src;
//using MicroSCADAStudioLibrary.Src.EnvironmentDesigner;

namespace MicroSCADAStudioLibrary.Src
{
    /*!
     * Implementa lista de bitmaps utilizados na aplicação.
     * 
     */
    public class CDesignBitmapList : CDesignSystem, ICustomBitmapList
    {
        private CCustomBitmapList customBitmapList;        
        public CDesignBitmapList(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.customBitmapList = new CCustomBitmapList();
            this.SetGUID(Guid.NewGuid());                     
            this.Name = "Bitmaps";
            this.imageIndex = 16;
        }
        //TODO:TEMPORARIO
        protected override void SetName(string value)
        {
            customObject.name = value;
            OnSetObjectName(new SetNameEventArgs(customObject.name));
        }     
        public CCustomBitmapList CustomBitmapList
        {
            get { return this.customBitmapList; }
        }
        //!Modifica Name para apenas leitura
        [Browsable(true), ReadOnly(true)]
        new public String Name
        {
            get { return this.customObject.name; }
            set { this.SetName(value); }
        }
        public String FileName
        {
            get { return this.customBitmapList.fileName; }
        }

        public ICustomBitmapItem AddBitmap()
        {
            CDesignBitmapItem bitmapItem;

            bitmapItem = new CDesignBitmapItem(this, project, customBitmapList);
            ObjectList.Add(bitmapItem);
            OnAddItem(new AddItemEventArgs(bitmapItem, bitmapItem.ImageIndex));            
            return bitmapItem;
        }
        /*!
         * 
         */
        public CDesignBitmapItem AddBitmap(String FileName)
        {
            return AddNewBitmap(new Bitmap(FileName));            
        }
        /*!
         * 
         */
        public CDesignBitmapItem AddBitmap(Image Original)
        {
            return AddNewBitmap(new Bitmap(Original));            
        }
        /*!
         * 
         */
        private CDesignBitmapItem AddNewBitmap(Bitmap bitmap)
        {
            CDesignBitmapItem bitmapItem;

            bitmapItem = (CDesignBitmapItem)AddBitmap();
            bitmapItem.SetGUID(Guid.NewGuid());
            bitmapItem.Name = "BitmapItem" + ObjectList.Count.ToString();
            //
            customBitmapList.fileStream.Position = customBitmapList.fileStream.Length;
            bitmapItem.Position = (int)customBitmapList.fileStream.Position;
            customBitmapList.serializer.Serialize(customBitmapList.fileStream, bitmap);
            //
            return bitmapItem;
        }
        public Bitmap GetBitmap(int Position)
        {           
            return customBitmapList.GetBitmap(Position);
        }
        public void New(String FileName)
        {            
            customBitmapList.fileName = Path.ChangeExtension(FileName,".tbm");
            if (File.Exists(this.customBitmapList.fileName))
                File.Delete(this.customBitmapList.fileName);
            customBitmapList.fileStream = new FileStream(customBitmapList.fileName, FileMode.Create);
            customBitmapList.serializer = new BinaryFormatter();
        }
        public void Open(String FileName)
        {
            customBitmapList.fileName = Path.ChangeExtension(FileName, ".tbm");
            customBitmapList.fileStream = new FileStream(customBitmapList.fileName, FileMode.Open, FileAccess.ReadWrite);
            customBitmapList.serializer = new BinaryFormatter();            
        }
        public void Open()
        {
            if (File.Exists(customBitmapList.fileName))
            {
                customBitmapList.fileStream = new FileStream(customBitmapList.fileName, FileMode.Open, FileAccess.ReadWrite);
                customBitmapList.serializer = new BinaryFormatter();
            }
        }
        public void Close()
        {
            if (customBitmapList.fileStream != null)
            {
                customBitmapList.fileStream.Close();
                customBitmapList.serializer = null;
            }
        }
        public void Clear()
        {
            while (ObjectList.Count > 0) 
                ((CDesignBitmapItem)ObjectList[0]).Dispose();
            customBitmapList.fileName = string.Empty;
            Close();
        }
        public void SaveBitmap(Stream stream,int i)
        {
            Bitmap bitmap = new Bitmap(stream);
            CDesignBitmapItem bitmapItem = (CDesignBitmapItem)ObjectList[i];
            //
            customBitmapList.fileStream.Position = customBitmapList.fileStream.Length;
            bitmapItem.Position = (int)customBitmapList.fileStream.Position;
            customBitmapList.serializer.Serialize(customBitmapList.fileStream, bitmap); 
        }
    }
}
