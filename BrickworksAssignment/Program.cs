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
                        //check horizontal
                        if(j + 1 < array.GetLength(1))
                            if (array[i, j] == array[i, j + 1])
                            {
                                checkedSquares[i, j + 1] = true;
                                seenNumbers.Add(array[i,j]);
                                checkd = true;
                            }
                        //check vertical
                        if(i + 1 < array.GetLength(0) && !checkd)
                            if (array[i, j] == array[i + 1, j]) //runs this check only if the first has failed
                            {
                                checkedSquares[i + 1, j] = true;
                                seenNumbers.Add(array[i, j]);
                            }
                        else throw new Exception("Something ain't right"); //Both checks have failed
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
            int[,] secondlayer = new int[firstLayer.GetLength(0), firstLayer.GetLength(1)];
            int currentnumber = 1;
            for (int i = 0; i < firstLayer.GetLength(0); i++)
            {
                for (int j = 0; j < firstLayer.GetLength(1); j++)
                {
                    if (secondlayer[i, j] == 0)
                    {
                        bool brickplaced = false;
                        if (j + 1 < firstLayer.GetLength(1))
                        {
                            if (firstLayer[i, j] != firstLayer[i, j + 1]) // if there is horizontal division place a brick
                            {
                                secondlayer[i, j] = currentnumber;
                                secondlayer[i, j + 1] = currentnumber;
                                currentnumber++;
                                brickplaced = true;
                            }
                        }
                        if (i + 1 < firstLayer.GetLength(0) && !brickplaced)
                        {
                            if (firstLayer[i, j] != firstLayer[i + 1, j]) //if there is vertical division place a brick
                            {
                                secondlayer[i, j] = currentnumber;
                                secondlayer[i + 1, j] = currentnumber;
                                currentnumber++;
                            }
                        }
                    }
                }
            }
            return secondlayer; //consider recursion
        }

        static int[,] recursiveMethod(int[,] firstLayer, int[,] secondlayer, int i, int j, int currentnumber)
        {
            if (j == firstLayer.GetLength(1)) return recursiveMethod(firstLayer, secondlayer, i + 1, 0, currentnumber);
            if (j + 1 == firstLayer.GetLength(1) && i + 1 == firstLayer.GetLength(0)) return secondlayer;
            if (secondlayer[i, j] == 0)
            {
                bool brickplaced = false;
                if (j + 1 < firstLayer.GetLength(1)) //am I not at the end
                {
                    if (firstLayer[i, j] != firstLayer[i, j + 1]) // can I place a horizontal brick
                    {
                        secondlayer[i, j] = currentnumber;
                        secondlayer[i, j + 1] = currentnumber;
                        currentnumber++;
                        brickplaced = true;
                        int[,] result = recursiveMethod(firstLayer, secondlayer, i, j + 1, currentnumber);
                        if (result.Cast<int>().Contains(0))
                        {
                            secondlayer[i, j] = 0;
                            secondlayer[i, j + 1] = 0;
                            currentnumber--;
                            brickplaced = false;
                        }
                    }
                    else return recursiveMethod(firstLayer, secondlayer, i, j + 1, currentnumber);

                }
                if (i + 1 < firstLayer.GetLength(0) && !brickplaced) // am I at the bottom
                {
                    if (firstLayer[i, j] != firstLayer[i + 1, j]) //can I place a vertical brick
                    {
                        secondlayer[i, j] = currentnumber;
                        secondlayer[i + 1, j] = currentnumber;
                        currentnumber++;
                        int[,] result = recursiveMethod(firstLayer, secondlayer, i, j + 1, currentnumber);
                        if (result.Cast<int>().Contains(0))
                        {
                            return new int[1, 1] { { -1 } };
                        }

                    }
                    else return recursiveMethod(firstLayer, secondlayer, i, j + 1, currentnumber);
                }
                return secondlayer;
            }
            else return recursiveMethod(firstLayer, secondlayer, i, j + 1, currentnumber);

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
            //printArray(bricks);
            inputValidator(bricks);
            Console.WriteLine();
            int[,] secondLayer1 = createSecondLayer(bricks);
            int[,] secondLayer2 = recursiveMethod(bricks, new int[m, n], 0, 0, 1);
            if (secondLayer1.Cast<int>().Contains(0)) //if the second layer contains a 0 that means a brick could not be placed without overlapping
            {
                printArray(secondLayer1);
                Console.WriteLine(-1);
            }
            else printArray(secondLayer1);
            Console.WriteLine();
            printArray(secondLayer2);
           
        }
    }
}