using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PracticaProj.Functions
{
    public class CaptchaGenerator
    {
        private static Random random = new Random();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public string _captcha;

        public string Generate()
        {
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void ShowCaptcha(System.Windows.Controls.Label label)
        {
            var captchaGenerator = new CaptchaGenerator();
            var captcha = captchaGenerator.Generate();

            //Сохраните каптчу в переменной, чтобы проверить ввод пользователя
            _captcha = captcha;

            // Отобразите каптчу на экране
            label.Content = captcha;
        }
    }
}
