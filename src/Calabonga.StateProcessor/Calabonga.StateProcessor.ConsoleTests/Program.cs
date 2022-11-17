using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calabonga.StateProcessor;
using Calabonga.StatesProcessor.ConsoleTests.Entities;
using Calabonga.StatesProcessor.ConsoleTests.States;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.StatesProcessor.ConsoleTests
{
    class Program
    {
        public static IServiceProvider ServiceProvider { get; set; }

        static async Task Main(string[] args)
        {
            ServiceProvider = DependencyContainer.Initialize();

            var states = ServiceProvider.GetService<IEnumerable<IAccidentState>>();

            Console.WriteLine("Total states found: {0}", states!.Count());

            var rules = ServiceProvider.GetService<IEnumerable<IStateRule<Accident, IAccidentState>>>()!.ToList();

            Console.WriteLine("Total rules found: {0}", rules.Count);

            var processor = ServiceProvider.GetService<AccidentStateProcessor>();

            var entity = await processor!.CreateAsync();
            var entityState = processor.GetCurrentState();
            Console.WriteLine("New entity state ID: {0}", entityState.Id);
            Console.WriteLine("New entity state Name: {0}", entityState.Name);
            Console.WriteLine("New entity state DisplayName: {0}", entityState.DisplayName);

            var processorResult = await processor.UpdateStatusAsync(entity, StateBind.Guid);
            if (processorResult.Succeeded)
            {
                Console.WriteLine("New state is: {0}", processorResult.NewState.DisplayName);
                Console.WriteLine("New state is: {0}", processorResult.OldStatus.DisplayName);
            }
            else
            {
                foreach (var error in processorResult.Errors)
                {

                    Console.WriteLine("Error: {0}", error);
                }
            }
        }

    }
}
