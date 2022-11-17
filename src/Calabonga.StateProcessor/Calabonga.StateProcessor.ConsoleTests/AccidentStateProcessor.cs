using Calabonga.StatesProcessor.ConsoleTests.Entities;
using Calabonga.StatesProcessor.ConsoleTests.States;
using Calabonga.StatusProcessor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calabonga.StatesProcessor.ConsoleTests
{

    /// <summary>
    /// Accident Rule Processor
    /// </summary>
    public class AccidentStateProcessor : StateProcessor<Accident, IAccidentState>
    {

        public AccidentStateProcessor(
            IEnumerable<IStateRule<Accident, IAccidentState>> rules,
            IEnumerable<IAccidentState> statuses)
            : base(rules, statuses)
        {

        }

        protected override Guid InitialStatus()
        {
            return StateFree.Guid;
        }

        public override IAccidentState GetCurrentState()
        {
            return States.First(x => x.Id == Entity.ActiveState);
        }
    }
}