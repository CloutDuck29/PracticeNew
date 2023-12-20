using PracticaProj.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PracticaProj
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        //счет неправильных паролей
        private bool isLocked = false;
        private DispatcherTimer Timer;
        private bool isCodeClose = false;
        public Auth()
        {
            InitializeComponent();
            Authentication.session.LoadUserSession();
            if (Authentication.session.BlockDate > DateTime.Now)
                gridAuth.Visibility = Visibility.Hidden;
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(timer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(isCodeClose)
            {
                base.OnClosing(e);
                return;
            }
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти из приложения?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Authentication.session.CloseUserSession();
                Application.Current.Shutdown();
            }

            base.OnClosing(e);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (Authentication.session.BlockDate < DateTime.Now)
            {
                gridAuth.Visibility = Visibility.Visible;
            }
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            //аунтентификация пользователя
            using (var db = PracticeNewEntities.GetContext())
            {
                if(!db.Users.Any(y => y.login == loginTxtBox.Text))
                {
                    MessageBox.Show("Пользователя не сущетсвует");

                    return;
                }
            }


            if (!await Authentication.Authenticate(loginTxtBox.Text, passwordTxtBox.Password))
            {
                Authentication.failedAttempts++;
                if (Authentication.failedAttempts == 1)
                {
                    Captcha newWindow = new Captcha(this);
                    newWindow.Show();
                    this.Hide();
                }

                //проверка на 3 попытки
                if (Authentication.failedAttempts == 3)
                {
                    MessageBox.Show("ОКНО ЗАБЛОКИРОВАНО!");
                    Authentication.session.AddBlockSession();
                    gridAuth.Visibility = Visibility.Hidden;
                    Authentication.failedAttempts = 0;
                    isLocked = false;
                    return;
                }
                MessageBox.Show($"Неверное имя пользователь или пароль. У Вас осталось - {3 - Authentication.failedAttempts} попыток");

                if (!isLocked)
                {

                    try
                    {
                        var result = await Authentication.Authenticate(loginTxtBox.Text, passwordTxtBox.Password);
                        if (result)
                        {
                            OrdersWindow newWindow = new OrdersWindow();
                            newWindow.Show();
                            Authentication.session.OpenUserSession(loginTxtBox.Text);
                            this.Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Пользователь заблокирован");
                    }
                }
            }
            else
            {
                OrdersWindow newWindow = new OrdersWindow();
                newWindow.Show();
                Authentication.session.OpenUserSession(loginTxtBox.Text);
                isCodeClose = true;
                this.Close();
            }
        }

        //открываем окно регистрации
        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            Registration newWindow = new Registration();
            newWindow.Show();
            isCodeClose = true;
            this.Close();
        }
    }
}
