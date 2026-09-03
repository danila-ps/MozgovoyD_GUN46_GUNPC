using System;

namespace cycles

{
    internal class Program
    
    {
        static void Main(string[] args)

        {

            // ==== Задание 1: первые 10 числе Фибоначии ====

            Console.WriteLine("Fibonacci numbers:");

            int first = 0;
            int second = 1;

            Console.Write(first + " " + second + " "); 

            for (int i = 2; i < 10; i++)
            {
                int next = first + second;
                Console.Write(next + " ");
                first = second;
                second = next; 


            }

            Console.WriteLine();
            Console.WriteLine();


            // ==== Задание 2: четные числа от 2 до 20 ====

            Console.WriteLine("Numbers from 2 to 20"); 
            for (int i = 2; i < 20; i +=2)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
            Console.WriteLine();

            // ==== Задание 3: таблица умножения 1-5 ====

            Console.WriteLine("Multiplication table:"); 

            for (int c = 1; c <= 5; c++)

            {
                for (int d = 1; d<=5; d++)

                {
                    Console.Write(c * d + "\t");


                }

                Console.WriteLine();

            }

            Console.WriteLine();

            // ==== Задание 4: проверка пароля через ду-вайл ====


            string password = "qwerty";
            string input;

            do

            {
                Console.WriteLine("Enter password:");
                input = Console.ReadLine();

                if (input != password)

                {

                    Console.WriteLine("Wrong password, try again");
                }

           

            }

            while (input != password);

            Console.WriteLine("Access granted!");


        }

    }


}