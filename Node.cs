using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleGame
{
    class Node
    {
        public List<Node> children = new List<Node>();
        public Node parent = default;
        public int[] state = new int[9];
        public int emptyTilePosition = 0;  
        public int stateSize = 3;

        public Node(int[] state)
        {
            this.state = state;
        }

        public bool GoalTest()
        {
            int[] goal = { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
            return this.state.SequenceEqual(goal);
        }

        public void Expand()
        {
            for (int i = 0; i < this.state.Length; i++)
            {
                if (this.state[i] == 0)
                {
                    this.emptyTilePosition = i;
                    break;
                }
            }

            int[] directions = { 1, this.stateSize, -1, -this.stateSize };

            foreach (int direction in directions)
            {
                this.TryMove(this.state, this.emptyTilePosition, direction);
            }
        }

        public void PrintConsole()
        {
            Console.WriteLine();
            for (int i = 0; i < this.stateSize; i++)
            {
                for (int j = 0; j < this.stateSize; j++)
                {
                    Console.Write(this.state[i * this.stateSize + j] + " ");
                }
                Console.WriteLine();
            }
        }

        private void TryMove(int[] currentState, int tilePosition, int offset)
        {
            int newTilePosition = tilePosition + offset;

            if (this.IsValidMove(tilePosition, newTilePosition, offset))
            {
                int[] newState = new int[currentState.Length];
                Array.Copy(currentState, newState, currentState.Length);

                this.Swap(newState, tilePosition, newTilePosition);

                Node child = new Node(newState) { parent = this };
                this.children.Add(child);
            }
        }

        private bool IsValidMove(int tilePosition, int newTilePosition, int offset)
        {
            if (offset == 1 && (tilePosition % this.stateSize == this.stateSize - 1) ||
            (offset == -1 && tilePosition % this.stateSize == 0) ||
            (newTilePosition < 0 || newTilePosition >= this.stateSize * this.stateSize))
            { 
                return false;
            }
            return true;
        }

        private void Swap(int[] state, int tilePosition, int newTilePosition)
        {
            int temp = state[tilePosition];
            state[tilePosition] = state[newTilePosition];
            state[newTilePosition] = temp;
        }
    }
}
