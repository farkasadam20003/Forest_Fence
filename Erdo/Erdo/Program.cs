using System;
using System.Collections.Generic;
using System.Linq;

namespace Erdo
{
    class Program
    {

        public static int n = 10;
        public static int sizex = 20;
        public static int sizey = sizex;
        public static int[,] trees = new int[n, 2];

        static void Main(string[] args)
        {
            GenerateForest();
            Rotation_matrix();
            Console.ReadLine();
        }

        public static void GenerateForest()
        {
            Random rng = new Random();
            for (int i = 0; i < n; i++)
            {
                trees[i, 0] = rng.Next(sizex);
                trees[i, 1] = rng.Next(sizey);
                Console.SetCursorPosition(trees[i, 0], trees[i, 1]);
                Console.Write("X");
            }
            Console.SetCursorPosition(sizex, sizey);
            //for (int i = 0; i < n; i++)
            //{
                //Console.Write("\r\n{0}, {1}", trees[i, 0], trees[i, 1]);
            //}
        }
        public static void Rotation_matrix()
        {
            //Length of fence
            var watch = System.Diagnostics.Stopwatch.StartNew();
            double[] rotated_trees = new double[n];
            double angle_step = Math.Atan(Convert.ToDouble(sizey) / (Convert.ToDouble(sizex)-1) / 2) - Math.Atan(Convert.ToDouble(sizey) / 2 / Convert.ToDouble(sizex));
            double angle = 0;
            double FenceLength = 0;
            List<int> min_index = new List<int>();
            Console.WriteLine();
            double xA, xB, yA, yB;
            int j = 0;
            while (angle < (2 * Math.PI))
            {
                for (int i = 0; i < n; i++)
                {
                    rotated_trees[i] = Math.Cos(angle) * trees[i, 0] + Math.Sin(angle) * trees[i, 1];
                }
                min_index.Add(Array.IndexOf(rotated_trees, rotated_trees.Min()));
                if (j > 0 && min_index[j] != min_index[j - 1])
                {
                    xA = Convert.ToDouble(trees[min_index[j], 0]);
                    xB = Convert.ToDouble(trees[min_index[j - 1], 0]);
                    yA = Convert.ToDouble(trees[min_index[j], 1]);
                    yB = Convert.ToDouble(trees[min_index[j - 1], 1]);
                    FenceLength += Math.Sqrt(Math.Pow(xA-xB,2) + Math.Pow(yA - yB, 2));
                }
                j++;
                angle += angle_step;
            }
            xA = Convert.ToDouble(trees[min_index[j-1], 0]);
            xB = Convert.ToDouble(trees[min_index[0], 0]);
            yA = Convert.ToDouble(trees[min_index[j-1], 1]);
            yB = Convert.ToDouble(trees[min_index[0], 1]);
            FenceLength += Math.Sqrt(Math.Pow(xA - xB, 2) + Math.Pow(yA - yB, 2));
            Console.WriteLine("The length of fence: {0}",FenceLength);
            watch.Stop();
            var elapsed = watch.Elapsed;
            Console.Write("\r\n{0}", elapsed);

            //Drawing fence corners
            Console.SetCursorPosition(trees[min_index[0], 0], trees[min_index[0], 1]);
            Console.Write("O");
            for (int k = 1; k < min_index.Count(); k++)
            {
                if (min_index[k] != min_index[k - 1])
                {
                    Console.SetCursorPosition(trees[min_index[k], 0], trees[min_index[k], 1]);
                    Console.Write("O");
                }
            }
        }

    }
}
