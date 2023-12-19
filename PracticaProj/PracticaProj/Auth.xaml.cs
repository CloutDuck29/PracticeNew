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
        public int failedAttempts = 0;
        private bool isLocked = false;
        public Auth()
        {
            InitializeComponent();
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var auth = new Authentication();

            //аунтентификация пользователя
            if (!await auth.Authenticate(loginTxtBox.Text, passwordTxtBox.Password))
            {

                if (failedAttempts == 2)
                {
                    MessageBox.Show("ОКНО ЗАБЛОКИРОВАНО!");
                    Thread.Sleep(TimeSpan.FromSeconds(30));
                    failedAttempts = 0;
                    isLocked = false;
                    return;
                }
                failedAttempts++;
                MessageBox.Show($"Неверное имя пользователь или пароль. У Вас осталось - {3 - failedAttempts} попыток");

                if (!isLocked)
                {

                    try
                    {
                        var result = await auth.Authenticate(loginTxtBox.Text, passwordTxtBox.Password);
                        if (result)
                        {
                            OrdersWindow newWindow = new OrdersWindow();
                            newWindow.Show();
                            this.Close();
                        }
                    }
                    catch
                    {
                        
                    }
                }

            }
            else
            {
                OrdersWindow newWindow = new OrdersWindow();
                newWindow.Show();
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
