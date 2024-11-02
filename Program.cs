using System;
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
                Console.Write("Не верный формат ввода. Введите число из преставленного списка. \n");
                getAction();
            }

            return action;
        }


    }
}