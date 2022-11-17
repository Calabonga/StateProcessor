using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calabonga.StateProcessor;
using Calabonga.StatesProcessor.ConsoleTests.Entities;

namespace Calabonga.StatesProcessor.ConsoleTests.Rules
{
    public class BindStatusRule : StateRule<Accident, IAccidentState>
    {

        public BindStatusRule(IEnumerable<IAccidentState> states)
            : base(states)
        { 

        }

        public override Task<RuleValidationResult> CanEnterAsync(RuleContext<Accident, IAccidentState> context)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(context.Processor.Entity.Name))
            {
                errors.Add("Name is required");
                return Task.FromResult(new RuleValidationResult(errors));
            }
            return Task.FromResult(new RuleValidationResult());
        }

        /// <summary>
        /// Check this rule where TEntity leaving from this <see cref="StateRule{TEntity,TStatus}.ActiveState"/>
        /// </summary>
        /// <param name="context">RuleContext for checking</param>
        /// <returns></returns>
        public override Task<RuleValidationResult> CanLeaveAsync(RuleContext<Accident, IAccidentState> context)
        {
            return Task.FromResult(new RuleValidationResult());
        }

        /// <summary>
        /// Returns state for current rule for future validations
        /// </summary>
        /// <param name="statuses"></param>
        /// <returns></returns>
        protected override IAccidentState ValidationForStatus(IEnumerable<IAccidentState> statuses)
        {
            return  statuses.SingleOrDefault(x => x.Name.Equals(AccidentStateTypes.Binded.ToString()));
        }
    }
}