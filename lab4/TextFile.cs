using System;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace lab4 {
    [Serializable]
    public class TextFile: IOriginator {
        public string Name { get; private set; }
        public string[] Tags { get; private set; } = new string[2];
        public string Content { get; set; }
        public TextFile(string name) {
            Name = name;
        }
        public TextFile(string name, string[] tags) {
            Name = name;
            Tags[0] = tags[0];
            Tags[1] = tags[1];
        }
        public void SerializeBin(FileStream text) {
            var formatter = new BinaryFormatter();
            formatter.Serialize(text, this);
            text.Close();
        }
        public void DeserializeBin(FileStream text) {
            var formatter = new BinaryFormatter();
            var deserialized = (TextFile)formatter.Deserialize(text);
            Name = deserialized.Name;
            Tags = deserialized.Tags;
            text.Close();
        }
        public void SerializeXML(FileStream text) {
            var formatter = new XmlSerializer(this.GetType());
            formatter.Serialize(text, this);
            text.Close();
        }
        public void DeserializeXML(FileStream text) {
            var formatter = new XmlSerializer(this.GetType());
            var deserialized = (TextFile)formatter.Deserialize(text);
            Name = deserialized.Name;
            Tags = deserialized.Tags;
            text.Close();
        }
        object IOriginator.GetMemento() {
            return new Memento {Content = this.Content};
        }
        void IOriginator.SetMemento(object memento) {
            if (memento is Memento) {
                var mem = (Memento)memento;
                Content = mem.Content;
            }
        }
    }
}
