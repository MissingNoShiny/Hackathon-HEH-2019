using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon {
    public class Library {
        public Dictionary<String, DataType> AttributeTypes {
            get; private set;
        }

        public List<String> AttributeNames {
            get; private set;
        }

        public List<Item> Items {
            get; private set;
        }

        public String Name;

        public Library(String name) {
            Name = name;
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

        public void AddAttribute(String attribute, DataType type) {
            AttributeTypes[attribute] = type;
            AttributeNames.Add(attribute);
            foreach (Item item in Items) {
                item.AddValue(null);
            }
        }

        public void RemoveAttribute(String attributeName) {
            if (!AttributeNames.Contains(attributeName))
                return;
            int index = AttributeNames.IndexOf(attributeName);
            foreach (Item item in Items)
                item.RemoveValue(index);
            AttributeTypes.Remove(attributeName);
            AttributeNames.Remove(attributeName);
        }

        //Reorganizes attributes order, given the new order
        public void ReorganizeAttributes(List<String> newAttributeNames) {
            if (new HashSet<String>(newAttributeNames) != new HashSet<string>(AttributeNames)
                || newAttributeNames.Count != AttributeNames.Count)
                return; //TODO : Should throw an error
            List<int> newOrder = new List<int>();
            foreach (String attributeName in newAttributeNames)
                newOrder.Add(AttributeNames.IndexOf(attributeName));
            foreach (Item item in Items)
                item.ReorganizeValues(newOrder);
        }

        //Searches an item in the library
        public List<Item> Search (String search) {
            List<Item> match = new List<Item>();
            foreach (Item item in Items) {
                for(int i = 0; i < AttributeNames.Count; i++) {
                    if (AttributeNames[i].Contains(search)) {
                        match.Add(item);
                        break;
                    }   
                }
            }
            return match;
        }
    }
}
