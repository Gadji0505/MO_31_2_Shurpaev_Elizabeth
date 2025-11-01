using System;
using System.IO;
using System.Windows.Forms;

namespace MO_31_2_Shurpaev_Elizabeth.NeiroNet
{
    abstract class Layer
    {
        protected string name_Layer;
        string pathDirWeights;
        string pathFileWeights;
        protected int numofneurons;
        protected int numofprevneurons;
        protected const double learningrate = 0.0006;
        protected const double momentum = 0.050;
        protected double[,] lastdeltaweights;
        protected Neuron[] neurons;


        public Neuron[] Neurons { get => neurons; set => neurons = value; }
        public double[] Date
        {
            set
            {
                for (int i = 0; i < numofneurons; i++)
                {
                    Neurons[i].Activator(value);
                }
            }
        }
        protected Layer(int non, int nopn, NeuronType nt, string nm_Layer)
        {
            numofneurons = non;
            numofprevneurons = nopn;
            Neurons = new Neuron[non];
            name_Layer = nm_Layer;
            pathDirWeights = AppDomain.CurrentDomain.BaseDirectory + "memory\\";
            pathFileWeights = pathDirWeights + name_Layer + "_memory.csv";

            lastdeltaweights = new double[non, nopn + 1];
            double[,] Weights;

            if (File.Exists(pathFileWeights))
                Weights = WeightInitialize(MemoryMode.GET, pathFileWeights);
            else
            {
                Directory.CreateDirectory(pathDirWeights);
                Weights = WeightInitialize(MemoryMode.INIT, pathFileWeights);
            }

            for (int i = 0; i < non; i++)
            {
                double[] tmp_weigts = new double[nopn + 1];
                for (int j = 0; j < nopn + 1; j++)
                {
                    tmp_weigts[j] = Weights[i, j];
                }
                Neurons[i] = new Neuron(tmp_weigts, nt);
            }
        }
        public double[,] WeightInitialize(MemoryMode mm, string path)
        {
            char[] delim = new char[] { ';', ' ' };
            string tmpStr;
            string[] tmpStrWeights;
            double[,] weights = new double[numofneurons, numofprevneurons + 1];

            switch (mm)
            {
                case MemoryMode.GET:
                    tmpStrWeights = File.ReadAllLines(path);
                    string[] memory_element;
                    for (int i = 0; i < numofneurons; i++)
                    {
                        memory_element = tmpStrWeights[i].Split(delim);
                        for (int j = 0; j < numofprevneurons + 1; j++)
                        {
                            weights[i, j] = double.Parse(memory_element[j].Replace(',', '.'),
                                System.Globalization.CultureInfo.InvariantCulture);
                        }
                    }
                    break;
                case MemoryMode.SET:
                    using (StreamWriter sw = new StreamWriter(path, false))
                    {
                        for (int i = 0; i < numofneurons; i++)
                        {
                            tmpStr = "";
                            for (int j = 0; j < numofprevneurons + 1; j++)
                            {
                                weights[i, j] = Neurons[i].Weights[j];
                                tmpStr += weights[i, j].ToString().Replace(',', '.') + ";";
                            }
                            sw.WriteLine(tmpStr.TrimEnd(';'));
                        }
                    }
                    break;
                case MemoryMode.INIT:
                    Random random = new Random();
                    for (int i = 0; i < numofneurons; i++)
                    {
                        double[] neuronWeights = GenerateRandomWeightsCLT(numofprevneurons + 1, random);
                        for (int j = 0; j < numofprevneurons + 1; j++)
                        {
                            weights[i, j] = neuronWeights[j];
                            //if (weights[i, j] >= 1.0)
                            //    weights[i, j] = 1.0;
                            //if (weights[i, j] <= -1.0)
                            //    weights[i, j] = -1.0;
                        }
                    }

                    using (StreamWriter sw = new StreamWriter(path, false))
                    {
                        for (int i = 0; i < numofneurons; i++)
                        {
                            string line = "";
                            for (int j = 0; j < numofprevneurons + 1; j++)
                            {
                                line += weights[i, j].ToString(System.Globalization.CultureInfo.InvariantCulture) + ";";
                            }
                            sw.WriteLine(line.TrimEnd(';'));
                        }
                    }
                    break;
                default:
                    break;
            }
            return weights;
        }

        public double[] GenerateRandomWeightsCLT(int count, Random random)
        {
            double[] weights = new double[count];
            const int samples = 12;

            for (int i = 0; i < count; i++)
            {
                double sum = 0;
                for (int j = 0; j < samples; j++)
                {
                    sum += random.NextDouble();
                }
                weights[i] = (sum - samples / 2.0) / Math.Sqrt(samples / 12.0);

            }

            return weights;
        }

        abstract public void Recognizee(Network net, Layer nextLayer);
        abstract public double[] BackwardPass(double[] stuff);
    }
}