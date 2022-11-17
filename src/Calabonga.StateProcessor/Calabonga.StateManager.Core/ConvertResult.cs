namespace Calabonga.StatusProcessor
{
    public class ConvertResult<T> {

        public T Result { get; }

        public ConvertResult(T result) {
            Result = result;
        }

        public bool Ok {
            get { return Result != null; }
        }
    }
}