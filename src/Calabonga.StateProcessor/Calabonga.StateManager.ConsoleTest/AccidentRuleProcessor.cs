using System;
using System.Collections.Generic;
using System.Linq;
using Calabonga.StateManager.ConsoleTest.Entities;
using Calabonga.StateManager.ConsoleTest.Statuses;
using Calabonga.StatusProcessor;

namespace Calabonga.StateManager.ConsoleTest {

    /// <summary>
    /// Accident Rule Processor
    /// </summary>
    public class AccidentRuleProcessor : RuleProcessor<Accident, IAccidentState> {

        public AccidentRuleProcessor(IEnumerable<IStatusRule<Accident, IAccidentState>> rules, IEnumerable<IAccidentState> statuses)
            : base(rules, statuses) { }


        protected override Guid InitialStatus()
        {
            return StateFree.Guid;
        }


    }
}