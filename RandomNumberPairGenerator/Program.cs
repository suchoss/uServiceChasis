using System;
using RabbitData;
using EasyNetQ;

namespace RandomNumberPairGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            var bus = RabbitHutch.CreateBus("host=localhost");

            while(true)
            {
                CalculatorInputs dataToSend = new CalculatorInputs()
                {
                    FirstNumber = random.Next(1000),
                    SecondNumber = random.Next(1000)
                };
                bus.Publish(dataToSend);
                Console.WriteLine("New pair [][] generated.");
            }
            
        }
    }
}
