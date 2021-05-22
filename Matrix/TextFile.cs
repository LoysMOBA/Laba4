using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Matrix
{
    [Serializable]
    public class TextFile
    {
        public List<string> Strings { get; set; }
        public string Root { get; set; }

        public TextFile(string Root, bool ThisIsFile) 
        {
            if (ThisIsFile)
                using (var sr = new StreamReader(Root))
                {
                    this.Root = Root;
                    Strings = new List<string>(sr.ReadToEnd().Split(' '));
                }
            else throw new Exception(message: "Not File, use next construct");
        }
        public TextFile(string Root,IEnumerable<string> Strings = null)
        {
            this.Root = Root;
            using (var sw = new StreamWriter(Root))
                sw.Write("");

            this.Strings = Strings == null ? new List<string>() : new List<string>(Strings);
        }

        public void Add(string str) { Strings.Add(str); Save(); }
        public void Add(IEnumerable<string> strs)
        {
            foreach (string str in strs)
                Strings.Add(str);
            Save();
        }
        public void Remove(string str) { Strings.Remove(str); Save(); }
        public void Remove(IEnumerable<string> strs)
        {
            foreach (string str in strs)
                Strings.Remove(str);
            Save();
        }
        public void Replace(string str) => Strings.Select(s => s == str ? str : s);

        public void Save()
        {
            using (var sw = new StreamWriter(Root))
                foreach (string str in Strings)
                    sw.Write(str);

            Strings.RemoveAll(s => s == "");
        }

        public void BinarySerialization(string stream)
        {
            using (var fs = new FileStream(stream, FileMode.OpenOrCreate))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(fs, this);
            }
        }
        public void XmlSerialization(string stream)
        {
            using (var fs = new FileStream(stream, FileMode.OpenOrCreate))
            {
                var formatter = new XmlSerializer(this.GetType());
                formatter.Serialize(fs, this);
            }
        }
        public void BinaryDeSerialization(string stream)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(stream, FileMode.OpenOrCreate))
            {
                TextFile txt = (TextFile)formatter.Deserialize(fs);
                this.Strings = txt.Strings;
                this.Root = txt.Root;
            }
        }
        public void XmlDeSetialization(string stream)
        {
            var formatter = new XmlSerializer(typeof(TextFile));
            using (var fs = new FileStream(stream, FileMode.OpenOrCreate))
            {
                TextFile txt = (TextFile)formatter.Deserialize(fs);
                this.Strings = txt.Strings;
                this.Root = Root;
            }
        }


        public TextFileMemento SaveState() => new TextFileMemento(this.Strings);
        public void Restore(TextFileMemento tfm)
        {
            Strings.Clear();
            foreach (string str in tfm.Strings) Strings.Add(str);
        }

        public void View()
        {
            foreach (string str in Strings)
                Console.Write(str);
            Console.WriteLine();
        }
    }
}
