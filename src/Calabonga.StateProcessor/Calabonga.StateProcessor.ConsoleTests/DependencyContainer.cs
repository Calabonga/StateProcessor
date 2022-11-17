using Calabonga.StatesProcessor.ConsoleTests.Entities;
using Calabonga.StatesProcessor.ConsoleTests.Rules;
using Calabonga.StatesProcessor.ConsoleTests.States;
using Calabonga.StatusProcessor;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Calabonga.StatesProcessor.ConsoleTests
{
    /// <summary>
    /// // Calabonga: update summary (2020-05-14 11:01 DependencyContainer)
    /// </summary>
    public class DependencyContainer
    {
        internal static IServiceProvider Initialize()
        {
            var services = new ServiceCollection();
            services.AddTransient<AccidentStateProcessor>();
            services.AddSingleton<IStateRule<Accident, IAccidentState>, BindStatusRule>();
            services.AddSingleton<IStateRule<Accident, IAccidentState>, FreeStateRule>();
            services.AddSingleton<IStateRule<Accident, IAccidentState>, DeleteStateRule>();
            services.AddSingleton<IAccidentState, StateBind>();
            services.AddSingleton<IAccidentState, StateFree>();
            services.AddSingleton<IAccidentState, StateCompleted>();
            services.AddSingleton<IAccidentState, StateArrivedAtPlace>();
            services.AddSingleton<IAccidentState, StateDeleted>();
            services.AddSingleton<IAccidentState, StateTemporarily>();
            return services.BuildServiceProvider();
        }
    }
}
