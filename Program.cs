using System;
using System.IO;
using System.Reflection;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace ClassicEncryptionAlgorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] methods = { "Метод перестановки символов", "Метод гаммирования",
                "Метод Виженера", "Шифр Полибия", "Метод Playfair", "Метод аффинного шифра" }; 

            chooseMethod(methods); 
            string method = getAnswerOnChoose(methods);

            Console.WriteLine("\nВыберите действие:\n1. Шифрование\n2. Дешифрование\n");
            int action = getAction(); //1 = шифрование; 2 = дешифрование

            Console.WriteLine("\nКак Вы хотите ввести текст?\n1. В консоль\n2. Через файл\n");
            string text = getText();

        }

        public static void chooseMethod(string[] methods) // вывод в консоль списка методов
        {
            Console.WriteLine("Выберите метод: ");
            for (int i = 0; i < methods.Length; i++)
            {
                Console.WriteLine(i + 1 + ". " + methods[i]);
            }
            Console.WriteLine(" ");
        }

        public static string getAnswerOnChoose(string[] methods) // выбор метода из списка
        {
            string result = "";
            try
            {
                int method = Convert.ToInt32(Console.ReadLine());

                while (method < 1 || method > methods.Length)
                {
                    Console.WriteLine("Метод с указанным номером не обнаружен. Введите другой номер. ");
                    method = Convert.ToInt32(Console.ReadLine());
                }
                Console.WriteLine("Вы выбрали: " + methods[method - 1]);
                result = methods[method - 1];
            }

            catch (Exception e) 
            {
                Console.Write("Не верный формат ввода. Введите число из преставленного списка. \n");
                getAnswerOnChoose(methods);
            }

            return result;
        }

        public static int getAction() // запрос вида криптографического преобразования
        {
            int action = 0;
            try
            {
                action = Convert.ToInt32(Console.ReadLine());
                while (action != 1 && action != 2)
                {
                    Console.WriteLine("Действие с указанным номером не обнаружено. Выберите номер из списка.");
                    action = Convert.ToInt32(Console.ReadLine());
                }
                if (action == 1)
                {
                    Console.WriteLine("Вы выбрали шифрование.");
                }
                else Console.WriteLine("Вы выбрали дешифрование.");
            }

            catch (Exception e)
            {
                Console.Write("Не верный формат ввода. Введите число из преставленного списка.\n");
                getAction();
            }

            return action;
        }

        public static string getText() // чтение исходного текста пользователя
        {
            string text = "";
            int choose = 0;
            try
            {
                choose = Convert.ToInt32(Console.ReadLine());
                while (choose != 1 && choose != 2)
                {
                    Console.WriteLine("Метод с указанным номером не обнаружен. Выберите номер из списка.");
                    choose = Convert.ToInt32(Console.ReadLine());
                }
            }

            catch (Exception e)
            {
                Console.Write("Не верный формат ввода. Введите число из преставленного списка.\n");
                getAction();
            }

            if (choose == 1)
            {
                Console.WriteLine("Вы выбрали ввод текста через консоль. Текст будет вводиться до нажатия на клавишу Enter.");
                text = Console.ReadLine();
                Console.WriteLine("\nИсходный текст из консоли:\n" + text);
            }
            else
            {
                Console.WriteLine("Вы выбрали ввод текста из файла. Введите путь к файлу.");
                string pathToFile = Console.ReadLine();

                while (!File.Exists(pathToFile))
                {
                    Console.WriteLine("Данный файл не существует по указанному пути. " +
                        "Перепроверьте данные и попробуйте снова.");
                    pathToFile = Console.ReadLine();
                }
                text = File.ReadAllText(pathToFile);
                Console.WriteLine("\nИсходный текст из файла:\n" + text);
            }

            return text;
        }


    }
}