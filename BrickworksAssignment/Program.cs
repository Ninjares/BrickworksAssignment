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
        static void drawLayer(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (i == 0)
                    {
                        if (j == 0) Console.Write("╔══");
                        else
                        {
                            if (array[i, j] == array[i, j - 1]) Console.Write("═══");
                            else Console.Write("╦══");
                        }
                    }
                    else
                    {
                        if (j == 0)
                            if (array[i, j] == array[i - 1, j]) Console.Write("║--"); else Console.Write("╠══");
                        else
                        {
                            int x = array[i, j];
                            int a = array[i - 1, j - 1];
                            int b = array[i - 1, j];
                            int c = array[i, j - 1];
                            if (a == b && c == x && c!=a && x!=b) Console.Write("═══");
                            if (a == c && b == x && a!=b && c!=x) Console.Write("║--");
                            if (a != b && b != x && c!=x && c!=a) Console.Write("╬══");
                            if (a != c && b == x && b!=a && c!=x) Console.Write("╣--");
                            if (a == c && b != x && a!=b && c!=x) Console.Write("╠══");
                            if (a != b && c == x && a!=c && b!=x) Console.Write("╩══");
                            if (a == b && c != x && a!=c && b!=x) Console.Write("╦══");

                        }
                    }
                }
                if (i == 0) Console.WriteLine("╗"); else if (array[i, array.GetLength(1)-1] == array[i - 1, array.GetLength(1)-1]) Console.WriteLine("║"); else Console.WriteLine("╣");
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        Console.Write($"║{(array[i, j] < 10 ? $" {array[i, j]}" : $"{array[i, j]}")}");
                    }
                    else
                    {
                        if(array[i,j-1] == array[i,j]) Console.Write($"|{(array[i, j] < 10 ? $" {array[i, j]}" : $"{array[i, j]}")}");
                        else Console.Write($"║{(array[i, j] < 10 ? $" {array[i, j]}" : $"{array[i, j]}")}");
                    }
                }
                Console.WriteLine("║");
            }
            for(int j=0; j<array.GetLength(1); j++)
            {
                if (j == 0) Console.Write("╚══");
                else
                {
                    if (array[array.GetLength(0) - 1, j] == array[array.GetLength(0) - 1, j - 1]) Console.Write("═══");
                    else Console.Write("╩══");
                }
            }
            Console.WriteLine("╝");
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
                            if (firstLayer[i, j] != firstLayer[i, j + 1])
                            {
                                secondlayer[i, j] = currentnumber;
                                secondlayer[i, j + 1] = currentnumber;
                                currentnumber++;
                                brickplaced = true;
                            }
                        }
                        if (i + 1 < firstLayer.GetLength(0) && !brickplaced)
                        {
                            if (firstLayer[i, j] != firstLayer[i + 1, j])
                            {
                                secondlayer[i, j] = currentnumber;
                                secondlayer[i + 1, j] = currentnumber;
                                currentnumber++;
                            }
                        }
                    }
                }
            }
            return secondlayer; 
        }

        static int[,] recursiveMethod(int[,] firstLayer, int[,] secondlayer, int i, int j, int currentnumber)
        {
            if (j == firstLayer.GetLength(1)) return recursiveMethod(firstLayer, secondlayer, i + 1, 0, currentnumber); //next row
            if (j + 1 == firstLayer.GetLength(1) && i + 1 == firstLayer.GetLength(0)) { return secondlayer; }// return work
            

            if (secondlayer[i, j] == 0)
            {
                bool horizontalTried = false;
                if (j + 1 < firstLayer.GetLength(1)) //am I not at the end
                {
                    if (firstLayer[i, j] != firstLayer[i, j + 1]) // can I place a horizontal brick
                    {
                        secondlayer[i, j] = currentnumber;
                        secondlayer[i, j + 1] = currentnumber;
                        currentnumber++;
                        int[,] result = recursiveMethod(firstLayer, secondlayer, i, j + 1, currentnumber);
                        if (result.Cast<int>().Contains(0))
                        {
                            
                                secondlayer[i, j] = 0;
                                secondlayer[i, j + 1] = 0;
                                currentnumber--;
                                horizontalTried = true;
                            
                        }
                        else return result;
                    } else horizontalTried = true;
                } else horizontalTried = true;

                if (i + 1 < firstLayer.GetLength(0) && horizontalTried)
                {
                    if (firstLayer[i, j] != firstLayer[i + 1, j])
                    {
                        secondlayer[i, j] = currentnumber;
                        secondlayer[i + 1, j] = currentnumber;
                        currentnumber++;
                        int[,] result = recursiveMethod(firstLayer, secondlayer, i, j + 1, currentnumber);
                        if (result.Cast<int>().Contains(0))
                        {
                            secondlayer[i, j] = 0;
                            secondlayer[i + 1, j] = 0;
                        }
                        return result;
                    }
                    else return recursiveMethod(firstLayer, secondlayer, i, j + 1, currentnumber);
                }
                else return recursiveMethod(firstLayer, secondlayer, i, j + 1, currentnumber);
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
            drawLayer(bricks);
            int[,] secondLayer1 = createSecondLayer(bricks);
            int[,] secondLayer2 = recursiveMethod(bricks, new int[m, n], 0, 0, 1);
            if (secondLayer2.Cast<int>().Contains(0)) //if the second layer contains a 0 that means a brick could not be placed without overlapping
            {
                Console.WriteLine(-1);
            }
            else drawLayer(secondLayer2);
           
        }
    }
}