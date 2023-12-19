using PracticaProj.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : Window
    {
        CaptchaGenerator captcha = new CaptchaGenerator();
        public Captcha()
        {
           InitializeComponent();
           captcha.ShowCaptcha(captchaLbl);

        }

        private void updButton_Click(object sender, RoutedEventArgs e)
        {
            captcha.ShowCaptcha(captchaLbl);   
        }

        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            if(txtBoxForCaptcha.Text == captcha._captcha)
            {
                MessageBox.Show("Каптча введена корректно!");
                CaptchaWindow.Close();
            }
            else
            {
                MessageBox.Show("Каптча неверна, попробуйте еще раз!");
                captcha.Generate();
                captcha.ShowCaptcha(captchaLbl);
            }
        }
    }
}
