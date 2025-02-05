﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Gooboi {
    public class LibraryManager {

        public static String DefaultLibrariesPath {
            get; private set;
        }

        public List<Library> Libraries {
            get; private set;
        }

        public LibraryManager() {
            DefaultLibrariesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\libraries\");
            Libraries = new List<Library>();
            if (!Directory.Exists(DefaultLibrariesPath))
                Directory.CreateDirectory(DefaultLibrariesPath);
        }

        //Opens all found libraries in a given folder
        public void OpenLibraries() {
            string[] filePaths = Directory.GetFiles(DefaultLibrariesPath, "*.libr", SearchOption.TopDirectoryOnly);
            foreach (String path in filePaths) {
                OpenLibrary(path);
            }
        }

        //Opens a serialized library
        public void OpenLibrary(String path) {
            IFormatter formatter = new BinaryFormatter();
            Console.WriteLine("Accès au chemin : " + path);
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            Library library = (Library)formatter.Deserialize(stream);
            Libraries.Add(library);
        }

        //Saves all libraries to a given folder
        public void SaveLibraries() {
            foreach (Library library in Libraries) {
                library.Save(DefaultLibrariesPath);
            }
        }

        //Deletes all serialized libraries in a given folder
        public void ClearFolder(String folderPath) {
            string[] filePaths = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + folderPath, "*.libr", SearchOption.TopDirectoryOnly);
            foreach (String path in filePaths)
                File.Delete(path);
        }

        public void AddLibrary(Library library) {
            Libraries.Add(library);
        }

        //Searches a library in the libraries list
        public List<Library> Search(String search) {
            List<Library> match = new List<Library>();
            foreach(Library lib in Libraries) {
                if (lib.Nom.Contains(search))
                    match.Add(lib);
            }
            return match;
        }
    }
}
