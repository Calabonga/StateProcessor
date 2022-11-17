using System;
using Calabonga.StatusProcessor;

namespace Calabonga.StateManager.ConsoleTest.Statuses
{
    public class StateTemporarily : EntityStatus, IAccidentState {

        public static Guid Guid => Guid.Parse("1F82F714-DDF3-412D-8535-A46E62AC299A");

        public static string StatusDisplayName => "Временно";

        protected override string StatusName() {
            return AccidentStatus.Temporarily.ToString();
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