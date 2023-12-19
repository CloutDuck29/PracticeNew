using PracticaProj.Functions;
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

        public Auth()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var auth = new Authentication();

            //аунтентификация пользователя
            if (!auth.Authenticate(loginTxtBox.Text, passwordTxtBox.Password))
            {
                failedAttempts++;
                MessageBox.Show($"Неверное имя пользователь или пароль. У Вас осталось - {3 - failedAttempts} попыток");
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
