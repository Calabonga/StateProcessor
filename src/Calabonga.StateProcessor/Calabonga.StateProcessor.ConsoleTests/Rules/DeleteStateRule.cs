using Calabonga.StatesProcessor.ConsoleTests.Entities;
using Calabonga.StatusProcessor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calabonga.StatesProcessor.ConsoleTests.Rules
{
    public class DeleteStateRule : StateRule<Accident, IAccidentState>
    {
        public DeleteStateRule(IEnumerable<IAccidentState> states) : base(states)
        {
        }

        public override Task<RuleValidationResult> CanEnterAsync(RuleContext<Accident, IAccidentState> context)
        {
            return Task.FromResult(new RuleValidationResult());
        }

        public override Task<RuleValidationResult> CanLeaveAsync(RuleContext<Accident, IAccidentState> context)
        {
            return Task.FromResult(new RuleValidationResult());
        }

        protected override IAccidentState ValidationForStatus(IEnumerable<IAccidentState> statuses)
        {
            return statuses.SingleOrDefault(x => x.Name.Equals(AccidentStateTypes.Deleted.ToString()));
        }
    }
}
