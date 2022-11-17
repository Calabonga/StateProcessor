using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calabonga.StatusProcessor
{
    /// <summary>
    /// The rule RuleProcessor.
    /// </summary>
    /// <typeparam name="TEntity">Entity with states</typeparam>
    /// <typeparam name="TState">The type of the state for RuleProcessor</typeparam>
    public abstract class StateRule<TEntity, TState> : IStateRule<TEntity, TState>
        where TEntity : IEntity, new()
        where TState : IState
    {
        protected StateRule(IEnumerable<TState> states)
        {
            SetStatusForRule(states);
        }

        /// <summary>
        /// The state this rule apply
        /// </summary>
        public TState ActiveState { get; private set; }

        /// <summary>
        /// Check this rule where TEntity entering this <see cref="ActiveState"/>
        /// </summary>
        /// <param name="context">RuleContext for checking</param>
        /// <returns></returns>
        public abstract Task<RuleValidationResult> CanEnterAsync(RuleContext<TEntity, TState> context);

        /// <summary>
        /// Check this rule where TEntity leaving from this <see cref="ActiveState"/>
        /// </summary>
        /// <param name="context">RuleContext for checking</param>
        /// <returns></returns>
        public abstract Task<RuleValidationResult> CanLeaveAsync(RuleContext<TEntity, TState> context);

        /// <summary>
        /// Returns state for current rule for future validations
        /// </summary>
        /// <param name="statuses"></param>
        /// <returns></returns>
        protected abstract TState ValidationForStatus(IEnumerable<TState> statuses);


        private void SetStatusForRule(IEnumerable<TState> statuses)
        {
            ActiveState = ValidationForStatus(statuses);
        }
    }
}