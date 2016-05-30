using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using _EPAM_UsersNote.BLL_Logic;
using _EPAM_UsersNote.Entites;
using _EPAM_UsersNote.Interfaces.BLL;

namespace _EPAM_UsersNote.PL.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string logPath = ConfigurationManager.AppSettings["LogPath"];
            FileStream stream = File.Create(logPath);  
            stream.Close();
            IUserNoteBLL start = null;
            IUserNoteAwardBLL startAward = null;
            try
            {
                start = new UsersNoteLogic();
                startAward = new UsersNoteAwardLogic();
            }
            catch (Exception e)
            {
                using (StreamWriter str = File.AppendText(logPath))
                {
                    str.WriteLine("{0}-{1}-{2}", e.Message, e.Source, e.TargetSite.Name);
                }
            }

            int count = 0;
            do
            {
                System.Console.WriteLine("Выберите действие:");
                System.Console.WriteLine("1 - Просмотр всех пользователей");
                System.Console.WriteLine("2 - Детальный просмотр пользователя");
                System.Console.WriteLine("3 - Добавить пользователя");
                System.Console.WriteLine("4 - Добавить награду");
                System.Console.WriteLine("5 - Наградить пользователя");
                System.Console.WriteLine("6 - Удалить пользователя");
                System.Console.WriteLine("7 - Удалить награду");
                System.Console.WriteLine("8 - Удалить награду у пользователя");
                System.Console.WriteLine("9 - Выход из программы");
                System.Console.Write("Введите номер:");
                while (!int.TryParse(System.Console.ReadLine(), out count))
                {
                }

                try
                {
                    switch (count)
                    {
                        case 1:
                        {
                            int temp = 0;
                            foreach (var item in start.GetAllUsers())
                            {
                                System.Console.WriteLine("{0} - {1}", ++temp, item.Name);
                            }
                        }

                            break;
                        case 2:
                        {
                            int temp = 0;
                            var collection = start.GetAllUsers();
                            foreach (var item in collection)
                            {
                                System.Console.WriteLine("{0} - {1}", ++temp, item.Name);
                            }

                            System.Console.Write("Введите номер:");
                            temp = 0;
                            int.TryParse(System.Console.ReadLine(), out temp);
                            if (temp > 0 && temp < collection.Count() + 1)
                            {
                                var item = collection.ElementAt(temp - 1);
                                System.Console.WriteLine();
                                System.Console.WriteLine("{0} - {1:d} г.р., {2} лет", start.GetUser(item.Id).Name, start.GetUser(item.Id).DateofBirth.Date, start.GetUser(item.Id).Age);
                                System.Console.WriteLine("Награжден:");
                                int y = 0;
                                foreach (var item1 in item.Awards)
                                {
                                    System.Console.WriteLine("{0} - {1}", ++y, item1);
                                }
                            }
                        }

                            break;
                        case 3:
                        {
                            System.Console.WriteLine("Введите имя пользователя:");
                            var name = System.Console.ReadLine();
                            System.Console.WriteLine("Введите дату рождения:");
                            DateTime date;
                            DateTime.TryParse(System.Console.ReadLine(), out date);
                            start.CreateUser(new Entites.User(name, date));
                        }

                            break;
                        case 4:
                        {
                            System.Console.WriteLine("Введите название награды");
                            string newAward = System.Console.ReadLine();
                                startAward.CreateAward(new Award(newAward));
                        }

                            break;
                        case 5:
                        {
                            System.Console.Clear();
                            if (start.GetAllUsers().Count() == 0)
                            {
                                System.Console.WriteLine("Нет пользователей для награждения, добавьте пользователя");
                                break;
                            }
                            else if (startAward.GetAllAwards().Count() == 0)
                            {
                                System.Console.WriteLine("Нет доступных наград, добавьте награду");
                                break;
                            }
                            else
                            {
                                int temp = 0;
                                foreach (var item in start.GetAllUsers())
                                {
                                    System.Console.WriteLine("{0} - {1}", ++temp, item.Name);
                                }

                                System.Console.WriteLine();
                                temp = 0;
                                foreach (var item in startAward.GetAllAwards())
                                {
                                    System.Console.WriteLine("{0} - {1}", ++temp, item.Title);
                                }

                                System.Console.WriteLine();
                                System.Console.WriteLine("Введите номер пользователя");
                                int x;
                                do
                                {
                                    int.TryParse(System.Console.ReadLine(), out x);
                                }
                                    while (x >= 0 & x < start.GetAllUsers().Count());
                                System.Console.WriteLine("Введите номер награды");
                                int y;
                                do
                                {
                                    int.TryParse(System.Console.ReadLine(), out y);
                                }
                                    while (y >= 0 & y < startAward.GetAllAwards().Count());

                                start.ToAwardUser(start.GetAllUsers().ElementAt(x - 1), startAward.GetAllAwards().ElementAt(y - 1));
                                break;
                            }
                        }

                        case 6:
                        {
                            int temp = 0;
                            foreach (var item in start.GetAllUsers())
                            {
                                System.Console.WriteLine("{0} - {1}", ++temp, item.Name);
                            }

                            System.Console.WriteLine("Введите номер пользователя для удаления");
                            int x;
                            int.TryParse(System.Console.ReadLine(), out x);
                            start.DeleteUser(start.GetAllUsers().ElementAt(x - 1).Id);
                        }

                            break;
                        case 7:
                        {
                            int temp = 0;
                            foreach (var item in startAward.GetAllAwards())
                            {
                                System.Console.WriteLine("{0} - {1}", ++temp, item.Title);
                            }

                            System.Console.WriteLine("Введите номер награды для удаления");
                            int x;
                            int.TryParse(System.Console.ReadLine(), out x);
                                startAward.DeleteAward(startAward.GetAllAwards().ElementAt(x - 1).Id);
                        }

                            break;

                        case 8:
                        {
                            int temp = 0;
                            foreach (var item in start.GetAllUsers())
                            {
                                System.Console.WriteLine("{0} - {1}", ++temp, item.Name);
                            }

                            System.Console.WriteLine("Введите номер пользователя");
                            int x;
                            int.TryParse(System.Console.ReadLine(), out x);

                            if (x > 0 && x < start.GetAllUsers().Count() + 1)
                            {
                                var item = start.GetAllUsers().ElementAt(x - 1);
                                System.Console.WriteLine();
                                System.Console.WriteLine("{0} - {1} г.р., {2} лет", start.GetUser(item.Id).Name, start.GetUser(item.Id).DateofBirth.Date, start.GetUser(item.Id).Age);
                                System.Console.WriteLine("Награжден:");
                                int y = 0;
                                foreach (var item1 in item.Awards)
                                {
                                    System.Console.WriteLine("{0} - {1}", ++y, item1);
                                }

                                System.Console.WriteLine("Введите номер награды для удаления");
                                int z = 0;
                                int.TryParse(System.Console.ReadLine(), out z);
                                if (z > 0 && z < item.Awards.Count() + 1)
                                {
                                    item.Awards[z - 1] = null;
                                }
                            }
                        }

                            break;
                    }
                }
                catch (Exception e)
                {
                    using (StreamWriter str = File.AppendText(logPath))
                    {
                        str.WriteLine("{0}-{1}-{2}", e.Message, e.Source, e.TargetSite.Name);
                    }
                }

                System.Console.ReadLine();
                System.Console.Clear();
            }
            while (count != 9);
            try
            {
                start.WriteUsers();
            }
            catch (IOException e)
            {
                using (StreamWriter str = File.AppendText(logPath))
                {
                    str.WriteLine("{0}-{1}-{2}", e.Message, e.Source, e.TargetSite.Name);
                }
            }
            catch (Exception e)
            {
                using (StreamWriter str = File.AppendText(logPath))
                {
                    str.WriteLine("{0}-{1}-{2}", e.Message, e.Source, e.TargetSite.Name);
                }
            }
        }  
    }
}
