using Calabonga.StatesProcessor.ConsoleTests.Entities;
using Calabonga.StatusProcessor;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Calabonga.StatesProcessor.ConsoleTests.Rules
{

    public class FreeStateRule : StateRule<Accident, IAccidentState>
    {

        public FreeStateRule(IEnumerable<IAccidentState> states)
            : base(states)
        {

        }

        public override Task<RuleValidationResult> CanEnterAsync(RuleContext<Accident, IAccidentState> context)
        {
            Contract.Assert(context != null, "context!=null");
            Contract.Assert(context.Processor != null, "context.Processor != null");
            Contract.Assert(context.Processor.Entity != null, "context.Processor.ProcessorEntity != null");
            var isEmpty = context.Processor.Entity.ActiveState == Guid.Empty;
            var result = new RuleValidationResult();
            if (!isEmpty)
            {
                result.AddError("EntityActiveState should be empty Guid");
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// Check this rule where TEntity leaving from this <see cref="StateRule{TEntity,TState}.ActiveState"/>
        /// </summary>
        /// <param name="context">RuleContext for checking</param>
        /// <returns></returns>
        public override Task<RuleValidationResult> CanLeaveAsync(RuleContext<Accident, IAccidentState> context)
        {
            var acceptedState = context.Processor.States.SingleOrDefault(x => x.Name.Equals(AccidentStateTypes.Binded.ToString()));
            var isOk = acceptedState != null && context.Processor.RequestedState.Id == acceptedState.Id;
            var result = new RuleValidationResult();
            if (!isOk)
            {
                result.AddError("RequestedState should be Binded");
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// Returns state for current rule for future validations
        /// </summary>
        /// <param name="statuses"></param>
        /// <returns></returns>
        protected override IAccidentState ValidationForStatus(IEnumerable<IAccidentState> statuses)
        {
            return statuses.SingleOrDefault(x => x.Name.Equals(AccidentStateTypes.Free.ToString()));
        }
    }
}
