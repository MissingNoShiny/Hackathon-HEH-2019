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

        public void AddItem(Item item) {//Add a given item
            foreach (Item item_temp in Items) {
                if (item.Equals(item_temp)) { }
                else {
                    Items.Add(item);
                }
            } 
        }

        public void RemoveItem(Item item) {//Remove a given item
            foreach (Item item_temp in Items) {
                if (item.Equals(item_temp)) {
                    Items.Remove(item_temp);
                } else { }
            }
        }

        public void ModifyItem(Item oldi, Item newi) {//replace an item with another
            foreach(Item item_temp in Items) {
                if (oldi.Equals(item_temp)) {
                    RemoveItem(oldi);
                    AddItem(newi);
                } else { }
            }
        }
    }
}
