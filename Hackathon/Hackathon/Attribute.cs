using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Gooboi {
    [Serializable]
    public class Attribute {

        private readonly Dictionary<String, DataType> dataTypeNames = new Dictionary<string, DataType> {
            ["Int32"] = DataType.STRING,
            ["Single"] = DataType.FLOAT,
            ["Boolean"] = DataType.BOOLEAN,
            ["String"] = DataType.STRING,
            ["DateTime"] = DataType.DATE,
            ["ImagePath"] = DataType.PATH
        };
        public DataType Type;
        public Object Value {
            get; private set;
        }
         
        public Attribute(Object attribute) {
            String className = attribute.GetType().Name;
            if (!dataTypeNames.ContainsKey(className))
                return;//TODO : Should throw an error
            Type = dataTypeNames[className];
            Value = attribute;
        }

    }
}
