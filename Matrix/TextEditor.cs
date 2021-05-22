using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    class TextEditor : IDisposable
    {
        public TextFile File;
        private TextFileHistory History;
        //private List<TextFileMemento> Mementos;
        public TextEditor(TextFile file) 
        { 
            if (file == null) throw new ArgumentNullException(); 
            this.File = file;
            History = new TextFileHistory();
            //Mementos = new List<TextFileMemento>();
        }

        
        private readonly string Commands = "Command:\n\t0 : Exit\n\t1 : Add\n\t2 : Remove\n\t3 : RemoveAll\n\t4 : Replace\n\t5 : SaveState\n\t6 : Restore\n\t7 : View\n";
        public void Start()
        {
            int command = -1;
            while (true)
            {
                
                Console.WriteLine(Commands);
                command = int.Parse(Console.ReadLine());
                if (command == 0)
                    return;
                else if (command == 1)
                {
                    Console.Write("Enter string: ");
                    File.Add(Console.ReadLine());
                }
                else if (command == 2)
                {
                    Console.Write("Enter string: ");
                    File.Remove(Console.ReadLine());
                }
                else if (command == 3)
                {
                    Console.Write("Enter strings: ");
                    File.Remove(Console.ReadLine().Split(' '));
                }
                else if (command == 4)
                {
                    Console.WriteLine("Enter string: ");
                    File.Replace(Console.ReadLine());
                }
                else if (command == 5)
                    History.History.Push(new TextFileMemento(this.File.Strings));
                else if (command == 6)
                {
                    if (History.History.Count > 0)
                        this.File.Restore(History.History.Pop());
                    else Console.WriteLine("\tHave`t saves");
                }
                else if (command == 7)
                    this.View();
            }
        }

        public void View() =>
            File.View();
        public void Dispose()
        {

        }
    }

    public class TextFileMemento
    {
        public List<string> Strings { get; private set; }

        public TextFileMemento(List<string> Strings) => this.Strings = new List<string>(Strings);
        public TextFileMemento(TextFile textFile) => Strings = new List<string>(textFile.Strings);
    }
    class TextFileHistory
    {
        public Stack<TextFileMemento> History { get; private set; }
        public TextFileHistory()
        {
            History = new Stack<TextFileMemento>();
        }
    }
}
