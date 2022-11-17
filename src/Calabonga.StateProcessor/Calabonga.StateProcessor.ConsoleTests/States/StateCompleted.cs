using Calabonga.StatusProcessor;
using System;

namespace Calabonga.StatesProcessor.ConsoleTests.States
{

    public class StateCompleted : EntityState, IAccidentState
    {

        public static Guid Guid => Guid.Parse("CC84CAF9-4FFF-46C5-9E5A-B0C62926B61D");

        public static string StateDisplayName => "Оформлено";

        protected override string StateName()
        {
            return AccidentStateTypes.Completed.ToString();
        }

        public override string GetDisplayName()
        {
            return StateDisplayName;
        }

        protected override Guid GetUniqueIdentifier()
        {
            return Guid;
        }
    }
}