using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Net;
using Newtonsoft.Json;
using System.Text.RegularExpressions;



namespace RegistrationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string patternEmail = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$";
            string patternPhone = @"^\+?[78][-\(]?\d{3}\)?-?\d{3}-?\d{2}-?\d{2}$";
            string patternPassword = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";
            int choose;
            string enterPhone;
            string enterPass;
            bool enterCheck=false;
            Account acc = new Account
            {
                Email = "eldar.oralov@gmail.com",
                Phone = "87718879988",
                Password = "qwerty",
                City = "Astana",
                Age = 24
            };
            Console.Write("1-Регистрация\n2-Вход\n3-Выход\n");
            while (!int.TryParse(Console.ReadLine(), out choose))
            {
                Console.WriteLine("Неправильно введен тип");
            }
            switch (choose)
            {
                case 1:
                    Console.WriteLine("Введите Email: ");
                    Regex r = new Regex(patternEmail, RegexOptions.IgnoreCase);
                    while(!r.Match(acc.Email = Console.ReadLine()).Success){
                        Console.WriteLine("Неправильно введен email");
                    }
                    Console.WriteLine("Введите номер телефона: ");
                    r = new Regex(patternPhone, RegexOptions.IgnoreCase);
                    while (!r.Match(acc.Phone = Console.ReadLine()).Success)
                    {
                        Console.WriteLine("Неправильно введен номер");
                    }
                    Console.WriteLine("Придумайте пароль: \n");
                    r = new Regex(patternPassword, RegexOptions.IgnoreCase);
                    while (!r.Match(acc.Password = Console.ReadLine()).Success)
                    {
                        Console.WriteLine("Пароль должен содержать число, буквы в верхнем и нижнем регистре и быть не короче 8 символов");
                    }
                    Console.WriteLine("Введите ваш город: ");
                    acc.City = Console.ReadLine();             
                    Console.WriteLine("Введите ваш возраст: ");
                    acc.Age = int.Parse(Console.ReadLine());
                    using (BinaryWriter writer = new BinaryWriter(File.Open("Data.bin", FileMode.OpenOrCreate)))
                    {
                        writer.Seek(0, SeekOrigin.End);
                        writer.Write(acc.Email);
                        writer.Write(acc.Phone);
                        writer.Write(acc.Password);
                        writer.Write(acc.City);
                        writer.Write(acc.Age);
                    }
                    break;
                case 2:
                    Console.WriteLine("Введите номер телефона: ");
                    enterPhone = Console.ReadLine();
                    Console.WriteLine("Введите пароль: ");
                    enterPass = Console.ReadLine();
                    using (BinaryReader reader = new BinaryReader(File.Open("Data.bin", FileMode.Open)))
                    {
                        while (reader.PeekChar() > -1)
                        {
                            string Email = reader.ReadString();
                            string Phone = reader.ReadString();
                            string Password = reader.ReadString();
                            string City = reader.ReadString();
                            int Age = reader.ReadInt32();

                            if (Phone == enterPhone && Password == enterPass)
                            {
                                enterCheck = true;
                                break;
                            }                            
                        }
                    }
                    if (enterCheck==true)
                    {
                        Console.WriteLine("Вы вошли в аккаунт ");
                    }
                    break;
                case 3:
                    break;
                
            }


            using (BinaryReader reader = new BinaryReader(File.Open("Data.bin", FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    string Email = reader.ReadString();
                    string Phone = reader.ReadString();
                    string Password = reader.ReadString();
                    string City = reader.ReadString();
                    int Age = reader.ReadInt32();


                    Console.WriteLine("Chto to: " + Email + Phone + Password + City + Age);
                }
            }




            Console.ReadKey();
        }
    }

}
