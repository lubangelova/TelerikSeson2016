﻿namespace Cube2
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class TestsGenerator
    {
        private string filePath;
        private HashSet<int> sizes;
        private const int MinSize = 4;
        private const int MaxSize = 100;
        private const int NumTests = 20;

        public const string FileNamesFormat = "{0}test.{1:000}.in.txt";
        private static readonly Random Rand = new Random();

        public void GenerateTests(string filePath = "../../Tests/")
        {
            this.filePath = filePath;
            if (!string.IsNullOrWhiteSpace(this.filePath) &&
                !Directory.Exists(this.filePath))
            {
                Directory.CreateDirectory(this.filePath);
            }

            this.sizes = new HashSet<int>();

            this.CreateTest(1, MinSize);
            this.CreateTest(NumTests, MaxSize);

            for (int i = 2; i <= NumTests - 1; i++)
            {
                int newSize;
                do
                {
                    newSize = Rand.Next(MinSize, MaxSize);
                } while (sizes.Contains(newSize));

                this.CreateTest(i, newSize);
            }
        }

        private void CreateTest(int testNumber, int size)
        {
            using (var sw = new StreamWriter(string.Format(FileNamesFormat, this.filePath, testNumber)))
            {
                sw.WriteLine(size);
            }

            sizes.Add(size);
            Console.WriteLine("Test {0} is ready!", testNumber);
        }
    }
}
