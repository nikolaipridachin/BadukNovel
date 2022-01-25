using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BadukNovel
{
    class BadukNovelScript
    {
        List<string> source_data;
        int current_index;
        string filename;

        public BadukNovelCommand CurrentCommand;

        public BadukNovelScript()
        {
            source_data = new List<string>();
            current_index = -1;
        }

        public bool Load(string param)
        {
            current_index = -1;
            filename = "src\\script\\" + param;
            if(File.Exists(filename))
            {
                source_data = new List<string>();
                string[] temp_data = File.ReadAllLines(filename);
                foreach(string current_line in temp_data)
                {
                    source_data.Add(current_line);
                }
                if(source_data.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ReadNextCommand()
        {
            current_index++;
            if (current_index < 0)
            {
                return false;
            }
            if (current_index < source_data.Count)
            {
                CurrentCommand = new BadukNovelCommand(source_data[current_index]);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
