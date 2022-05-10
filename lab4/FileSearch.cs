using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
namespace lab4 {
    [Serializable]
    public class FileSearch {
        public string DirName { get; private set; } 
        private List<TextFile> Files { get; set; } = new List<TextFile>();
        public FileSearch(string directory) {
            if (!Directory.Exists(directory)) {
                throw new Exception("Такой директории не существует.");
            }
            DirName = directory;
        }
        public void GetFiles() {
            string[] names = Directory.GetFiles(DirName);
            foreach (string fileName in names) {
                Console.WriteLine(fileName.Remove(0, DirName.Length + 1));
            }
        }
        public void AddFile(string name) {
            name = DirName + "/" + name;
            if (!File.Exists(name)) {
                throw new Exception("Файл не существует.");
            }
            foreach (var file in Files) {
                if (file.Name == name) {
                    throw new Exception("Файл уже добавлен.");
                }
            }
            string[] tags = new string[2];
            Console.WriteLine($"Введите теги для {name.Remove(0, DirName.Length + 1)}:");
            tags[0] = Console.ReadLine();
            tags[1] = Console.ReadLine();
            Files.Add(new TextFile(name, tags));
            Console.WriteLine("Файл добавлен.");
        }
        public void Search(string[] tags) {
            Console.WriteLine("Файлы найдены:");
            var filesFound = false;
            foreach (var file in Files) {
                if (file.Tags[0] == tags[0] || file.Tags[0] == tags[1] || file.Tags[1] == tags[0] || file.Tags[1] == tags[1]) {
                    Console.WriteLine($"{file.Name.Remove(0, DirName.Length + 1)}");
                    filesFound = true;
                }
            }
            if (!filesFound) {
                throw new Exception("Файлы не найдены.");
            }
        }
        public void Serialize(FileStream file) {
            var formatter = new BinaryFormatter();
            formatter.Serialize(file, this);
            file.Close();
        }
        public void Deserialize(FileStream file) {
            var formatter = new BinaryFormatter();
            var deserialized = (FileSearch)formatter.Deserialize(file);
            Files = deserialized.Files;
            file.Close();
        }
    }
}