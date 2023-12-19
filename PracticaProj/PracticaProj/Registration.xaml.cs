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
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            Auth newWindow = new Auth();
            newWindow.Show();
            this.Close();
        }


        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            
            var auth = new Authentication();
            if(auth.IsLoginTaken(loginTxtBox.Text) == true)
            {
                MessageBox.Show("Логин уже занят");
            }
            else
            {
                //регистрация нового пользователя
                auth.Register(loginTxtBox.Text, passwordTxtBox.Password, nameTxtBox.Text, surnameTxtBox.Text, patronymicTxBox.Text);
                MessageBox.Show("Вы успешно зарегистрировались!");
                Auth newWindow = new Auth();
                newWindow.Show();
                this.Close();
            }


        }
    }
}
