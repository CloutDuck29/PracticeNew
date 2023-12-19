using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PracticaProj.Functions
{
    public class Authentication
    {
        //Принимает логин и пароль, хэширует его и сохраняте  нового пользователя в БД
        public void Register (string username, string password)
        {
            using (var db = PracticeNewEntities.GetContext())
            {
                
                var hashedPassword = ComputeHash(password, new SHA256CryptoServiceProvider());
                db.Users.Add(new User { login = username, password = hashedPassword, family_name = "test", first_name = "test" });
                db.SaveChanges();

            }
        }

        //Принимает логин и пароль, проверяет существует ли пользователь с таким именем в БД, и возвращает
        public bool Authenticate(string username, string password)
        {
            MessageBox.Show(username, password);

            using (var db = PracticeNewEntities.GetContext())
            {
                var user = db.Users.SingleOrDefault(x => x.login == username);
                if (user == null || !VerifyPassword(password, user.password))
                {
                    return false;
                }
                return true;
            }
        }

        //Принимает логин и пароль, проверяет существует ли логин в БД
        public bool IsLoginTaken(string username)
        {
            using (var db = PracticeNewEntities.GetContext())
            {
                return db.Users.Any(x => x.login == username);
            }

        }

        //Принимает пароль и алгоритм хэширования, возвращает хэш пароля
        public string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
        //Принимает пароль и хэш пароля, сравнивает их
        public bool VerifyPassword(string input, string hashedPassword)
        {
            string hashedInput = ComputeHash(input, new SHA256CryptoServiceProvider());
            return hashedInput == hashedPassword;
        }
    }
}
