using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Matrix
{
    class Indexator
    {
        string Root;
        List<string> KeyWords;
        string[] Files;

        public Indexator(string Root, List<string> KeyWords = null)
        {
            this.Root = Root;
            this.KeyWords = KeyWords ?? new List<string>();
            if (Directory.Exists(Root))
                this.Files = Directory.GetFiles(Root);
        }
        public void Add(string str) => KeyWords.Add(str);
        public void Remove(string str) => KeyWords.Remove(str);

        public void Update(string Root = null)
        {
            if (Root != null)
                this.Root = Root;

            if (Directory.Exists(this.Root))
                this.Files = Directory.GetFiles(this.Root);

            this.Files = GetFiles(this.Root);
        }
        private bool Contains(string Str)
        {
            foreach(string str in KeyWords)
                if (!Str.Contains(str))
                    return false;
            return true;
        }
        public string[] GetFiles(string Root = null)
        {
            List<string> files = new List<string>();
            for(int i = 0; i < Files.Length; i++)
                if (Contains(File.ReadAllText(Files[i])))
                    files.Add(Files[i]);
            return files.ToArray();
        }
        public void View()
        {
            Console.WriteLine("Files: ");
            for (int i = 0; i < Files.Length; i++)
                Console.WriteLine("\t" + Files[i]);
        }

        private readonly string Commands = "Commands:\n\t0 : Exit\n\t1 : Add\n\t2 : Remove\n\t3 : Change path\n\t4 : View\n"; 
        public void Start()
        {
            int command = -1;
            while (true)
            {
                Console.WriteLine(Commands);
                command = int.Parse(Console.ReadLine());
                this.Update();
                if (command == 0)
                    return;
                else if(command == 1)
                {
                    Console.Write("Enter string: ");
                    this.Add(Console.ReadLine());
                }
                else if(command == 2)
                {
                    Console.Write("Enter string: ");
                    this.Remove(Console.ReadLine());
                }
                else if(command == 3)
                {
                    Console.Write("Enter new path: ");
                    this.Update(Console.ReadLine());
                }
                else if(command == 4)
                {
                    Console.WriteLine("Key Words: ");
                    foreach (string str in KeyWords)
                        Console.WriteLine("\t" + str);
                    Console.WriteLine();
                    this.View();
                    
                }
            }
        }
    }
}
