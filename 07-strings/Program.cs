using System;
using System.Text;

namespace Strings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ===== Задание 1: конкатенация строк =====
            Console.WriteLine("Task 1:");
            Console.WriteLine(ConcatenateStrings("Hello, ", "world!"));
            Console.WriteLine();

            // ===== Задание 2: приветствие пользователя =====
            Console.WriteLine("Task 2:");
            Console.WriteLine(GreetUser("Danil", 33));
            Console.WriteLine();

            // ===== Задание 3: информация о строке =====
            Console.WriteLine("Task 3:");
            Console.WriteLine(GetStringInfo("Hello World"));
            Console.WriteLine();

            // ===== Задание 4: первые 5 символов =====
            Console.WriteLine("Task 4:");
            Console.WriteLine(GetFirstFiveChars("Programming"));
            Console.WriteLine(GetFirstFiveChars("abc"));   
            Console.WriteLine();

            // ===== Задание 5: StringBuilder из массива строк =====
            Console.WriteLine("Task 5:");
            string[] words = { "This", "is", "a", "sentence" };
            StringBuilder sentence = BuildSentence(words);
            Console.WriteLine(sentence.ToString());
            Console.WriteLine();

            // ===== Задание 6: замена слов в строке =====
            Console.WriteLine("Task 6:");
            string result = ReplaceWords("Hello world", "world", "universe");
            Console.WriteLine(result);
        }

        // Задание 1: объединяет (конкатенирует) две строки
        static string ConcatenateStrings(string first, string second)
        {
            return first + second;
        }

        // Задание 2: формирует приветственное сообщение с именем и возрастом
        static string GreetUser(string name, int age)
        {
            return $"Hello, {name}!\nYou are {age} years old.";
        }

        // Задание 3: возвращает информацию о строке - длину, верхний и нижний регистр
        static string GetStringInfo(string input)
        {
            int length = input.Length;
            string upper = input.ToUpper();
            string lower = input.ToLower();

            return $"Length: {length}\nUpper case: {upper}\nLower case: {lower}";
        }

        // Задание 4: возвращает первые 5 символов строки
        static string GetFirstFiveChars(string input)
        {
          
            int length = Math.Min(5, input.Length);
            return input.Substring(0, length);
        }

        // Задание 5: объединяет массив строк в одно предложение через пробел, используя StringBuilder
        static StringBuilder BuildSentence(string[] inputWords)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < inputWords.Length; i++)
            {
                builder.Append(inputWords[i]);

                if (i < inputWords.Length - 1)
                {
                    builder.Append(" ");
                }
            }

            return builder;
        }

        // Задание 6: заменяет все вхождения одного слова на другое в строке
        static string ReplaceWords(string inputString, string wordToReplace, string replacementWord)
        {
            return inputString.Replace(wordToReplace, replacementWord);
        }
    }
}
