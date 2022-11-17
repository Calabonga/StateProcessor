using System.Collections.Generic;
using System.Linq;
using Calabonga.StateManager.ConsoleTest.Entities;
using Calabonga.StatusProcessor;

namespace Calabonga.StateManager.ConsoleTest.Rules
{
    public class BindedStatusRule : StatusRule<Accident, EntityStatus> {

        public BindedStatusRule(IEnumerable<EntityStatus> statuses) : base(statuses) { }

        public override RuleValidationResult CanEnter(RuleContext<Accident, EntityStatus> context) {
            return new RuleValidationResult();
        }

        /// <summary>
        /// Check this rule where TEntity leaving from this <see cref="StatusRule{TEntity,TStatus}.EntityActiveStatus"/>
        /// </summary>
        /// <param name="context">RuleContext for checking</param>
        /// <returns></returns>
        public override RuleValidationResult CanLeave(RuleContext<Accident, EntityStatus> context) {
            return new RuleValidationResult();
        }

        /// <summary>
        /// Returns status for current rule for future validations
        /// </summary>
        /// <param name="statuses"></param>
        /// <returns></returns>
        protected override EntityStatus ValidationForStatus(IEnumerable<EntityStatus> statuses) {
            return statuses.SingleOrDefault(x => x.Name.Equals(AccidentStatus.Binded.ToString()));
        }

    }
}