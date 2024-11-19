using System;
using System.Collections.Generic;

namespace PuzzleGame
{
    class UniformedSearch
    {
        bool isStepByStepMode = default;

        public UniformedSearch(bool stepByStepMode)
        {
            this.isStepByStepMode = stepByStepMode;
        }

        public List<Node> BFS(Node root, out int stepCount, out int memoryComplexity)
        {
            stepCount = 0;
            memoryComplexity = 0;
            List<Node> pathToSolution = new List<Node>();
            Queue<Node> queue = new Queue<Node>();
            HashSet<string> visitedStates = new HashSet<string>();
            queue.Enqueue(root);
            bool isGoalFound = false;

            while (queue.Count > 0 && !isGoalFound)
            {
                List<Node> repeatNodes = new List<Node>();
                List<Node> newNodes = new List<Node>();

                Node currentNode = queue.Dequeue();

                visitedStates.Add(string.Join(",", currentNode.state));
                stepCount++;

                if (currentNode.GoalTest())
                {
                    Console.WriteLine("Целевое состояние достигнуто");
                    isGoalFound = true;
                    this.PathTracer(pathToSolution, currentNode);
                    break;
                }

                currentNode.Expand();

                foreach (Node child in currentNode.children)
                {
                    string childState = string.Join(",", child.state);

                    if (!visitedStates.Contains(childState))
                    {
                        queue.Enqueue(child);
                        newNodes.Add(child);

                    }
                    else
                    {
                        repeatNodes.Add(child);
                    }

                }
                memoryComplexity = Math.Max(memoryComplexity, queue.Count);
                this.LogInfo(newNodes, repeatNodes, queue, stepCount, currentNode);

            }

            return pathToSolution;
        }


        public List<Node> DFS(Node root, out int stepCount, out int memoryComplexity)
        {
            stepCount = 0;
            memoryComplexity = 0;
            Stack<Node> stack = new Stack<Node>();
            List<Node> repeatNodes = new List<Node>();
            List<Node> newNodes = new List<Node>();
            HashSet<string> visited = new HashSet<string>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                Node currentNode = stack.Pop();
                stepCount++;

                if (currentNode.GoalTest())
                {
                    List<Node> path = new List<Node>();
                    PathTracer(path, currentNode);
                    return path;
                }

                visited.Add(string.Join(",", currentNode.state));
                currentNode.Expand();

                foreach (var child in currentNode.children)
                {
                    string childState = string.Join(",", child.state);
                    if (!visited.Contains(childState))
                    {
                        newNodes.Add(child);
                        stack.Push(child);
                    }
                    else
                    {
                        repeatNodes.Add(child);
                    }
                }

                memoryComplexity = Math.Max(memoryComplexity, stack.Count);
                this.LogInfo(newNodes, repeatNodes, stack, stepCount, currentNode);
            }

            return new List<Node>();
        }

        
        private void LogInfo(List<Node> newNodes, List<Node> repeatNodes, IEnumerable<Node> border, int stepCount, Node currentNode)
        {
            if (!this.isStepByStepMode) { return; }

            Console.WriteLine("\nШаг " + stepCount.ToString());
            Console.WriteLine("\nТекущая вершина:");
            currentNode.PrintConsole();
            Console.WriteLine("");

            this.LogNodesInfo("Вновь найденные вершины: ", newNodes);
            this.LogNodesInfo("Выявленные повторные вершины: ", repeatNodes);

            Console.WriteLine("\nТекущее состояние каймы:");

            foreach (var node in border)
            {
                Console.WriteLine(string.Join(",", node.state));
            }

            Console.WriteLine("\nЦелевое состояние не достигнуто");
            Console.WriteLine("\nНажмите любую клавишу для следующего шага...\n");

            Console.ReadKey();

        }

        private void LogNodesInfo(string message, List<Node> nodes)
        {
            if (nodes.Count == 0)
            {
                Console.WriteLine(message + "не найдено");
                return;
            }

            Console.WriteLine(message);

            foreach (var node in nodes)
            {
                node.PrintConsole();
            }

            Console.WriteLine();
        }

        private void PathTracer(List<Node> path, Node endNode)
        {
            Node node = endNode;
            path.Add(node);

            while (node.parent != null)
            {
                node = node.parent;
                path.Add(node);
            }

            path.Reverse();
        }
    }
}

