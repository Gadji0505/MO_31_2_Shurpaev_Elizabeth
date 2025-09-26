using NeuroNetModel.NeuroNet;
using static System.Math;

namespace NeuroNetModel.NeuroNet
{
    class Neuron
    {
        private NeuronType type; // тип нейрона
        private double[] weights; // веса
        private double[] inputs; // входы
        private double output; // выход
        private double derivative; // производная

        // константы для функции активации
        private double a = 0.01d;

        // свойства
        public double[] Weights { get => weights; set => weights = value; }
        public double[] Inputs { get => inputs; set => inputs = value; }
        public double Output { get => output; }
        public double Derivative { get => derivative; }

        // конструктор
        public Neuron(double[] memoryWeights, NeuronType typeNeuron)
        {
            type = typeNeuron;
            weights = memoryWeights;
        }

        public void Activator(double[] i)
        {
            inputs = i; // передача вектора входного сигнала в массив входных данных нейрона
            double sum = weights[0]; // аффинное преобразование через смещение (нулевой вес)

            for (int j = 0; j < inputs.Length; j++) // цикл вычисления индуцированного поля
            {
                sum += inputs[j] * weights[j + 1]; // линейные преобразования входных сигналов
            }

            switch (type)
            {
                case NeuronType.Hidden: // для нейронов скрытого слоя
                    output = LeakyReLU(sum);
                    derivative = LeakyReLU_Derivativator(sum);
                    break;
                case NeuronType.Output: // для нейронов выходного слоя
                    output = Exp(sum);
                    break;
            }
        }

        // функция активации нейрона
        private double LeakyReLU(double sum)
        {
            return sum > 0 ? sum : a * sum;
        }

        private double LeakyReLU_Derivativator(double sum)
        {
            return sum > 0 ? 1 : a;
        }

        //private double Exp(double sum)
        //{
        //    return 1.0 / (1.0 + Exp(-sum));
        //}
    }
}