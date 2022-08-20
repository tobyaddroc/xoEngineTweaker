using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

#pragma warning disable CS8600
#pragma warning disable IDE0059

namespace xoEngineTweaker
{
    public static class Entry
    {
        public static int Main()
        {
            int _3x3;
            var wc = new WebClient();
            string passwd, ip;
            startpoint:
            Console.Write("Патчить 3x3 игру? (1 or 0): ");
            _3x3 = Convert.ToInt32(Console.ReadLine());
            Console.Write("IP сервера: ");
            ip = Console.ReadLine();
            Console.Write("Пароль сервера: ");
            passwd = Console.ReadLine();
            Console.Write("\nУстанавливается соединение с сервером... ");
            string establish, verify;
            establish = wc.DownloadString($"http://{ip}/xoengine/connect.php?logintest");
            if (establish == "0x1: Welcome")
            {
                Console.Write("Успешно\n");
                Console.Write($"Ответ сервера: {establish}\n");
                Console.Write("Проверка пароля... ");
                verify = wc.DownloadString($"http://{ip}/xoengine/connect.php?usepassword={passwd}");
                if (verify == "0x391: Bad password")
                {
                    Console.Write("Неудачно\nСервер отклонил введенный вами пароль, давайте попробуем стандартный пароль.\nПроверка пароля... ");
                    verify = wc.DownloadString($"http://{ip}/xoengine/connect.php?usepassword=547896321");
                    if (verify == "0x391: Bad password")
                    {
                        Console.Write("Неудачно\n");
                        goto startpoint;
                    }
                    else if (verify == "0x2: Connection allowed")
                    {
                        passwd = "547896321";
                        Console.Write("Успешно\n");
                    }
                }
                else if (verify == "0x2: Connection allowed")
                {
                    Console.Write("Успешно\n");
                }
            }
            else
            {
                Console.Write("Неудачно\n");
                goto startpoint;
            }
            if (_3x3 == 1)
            {
                Console.WriteLine("Отправка полезной нагрузки... Удачно");
                Console.Write("Вы можете изменять данные на сервере,\nдля этого необходимо ввести 10 чисел.\nЧисло 0 = клетка/ход не определены\nЧисло 1 = определено крестиками\nЧисло 2 = определено ноликами\n1-9 символы в строке с введенными вами числами, это клетки\nА последняя десятая цифра, это ход\nПример отправки данных: 0001000002 - Ходят нолики, 4 клетка занята крестиками, все остальное не занято.\nПопробуйте: ");
                string srvbuf = Console.ReadLine();
                Console.Write($"Отправка полезной нагрузки с данными RDATA=\"{srvbuf}\"... ");
                string srvbufans = wc.DownloadString($"http://{ip}/xoengine/pushup.php?binarydata={srvbuf}&usepassword={passwd}");
                if (srvbufans == "0x9: Pushed")
                {
                    Console.Write("Успешно");
                    goto startpoint;
                }
                else
                {
                    Console.Write("Неудачно");
                    goto startpoint;
                }
            }
            else if (_3x3 == 0)
            {
                Console.WriteLine("Отправка полезной нагрузки... Удачно");
                Console.Write("Вы можете изменять данные на сервере,\nдля этого необходимо ввести 17 чисел.\nЧисло 0 = клетка/ход не определены\nЧисло 1 = определено крестиками\nЧисло 2 = определено ноликами\n1-9 символы в строке с введенными вами числами, это клетки\nА последняя 17 цифра, это ход\nПример отправки данных: 00010000000000002 - Ходят нолики, 4 клетка занята крестиками, все остальное не занято.\nПопробуйте: ");
                string srvbuf = Console.ReadLine();
                Console.Write($"Отправка полезной нагрузки с данными RDATA=\"{srvbuf}\"... ");
                string srvbufans = wc.DownloadString($"http://{ip}/xoengine/pushup.php?binarydata={srvbuf}&usepassword={passwd}");
                if (srvbufans == "0x9: Pushed")
                {
                    Console.Write("Успешно\n");
                    goto startpoint;
                }
                else
                {
                    Console.Write("Неудачно\n");
                    goto startpoint;
                }
            }
            return 0;
        }
    }
}
