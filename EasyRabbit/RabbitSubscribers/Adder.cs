using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Orion.Rabbit;
using RabbitData;
using System;
using Microsoft.Extensions.Logging;
using EasyRabbit.Models;

namespace EasyRabbit.RabbitSubscribers
{
    public class Adder : IScopedProcessingService<CalculatorInputs>
    {
        private ILogger<Adder> _logger;
        private ApplicationDbContext _db;

        public Adder(ApplicationDbContext dbContext, ILogger<Adder> logger)
        {
            _logger = logger;
            _db = dbContext;
        }

        public async Task HandleMessageAsync(CalculatorInputs message)
        {

            Console.WriteLine($"Calculator: [{message.FirstNumber}] + [{message.SecondNumber}] = {message.FirstNumber + message.SecondNumber}");
            _db.Calculations.Add(new Calculation()
            {
                FirstNumber = message.FirstNumber,
                SecondNumber = message.SecondNumber,
                Result = message.FirstNumber + message.SecondNumber
            });

            await _db.SaveChangesAsync();
            
        }
    }
}
