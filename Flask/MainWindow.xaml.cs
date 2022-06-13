using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace Flask
{
    public partial class MainWindow : Window
    {
        Generation generation = new Generation();
        Button Memory = null;
        Image MemoryIm = null;
        int Result = 0;
        int LevelComplete;
        int score = 0;
        int scoreLvl = 0;
        string isCheck = null;
        string isCheckIm = null;
        string line;
        string lvlline;
        string[] lvlArr;
        string path = @"Saves\Save.txt";
        string pathSaveLevel = @"Saves\LevelSave.txt";
        string pathSaveScore = @"Saves\Score.txt";

        string[,,] LevelTask = new string[11, 4, 2];

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LevelLoad();
        }

        public async void LevelLoad()
        {
            try
            {
                using (var sr = new StreamReader(path))
                {
                    line = await sr.ReadToEndAsync();
                }
                using (var sr = new StreamReader(pathSaveScore))
                {
                    score = Convert.ToInt32(await sr.ReadToEndAsync());
                }
                using (var sr = new StreamReader(pathSaveLevel))
                {
                    lvlline = await sr.ReadToEndAsync();
                }
                lvlArr = lvlline.Split('\n', ' ');

                if (Convert.ToInt32(line) < 4)
                {
                    LevelComplete = 4;
                    scoreLvl = 50;
                }
                else if (Convert.ToInt32(line) < 7)
                {
                    LevelComplete = 5;
                    scoreLvl = 60;
                }
                else if (Convert.ToInt32(line) < 10)
                {
                    LevelComplete = 6;
                    scoreLvl = 70;
                }
                else if (Convert.ToInt32(line) < 13)
                {
                    LevelComplete = 7;
                    scoreLvl = 80;
                }
                else if (Convert.ToInt32(line) < 16)
                {
                    LevelComplete = 8;
                    scoreLvl = 90;
                }
                else
                {
                    LevelComplete = 9;
                    scoreLvl = 100;
                }
                Score.Text = Convert.ToString(score);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            int lvlnum = Convert.ToInt32(line);
            if (line == "1")
            {
                if (lvlline != "")
                {
                    LevelText.Text = "Level " + line;
                    int t = 0;
                    for (int i = 0; i <= 10; i++)
                    {
                        for (int j = 0; j <= 3; j++)
                        {
                            for (int x = 0; x <= 1; x++)
                            {
                                LevelTask[i, j, x] = lvlArr[t];
                                t++;
                            }
                        }
                    }
                    FillLevel(LevelTask, LevelComplete);
                }
                else
                {
                    Generation(lvlnum);
                }
            }
            else
            {
                LevelText.Text = "Level " + line;
                int t = 0;
                for (int i = 0; i <= 10; i++)
                {
                    for (int j = 0; j <= 3; j++)
                    {
                        for (int x = 0; x <= 1; x++)
                        {
                            LevelTask[i, j, x] = lvlArr[t];
                            t++;
                        }
                    }
                }
                FillLevel(LevelTask, LevelComplete);
            }

            Main.Visibility = Visibility.Visible;
            LevelText.Visibility = Visibility.Visible;
            ScoreStack.Visibility = Visibility.Visible;
        }

        public void LevelReload()
        {
            int lvlnum = Convert.ToInt32(line);
            Result = 0;
            FillLevel(LevelTask, lvlnum);
        }

        public void CheckResult()
        {
            if (Result == LevelComplete)
            {
                NextLevel();
            }
        }

        public void Generation(int lvl)
        {
            string[,,] LevelTaskMain = {
            { {"#a8a5ac", "бык" }, {"#a8a5ac", "бык" }, {"#a8a5ac", "бык" }, {"#a8a5ac", "бык" } }, //1
            { {"#a3de00", "муравей" }, {"#a3de00", "муравей" }, {"#a3de00", "муравей" },  {"#a3de00", "муравей" } }, //2
            { {"#f4e600", "пчела" }, {"#f4e600", "пчела" },  {"#f4e600", "пчела" }, {"#f4e600", "пчела" } }, //3
            { {"#f18f16", "утка" }, {"#f18f16", "утка" }, {"#f18f16", "утка" }, {"#f18f16", "утка" } }, //4
            { {"#e41e1c", "звезда" }, {"#e41e1c", "звезда" }, {"#e41e1c", "звезда" },  {"#e41e1c", "звезда" } }, //5
            { {"#930bcf", "единорог" }, {"#930bcf", "единорог" }, {"#930bcf", "единорог" }, {"#930bcf", "единорог" } }, //6
            { {"#4369d6", "рыба" }, {"#4369d6", "рыба" }, {"#4369d6", "рыба" }, {"#4369d6", "рыба" } }, //7
            { {"#01c5db", "овца" },  {"#01c5db", "овца" }, {"#01c5db", "овца" }, {"#01c5db", "овца" } }, //8
            { {"#d737c6", "свинья" }, {"#d737c6", "свинья" }, {"#d737c6", "свинья" }, {"#d737c6", "свинья" } }, //9
            { {"Transparent", "" }, {"Transparent", "" }, {"Transparent", "" }, {"Transparent", "" } }, //10
            { {"Transparent", "" }, {"Transparent", "" }, {"Transparent", "" }, {"Transparent", "" } }, //11
            };

            if (lvl < 4)
            {
                LevelComplete = 4;
                scoreLvl = 50;
                LevelTask = generation.GenerationShaffle(LevelTaskMain, LevelComplete - 1);
            }
            else if (lvl < 7)
            {
                LevelComplete = 5;
                scoreLvl = 60;
                LevelTask = generation.GenerationShaffle(LevelTaskMain, LevelComplete - 1);
            }
            else if (lvl < 10)
            {
                LevelComplete = 6;
                scoreLvl = 70;
                LevelTask = generation.GenerationShaffle(LevelTaskMain, LevelComplete - 1);
            }
            else if (lvl < 13)
            {
                LevelComplete = 7;
                scoreLvl = 80;
                LevelTask = generation.GenerationShaffle(LevelTaskMain, LevelComplete - 1);
            }
            else if (lvl < 16)
            {
                LevelComplete = 8;
                scoreLvl = 90;
                LevelTask = generation.GenerationShaffle(LevelTaskMain, LevelComplete - 1);
            }
            else
            {
                LevelComplete = 9;
                scoreLvl = 100;
                LevelTask = generation.GenerationShaffle(LevelTaskMain, LevelComplete - 1);
            }

            File.WriteAllText(@"Saves\LevelSave.txt", string.Concat(LevelTask.Cast<string>().Select(
                (s, i) => s + ((i + 1) % LevelTask.GetLength(2) == 0 ? "\n" : " "))));

            FillLevel(LevelTask, LevelComplete);
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (NamePlayer.Text != "")
            {
                File.AppendAllText(@"Saves\LeaderBoard.txt", "\n" + NamePlayer.Text + " " + score);
                Close();
            }
            else
            {
                ErrorLabel.Content = "Введите, пожалуйста, имя";
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NamePlayer.Text = "";
            ExitMessage.Visibility = Visibility.Collapsed;
            Main.Visibility = Visibility.Visible;
            TextA.Visibility = Visibility.Visible;
            PlayGrid.IsEnabled = true;

            BlurEffect myEffect = new BlurEffect();
            myEffect.Radius = 0;
            PlayGrid.Effect = myEffect;
        }
        public void NextLevel()
        {
            BlurEffect myEffect = new BlurEffect();
            myEffect.Radius = 10;
            PlayGrid.Effect = myEffect;

            LevelCompleteStack.Visibility = Visibility.Visible;
            PlayGrid.IsEnabled = false;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            AddFlask.IsEnabled = true;
            L.Visibility = Visibility.Collapsed;
            Result = 0;
            int lineInt = Convert.ToInt32(line);
            lineInt++;
            line = Convert.ToString(lineInt);
            File.WriteAllText(path, line);

            score = score + scoreLvl;
            Score.Text = Convert.ToString(score);
            File.WriteAllText(pathSaveScore, Convert.ToString(score));

            LevelText.Text = "Level " + line;
            Generation(lineInt);

            BlurEffect myEffect = new BlurEffect();
            myEffect.Radius = 0;
            PlayGrid.Effect = myEffect;

            LevelCompleteStack.Visibility = Visibility.Collapsed;
            PlayGrid.IsEnabled = true;
        }

        private void Sver_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            TextA.Background = null;
            TextAI.Source = null;
            LevelReload();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            Header.Visibility = Visibility.Collapsed;
            Main.Visibility = Visibility.Collapsed;
            TextA.Visibility = Visibility.Collapsed;
            Help.Visibility = Visibility.Collapsed;

            BackHelp.Visibility = Visibility.Visible;
            HelpPanel.Visibility = Visibility.Visible;
        }

        private void BackHelp_Click(object sender, RoutedEventArgs e)
        {
            Header.Visibility = Visibility.Visible;
            Main.Visibility = Visibility.Visible;
            TextA.Visibility = Visibility.Visible;
            Help.Visibility = Visibility.Visible;

            BackHelp.Visibility = Visibility.Collapsed;
            HelpPanel.Visibility = Visibility.Collapsed;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            L.Visibility = Visibility.Visible;
            scoreLvl = scoreLvl - 10;
            AddFlask.IsEnabled = false;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        public void FillLevel(string[,,] LevelTask, int lvl)
        {
            char[] Alphabet = Enumerable.Range('a', 'k' - 'a' + 1).Select(c => (char)c).ToArray();
            char[] AlphabetA = Enumerable.Range('A', 'K' - 'A' + 1).Select(c => (char)c).ToArray();
            Button b = new Button();
            Image im = new Image();
            StackPanel s = new StackPanel();
            string name;
            string nameIm;
            string nameA;
            string nameStack;

            for (int i = 0; i <= 10; i++) //кнопки
            {
                for (int j = 0; j <= 3; j++)
                {
                    name = Convert.ToString(Alphabet[i]) + Convert.ToString(j + 1);
                    b = FindName(name) as Button;
                    b.Background = new BrushConverter().ConvertFromString(LevelTask[i, j, 0]) as SolidColorBrush;
                    b.IsEnabled = true;
                }
            }

            for (int i = 0; i <= 3; i++) //перезагрузка доп столбца
            {
                name = "l" + Convert.ToString(i + 1);
                b = FindName(name) as Button;
                b.Background = new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush;
                b.IsEnabled = true;

                nameIm = "l" + Convert.ToString(i + 1) + "i";
                im = FindName(nameIm) as Image;
                im.Source = new BitmapImage(new Uri("icons/ButtIcons/" + "" + ".png", UriKind.RelativeOrAbsolute));
            }

            for (int i = 0; i <= 10; i++) //картинки
            {
                for (int j = 0; j <= 3; j++)
                {
                    nameIm = Convert.ToString(Alphabet[i]) + Convert.ToString(j + 1) + "i";
                    im = FindName(nameIm) as Image;
                    im.Source = new BitmapImage(new Uri("icons/ButtIcons/" + LevelTask[i, j, 1] + ".png", UriKind.RelativeOrAbsolute));
                }
            }

            for (int i = AlphabetA.Length - 1; i >= 0; i--) //активируем StackPanel's
            {
                nameA = Convert.ToString(AlphabetA[i]);
                s = this.FindName(nameA) as StackPanel;
                s.IsEnabled = true;
                s.Visibility = Visibility.Visible;
            }

            int h = LevelComplete + 2;

            for (int i = 10; i >= h; i--)
            {
                nameStack = Convert.ToString(AlphabetA[i]);
                s = FindName(nameStack) as StackPanel;
                s.Visibility = Visibility.Collapsed;
            }
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            string nameButton = ((Button)e.OriginalSource).Name;
            string nameStack = Convert.ToString(nameButton[0]);

            Button but1 = FindName(nameStack + '1') as Button;
            Button but2 = FindName(nameStack + '2') as Button;
            Button but3 = FindName(nameStack + '3') as Button;
            Button but4 = FindName(nameStack + '4') as Button;

            Image im1 = FindName(nameStack + '1' + 'i') as Image;
            Image im2 = FindName(nameStack + '2' + 'i') as Image;
            Image im3 = FindName(nameStack + '3' + 'i') as Image;
            Image im4 = FindName(nameStack + '4' + 'i') as Image;

            StackPanel s = new StackPanel();

            if (isCheck == null)
            {
                if (but4.Background != new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush)
                {
                    isCheckIm = Convert.ToString(im4.Source);
                    MemoryIm = im4;
                    TextAI.Source = new BitmapImage(new Uri(isCheckIm, UriKind.RelativeOrAbsolute));
                    isCheck = new BrushConverter().ConvertToString(but4.Background);
                    Memory = but4;
                    TextA.Background = new BrushConverter().ConvertFromString(isCheck) as SolidColorBrush;
                    if (but3.Background != new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush)
                    {
                        isCheckIm = Convert.ToString(im3.Source);
                        MemoryIm = im3;
                        TextAI.Source = new BitmapImage(new Uri(isCheckIm, UriKind.RelativeOrAbsolute));
                        isCheck = new BrushConverter().ConvertToString(but3.Background);
                        Memory = but3;
                        TextA.Background = new BrushConverter().ConvertFromString(isCheck) as SolidColorBrush;
                        if (but2.Background != new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush)
                        {
                            isCheckIm = Convert.ToString(im2.Source);
                            MemoryIm = im2;
                            TextAI.Source = new BitmapImage(new Uri(isCheckIm, UriKind.RelativeOrAbsolute));
                            isCheck = new BrushConverter().ConvertToString(but2.Background);
                            Memory = but2;
                            TextA.Background = new BrushConverter().ConvertFromString(isCheck) as SolidColorBrush;
                            if (but1.Background != new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush)
                            {
                                isCheckIm = Convert.ToString(im1.Source);
                                MemoryIm = im1;
                                TextAI.Source = new BitmapImage(new Uri(isCheckIm, UriKind.RelativeOrAbsolute));
                                isCheck = new BrushConverter().ConvertToString(but1.Background);
                                Memory = but1;
                                TextA.Background = new BrushConverter().ConvertFromString(isCheck) as SolidColorBrush;
                            }
                        }
                    }
                }
            }
            else
            {
                if (but4.Background == new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush)
                {
                    if (Memory.Name != but4.Name)
                    {
                        im4.Source = new BitmapImage(new Uri(isCheckIm, UriKind.RelativeOrAbsolute));
                        MemoryIm.Source = null;
                        isCheckIm = null;
                        TextAI.Source = null;
                        but4.Background = new BrushConverter().ConvertFromString(isCheck) as SolidColorBrush;
                        Memory.Background = new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush;
                        isCheck = null;
                        TextA.Background = new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush;
                        scoreLvl--;
                    }
                }
                else if (but3.Background == new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush)
                {
                    string Back4 = new BrushConverter().ConvertToString(but4.Background);
                    if ((Back4 == isCheck) && (Memory.Name != but4.Name))
                    {
                        im3.Source = new BitmapImage(new Uri(isCheckIm, UriKind.RelativeOrAbsolute));
                        MemoryIm.Source = null;
                        isCheckIm = null;
                        TextAI.Source = null;
                        but3.Background = new BrushConverter().ConvertFromString(isCheck) as SolidColorBrush;
                        Memory.Background = new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush;
                        isCheck = null;
                        TextA.Background = new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush;
                        scoreLvl--;
                    }
                    else
                    {
                        isCheckIm = null;
                        TextAI.Source = null;
                        isCheck = null;
                        TextA.Background = new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush;
                        AnimatedTranslateTransformA.X = 0;
                        AnimatedTranslateTransformA.Y = 0;
                    }
                }
                else if (but2.Background == new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush)
                {
                    string Back3 = new BrushConverter().ConvertToString(but3.Background);
                    if ((Back3 == isCheck) && (Memory.Name != but3.Name))
                    {
                        im2.Source = new BitmapImage(new Uri(isCheckIm, UriKind.RelativeOrAbsolute));
                        MemoryIm.Source = null;
                        isCheckIm = null;
                        TextAI.Source = null;
                        but2.Background = new BrushConverter().ConvertFromString(isCheck) as SolidColorBrush;
                        Memory.Background = new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush;
                        isCheck = null;
                        TextA.Background = new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush;
                        scoreLvl--;
                    }
                    else
                    {
                        isCheckIm = null;
                        TextAI.Source = null;
                        isCheck = null;
                        TextA.Background = new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush;
                        AnimatedTranslateTransformA.X = 0;
                        AnimatedTranslateTransformA.Y = 0;
                    }
                }
                else if (but1.Background == new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush)
                {
                    string Back2 = new BrushConverter().ConvertToString(but2.Background);
                    if ((Back2 == isCheck) && (Memory.Name != but2.Name))
                    {
                        im1.Source = new BitmapImage(new Uri(isCheckIm, UriKind.RelativeOrAbsolute));
                        MemoryIm.Source = null;
                        isCheckIm = null;
                        TextAI.Source = null;
                        but1.Background = new BrushConverter().ConvertFromString(isCheck) as SolidColorBrush;
                        Memory.Background = new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush;
                        isCheck = null;
                        TextA.Background = new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush;
                        scoreLvl--;
                    }
                    else
                    {
                        isCheckIm = null;
                        TextAI.Source = null;
                        isCheck = null;
                        TextA.Background = new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush;
                        AnimatedTranslateTransformA.X = 0;
                        AnimatedTranslateTransformA.Y = 0;
                    }
                }
                else
                {
                    isCheckIm = Convert.ToString(im1.Source);
                    MemoryIm = im1;
                    TextAI.Source = new BitmapImage(new Uri(isCheckIm, UriKind.RelativeOrAbsolute));
                    isCheck = new BrushConverter().ConvertToString(but1.Background);
                    Memory = but1;
                    TextA.Background = new BrushConverter().ConvertFromString(isCheck) as SolidColorBrush;
                }
            }

            string a1s = new BrushConverter().ConvertToString(but1.Background);
            string a2s = new BrushConverter().ConvertToString(but2.Background);
            string a3s = new BrushConverter().ConvertToString(but3.Background);
            string a4s = new BrushConverter().ConvertToString(but4.Background);

            if ((a1s == a2s) && (a1s == a3s) && (a1s == a4s) && (but1.Background != new BrushConverter().ConvertFromString("Transparent") as SolidColorBrush) && s.IsEnabled != false)
            {
                but1.IsEnabled = false;
                but2.IsEnabled = false;
                but3.IsEnabled = false;
                but4.IsEnabled = false;
                s.IsEnabled = false;
                Result++;
                CheckResult();
            }
        }
    }
}