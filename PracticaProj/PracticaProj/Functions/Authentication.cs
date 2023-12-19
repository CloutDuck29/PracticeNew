using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO.Packaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PracticaProj.Functions
{
    public static class Authentication
    {

        public static int failedAttempts = 0;

        //Принимает логин и пароль, хэширует его и сохраняте  нового пользователя в БД
        public static void Register (string username, string password, string name, string surname, string patronymics)
        {
            using (var db = PracticeNewEntities.GetContext())
            {
                var hashedPassword = ComputeHash(password, new SHA256CryptoServiceProvider());
                db.Users.Add(new User { login = username, password = hashedPassword, first_name = name, family_name = surname, patronymic = patronymics });
                db.SaveChanges();
            }
        }

        //Принимает логин и пароль, проверяет существует ли пользователь с таким именем в БД, и возвращает
        public static async Task<bool> Authenticate(string username, string password)
        {

            using (var db = PracticeNewEntities.GetContext())
            {
                var user = await db.Users.SingleOrDefaultAsync(x => x.login == username);
                if (user == null || !VerifyPassword(password, user.password))
                {
                    return false;
                }
                return true;
            }
        }

        //Принимает логин и пароль, проверяет существует ли логин в БД
        public static bool IsLoginTaken(string username)
        {
            using (var db = PracticeNewEntities.GetContext())
            {
                return db.Users.Any(x => x.login == username);
            }

        }

        //Принимает пароль и алгоритм хэширования, возвращает хэш пароля
        public static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
        //Принимает пароль и хэш пароля, сравнивает их
        public static bool VerifyPassword(string input, string hashedPassword)
        {
            string hashedInput = ComputeHash(input, new SHA256CryptoServiceProvider());
            return hashedInput == hashedPassword;
        }
    }
}
