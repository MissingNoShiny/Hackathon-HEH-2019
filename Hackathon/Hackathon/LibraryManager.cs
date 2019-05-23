using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon {
    public class LibraryManager {

        public List<Library> Libraries {
            get; private set;
        }

        //Opens all found libraries in a given folder
        public void OpenLibraries(String folderPath) {
            string[] filePaths = Directory.GetFiles(folderPath, "*.libr", SearchOption.TopDirectoryOnly);
            foreach (String path in filePaths)
                OpenLibrary(path);
        }

        //Opens a serialized library
        public void OpenLibrary(String path) {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            Library library = (Library)formatter.Deserialize(stream);
            Libraries.Add(library);
        }

        //Saves all libraries to a given folder
        public void SaveLibraries(String folderPath) {
            IFormatter formatter = new BinaryFormatter();
            foreach (Library library in Libraries) {
                String path = folderPath + library.Name + ".libr";
                Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, library);
            }
        }

        //Deletes all serialized libraries in a given folder
        public void clearFolder(String folderPath) {
            string[] filePaths = Directory.GetFiles(folderPath, "*.libr", SearchOption.TopDirectoryOnly);
            foreach (String path in filePaths)
                File.Delete(path);
        }
    }
}
