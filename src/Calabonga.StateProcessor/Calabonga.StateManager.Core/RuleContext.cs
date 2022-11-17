namespace Calabonga.StatusProcessor {

    /// <summary>
    /// Rule validation context for RuleProcessor
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TStatus"></typeparam>
    public class RuleContext<TEntity, TStatus> where TEntity : IEntity, new() where TStatus : IEntityStatus
    {

        public RuleContext(RuleProcessor<TEntity, TStatus> processor, object payload = null)
        {
            Processor = processor;
            RuleData = payload;
        }

        /// <summary>
        /// Current rule processor
        /// </summary>
        public RuleProcessor<TEntity, TStatus> Processor { get; set; }

        /// <summary>
        /// Payload object
        /// </summary>
        public object RuleData { get; set; }
    }
}