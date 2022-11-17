using System;
using Calabonga.StatusProcessor;

namespace Calabonga.StateManager.ConsoleTest.Statuses {

    public class StateCompleted : EntityStatus, IAccidentState {

        public static Guid Guid => Guid.Parse("CC84CAF9-4FFF-46C5-9E5A-B0C62926B61D");

        public static string StatusDisplayName => "Оформлено";

        protected override string StatusName() {
            return AccidentStatus.Completed.ToString();
        }

        public override string GetDisplayName()
        {
            return StatusDisplayName;
        }

        protected override Guid GetUniqueInentifier() {
            return Guid;
        }
    }
}