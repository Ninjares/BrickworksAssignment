using System;
using System.Linq;

namespace BrickworksAssignment
{
    class Program
    {
        static void printArray(int[][] array)
        {
            foreach(int[] subarray in array)
                Console.WriteLine(string.Join(' ', subarray));
        }
        static void Main(string[] args)
        {
            int[] dimension = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[][] bricks = new int[dimension[0]][];
            for (int i = 0; i < dimension[0]; i++)
            {
                bricks[i] = Console.ReadLine().Split().Select(int.Parse).ToArray();
                if (bricks[i].Length != dimension[1]) throw new IndexOutOfRangeException("Incorrect range");
            }

            printArray(bricks);
        }
    }
}
