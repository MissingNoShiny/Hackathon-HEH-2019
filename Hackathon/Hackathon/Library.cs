using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon {
    public class Library {
        public Dictionary<String, DataType> Attributes {
            get; private set;
        }
        public List<Item> Items {
            get; private set;
        }
        public String Name;

        public Library() {

        }

        public void AddItem(Item item) {
            
        }

        public void RemoveItem(Item item) {

        }

        public void ModifyItem(Item oldi, Item newi) {

        }
    }
}
