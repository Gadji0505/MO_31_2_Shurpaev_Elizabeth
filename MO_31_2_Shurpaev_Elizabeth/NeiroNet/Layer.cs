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
        protected const double learningrate = 0.060;
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
            switch (mm)
            {
                case MemoryMode.GET:
                    break;
                case MemoryMode.SET:
                    break;
                case MemoryMode.INIT:
                    break;
                default:
                    break;
            }
        }
        
    }
}
