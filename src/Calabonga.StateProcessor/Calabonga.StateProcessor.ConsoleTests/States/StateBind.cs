using System;
using Calabonga.StateProcessor;

namespace Calabonga.StatesProcessor.ConsoleTests.States {

    public class StateBind : EntityState, IAccidentState {

        public static Guid Guid => Guid.Parse("2F45B0D0-1AA7-49CE-9F0D-D8D0485B15DD");

        public static string StateDisplayName => "Закреплено";

        protected override string StateName() {
            return AccidentStateTypes.Binded.ToString();
        }

        /// <summary>
        /// UI friendly status name
        /// </summary>
        /// <returns></returns>
        public override string GetDisplayName()
        {
            return StateDisplayName;
        }

        protected override Guid GetUniqueIdentifier() {
            return Guid;
        }
    }
}