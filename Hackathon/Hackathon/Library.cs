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
        //Add a given item
        public void AddItem(Item item) {
            foreach (Item item_temp in Items) {
                if (!item.Equals(item_temp))
                    Items.Add(item);
            } 
        }
        //Removes a given item
        public void RemoveItem(Item item) {
            foreach (Item item_temp in Items) {
                if (item.Equals(item_temp)) 
                    Items.Remove(item_temp); 
            }
        }
        //Replaces an item with another
        public void ModifyItem(Item oldi, Item newi) {
            foreach(Item item_temp in Items) {
                if (oldi.Equals(item_temp)) {
                    RemoveItem(oldi);
                    AddItem(newi);
                } 
            }
        }
    }
}
