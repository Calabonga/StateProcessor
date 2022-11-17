using System.Collections.Generic;
using System.Linq;

namespace Calabonga.StatusProcessor
{
    /// <summary>
    /// Rule Validation result for RuleProcessor
    /// </summary>
    public class RuleValidationResult {
        private readonly IList<string> _errors;

        public RuleValidationResult()
        {
            _errors= new List<string>();
        }

        public RuleValidationResult(IEnumerable<string> errors):this() {
            _errors = errors.ToList();
        }

        public bool IsOk {
            get { return Errors != null && !Errors.Any(); }
        }

        public IEnumerable<string> Errors {
            get { return _errors; }
        }

        public void AddError(string errorMessage) {
            _errors.Add(errorMessage);
        }
    }
}