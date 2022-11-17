using System.Collections.Generic;

namespace Calabonga.StatusProcessor {
    /// <summary>
    /// Represent operation result of the status changing
    /// </summary>
    public class ProcessorResult<TStatus> {

        /// <summary>
        /// Requested status for Entity
        /// </summary>
        public IEntityStatus NewStatus { get; set; }

        /// <summary>
        /// Current status for Entity after changing operations complete
        /// </summary>
        public TStatus OldStatus { get; set; }

        /// <summary>
        /// Is operation completed
        /// </summary>
        public bool IsOk { get; set; }

        /// <summary>
        /// Operation additional message
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }
}