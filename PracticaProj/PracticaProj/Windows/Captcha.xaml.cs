﻿using PracticaProj.Functions;
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
            captcha.CheckCaptcha(txtBoxForCaptcha, CaptchaWindow);

        }

        private void updButton_Click(object sender, RoutedEventArgs e)
        {
            captcha.ShowCaptcha(captchaLbl);   
        }


    }
}
