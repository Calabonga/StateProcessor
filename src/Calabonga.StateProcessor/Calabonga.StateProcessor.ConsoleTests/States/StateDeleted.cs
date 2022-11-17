using Calabonga.StatusProcessor;
using System;

namespace Calabonga.StatesProcessor.ConsoleTests.States
{

    public class StateDeleted : EntityState, IAccidentState
    {

        public static Guid Guid => Guid.Parse("49446068-0CCE-4168-B7AC-CD36E032F2BC");

        public static string StateDisplayName => "Удалено";

        protected override string StateName()
        {
            return AccidentStateTypes.Deleted.ToString();
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