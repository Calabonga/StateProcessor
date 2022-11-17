using Calabonga.StatusProcessor;
using System;

namespace Calabonga.StatesProcessor.ConsoleTests.States
{
    public class StateTemporarily : EntityState, IAccidentState
    {

        public static Guid Guid => Guid.Parse("1F82F714-DDF3-412D-8535-A46E62AC299A");

        public static string StateDisplayName => "Временно";

        protected override string StateName()
        {
            return AccidentStateTypes.Temporarily.ToString();
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