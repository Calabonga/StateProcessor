using System.Threading.Tasks;

namespace Calabonga.StatusProcessor
{
    /// <summary>
    /// Rule for RuleProcessor interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TState"></typeparam>
    public interface IStateRule<TEntity, TState>
        where TEntity : IEntity, new()
        where TState : IState
    {
        TState ActiveState { get; }

        Task<RuleValidationResult> CanLeaveAsync(RuleContext<TEntity, TState> context);

        Task<RuleValidationResult> CanEnterAsync(RuleContext<TEntity, TState> context);
    }
}