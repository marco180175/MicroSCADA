using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.Visuals;
using MicroSCADAStudioLibrary.Src.Tags;
using MicroSCADAStudioLibrary.Src.TypeConverter;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    
    /*!
     * Objeto Campo animação. Mostra uma imagen de acordo com o valor do tagValue
     */
    public class CDesignAnimation : CDesignCustomField, ICustomAnimation, IDesignCollection,
                                    IDesignZoneCollection
    {        
        private CCustomAnimation customAnimation;//!< Template para auxiliar criação da classe
        
        /*!
         * Construtor
         * @param AOwner Referencia para proprietatio
         */
        public CDesignAnimation(Object AOwner, CDesignProject Project, Control Parent)
            : base(AOwner, Project, Parent)
        {
            this.InitializeObject(); 
            this.customAnimation = new CCustomAnimation(this.pictureBox);
            this.imageIndex = 34;             
        }
        /*!
         * Retorna matriz de objetos de mesmo tipo. Esta função é chamada
         * na classe base CDesignSystem.
         * @param Objects Lista de objetos do owner
         * @return Array Matriz de objetos de mesmo tipo
         */
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignAnimation> subSet = Objects.OfType<CDesignAnimation>();
            return subSet.ToArray();
        }
        #region Propriedades
        //!
        [Browsable(false)]
        public override Color BackColor
        {
            get { return this.pictureBox.BackColor; }
            set { this.pictureBox.BackColor = value; }
        }
        //!
        [ReadOnly(true)]
        public override CFieldType FieldType
        {
            get { return CFieldType.ftRead; }
        }
        public int ZoneCount
        {
            get { return this.ObjectList.Count; }
        }    
        //! Lista de items
        [Editor(typeof(CollectionEditorPreset), typeof(UITypeEditor))]
        public ArrayList Items
        {
            get { return ObjectList; }
        }       
        //! Incremento ou Range
        public CDinamicType DinamicType { get; set; }
        #endregion
        /*!
         * Adiciona nova zona de texto.
         * @return Referencia para nova zona.
         */
        public ICustomPictureZone AddZone()
        {             
            CDesignAnimationZone pictureZone = new CDesignAnimationZone(this, project);
            ObjectList.Add(pictureZone);
            OnAddItem(new AddItemEventArgs(pictureZone, pictureZone.ImageIndex));                        
            return pictureZone;
        }
        /*!
         * Adiciona nova zona de texto.
         * @return Referencia para nova zona.
         */
        public CDesignAnimationZone AddZoneEx()
        {             
            CDesignAnimationZone pictureZone = (CDesignAnimationZone)AddZone();
            pictureZone.SetGUID(Guid.NewGuid());            
            pictureZone.Name = "PictureZone" + ObjectList.Count.ToString();
            pictureZone.MaxValue = ObjectList.Count - 1;
            pictureZone.MinValue = ObjectList.Count - 1;
            return pictureZone;
        }
        /*!
         * 
         */
        public IDesignCollectionItem NewItem()
        {
            return (IDesignCollectionItem)AddZoneEx();
        }
        /*!
         * Evento OnPaint
         * @param sender
         * @param e
         */
        protected override void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            //falta verificar valor do tag e mostrar a zona
            if(ObjectList.Count > 0)
            {
                CDesignAnimationZone zone = (CDesignAnimationZone)ObjectList[0];
                if (zone.BitmapItem != null)
                    customAnimation.DrawPictureZone(e.Graphics, zone.GetBitmapItem().GetBitmap());
                else
                    customAnimation.DrawPictureZone(e.Graphics, null);
            }
            else
                customAnimation.DrawPictureZone(e.Graphics, null);
            //
            if (selected)
                DrawSelectedRect(e.Graphics);
        }
        
        public Bitmap GetZone(int index)
        {
            Bitmap bitmap = new Bitmap(Width, Height);
            if (index < ObjectList.Count)
            {
                Graphics g = Graphics.FromImage(bitmap);
                CDesignAnimationZone zone = (CDesignAnimationZone)ObjectList[index];
                if (zone.BitmapItem != null)
                    customAnimation.DrawPictureZone(g, zone.GetBitmapItem().GetBitmap());
                else
                    customAnimation.DrawPictureZone(g, null);
            }
            return bitmap;
        }
           
        /*!
         * 
         */
        public override void LinkObjects()
        {     
            //
            base.LinkObjects();
            //
            foreach (CDesignAnimationZone zone in ObjectList)            
                zone.LinkObjects();            
        }        
        
        public CDesignSystem GetThis()
        {
            return this;
        }
        
    }

    /*!
     * Zona com imagem do objeto animação
     */
    public class CDesignAnimationZone : CDesignSystem, ICustomPictureZone, IDesignCollectionItem
    {
        protected int indexBitmapItem;//!< Indice do bitmap 
        /*!
         * Construtor
         * @param AOwner Referencia para proprietario 
         * @param Project Referencia para projeto
         */
        public CDesignAnimationZone(Object AOwner, CDesignProject Project)
            : base(AOwner, Project)
        {
            this.indexBitmapItem = this.ReferenceList.AddReference();
            this.imageIndex = 30;
        }
        /*!
         * 
         */
        public override void Dispose()
        {
            this.SetReference(indexBitmapItem, null);
            base.Dispose();
        }
        /*!
         * Retorna matriz de objetos de mesmo tipo. Esta função é chamada
         * na classe base CDesignSystem.
         * @param Objects Lista de objetos do owner
         * @return Array Matriz de objetos de mesmo tipo
         */
        protected override CDesignSystem[] GetArrayOfObjects(ArrayList Objects)
        {
            IEnumerable<CDesignAnimationZone> subSet = Objects.OfType<CDesignAnimationZone>();
            return subSet.ToArray();
        }
        //! Interface Bitmap da fundo
        [Browsable(false)]
        public ICustomBitmapItem BitmapItem
        {
            get { return this.GetBitmapItem(); }
            set { this.SetBitmapItem((CDesignBitmapItem)value); }
        }
        //! Bitmap de fundo
        [EditorAttribute(typeof(CBitmapDialogTypeEditor),
                         typeof(System.Drawing.Design.UITypeEditor))]
        public CDesignBitmapItem ImageOfObject
        {
            get { return (CDesignBitmapItem)this.BitmapItem; }
            set { this.BitmapItem = value; }
        }
        //! Bitmap de fundo
        //[EditorAttribute(typeof(CBitmapDialogTypeEditor),
        //                 typeof(System.Drawing.Design.UITypeEditor))]
        //public string ImageFileName
        //{
        //    //CDesignBitmapList bitmapList = (CDesignBitmapList)project.BitmapList;
        //    //BitmapItem = bitmapList.AddBitmap(FileName);    
        //}
        public float MaxValue { get; set; }
        public float MinValue { get; set; }
        /*!
         * Seta bitmap da zona
         * @param Value Referencia para bitmap
         */
        protected void SetBitmapItem(CDesignBitmapItem Value)
        {
            this.SetReference(indexBitmapItem, Value);
        }
        /*!
         * Retorna referencia para bitmap
         */
        public CDesignBitmapItem GetBitmapItem()
        {
            return (CDesignBitmapItem)this.GetReference(indexBitmapItem);
        }
        /*!
         * Seta GUID do bitmap
         */
        public void SetGuidBitmapItem(Guid Value)
        {
            this.SetReferenceGuid(indexBitmapItem, Value);
        }
        /*!
         * Procura bitmap na dicionario usando guid como chave\n
         * sendo encontrado linka obj com guid
         */
        public override void LinkObjects()
        {
            Object obj;
            Guid guid = GetReferenceGuid(indexBitmapItem);
            if (CHashObjects.ObjectDictionary.ContainsKey(guid))
                obj = CHashObjects.ObjectDictionary[guid];
            else
                obj = null;
            SetReference(indexBitmapItem, obj);
        }
    }
}
