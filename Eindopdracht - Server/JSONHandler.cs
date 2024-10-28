using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht___Server
{
    internal class JSONHandler
    {
        private static String path;

        public JSONHandler() 
        {
            string currentPath = Directory.GetCurrentDirectory(); 
            Console.WriteLine(path);
            path = Path.Combine(currentPath, "History.txt");
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                StreamWriter sw = File.CreateText(path);
            }

        }

        public static void saveList(List<String> history)
        {
            File.WriteAllLines(path, history);
        }

        public static List<String> readList()
        {
            StreamReader reader = new StreamReader(path);   
            List<String> list = new List<String>();
            
            String line = reader.ReadLine();
           
            while (line != null)
            {
                list.Add(line);
                
                line = reader.ReadLine();
            }
            
            reader.Close();

            return list;
        }

    }
}
