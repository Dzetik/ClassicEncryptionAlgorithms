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

            Console.WriteLine(method);
        }

        public static void chooseMethod(string[] methods) // вывод в консоль списка
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

       
    }
}