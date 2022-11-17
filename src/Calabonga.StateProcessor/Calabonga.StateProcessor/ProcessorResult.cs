using System.Collections.Generic;

namespace Calabonga.StatusProcessor
{
    /// <summary>
    /// Represent operation result of the state changing
    /// </summary>
    public class ProcessorResult<TStatus>
    {

        /// <summary>
        /// Requested state for Entity
        /// </summary>
        public IState NewState { get; set; }

        /// <summary>
        /// Current state for Entity after changing operations complete
        /// </summary>
        public TStatus OldStatus { get; set; }

        /// <summary>
        /// Is operation completed
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Operation additional message
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }
}