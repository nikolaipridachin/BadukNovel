using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadukNovel
{
    class BadukNovelCharacter
    {
        public string name;
        public string emotion;
        public string src_image;

        public BadukNovelCharacter()
        {
            SetCharacter("liza");
            SetEmotion("neutral");
            src_image = "src\\characters\\liza\\1-1.png";
        }

        public BadukNovelCharacter(string character_name, string character_emotion)
        {
            SetCharacter(character_name);
            SetEmotion(character_emotion);
            UpdateSrcImage();
            //src_image = "src\\characters\\" + character_name + "\\" + character_name + "_" + emotion + ".png";
        }

        public void SetCharacter(string param)
        {
            name = param;
            UpdateSrcImage();
        }

        public void SetEmotion(string param)
        {
            emotion = param;
            UpdateSrcImage();
        }

        public bool UpdateSrcImage()
        {
            src_image = "src\\characters\\" + name + "\\" + name + "_" + emotion + ".png";
            return true;
        }
    }
}
