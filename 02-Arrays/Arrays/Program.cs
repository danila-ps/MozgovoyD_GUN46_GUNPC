using System;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;

namespace Arrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //===== Задание 1: Числа Фибоначии (первые 8) =====
            int[] fibonacci = { 0, 1, 1, 2, 3, 5, 8, 13 };
            Console.WriteLine("Fibonacci numbers:");
            foreach (int num in fibonacci)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine("");
            Console.WriteLine("");

            //===== Задание 2: 12 месяцев =====

            string[] months =
            {
                "January", "Febuary", "March", "April",
                "May", "June", "July", "August",
                "September", "October", "November", "December"
            };
            Console.WriteLine("Months:");
            foreach (string month in months)
            {
                Console.Write(month + " ");
            }
            Console.WriteLine("");
            Console.WriteLine("");

            //===== Задание 3: Создайте двумерный массив (матрицу) 3x3 ====

            int[,] matrix =
             {

               { 2, 3, 4
                },
               { 4, 9, 16
                },
               { 8, 27, 64
                }
            };

            Console.WriteLine("Matrix:");
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Console.Write(matrix[row, col] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            //===== Задание 4: ломанный массив ====

            double[][] jaggedArray = new double[3][];
            jaggedArray[0] = new double[] { 1, 2, 3, 4, 5 };
            jaggedArray[1] = new double[] { Math.E, Math.PI };
            jaggedArray[2] = new double[] { Math.Log10(1), Math.Log10(10), Math.Log10(100), Math.Log10(1000) };

            Console.WriteLine("Jagged array:");
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                Console.Write($"Array {i}: ");
                foreach (double value in jaggedArray[i])
                {
                    Console.Write(value + "");

                }
                Console.WriteLine();
            }
            Console.WriteLine();

            // Задание 5 и 6 
            //Скопируйте первые 3 элемента первого массива во второй.Воспользуйтесь классом Array.
            //Измените размер первого массива так, чтобы в нём стало в два раза больше элементов Воспользуйтесь классом Array, метод Resize.
            //ВАЖНО! Массив передаётся через ref. Это же ключевое слово вы будете использовать при вызове метода Resize, то есть: Array.Resize(ref array, newSize);

            int[] array = { 1, 2, 3, 4, 5 };
            int[] array2 = { 7, 8, 9, 10, 11, 12, 13 };

            int[] result = CopyArrays(array, array2, 3);
            Console.WriteLine("TASK 5 - array2 after copy:" + string.Join(" ", result));

            ResizeArray(ref array, array.Length * 2);
            Console.WriteLine("TASK 6 - array after resize:" + string.Join(" ", array));

        }

        static int[] CopyArrays(int[] source, int[] destination, int count)
        {
            Array.Copy(source, destination, count);
            return destination;
        }
        
        static void ResizeArray(ref int[] array, int newSize)
           {
            Array.Resize(ref array, newSize);

        }


    }

}