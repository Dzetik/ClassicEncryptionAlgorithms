using System;
using System.IO;
using System.Reflection;
using System.Text;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

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
            text = "А мне ещё делать 6 методов шифрования";

            string key = getKey();

            string finalText = choosingSolutionMethod(method, action, key, text);
            Console.WriteLine("\n" + finalText);

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

        public static string getKey()
        {
            Console.WriteLine("\nВведите ключ:");
            string key = Console.ReadLine();
            while (key == "")
            {
                Console.WriteLine("Вы не ввели ключ. Попробуйте снова.");
                key = Console.ReadLine();
            }
            Console.WriteLine("Введенный ключ: " + key);
            return key;
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
                while (text == "")
                {
                    Console.WriteLine("Вы не ввели текст. Попробуйте снова.");
                    text = Console.ReadLine();
                }
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

        public static int getKeyForIntMetods(string strKey)
        {
            int key = 0;
            try
            {
                key = Convert.ToInt32(strKey);
                while (key < 2 || key > 8)
                {
                    Console.Write("Не верный ключ. Для данного метода введите целое число от 2 до 8.\n");
                    key = Convert.ToInt32(getKey());
                }
            }
            catch (Exception e)
            {
                getKeyForIntMetods("0");
            }
            return key;

        }

        public static string choosingSolutionMethod(string method, int action, string key, string text)
        {
            switch (method)
            {
                case "Метод перестановки символов":
                    if (action == 1)
                    {
                        return charPermutationEncryption(text, key);
                    }
                    else 
                    { 
                        return charPermutationDecryption(text); 
                    }
               /* case "Метод гаммирования":
                    if (action == 1)
                    {

                    }
                    else { }
                    break;
                case "Метод Виженера":
                    if (action == 1)
                    {

                    }
                    else { }
                    break;
                case "Шифр Полибия":
                    if (action == 1)
                    {

                    }
                    else { }
                    break;
                case "Метод Playfair":
                    if (action == 1)
                    {

                    }
                    else { }
                    break;
                case "Метод аффинного шифра":
                    if (action == 1)
                    {

                    }
                    else { }
                    break;*/
            }
            return "aaa";
        }

        public static string charPermutationEncryption(string text, string strKey)
        {
            getKeyForIntMetods(strKey);



            return text;
        }
        public static string charPermutationDecryption(string text)
        {
            return text;
        }

    }
}