using System;
using System.Collections.Generic;
using System.Linq;

using System.Net;
using System.IO;



namespace Matrix
{ 
    class Program
    {
        static readonly string file_root = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\text.txt";

        static void Main(string[] args)
        {
            int command = -1;
            while (true)
            {
                Console.WriteLine("Select:\n\t1 : TextEditor\n\t2 : indexator");
                Console.Write("Enter command: "); command = int.Parse(Console.ReadLine());
                if(command == 1)
                {
                    Console.WriteLine("Enter path to text file: "); TextFile file = new TextFile(Console.ReadLine());
                    TextEditor textEditor = new TextEditor(file);
                    textEditor.Start();
                }
                else if (command == 2)
                {
                    Console.Write("Enter start path: "); 
                    Indexator indexator = new Indexator(Console.ReadLine());
                    indexator.Start();
                }
            }
        }
    }
}
