using System;
using System.Collections.Generic;
using System.Linq;

namespace BrickworksAssignment
{
    class Program
    {
        static bool inputValidator(int[,] array)
        {
            List<int> seenNumbers = new List<int>();
            bool[,] checkedSquares = new bool[array.GetLength(0), array.GetLength(1)];
            for(int i=0; i< array.GetLength(0); i++)
            {
                for(int j=0; j<array.GetLength(1); j++)
                {
                    if (!checkedSquares[i, j]) //If the square isn't checked
                    {
                        bool checkd = false;
                        if (seenNumbers.Contains(array[i, j])) throw new Exception("Number encountered more than 2 times");
                        checkedSquares[i, j] = true;
                        //check right
                        if(j + 1 < array.GetLength(1))
                            if (array[i, j] == array[i, j + 1])
                            {
                                checkedSquares[i, j + 1] = true;
                                seenNumbers.Add(array[i,j]);
                                checkd = true;
                            }
                        //check down
                        if(i + 1 < array.GetLength(0) && !checkd)
                            if (array[i, j] == array[i + 1, j]) //runs this check only if the first has failed
                            {
                                checkedSquares[i + 1, j] = true;
                                seenNumbers.Add(array[i, j]);
                            }
                        else throw new Exception("something ain't right"); //Both checks have failed
                    }
                }
            }
            return true;
        }
        static void printArray(int[,] array)
        {
            for(int i=0; i<array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                    Console.Write(j == array.GetLength(1) - 1 ? $"{array[i, j]}\n" : $"{array[i, j]} ");
            }
        }
        static int[,] createSecondLayer(int[,] firstLayer)
        {
            return firstLayer;
        }
        static void Main(string[] args)
        {
            int[] dimension = Console.ReadLine().Trim().Split().Select(int.Parse).ToArray();
            int m = dimension[0];
            int n = dimension[1];
            if (m < 1 || m > 100 || n < 1 || m > 100) throw new IndexOutOfRangeException();
            int[,] bricks = new int[m,n];
            for (int i = 0; i < m; i++)
            {
                int[] line = Console.ReadLine().Trim().Split().Select(int.Parse).ToArray();
                for (int j = 0; j < n; j++) bricks[i, j] = line[j];
            }
            printArray(bricks);
            inputValidator(bricks);
        }
    }
}
