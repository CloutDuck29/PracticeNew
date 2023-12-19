using PracticaProj.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracticaProj
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        //счет неправильных паролей
        private bool isLocked = false;
       
        public Auth()
        {
            InitializeComponent();
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            //аунтентификация пользователя
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
                    Thread.Sleep(TimeSpan.FromSeconds(30));
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
                this.Close();
            }
        }

        //открываем окно регистрации
        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            Registration newWindow = new Registration();
            newWindow.Show();
            this.Close();
        }
    }
}
