namespace Calabonga.StatusProcessor
{
    /// <summary>
    /// Rule validation context for RuleProcessor
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TState"></typeparam>
    public class RuleContext<TEntity, TState>
        where TEntity : IEntity, new() 
        where TState : IState
    // /where TState : IState

    {
        public RuleContext(StateProcessor<TEntity, TState> processor, object payload = null)
        {
            Processor = processor;
            RuleData = payload;
        }




        /// <summary>
        /// Current rule processor
        /// </summary>
        public StateProcessor<TEntity, TState> Processor { get; }

        /// <summary>
        /// Payload object
        /// </summary>
        public object RuleData { get; }
    }
}