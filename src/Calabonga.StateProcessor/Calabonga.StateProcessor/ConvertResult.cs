namespace Calabonga.StatesProcessor
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConvertResult<T> {

        /// <summary>
        /// Result of the conversation
        /// </summary>
        public T Result { get; }

        public ConvertResult(T result) {
            Result = result;
        }

        /// <summary>
        /// Indicate success status
        /// </summary>
        public bool Ok => Result != null;
    }
}