using System.Collections.Generic;

namespace Calabonga.StatusProcessor {

    /// <summary>
    /// The rule RuleProcessor.
    /// </summary>
    /// <typeparam name="TEntity">Entity with states</typeparam>
    /// <typeparam name="TStatus">The type of the state for RuleProcessor</typeparam>
    public abstract class StatusRule<TEntity, TStatus> : IStatusRule<TEntity, TStatus>
        where TEntity : IEntity, new() where TStatus : EntityStatus
    {

        public StatusRule(IEnumerable<TStatus> statuses)
        {
            SetStatusForRule(statuses);
        }

        /// <summary>
        /// The state this rule apply
        /// </summary>
        public EntityStatus EntityActiveStatus { get; private set; }

        /// <summary>
        /// Check this rule where TEntity entering this <see cref="EntityActiveStatus"/>
        /// </summary>
        /// <param name="context">RuleContext for checking</param>
        /// <returns></returns>
        public abstract RuleValidationResult CanEnter(RuleContext<TEntity, TStatus> context);

        /// <summary>
        /// Check this rule where TEntity leaving from this <see cref="EntityActiveStatus"/>
        /// </summary>
        /// <param name="context">RuleContext for checking</param>
        /// <returns></returns>
        public abstract RuleValidationResult CanLeave(RuleContext<TEntity, TStatus> context);

        /// <summary>
        /// Returns status for current rule for future validations
        /// </summary>
        /// <param name="statuses"></param>
        /// <returns></returns>
        protected abstract EntityStatus ValidationForStatus(IEnumerable<TStatus> statuses);


        private void SetStatusForRule(IEnumerable<TStatus> statuses) {
            EntityActiveStatus = ValidationForStatus(statuses);
        }
    }
}