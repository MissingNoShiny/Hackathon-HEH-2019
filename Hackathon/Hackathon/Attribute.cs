using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hackathon {
    public class Attribute {

<<<<<<< HEAD
        public Attribute(Object attribute) {
            
=======
        public DataType Type;
        public Object Value;
        
        //Builder for the types int, float, bool, string, datetime and imagepath. 
        public Attribute(Object attribute) {
            
            switch (attribute.GetType().Name) {
                case "Int32":
                    Type = DataType.INTEGER;
                    Value = attribute;
                    break;
                case "Single":
                    Type = DataType.FLOAT;
                    Value = attribute;
                    break;
                case "Boolean":
                    Type = DataType.BOOLEAN;
                    Value = attribute;
                    break;
                case "String":
                    Type = DataType.STRING;
                    Value = attribute;
                    break;
                case "DateTime":
                    Type = DataType.DATE;
                    Value = attribute;
                    break;
                case "ImagePath"://TODO: implémenter la classe
                    Type = DataType.PATH;
                    Value = attribute;
                default:
                    throw new Exception("Valeur érronée."); 
            }
>>>>>>> master
        }

    }
}
