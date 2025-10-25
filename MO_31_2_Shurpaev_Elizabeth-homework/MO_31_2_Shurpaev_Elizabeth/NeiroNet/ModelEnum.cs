namespace MO_31_2_Shurpaev_Elizabeth.NeiroNet
{
    enum MemoryMode // режимы работы памяти
    {
        GET,    // считывание памяти
        SET,    // сохранение памяти
        INIT    // инициализация памяти
    }

    enum NeuronType // тип нейрона
    {
        Hidden, // скрытый
        Output  // выходной
    }

    enum NetworkMode // режимы работы сети
    {
        Train,   // обучение
        Test,    // проверка
        Demo     // распознавание
    }
}