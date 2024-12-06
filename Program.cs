using System;
using System.Collections.Generic;

namespace PuzzleGame
{
    class Program
    {
        static void Main(string[] args)
        {
           int[] initialPuzzleState =
           {
                5, 8, 3,
                4, 0, 2,
                7, 6, 1,
            };

            Node root = new Node(initialPuzzleState);

            string algorithmKey = ChooseAlgorithm();
            string modeKey = ChooseMode();

            bool isStepByStepMode = modeKey.Equals("y");
            UniformedSearch uniformedSearch = new UniformedSearch(isStepByStepMode);

            List<Node> solution = new List<Node>();
            int stepCount = 0;
            int memoryComplexity = 0;

            if (algorithmKey == "1")
            {
                solution = uniformedSearch.BFS(root, out stepCount, out memoryComplexity);
            }
            else if (algorithmKey == "2")
            {
                solution = uniformedSearch.DFS(root, out stepCount, out memoryComplexity);
            }

            if (solution.Count > 0)
            {
                Console.WriteLine($"Стоимость пути: {solution.Count - 1}");
                Console.WriteLine($"Количество шагов (временная сложность): {stepCount}");
                Console.WriteLine($"Количество единиц памяти (ёмкостная сложность): {memoryComplexity}");
            }
            else
            {
                Console.WriteLine("Решение не найдено.");
            }

            Console.ReadLine();
        }

        private static string ChooseAlgorithm()
        {
            while (true)
            {
                Console.Write("Выберите алгоритм 1 - BFS, 2 - DFS?: ");
                string key = Console.ReadLine();

                if (key.ToLower() == "1"
                    || key.ToLower() == "2")
                {
                    return key.ToLower();
                }

                Console.WriteLine("Неверный ввод. Попробуйте снова.");
            }
        }

        private static string ChooseMode()
        {
            while (true)
            {
                Console.Write("Включить пошаговый режим (y/n)?: ");
                string key = Console.ReadLine();

                if (key.ToLower() == "y"
                    || key.ToLower() == "n")
                {
                    return key.ToLower();
                }

                Console.WriteLine("Неверный ввод. Попробуйте снова.");
            }
        }
    }
}
