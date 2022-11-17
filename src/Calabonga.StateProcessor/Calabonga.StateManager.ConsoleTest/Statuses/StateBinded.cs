using System;
using Calabonga.StatusProcessor;

namespace Calabonga.StateManager.ConsoleTest.Statuses {

    public class StateBinded : EntityStatus, IAccidentState {

        public static Guid Guid => Guid.Parse("2F45B0D0-1AA7-49CE-9F0D-D8D0485B15DD");

        public static string StatusDisplayName => "Закреплено";

        protected override string StatusName() {
            return AccidentStatus.Binded.ToString();
        }

        /// <summary>
        /// UI friendly status name
        /// </summary>
        /// <returns></returns>
        public override string GetDisplayName()
        {
            return StatusDisplayName;
        }

        protected override Guid GetUniqueInentifier() {
            return Guid;
        }
    }
}