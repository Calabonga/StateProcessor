using System;
using Calabonga.StatusProcessor;

namespace Calabonga.StateManager.ConsoleTest.Statuses {

    public class StateFree : EntityStatus, IAccidentState {

        public static Guid Guid => Guid.Parse("FE9B619B-A034-4D73-92C4-FDFC92C1F277");

        public static string StatusDisplayName => "Свободно";

        protected override string StatusName() {
            return AccidentStatus.Free.ToString();
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
