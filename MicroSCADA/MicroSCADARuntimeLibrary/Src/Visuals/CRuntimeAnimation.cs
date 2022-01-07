using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADARuntimeLibrary.Src.Tags;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    public class CRuntimeAnimationZone : CRuntimeSystem, ICustomPictureZone
    {
        private int indexBitmapItem;
        public CRuntimeAnimationZone(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.indexBitmapItem = this.ReferenceList.AddReference();

        }

        public ICustomBitmapItem BitmapItem
        {
            get { return this.GetBitmapItem(); }
            set { this.SetBitmapItem((CRuntimeBitmapItem)value); }
        }

        protected void SetBitmapItem(CRuntimeBitmapItem Value)
        {
            this.SetReference(indexBitmapItem, Value);
        }

        public CRuntimeBitmapItem GetBitmapItem()
        {
            return (CRuntimeBitmapItem)this.GetReference(indexBitmapItem);
        }

        public void SetGuidBitmapItem(Guid Value)
        {
            this.SetReferenceGuid(indexBitmapItem, Value);
        }

        public void LinkObjects()
        {
            Guid keyGUID = GetReferenceGuid(indexBitmapItem);
            Object obj;
            //
            if (CHashObjects.ObjectDictionary.ContainsKey(keyGUID))
                obj = CHashObjects.ObjectDictionary[keyGUID];
            else
                obj = null;
            SetReference(indexBitmapItem, obj);
        }
    }
    
    class CRuntimeAnimation : CRuntimeCustomField, ICustomAnimation
    {       
        private CCustomAnimation customAnimation;
        /*!
         * Construtor
         * @param AOwner      
         * @param Project         
         */
        public CRuntimeAnimation(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.customAnimation = new CCustomAnimation(this.pictureBox);           
            this.pictureBox.Paint += new PaintEventHandler(this.pictureBox_Paint);            
        }
        /*!
         * Cria e adiciona nova zona de texto.
         * @return Referencia para nova zona.
         */
        public ICustomPictureZone AddZone()
        {
            CRuntimeAnimationZone pictureZone;
            pictureZone = new CRuntimeAnimationZone(this, project);
            ObjectList.Add(pictureZone);            
            return pictureZone;
        }
        /*!
        * Evento OnPaint
        * @param sender
        * @param e
        */
        protected void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            int index = int.Parse(m_value);
            if (index >= 0 && index < ObjectList.Count)
            {
                CRuntimeAnimationZone pictureZone = (CRuntimeAnimationZone)ObjectList[index];
                CRuntimeBitmapItem item = (CRuntimeBitmapItem)pictureZone.BitmapItem;
                if (item != null)
                {
                    Bitmap bitmap = item.GetBitmap();
                    if (bitmap != null)
                        customAnimation.DrawPictureZone(e.Graphics, bitmap);
                    else
                        customAnimation.DrawPictureZone(e.Graphics, null);
                }
                else
                    customAnimation.DrawPictureZone(e.Graphics, null);
            }
            else
            {
                pictureBox.BackColor = Color.White;
                Font font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                int y = (pictureBox.Height - font.Height * 2) / 2;
                Rectangle rect = new Rectangle(0, y, pictureBox.Width, font.Height * 2);
                Brush brush = new SolidBrush(Color.Black);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                e.Graphics.DrawString("Index out of range.\n" + m_value, font, brush, rect, stringFormat);
                stringFormat.Dispose();
                brush.Dispose();
                font.Dispose();
            }
        }        
        /*!
         * 
         */
        public override void LinkObjects()
        {
            //
            base.LinkObjects();
            //
            foreach (CRuntimeAnimationZone zone in ObjectList)            
                zone.LinkObjects();            
        }        
    }
}
