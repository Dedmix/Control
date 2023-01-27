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
using System.Windows.Threading;

namespace _3063User_5.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        int CaptchaCouter = 0;
        string capthaSample = "";
        Random random = new Random();
        public Authorization()
        {
            InitializeComponent();
            Captcha.Text = "";
#if DEBUG
            Login.Text = "dlh4o1tzrbse@yahoo.com";
            Password.Password = "2L6KZG";
#endif
            Labelcap.Visibility = Visibility.Collapsed;
            CaptchaCanvas.Visibility = Visibility.Collapsed;
            Captcha.Visibility = Visibility.Collapsed;
        }

        private void input_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Class.Class1.GetUserCatalog());
        }

        private void logged_Click(object sender, RoutedEventArgs e) // проверка логина и пароля 
        {
            if (Login.Text != "" && Password.Password != "")
            {
                if (Class.Class2.Autgorize(Login.Text, Password.Password) == true)
                {
                    if (Captcha.Text == capthaSample)
                    {
                        var userdata = Class.Class2.GetUserDate();
                        if (userdata != null)
                        {
                            var userrole = userdata.UserRole;
                            switch (userrole)
                            {
                                case "Администратор":
                                    NavigationService.Navigate(Class.Class1.GetAdminCatalog());
                                    break;
                                case "Менеджер":
                                    NavigationService.Navigate(Class.Class1.GetUserCatalog());
                                    break;
                                case "Клиент":
                                    NavigationService.Navigate(Class.Class1.GetUserCatalog());
                                    break;
                                default:
                                    return;
                            }
                            Login.Text = null;
                            Password.Password = null;
                            Captcha.Text = null;
                        }
                    }
                    else if (CaptchaCouter < 1)
                    {
                        CaptchaCouter++;
                        MessageBox.Show("Неверная капча!");
                        CaptchaCreate();
                        //CaptchaSample.Visibility = Visibility.Visible;
                        Labelcap.Visibility = Visibility.Visible;
                        CaptchaCanvas.Visibility = Visibility.Visible;
                        Captcha.Visibility = Visibility.Visible;
                        Login.Text = null;
                        Password.Password = null;
                        Captcha.Text = null;
                        return;
                    }
                    else
                    {
                        CaptchaCouter = 0;
                        BlockActivate();
                        Login.Text = null;
                        Password.Password = null;
                        Captcha.Text = null;
                        return;
                    }
                }
                else
                {
                    CaptchaCouter++;
                    MessageBox.Show("Не существует такого пользователя!");
                    CaptchaCreate();
                    //CaptchaSample.Visibility = Visibility.Visible;
                    Labelcap.Visibility = Visibility.Visible;
                    CaptchaCanvas.Visibility = Visibility.Visible;
                    Captcha.Visibility = Visibility.Visible;
                    Login.Text = null;
                    Password.Password = null;
                    Captcha.Text = null;
                    return;
                }
            }
            else
            {
                MessageBox.Show("Не введен пароль и логин!");
                return;
            }
        }
        private void CaptchaCreate() //геренатор капчи
        {
            CaptchaCanvas.Children.Clear();
            string letters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";

            for (int i = 0; i < 5; i++)
            {
                var Char = letters.Substring(random.Next(0, letters.Length - 1), 1);
                capthaSample += Char;
                LetterCreate(Char, i);
                LineCreate();
            }
        }

        private void LineCreate() //линии
        {
            Line a = new Line();
            a.StrokeThickness = random.Next(1, 4);
            a.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            a.X1 = 5;
            a.Y1 = random.Next(10, 50);
            a.X2 = 240;
            a.Y2 = random.Next(10, 50);
            CaptchaCanvas.Children.Add(a);
        }

        private void LetterCreate(string a, int i) //текст
        {
            Label letter = new Label();
            letter.Content = a;
            letter.FontSize = 30;
            letter.RenderTransformOrigin = new Point(0.5, 0.5);
            letter.RenderTransform = new RotateTransform(random.Next(-30, 30));
            letter.FontWeight = FontWeights.Bold;
            CaptchaCanvas.Children.Add(letter);
            Canvas.SetLeft(letter, 50 + (i * 20));
            Canvas.SetTop(letter, random.Next(1, 10));
        }

        private void BlockActivate()
        {
            StackPanel.IsEnabled = false;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += new EventHandler((s, e) =>
            {
                timer.Stop();
                StackPanel.IsEnabled = true;
            });
            timer.Start();
        }
    }
}
