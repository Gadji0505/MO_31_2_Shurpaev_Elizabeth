namespace MO_31_2_Shurpaev_Elizabeth.NeiroNet
{
    class Network
    {
        private InpytLayer input_layer = null;
        private HiddenLayer hidden_layer1 = new HiddenLayer(71, 15, NeuronType.Hidden, nameof(hidden_layer1));
        private HiddenLayer hidden_layer2 = new HiddenLayer(34, 71, NeuronType.Hidden, nameof(hidden_layer2));
        private OutputLayer output_layer = new OutputLayer(10, 34, NeuronType.Output, nameof(output_layer));

        private double[] fact = new double[10]; //массив файктического выхода сети
        private double[] e_error_avr; //среднее значение энергии ошибки эпохи обучения

        //свойста
        public double[] Fact { get => fact; } //массив фактического выхода сети

        //среднее значение энергии ошибки эпохи обучения
        public double[] E_error_avr { get => e_error_avr; set => e_error_avr = value; }

        //конструктор
        public Network() { }

        public void ForwardPass(Network net, double[] netInput)
        {
            net.hidden_layer1.Date = netInput;
            net.hidden_layer1.Recognizee(null, net.hidden_layer2);
            net.hidden_layer2.Recognizee(null, net.output_layer);
            net.output_layer.Recognizee(net, null);
        }
    }
}
