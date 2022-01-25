using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadukNovel
{
    class BadukNovelCommand
    {
        string source_data;
        public string type = "undefined";
        public string message = "";
        public string background = "";
        public string name = "";
        public string character = "";
        public string emotion = "";

        public BadukNovelCommand()
        {

        }
        public BadukNovelCommand(string source_string)
        {
            Init(source_string);
        }

        public void Init(string param)
        {
            source_data = param;
            ProcessData();
        }

        public void ProcessData()
        {
            string[] rows = source_data.Split(' ');
            int rl = rows.Length;
            if (source_data == "")
            {
                type = "empty_string";
                return;
            }
            switch(rows[0])
            {
                case "s":
                    type = "say";
                    message = source_data.Substring(2, source_data.Length - 2);
                    break;
                case "bg":
                    if(rl >= 2)
                    {
                        type = "background";
                        background = rows[1];
                    }
                    break;
                case "name":
                    type = "name";
                    name = source_data.Substring(5, source_data.Length - 5);
                    break;
                case "show":
                    if (rl >= 2)
                    {
                        type = "show";
                        character = rows[1];
                        emotion = rows[2];
                    }
                    break;
                default:
                    type = "undefined";
                    break;
            }
        }
    }
}
