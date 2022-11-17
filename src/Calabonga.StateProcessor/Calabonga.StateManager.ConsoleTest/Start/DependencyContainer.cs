using Autofac;
using Calabonga.StateManager.ConsoleTest.Entities;
using Calabonga.StateManager.ConsoleTest.Rules;
using Calabonga.StateManager.ConsoleTest.Statuses;
using Calabonga.StatusProcessor;

namespace Calabonga.StateManager.ConsoleTest.Start {
    public class DependencyContainer {

        internal static IContainer Initialize() {

            var builder = new ContainerBuilder();
            builder.RegisterType<AccidentRuleProcessor>().AsSelf();
            builder.RegisterType<FreeStatusRule>().As<IStatusRule<Accident, EntityStatus>>();
            builder.RegisterType<BindedStatusRule>().As<IStatusRule<Accident, EntityStatus>>();
          
            builder.RegisterType<StateFree>().As<IAccidentState>();
            builder.RegisterType<StateBinded>().As<IAccidentState>();
            builder.RegisterType<StateCompleted>().As<IAccidentState>();
            builder.RegisterType<StateArrivedAtPlace>().As<IAccidentState>();
            builder.RegisterType<StateDeleted>().As<IAccidentState>();
            builder.RegisterType<StateTemporarily>().As<IAccidentState>();
            
            return builder.Build();
        }
    }
}
