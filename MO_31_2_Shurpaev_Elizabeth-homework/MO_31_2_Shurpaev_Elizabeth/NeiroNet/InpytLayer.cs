using System;
using System.IO;

namespace MO_31_2_Shurpaev_Elizabeth.NeiroNet
{
    // Предполагается, что где-то определен enum NetworkMode
    public enum NetworkMode { Train, Test }

    class InpytLayer
    {
        //поля
        private double[,] trainset;
        private double[,] testset;

        //свойства (Исправлена опечатка в имени свойства Teatset -> Testset)
        public double[,] Trainset { get => trainset; }
        public double[,] Testset { get => testset; } // Исправлено

        // Конструктор
        public InpytLayer(NetworkMode nm)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string[] tmpArrStr;
            string[] tmpStr;

            switch (nm)
            {
                case NetworkMode.Train:
                    // В режиме Train читаем train.txt
                    tmpArrStr = File.ReadAllLines(path + "train.txt");
                    // 16 колонок, где последние колонки обычно содержат целевые значения
                    trainset = new double[tmpArrStr.Length, 16]; 

                    for (int i = 0; i < tmpArrStr.Length; i++)
                    {
                        // Разделитель - пробел
                        tmpStr = tmpArrStr[i].Split(' '); 

                        for (int j = 0; j < 16; j++)
                        {
                            trainset[i, j] = double.Parse(tmpStr[j]);
                        }
                    }
                    // Перетасовка обучающей выборки
                    Shuffling_Array_Rows(trainset);
                    break;

                case NetworkMode.Test:
                    // В режиме Test, возможно, нужно читать test.txt, 
                    // но согласно Вашему коду, читается 'train.txt'
                    // Оставляем как в оригинале, но обычно здесь должен быть 'test.txt'
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
                    // Перетасовка тестовой выборки
                    Shuffling_Array_Rows(testset);
                    break;
            }
        }

        /// <summary>
        /// Перетасовывает строки двумерного массива (выборки) 
        /// методом Фишера-Йетса.
        /// </summary>
        /// <param name="arr">Двумерный массив (выборка).</param>
        public void Shuffling_Array_Rows(double[,] arr)
        {
            // Получаем количество строк в массиве
            int rows = arr.GetLength(0);
            // Получаем количество столбцов (длина строки/примера)
            int cols = arr.GetLength(1); 

            // Используем статический экземпляр Random (лучше, чем создавать новый в цикле)
            Random rand = new Random();

            // Алгоритм Фишера-Йетса
            // Идем с конца до второй строки (i > 0)
            for (int i = rows - 1; i > 0; i--)
            {
                // Генерируем случайный индекс j от 0 до i (включительно)
                int j = rand.Next(i + 1);

                // Если j != i, меняем местами текущую строку arr[i, ...] со случайной строкой arr[j, ...]
                if (i != j)
                {
                    // Временный массив для хранения текущей строки arr[i, ...]
                    double[] tempRow = new double[cols]; 

                    // 1. Копируем arr[i, ...] во tempRow
                    for (int k = 0; k < cols; k++)
                    {
                        tempRow[k] = arr[i, k];
                    }

                    // 2. Копируем arr[j, ...] в arr[i, ...]
                    for (int k = 0; k < cols; k++)
                    {
                        arr[i, k] = arr[j, k];
                    }

                    // 3.
