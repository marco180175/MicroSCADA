using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary
{
    /*!
     * Interface para super classe de todos os objetos do editor e runtime.
     * Usar a interface neste caso facilita a leitura e escrita em arquivos
     * usando as mesmas classes.
     * A ideia e emular uma herançã multipla usando a interface para
     * obrigar a criação da classe com campos e metodos comums.
     * Ex:
     * CDesignObject:Object,ICustomObject
     * {
     *  //Propriadades comuns a ambos os projectos
     *  protected CCustomObject customObject;
     *  //Propriedades e funçoes especificas desta classe
     * }
     * CRunTimeObject:Object,ICustomObject
     * {
     *  //Propriadades comuns a ambos os projectos
     *  protected CCustomObject customObject;
     *  //Propriedades e funçoes especificas desta classe
     *  
     * }
     */

    /*!
     * Interface     
     */
    public interface ICustomObject : IDisposable
    {        
        string Name { get; set; }
        string Description { get; set; }
        ArrayList ObjectList { get; }        
        object Owner { get; }
        Guid GUID { get; }
        void SetGUID(Guid Value);
    }
    /*!
     * Classe auxiliar que implementa funções da classe
     * que ira implementar "ICustomObject"
     */
    public sealed class CCustomObject
    {
        public string name;
        public string description;
        public ArrayList objectList;
        public object owner;
        public Guid guid;
        public CCustomObject()
        {
            objectList = new ArrayList();
            name = string.Empty;
            description = string.Empty;
            guid = Guid.Empty;
        }
        /*!
         * Retorna nome do objeto seguido dos nomes dos objetos proprietatios
         * ate o objeto Project.
         * @return Nome completo do objeto
         */
        public static string GetFullName(string name, object owner)
        {
            Stack<String> retNameList = new Stack<String>();
            String retName = String.Empty;
            ICustomObject customObject = (ICustomObject)owner;
            retNameList.Push(name);
            while (customObject != null)
            {
                retNameList.Push(customObject.Name + '.');
                customObject = (ICustomObject)customObject.Owner;
            }
            while (retNameList.Count > 0)
                retName += retNameList.Pop();
            //
            return retName;
        }                
        
    }
}
