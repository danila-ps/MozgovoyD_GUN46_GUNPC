using System;
using System.Collections.Generic;

namespace HomeWork
{
    internal class Program
    {
        // ===== Задание 1: список (List) =====
        private class ListTask
        {
            private readonly List<string> _names = new List<string>() { "Alex", "Maria", "Ivan" };

            public void TaskLoop()
            {
                Console.WriteLine("List task started. Type -exit to stop.");

                Console.WriteLine("Current list:");
                PrintList();

                Console.WriteLine("Enter a new string to add (or -exit to stop):");
                string newItem = Console.ReadLine();

                if (newItem == "-exit")
                {
                    return;
                }

                _names.Add(newItem);
                Console.WriteLine("List after adding your string:");
                PrintList();

                Console.WriteLine("Enter another string to insert in the middle (or -exit to stop):");
                string middleItem = Console.ReadLine();

                if (middleItem == "-exit")
                {
                    return;
                }

                int middleIndex = _names.Count / 2;
                _names.Insert(middleIndex, middleItem);

                Console.WriteLine("Final list:");
                PrintList();
            }

            private void PrintList()
            {
                foreach (string name in _names)
                {
                    Console.WriteLine(name);
                }
            }
        }

        // ===== Задание 2: словарь (Dictionary) =====
        private class DictionaryTask
        {
            private readonly Dictionary<string, int> _students = new Dictionary<string, int>();

            public void TaskLoop()
            {
                Console.WriteLine("Dictionary task started. Type -exit to stop.");

                string input;
                do
                {
                    Console.WriteLine("Enter student name to ADD, or a name to LOOK UP, or -exit to stop:");
                    input = Console.ReadLine();

                    if (input == "-exit")
                    {
                        break;
                    }

                    if (_students.ContainsKey(input))
                    {
                        Console.WriteLine($"{input}'s grade is: {_students[input]}");
                    }
                    else
                    {
                        Console.WriteLine("Student not found. Let's add them.");
                        Console.WriteLine("Enter grade (2-5):");

                        bool isValid = int.TryParse(Console.ReadLine(), out int grade);

                        if (!isValid || grade < 2 || grade > 5)
                        {
                            Console.WriteLine("Error! Grade must be a number between 2 and 5.");
                        }
                        else
                        {
                            _students[input] = grade;
                            Console.WriteLine("Added successfully.");
                        }
                    }
                }
                while (input != "-exit");
            }
        }

        // ===== Задание 3: двусвязный список =====
        private class LinkedListTask
        {
            // Узел списка: хранит значение, ссылку на "соседа спереди" и "соседа сзади"
            private class Node
            {
                public int Value;
                public Node Next;     // следующий узел
                public Node Previous; // предыдущий узел

                public Node(int value)
                {
                    Value = value;
                }
            }

            private Node _head; // самый первый узел списка
            private Node _tail; // самый последний узел списка

            // Добавляет новый узел в конец списка
            private void AddLast(int value)
            {
                var newNode = new Node(value);

                if (_head == null)
                {
                    // список пока пустой - новый узел становится и головой, и хвостом
                    _head = newNode;
                    _tail = newNode;
                }
                else
                {
                    // цепляем новый узел после текущего хвоста
                    newNode.Previous = _tail;
                    _tail.Next = newNode;
                    _tail = newNode;
                }
            }

            public void TaskLoop()
            {
                Console.WriteLine("Doubly linked list task started. Type -exit to stop early.");
                Console.WriteLine("Enter from 3 to 6 numbers, one by one:");

                int count = 0;
                while (count < 6)
                {
                    Console.WriteLine($"Enter number {count + 1} (or -exit to stop):");
                    string input = Console.ReadLine();

                    if (input == "-exit")
                    {
                        break;
                    }

                    bool isValid = int.TryParse(input, out int number);

                    if (!isValid)
                    {
                        Console.WriteLine("Error! Please enter a valid number.");
                        continue;
                    }

                    AddLast(number);
                    count++;

                    if (count >= 3)
                    {
                        Console.WriteLine("Type -exit to stop, or keep entering numbers (up to 6 total).");
                    }
                }

                Console.WriteLine("Forward order:");
                PrintForward();

                Console.WriteLine("Backward order:");
                PrintBackward();
            }

            private void PrintForward()
            {
                Node current = _head;
                while (current != null)
                {
                    Console.Write(current.Value + " ");
                    current = current.Next;
                }
                Console.WriteLine();
            }

            private void PrintBackward()
            {
                Node current = _tail;
                while (current != null)
                {
                    Console.Write(current.Value + " ");
                    current = current.Previous;
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter 1, 2 or 3 to check task 1, 2 or 3");
            bool isValid = int.TryParse(Console.ReadLine(), out int task);

            if (!isValid)
            {
                Console.WriteLine("Error! Please enter a number.");
                return;
            }

            switch (task)
            {
                case 1:
                    CheckTaskFirst();
                    break;
                case 2:
                    CheckTaskSecond();
                    break;
                case 3:
                    CheckTaskThird();
                    break;
                default:
                    Console.WriteLine("Error! Invalid task number.");
                    break;
            }
        }

        private static void CheckTaskFirst()
        {
            var listTask = new ListTask();
            listTask.TaskLoop();
        }

        private static void CheckTaskSecond()
        {
            var dictionaryTask = new DictionaryTask();
            dictionaryTask.TaskLoop();
        }

        private static void CheckTaskThird()
        {
            var linkedListTask = new LinkedListTask();
            linkedListTask.TaskLoop();
        }
    }
}
