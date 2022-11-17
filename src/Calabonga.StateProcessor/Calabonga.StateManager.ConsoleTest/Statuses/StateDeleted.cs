using System;
using Calabonga.StatusProcessor;

namespace Calabonga.StateManager.ConsoleTest.Statuses {

    public class StateDeleted : EntityStatus, IAccidentState {
        public static Guid Guid => Guid.Parse("49446068-0CCE-4168-B7AC-CD36E032F2BC");

        public static string StatusDisplayName => "Удалено";

        protected override string StatusName() {
            return AccidentStatus.Deleted.ToString();
        }

        public override string GetDisplayName() {
            return StatusDisplayName;
        }

        protected override Guid GetUniqueInentifier() {
            return Guid;
        }
    }
}