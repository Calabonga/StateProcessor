namespace Calabonga.StatusProcessor {

    /// <summary>
    /// Rule for RuleProcessor interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TStatus"></typeparam>
    public interface IStatusRule<TEntity, TStatus>
        where TEntity : IEntity, new() where TStatus : IEntityStatus {

        EntityStatus EntityActiveStatus { get; }

        RuleValidationResult CanEnter(RuleContext<TEntity, TStatus> context);

        RuleValidationResult CanLeave(RuleContext<TEntity, TStatus> context);
    }
}