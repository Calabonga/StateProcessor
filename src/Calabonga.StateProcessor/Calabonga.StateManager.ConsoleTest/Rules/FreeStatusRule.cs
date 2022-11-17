using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Calabonga.StateManager.ConsoleTest.Entities;
using Calabonga.StatusProcessor;

namespace Calabonga.StateManager.ConsoleTest.Rules {

    public class FreeStatusRule : StatusRule<Accident, EntityStatus> {

        public FreeStatusRule(IEnumerable<EntityStatus> statuses) : base(statuses) { }

        public override RuleValidationResult CanEnter(RuleContext<Accident, EntityStatus> context) {
            Contract.Assert(context != null, "context!=null");
            Contract.Assert(context.Processor != null, "context.Processor != null");
            Contract.Assert(context.Processor.Entity != null, "context.Processor.ProcessorEntity != null");
            var isEmpty= context.Processor.Entity.EntityActiveStatus == Guid.Empty;
            var result = new RuleValidationResult();
            if (!isEmpty)
            {
                result.AddError("EntityActiveStatus should be empty Guid");
            }
            return result;
        }

        /// <summary>
        /// Check this rule where TEntity leaving from this <see cref="StatusRule{TEntity,TStatus}.EntityActiveStatus"/>
        /// </summary>
        /// <param name="context">RuleContext for checking</param>
        /// <returns></returns>
        public override RuleValidationResult CanLeave(RuleContext<Accident, EntityStatus> context) {
            var acceptedStatus = context.Processor.Statuses.SingleOrDefault(x => x.Name.Equals(AccidentStatus.Binded.ToString()));
            var isOk= acceptedStatus != null && context.Processor.RequestedStatus.Id == acceptedStatus.Id;
            var result = new RuleValidationResult();
            if (!isOk) {
                result.AddError("RequestedStatus should be Binded");
            }
            return result;
        }

        /// <summary>
        /// Returns status for current rule for future validations
        /// </summary>
        /// <param name="statuses"></param>
        /// <returns></returns>
        protected override EntityStatus ValidationForStatus(IEnumerable<EntityStatus> statuses) {
            return statuses.SingleOrDefault(x => x.Name.Equals(AccidentStatus.Free.ToString()));
        }

    }
}
