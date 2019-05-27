using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon {
    [Serializable]
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

        public String Nom {//was Name
            get; private set;
        }

        public int Nombre {//was ItemsCount
            get => Items.Count;
        }

        public Library(String name, List<String> attributeNames, Dictionary<String, DataType> attributeTypes) {
            Items = new List<Item>();
            Nom = name;
            AttributeNames = attributeNames;
            AttributeTypes = attributeTypes;
        }

        //Add a given item
        public void AddItem(Item item) {
            if (!Items.Contains(item))
                Items.Add(item);
        }

        //Removes a given item
        public void RemoveItem(Item item) {
            Items.Remove(item);
        }

        public void RemoveItem(int index) {
            Items.RemoveAt(index);
        }

        //Replaces an item with another
        public void ModifyItem(Item oldi, Item newi) {
            if (!Items.Contains(oldi))
                return;
            Items[Items.IndexOf(oldi)] = newi;
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
            foreach (Item item in Items) {
                item.RemoveValue(index);
                if (item.IsNull())
                    Items.Remove(item);
            }
            AttributeTypes.Remove(attributeName);
            AttributeNames.Remove(attributeName);
        }

        public void RenameAttribute(String oldName, String newName) {
            if (!AttributeNames.Contains(oldName) && !AttributeNames.Contains(newName))
                return;
            AttributeNames[AttributeNames.IndexOf(oldName)] = newName;
            AttributeTypes[newName] = AttributeTypes[oldName];
            AttributeTypes.Remove(oldName);

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
        public List<Item> Search(String search) {
            List<Item> match = new List<Item>();
            foreach (Item item in Items) {
                for (int i = 0; i < AttributeNames.Count; i++) {
                    if (AttributeNames[i].Contains(search)) {
                        match.Add(item);
                        break;
                    }
                }
            }
            return match;
        }

        public void Save(String path) {
            IFormatter formatter = new BinaryFormatter();
            String npath = path + Nom + ".libr";
            Stream stream = new FileStream(npath, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, this);
        }
    }
}
