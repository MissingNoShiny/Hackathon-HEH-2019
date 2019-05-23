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
        //Override Equals to compare Item 
        public override bool Equals(object obj) {
            Item item = (Item) obj;
            if((item == null || !this.GetType().Equals(item.GetType())))
                return false;
            else {
                for (int i = 0; i <= (item.Values.Count - 1); i++) {
                    if (this.Values[i] == item.Values[i])
                        return true;
                }
            }
            return false;
        }

        public void AddValue(Attribute value) {
            Values.Add(value);
        }

        public void RemoveValue(int index) {
            Values.RemoveAt(index);
        }
    }
}
