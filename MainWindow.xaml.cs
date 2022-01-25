using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace BadukNovel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SceneManager Scene;
        BadukNovelScript Script;

        public MainWindow()
        {
            InitializeComponent();

            Scene = new SceneManager();
            Script = new BadukNovelScript();
            Scene.LinkSceneObjects(this);
            Scene.UpdateObjectSizes();
            Scene.PlayBackgroundMusic();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.F)
            {
                ToogleFullscreen();
            }
            if(e.Key == Key.Q)
            {
                Scene.NextBackground();
            }
            if (e.Key == Key.E)
            {
                if(TextBlock_MessageBox.Visibility == Visibility.Visible)
                {
                    TextBlock_MessageBox.Visibility = Visibility.Hidden;
                    TextBlock_Background.Visibility = Visibility.Hidden;
                }
                else
                {
                    TextBlock_MessageBox.Visibility = Visibility.Visible;
                    TextBlock_Background.Visibility = Visibility.Visible;
                }
            }
            if (e.Key == Key.R)
            {                
                if (Image_Character.Visibility == Visibility.Visible)
                {
                    Image_Character.Visibility = Visibility.Hidden;
                }
                else
                {
                    Image_Character.Visibility = Visibility.Visible;
                }
            }

            if(e.Key == Key.Escape)
            {
                Environment.Exit(1);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Scene.SetScript(Script);
            Scene.LinkSceneObjects(this);
            Scene.UpdateObjectSizes();
            Scene.SetBackground("fon15");
            Scene.SetCharacter("alpha");
            Scene.SetName("");
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            Scene.TypeMessageTick();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Scene.UpdateObjectSizes();
        }

        private void ToogleFullscreen()
        {
            if (Scene.is_FullScreen)
            {
                Scene.is_FullScreen = false;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.ResizeMode = ResizeMode.NoResize;
                this.WindowState = WindowState.Normal;
                this.Topmost = false;
            }
            else
            {
                Scene.is_FullScreen = true;
                this.Visibility = Visibility.Collapsed;
                this.WindowStyle = WindowStyle.None;
                this.ResizeMode = ResizeMode.NoResize;
                this.WindowState = WindowState.Maximized;
                this.Topmost = true;
                this.Visibility = Visibility.Visible;
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Scene.SetEmotion("angry");
            if(Scene.state == GameState.playing)
            {
                Scene.ProcessNextCommand();
            }
        }

        private void Canvas_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid_MenuButton1.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x22, 0x22, 0x22));
        }

        private void Canvas_MenuButton1_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid_MenuButton1.Background = new SolidColorBrush(Colors.Black);
        }

        private void Canvas_MouseEnter_1(object sender, MouseEventArgs e)
        {
            Grid_MenuButton2.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x22, 0x22, 0x22));
        }

        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid_MenuButton2.Background = new SolidColorBrush(Colors.Black);
        }

        private void Canvas_MenuButton3_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid_MenuButton3.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x22, 0x22, 0x22));
        }

        private void Canvas_MenuButton3_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid_MenuButton3.Background = new SolidColorBrush(Colors.Black);
        }

        private void Canvas_MenuButton4_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid_MenuButton4.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x22, 0x22, 0x22));
        }

        private void Canvas_MenuButton4_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid_MenuButton4.Background = new SolidColorBrush(Colors.Black);
        }

        private void Grid_MenuButton1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                StartNewGame();
            }
        }

        public void StartNewGame()
        {
            HideMenu();
            Scene.state = GameState.playing;
            Script.Load("testscript.gns");
            Scene.ProcessNextCommand();
        }
        public void HideMenu()
        {
            Grid_Menu.Visibility = Visibility.Hidden;
        }

        private void Grid_MenuButton4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                Environment.Exit(1);
            }
        }
    }
}
