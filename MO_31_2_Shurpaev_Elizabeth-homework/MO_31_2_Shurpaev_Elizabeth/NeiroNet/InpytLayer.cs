using System;
using System.IO;

namespace MO_31_2_Shurpaev_Elizabeth.NeiroNet
{
    class InpytLayer
    {
        private double[,] trainset;
        private double[,] testset;

        public double[,] Trainset { get => trainset; }
        public double[,] Testset { get => testset; }

        public InpytLayer(NetworkMode nm)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string[] tmpArrStr;
            string[] tmpStr;

            switch (nm)
            {
                case NetworkMode.Train:
                    tmpArrStr = File.ReadAllLines(path + "train.txt");
                    trainset = new double[tmpArrStr.Length, 16];

                    for (int i = 0; i < tmpArrStr.Length; i++)
                    {
                        tmpStr = tmpArrStr[i].Split(' ');

                        for (int j = 0; j < 16; j++)
                        {
                            trainset[i, j] = double.Parse(tmpStr[j]);
                        }
                    }
                    Shuffling_Array_Rows(trainset);
                    break;

                case NetworkMode.Test:
                    tmpArrStr = File.ReadAllLines(path + "train.txt");
                    testset = new double[tmpArrStr.Length, 16];

                    for (int i = 0; i < tmpArrStr.Length; i++)
                    {
                        tmpStr = tmpArrStr[i].Split(' ');

                        for (int j = 0; j < 16; j++)
                        {
                            testset[i, j] = double.Parse(tmpStr[j]);
                        }
                    }
                    Shuffling_Array_Rows(testset);
                    break;
            }
        }

        public void Shuffling_Array_Rows(double[,] arr)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);
            Random rand = new Random();

            for (int i = rows - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);

                if (i != j)
                {
                    double[] tempRow = new double[cols];

                    for (int k = 0; k < cols; k++)
                    {
                        tempRow[k] = arr[i, k];
                    }

                    for (int k = 0; k < cols; k++)
                    {
                        arr[i, k] = arr[j, k];
                    }

                    for (int k = 0; k < cols; k++)
                    {
                        arr[j, k] = tempRow[k];
                    }
                }
            }
        }
    }
}
