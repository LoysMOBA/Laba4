using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    class Searhing
    {
        public SortedSet<string> KeyWords;
        public Searhing(IEnumerable<string> words = null) =>
            KeyWords = words == null ? new SortedSet<string>() : new SortedSet<string>(words);

        public void Add(string str) => KeyWords.Add(str); 
        public void Add(IEnumerable<string> strs)
        {
            foreach (string str in strs)
                KeyWords.Add(str);
        }
        public void Remove(string str) => KeyWords.Remove(str); 
        public void Remove(IEnumerable<string> strs)
        {
            foreach (string str in strs)
                KeyWords.Remove(str);
        }

        public bool Contains(TextFile textFile)
        {
            for (int i = 0; i < textFile.Strings.Count(); i++)
                if (KeyWords.Contains(textFile.Strings[i]))
                    return true;
            return false;
        }
        public List<TextFile> GetFiles(IEnumerable<TextFile> textFiles)
        {
            List<TextFile> Files = new List<TextFile>();

            foreach (var file in textFiles)
                if (this.Contains(file))
                    Files.Add(file);

            return Files;
        } 
    }
}
