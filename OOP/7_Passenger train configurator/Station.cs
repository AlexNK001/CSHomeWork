using System;
using System.Collections.Generic;

namespace _7_Passenger_train_configurator
{
    public class Station
    {
        private readonly List<Train> _trains;
        private readonly TrainFactory _creatorHandler;

        public Station(TrainFactory creator)
        {
            _creatorHandler = creator;
            _trains = new List<Train>();
        }

        public void Work()
        {
            bool isWork = true;

            while (isWork)
            {
                TextStorage.ShowCountTrains(_trains.Count);
                ShowTrains();
                TextStorage.ShowMainMenu();

                switch (Console.ReadLine())
                {
                    case TextStorage.CommandAddTrain:
                        AddTrain();
                        break;

                    case TextStorage.CommandExit:
                        isWork = false;
                        break;

                    default:
                        TextStorage.ReportIncorrectInput();
                        break;
                }

                Console.Clear();
            }
        }

        private void ShowTrains()
        {
            for (int i = 0; i < _trains.Count; i++)
            {
                TextStorage.ShowBriefInformation(_trains[i]);
            }
        }

        private void AddTrain()
        {
            Train train = _creatorHandler.Create();
            TextStorage.ShowFullInformation(train);
            _trains.Add(train);
            Console.ReadKey();
        }
    }
}
