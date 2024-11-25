using System;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
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

        public static string choosingSolutionMethod(string method, int action, string strKey, string text)
        {
            //абвгдеёжзийклмнопрстуфхцчшщъыьэюя 123456789.,?;:!+-= */ ()[]{ }
            string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя 123456789.,?;:!+-= */ ()[]{ }";
            //string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int key = getKeyForIntMetods(strKey);

            switch (method)
            {              
                case "Метод перестановки символов":
                    if (action == 1)
                    {
                        return charPermutationEncryption(text, key);
                    }
                    else 
                    { 
                        return charPermutationDecryption(text, key); 
                    }
               case "Метод гаммирования":
                    Console.WriteLine("\nДанный метод распознает только русский алфавит, цифры и часть символов.");
                    if (action == 1)
                    {
                        return gammingMethodEncryption(text, strKey, alphabet);
                    }
                    else 
                    {
                        return gammingMethodDecryption(text, strKey, alphabet);    
                    }
                case "Метод Виженера":
                    Console.WriteLine("\nДанный метод распознает только русский алфавит, цифры и часть символов.");
                    if (action == 1)
                    {
                        return vigenerMethodEncryption(text, strKey, alphabet);
                    }
                    else 
                    {
                        return vigenerMethodDecryption(text, strKey, alphabet);
                    }
                case "Шифр Полибия":
                    Console.WriteLine("\nДанный метод распознает только строчные русские буквы и часть символов.");
                    if (action == 1)
                    {
                        return polybiusCipherEncryption(text);
                    }
                    else 
                    {
                        return polybiusCipherDecryption(text);
                    }
                case "Метод Playfair":
                    Console.WriteLine("\nДанный метод и его ключ распознают только английские буквы.");
                    if (action == 1)
                    {
                        return PlayfairMethodEncryption(text, strKey);
                    }
                    else 
                    { 
                        return PlayfairMethodDecryption(text, strKey);
                    }
                case "Метод аффинного шифра":
                    Console.WriteLine("\nДанный метод распознает только русский алфавит, цифры и часть символов.");
                    if (action == 1)
                    {
                        return affineCipherEncryption(text, key, alphabet);
                    }
                    else 
                    {
                        return affineCipherDecryption(text, key, alphabet);
                    }
                default:
                    return "Метод не обнаружен.";
            }
        }

        public static string charPermutationEncryption(string text, int key)
        {
            int countNewSymbol = 0;
            if (text.Length % key != 0) countNewSymbol = key - text.Length % key;

            int count = 0;
            while (count < countNewSymbol) // дозаполнение сообщения символами
            {
                text = text + ".";
                count++;
            }

            string encryptedText = "";
            int countBlocks = text.Length / key; 
            char[] charText = text.ToCharArray();

            //Array.Reverse(charText); // повышение уровня шифрования
            
            char[,] charBlocks = new char[countBlocks, key];
            for (int i = 0; i < countBlocks; i++)
            {
                for (int j = 0; j < key; j++)
                {
                    charBlocks[i, j] = charText[j + i * key];
                }
            }

            for (int j = 0; j < key; j++)
            {
                for (int i = 0; i < countBlocks; i++)
                {
                    encryptedText += charBlocks[i, j];
                }
            }

            return encryptedText;

        }

        public static string charPermutationDecryption(string text, int key)
        {
            string decryptedText = "";
            int countBlocks = text.Length / key;
            char[] charText = text.ToCharArray();

            Console.WriteLine(text.Length + "/" + key + "=" + countBlocks);

            char[,] charBlocks = new char[key, countBlocks];
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < countBlocks; j++)
                {
                    charBlocks[i, j] = charText[j + i * countBlocks];
                }
            }

            for (int i = 0; i < countBlocks; i++)
            {
                for (int j = 0; j < key; j++)
                {
                    decryptedText += charBlocks[j, i];
                }
            }

            return decryptedText;
        }

        public static string gammingMethodEncryption(string text, string key, string alphabet)
        {
            string encryptedText = "";

            while (key.Length < text.Length)
            {
                key += key;
            }
            key = key.Substring(0, text.Length);

            for (int i = 0; i < text.Length; i++)
            {
                int charText = -1;
                int charKey = -1;

                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (text[i] == alphabet[j]) 
                    { 
                        charText = j; 
                    }
                    if (key[i] == alphabet[j])
                    {
                        charKey = j;
                    }
                }

                if (charText == -1 || charKey == -1) encryptedText += "?";
                else
                {
                    encryptedText += alphabet[(charText + charKey) % alphabet.Length];
                }

            }

            return encryptedText;
        }

        public static string gammingMethodDecryption(string text, string key, string alphabet)
        {
            string decryptedText = "";

            while (key.Length < text.Length)
            {
                key += key;
            }
            key = key.Substring(0, text.Length);

            for (int i = 0; i < text.Length; i++)
            {
                int charText = -1;
                int charKey = -1;

                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (text[i] == alphabet[j])
                    {
                        charText = j;
                    }
                    if (key[i] == alphabet[j])
                    {
                        charKey = j;
                    }
                }

                if (charText == -1 || charKey == -1) decryptedText += "?";
                else
                {
                    decryptedText += alphabet[(charText - charKey + alphabet.Length) % alphabet.Length];
                }
            }

            return decryptedText;
        }

        public static string vigenerMethodEncryption(string text, string key, string alphabet)
        {
            string encryptedText = "";

            while (key.Length < text.Length)
            {
                key += key;
            }
            key = key.Substring(0, text.Length);

            for (int i = 0; i < text.Length; i++)
            {
                int charTextIndex = alphabet.IndexOf(text[i]);
                int charKey = alphabet.IndexOf(key[i]);
                int indexOfFinalChar = (alphabet.Length + charTextIndex + charKey) % alphabet.Length;
                encryptedText += alphabet[indexOfFinalChar];
            }

            return encryptedText;
        }

        public static string vigenerMethodDecryption(string text, string key, string alphabet)
        {
            string decryptedText = "";

            while (key.Length < text.Length)
            {
                key += key;
            }
            key = key.Substring(0, text.Length);

            for (int i = 0; i < text.Length; i++)
            {
                int charTextIndex = alphabet.IndexOf(text[i]);
                int charKey = alphabet.IndexOf(key[i]);
                int indexOfFinalChar = (alphabet.Length + charTextIndex - charKey) % alphabet.Length;
                decryptedText += alphabet[indexOfFinalChar];
            }

            return decryptedText;
        }

        public static string polybiusCipherEncryption(string text)
        {
            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя,. ";
            string encryptedText = "";

            int columnRows = Convert.ToInt32(Math.Sqrt(alphabet.Length));
            char[,] alphabetArr = new char[columnRows, columnRows];

            for (int i = 0; i < columnRows; i++)
            {
                for (int j = 0; j < columnRows; j++)
                {

                    alphabetArr[i, j] = alphabet[j];
                }
                alphabet = alphabet.Remove(0, columnRows);
            }

            for (int k = 0; k < text.Length; k++)
            {
                char encryptedChar = '?';
                for (int i = 0; i < columnRows; i++)
                {
                    for (int j = 0; j < columnRows; j++)
                    {
                        if (text[k] == alphabetArr[i, j])
                        {
                            if (i-1 >= 0)
                            {
                                encryptedChar = alphabetArr[i-1, j];
                            }
                            else
                            {
                                encryptedChar = alphabetArr[columnRows-1, j];
                            }
                        }
                    }
                }
                encryptedText += encryptedChar;
            }

            return encryptedText;
        }

        public static string polybiusCipherDecryption(string text)
        {
            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя,. ";
            string decryptedText = "";

            int columnRows = Convert.ToInt32(Math.Sqrt(alphabet.Length));
            char[,] alphabetArr = new char[columnRows, columnRows];

            for (int i = 0; i < columnRows; i++)
            {
                for (int j = 0; j < columnRows; j++)
                {

                    alphabetArr[i, j] = alphabet[j];
                }
                alphabet = alphabet.Remove(0, columnRows);
            }

            
            for (int k = 0; k < text.Length; k++)
            {
                char decryptedChar = '?';
                for (int i = 0; i < columnRows; i++)
                {
                    for (int j = 0; j < columnRows; j++)
                    {
                        if (text[k] == alphabetArr[i, j])
                        {
                            if (i+1 < columnRows)
                            {
                                decryptedChar = alphabetArr[i+1, j];
                            }
                            else
                            {
                                decryptedChar = alphabetArr[0, j];
                            }
                        }
                    }
                }
                decryptedText += decryptedChar;
            }

            return decryptedText;
        }

        public static string PlayfairMethodEncryption(string text, string key)
        {
            key = key.ToUpper().Replace("J", "I");
            HashSet<char> seen = new HashSet<char>();
            string preparedKey = "";

            foreach (char c in key)
            {
                if (char.IsLetter(c) && !seen.Contains(c))
                {
                    seen.Add(c);
                    preparedKey += c;
                }
            }

            // Создание таблицы
            char[,] table = new char[5, 5];
            int index = 0;

            foreach (char c in preparedKey)
            {
                table[index / 5, index % 5] = c;
                index++;
            }

            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (c == 'J') continue; // Пропуск 'J'
                if (!seen.Contains(c))
                {
                    seen.Add(c);
                    table[index / 5, index % 5] = c;
                    index++;
                }
            }

            // Подготовка текста
            text = text.ToUpper().Replace(" ", "").Replace("J", "I");
            string preparedText = "";

            for (int i = 0; i < text.Length; i++)
            {
                preparedText += text[i];
                if (i + 1 < text.Length && text[i] == text[i + 1])
                {
                    preparedText += 'X'; // 'X' между одинаковыми буквами
                }
            }

            if (preparedText.Length % 2 != 0)
            {
                preparedText += 'X'; // 'X' в конце, если длина нечетная
            }

            // Шифрование
            string encryptedText = "";
            for (int i = 0; i < preparedText.Length; i += 2)
            {
                char a = preparedText[i];
                char b = preparedText[i + 1];

                int[] posA = FindPosition(a, table);
                int[] posB = FindPosition(b, table);

                if (posA[0] == posB[0]) // В одной строке
                {
                    encryptedText += table[posA[0], (posA[1] + 1) % 5];
                    encryptedText += table[posB[0], (posB[1] + 1) % 5];
                }
                else if (posA[1] == posB[1]) // В одном столбце
                {
                    encryptedText += table[(posA[0] + 1) % 5, posA[1]];
                    encryptedText += table[(posB[0] + 1) % 5, posB[1]];
                }
                else // В разных строках и столбцах
                {
                    encryptedText += table[posA[0], posB[1]];
                    encryptedText += table[posB[0], posA[1]];
                }
            }
            return encryptedText;
        }

        static int[] FindPosition(char c, char[,] table)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (table[i, j] == c)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[] { 0, 0 };
        }

        public static string PlayfairMethodDecryption(string text, string key)
        {
            key = key.ToUpper().Replace("J", "I");
            HashSet<char> seen = new HashSet<char>();
            string preparedKey = "";

            foreach (char c in key)
            {
                if (char.IsLetter(c) && !seen.Contains(c))
                {
                    seen.Add(c);
                    preparedKey += c;
                }
            }

            // Создание таблицы
            char[,] table = new char[5, 5];
            int index = 0;

            foreach (char c in preparedKey)
            {
                table[index / 5, index % 5] = c;
                index++;
            }

            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (c == 'J') continue; // Пропуск 'J'
                if (!seen.Contains(c))
                {
                    seen.Add(c);
                    table[index / 5, index % 5] = c;
                    index++;
                }
            }

            // Подготовка текста
            text = text.ToUpper().Replace(" ", "").Replace("J", "I");
            string preparedText = text;

            // Дешифрование
            string result = "";
            for (int i = 0; i < preparedText.Length; i += 2)
            {
                char a = preparedText[i];
                char b = preparedText[i + 1];

                int[] posA = FindPosition(a, table);
                int[] posB = FindPosition(b, table);

                if (posA[0] == posB[0]) // В одной строке
                {
                    result += table[posA[0], (posA[1] + 4) % 5]; // Сдвиг влево
                    result += table[posB[0], (posB[1] + 4) % 5]; // Сдвиг влево
                }
                else if (posA[1] == posB[1]) // В одном столбце
                {
                    result += table[(posA[0] + 4) % 5, posA[1]]; // Сдвиг вверх
                    result += table[(posB[0] + 4) % 5, posB[1]]; // Сдвиг вверх
                }
                else // В разных строках и столбцах
                {
                    result += table[posA[0], posB[1]];
                    result += table[posB[0], posA[1]];
                }
            }
            return result;
        }

        public static string affineCipherEncryption(string text, int key, string alphabet)
        {
            Console.WriteLine("\nВведите второй ключ.");
            int secondKey = getKeyForIntMetods(getKey());

            string encryptedText = "";
            int indexOfFinalChar = 0;

            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (text[i] == alphabet[j])
                    {
                        indexOfFinalChar = (key * j + secondKey) % alphabet.Length;
                        encryptedText += alphabet[indexOfFinalChar];
                        break;
                    }                    
                }
            }
            return encryptedText;
        }

        public static string affineCipherDecryption(string text, int key, string alphabet)
        {
            Console.WriteLine("\nВведите второй ключ.");
            int secondKey = getKeyForIntMetods(getKey());

            int multiplicative = 0;
            for (int x = 1; x < alphabet.Length; x++)
            {
                if ((key % alphabet.Length * x) % alphabet.Length == 1)
                {
                    multiplicative = x;
                }
            }

            string decryptedText = "";
            int indexOfFinalChar = 0;

            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (text[i] == alphabet[j])
                    {
                        indexOfFinalChar = multiplicative * (j - secondKey + alphabet.Length) % alphabet.Length;
                        decryptedText += alphabet[indexOfFinalChar];
                        break;
                    }
                }
            }
            return decryptedText;
        }

    }
}