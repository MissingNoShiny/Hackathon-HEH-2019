using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon {
    public class Item {
        public List<Attribute> Values {
            get; private set;
        }

        public Item(List<Attribute> values) {
            Values = values;
        }

        public List<DataType> getDataTypes() {
            return null;
        }
    }
}
