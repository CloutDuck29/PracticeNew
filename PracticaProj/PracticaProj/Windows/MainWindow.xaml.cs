using PracticaProj.Functions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace PracticaProj
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var auth = new Authentication();

            //регистрация нового пользователя
            auth.Register(loginTxtBox.Text, passwordTxtBox.Password, nameTxtBox.Text, surnameTxtBox.Text, patronymicTxBox.Text);

            //аунтентификация пользователя
            if (!auth.Authenticate(loginTxtBox.Text, passwordTxtBox.Password))
                MessageBox.Show("Неверное имя пользователь или пароль");
            else
            {
                
                OrdersWindow newWindow = new OrdersWindow();
                newWindow.Show();
                this.Close();
            }

        }

    }
}
