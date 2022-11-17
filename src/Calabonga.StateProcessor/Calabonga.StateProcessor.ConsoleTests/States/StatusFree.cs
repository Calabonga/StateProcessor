using System;
using Calabonga.StateProcessor;

namespace Calabonga.StatesProcessor.ConsoleTests.States {

    public class StateFree : EntityState, IAccidentState {

        public static Guid Guid => Guid.Parse("FE9B619B-A034-4D73-92C4-FDFC92C1F277");

        public static string StateDisplayName => "Свободно";

        protected override string StateName() {
            return AccidentStateTypes.Free.ToString();
        }

        public override string GetDisplayName()
        {
            return StateDisplayName;
        }

        protected override Guid GetUniqueIdentifier() {
            return Guid;
        }
    }
}
