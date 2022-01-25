using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;
using System.Windows.Threading;

namespace BadukNovel
{
    enum GameState
    {
        playing,
        title
    }
    class SceneManager
    {
        public GameState state;
        //public string mode;
        public string bg_filename;
        public bool is_FullScreen;
        BadukNovelCharacter Character;
        BadukNovelScript Script;

        MediaPlayer BackgroundMusicPlayer;

        List<string> BackgroundList;
        int bg_index = 11;

        // Scene Controls
        MainWindow Window;
        System.Windows.Controls.Image BackGroundImage;
        Image CharacterImage;
        TextBlock MessageBox;
        TextBlock MessageBoxBackground;
        Canvas Canvas_Name;
        TextBlock TextBlock_Name;
        TextBlock TextBlock_Name_shadow1;
        TextBlock TextBlock_Name_shadow2;
        TextBlock TextBlock_Name_shadow3;
        TextBlock TextBlock_Name_shadow4;
        TextBlock TextBlock_Name_shadow5;
        TextBlock TextBlock_Name_shadow6;
        TextBlock TextBlock_Name_shadow7;
        TextBlock TextBlock_Name_shadow8;

        Grid Grid_MenuButton1;
        Grid Grid_MenuButton2;
        Grid Grid_MenuButton3;
        Grid Grid_MenuButton4;
        TextBlock TextBlock_MenuButton1;
        TextBlock TextBlock_MenuButton2;
        TextBlock TextBlock_MenuButton3;
        TextBlock TextBlock_MenuButton4;

        DispatcherTimer TypeTextTimer;
        string message_to_say;
        string current_message;

        public SceneManager()
        {
            state = GameState.title;
            is_FullScreen = false;
            bg_filename = AppDomain.CurrentDomain.BaseDirectory + "src\\bg\\fon12.jpg";
            Character = new BadukNovelCharacter();
            InitBackgroungList();
            BackgroundMusicPlayer = new MediaPlayer();
            TypeTextTimer = new System.Windows.Threading.DispatcherTimer();
            //TypeTextTimer.Tick += new EventHandler(TypeMessageTick);
            TypeTextTimer.Interval = TimeSpan.FromMilliseconds(10);
        }

        public void TypeMessageTick()
        {
            if(current_message == null)
            {
                return;
            }
            if(current_message.Length < message_to_say.Length)
            {
                current_message += message_to_say.Substring(current_message.Length, 1);
                MessageBox.Text = current_message;
            }
            else
            {
                TypeTextTimer.Stop();
            }
        }



        public void SetMode(GameState p_stape)
        {
            switch (p_stape)
            {
                case GameState.title:
                    HideAll();
                    ShowMenu();
                    break;
                case GameState.playing:
                    HideAll();
                    ShowMenu();
                    break;
                default:
                    break;
            }
        }

        public void LinkSceneObjects(MainWindow p_Window)
        {
            Window = p_Window;
            BackGroundImage = p_Window.Image_Background;
            CharacterImage = p_Window.Image_Character;
            MessageBox = p_Window.TextBlock_MessageBox;
            MessageBoxBackground = p_Window.TextBlock_Background;
            Canvas_Name = p_Window.Canvas_Name;
            TextBlock_Name = p_Window.TextBlock_Name;
            TextBlock_Name_shadow1 = p_Window.TextBlock_Name_shadow1;
            TextBlock_Name_shadow2 = p_Window.TextBlock_Name_shadow2;
            TextBlock_Name_shadow3 = p_Window.TextBlock_Name_shadow3;
            TextBlock_Name_shadow4 = p_Window.TextBlock_Name_shadow4;
            TextBlock_Name_shadow5 = p_Window.TextBlock_Name_shadow5;
            TextBlock_Name_shadow6 = p_Window.TextBlock_Name_shadow6;
            TextBlock_Name_shadow7 = p_Window.TextBlock_Name_shadow7;
            TextBlock_Name_shadow8 = p_Window.TextBlock_Name_shadow8;

            Grid_MenuButton1 = p_Window.Grid_MenuButton1;
            Grid_MenuButton2 = p_Window.Grid_MenuButton2;
            Grid_MenuButton3 = p_Window.Grid_MenuButton3;
            Grid_MenuButton4 = p_Window.Grid_MenuButton4;
            TextBlock_MenuButton1 = p_Window.TextBlock_MenuButton1;
            TextBlock_MenuButton2 = p_Window.TextBlock_MenuButton2;
            TextBlock_MenuButton3 = p_Window.TextBlock_MenuButton3;
            TextBlock_MenuButton4 = p_Window.TextBlock_MenuButton4;
        }

        public void SetScript(BadukNovelScript p_script)
        {
            Script = p_script;
        }

        public void UpdateObjectSizes()
        {
            if(Window.ActualHeight != 0 && Window.ActualWidth != 0)
            {
                BackGroundImage.Width = Window.ActualWidth;
                BackGroundImage.Height = Window.ActualHeight;

                CharacterImage.Width = Window.ActualWidth * 0.4;
                CharacterImage.Height = Window.ActualHeight * 0.9;
                CharacterImage.VerticalAlignment = VerticalAlignment.Bottom;
                CharacterImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                CharacterImage.Margin = new Thickness(Window.ActualWidth * 0.6, 0, 0, 0);

                MessageBox.Height = Window.ActualHeight * 0.2;
                MessageBox.Width = Window.ActualWidth;
                MessageBox.FontSize = Window.ActualHeight / 800 * 36;
                MessageBoxBackground.Height = Window.ActualHeight * 0.2;
                MessageBoxBackground.Width = Window.ActualWidth;

                Canvas_Name.Margin = new Thickness(30, 0, 0, Window.ActualHeight * 0.20);
                Canvas_Name.Width = Window.ActualWidth / 1600 * 300;
                Canvas_Name.Height = Window.ActualHeight / 900 * 100;
                TextBlock_Name.FontSize = Window.ActualHeight / 800 * 48;
                TextBlock_Name_shadow1.FontSize = Window.ActualHeight / 800 * 48;
                TextBlock_Name_shadow2.FontSize = Window.ActualHeight / 800 * 48;
                TextBlock_Name_shadow3.FontSize = Window.ActualHeight / 800 * 48;
                TextBlock_Name_shadow4.FontSize = Window.ActualHeight / 800 * 48;
                TextBlock_Name_shadow5.FontSize = Window.ActualHeight / 800 * 48;
                TextBlock_Name_shadow6.FontSize = Window.ActualHeight / 800 * 48;
                TextBlock_Name_shadow7.FontSize = Window.ActualHeight / 800 * 48;
                TextBlock_Name_shadow8.FontSize = Window.ActualHeight / 800 * 48;

                Grid_MenuButton1.Width = Window.ActualWidth * 0.3;
                Grid_MenuButton2.Width = Window.ActualWidth * 0.3;
                Grid_MenuButton3.Width = Window.ActualWidth * 0.3;
                Grid_MenuButton4.Width = Window.ActualWidth * 0.3;
            }
        }

        public void ProcessNextCommand()
        {
            bool command_loaded = Script.ReadNextCommand();
            if(command_loaded)
            {
                bool process_next = true;
                switch (Script.CurrentCommand.type)
                {
                    case "say":
                        Say(Script.CurrentCommand.message);
                        break;
                    case "background":
                        SetBackground(Script.CurrentCommand.background);
                        break;
                    case "empty_string":
                        process_next = false;
                        break;
                    case "name":
                        SetName(Script.CurrentCommand.name);
                        break;
                    case "show":
                        SetCharacter(Script.CurrentCommand.character, Script.CurrentCommand.emotion);
                        break;
                    default:
                        break;
                }
                if(process_next)
                {
                    ProcessNextCommand();
                }
            }
        }

        public void PlayBackgroundMusic()
        {
            BackgroundMusicPlayer.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + "src\\sound\\background\\menu_sound.mp3", UriKind.Absolute));
            BackgroundMusicPlayer.MediaEnded += new EventHandler(BackgroundMusicEnded);
            BackgroundMusicPlayer.Play();
        }

        public void StopBackgroundMusic()
        {
            BackgroundMusicPlayer.Stop();
        }

        private void BackgroundMusicEnded(object sender, EventArgs e)
        {
            BackgroundMusicPlayer.Position = TimeSpan.Zero;
            BackgroundMusicPlayer.Play();
        }

        public void HideAll()
        {

        }

        public void ShowMenu()
        {

        }

        public void ShowGame()
        {

        }

        public void Say(string p_message_to_say)
        {
            message_to_say = p_message_to_say;
            current_message = "";
            MessageBox.Text = "";
            TypeTextTimer.Start();
        }

        public void SetCharacter(string character_name, string charater_emotion = "neutral")
        {
            Character = new BadukNovelCharacter(character_name, charater_emotion);
            if(File.Exists(AppDomain.CurrentDomain.BaseDirectory + Character.src_image))
            {
                CharacterImage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + Character.src_image, UriKind.Absolute));
            }
            else
            {
                ;
            }
        }

        public void SetEmotion(string param)
        {

        }

        public void SetName(string name)
        {
            TextBlock_Name.Text = name;
            TextBlock_Name_shadow1.Text = name;
            TextBlock_Name_shadow2.Text = name;
            TextBlock_Name_shadow3.Text = name;
            TextBlock_Name_shadow4.Text = name;
            TextBlock_Name_shadow5.Text = name;
            TextBlock_Name_shadow6.Text = name;
            TextBlock_Name_shadow7.Text = name;
            TextBlock_Name_shadow8.Text = name;
        }

        public void SetBackground(string param)
        {
            string temp_filename = AppDomain.CurrentDomain.BaseDirectory + "src\\bg\\" + param + ".jpg";
            if(File.Exists(temp_filename))
            {
                bg_filename = temp_filename;
                BackGroundImage.Source = new BitmapImage(new Uri(bg_filename, UriKind.Absolute));
            }
        }

        void InitBackgroungList()
        {
            BackgroundList = new List<string>();
            string target_directory = "src\\bg";
            string[] data = Directory.GetFiles(target_directory);
            foreach(string current_file_name in data)
            {
                string current_fon = current_file_name.Replace(".jpg", "").Replace("src\\bg\\", "");
                BackgroundList.Add(current_fon);
                BackgroundList.Sort();
            }
        }

        public void NextBackground()
        {
            bg_index++;
            if (bg_index >= BackgroundList.Count)
            {
                bg_index = 0;
            }
            bg_filename = BackgroundList[bg_index];
            SetBackground(bg_filename);
        }
    }
}
