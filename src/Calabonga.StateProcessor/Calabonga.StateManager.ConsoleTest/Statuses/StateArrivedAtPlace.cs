using System;
using Calabonga.StatusProcessor;

namespace Calabonga.StateManager.ConsoleTest.Statuses {

    public class StateArrivedAtPlace : EntityStatus, IAccidentState {

        public static Guid Guid => Guid.Parse("C66AB26B-22A5-496D-982C-520473CB6DA2");

        public static string StatusDisplayName => "Прибыл на место";

        protected override string StatusName() {
            return AccidentStatus.ArrivedAtPlace.ToString();
        }

        /// <summary>
        /// UI friendly status name
        /// </summary>
        /// <returns></returns>
        public override string GetDisplayName() {
            return StatusDisplayName;
        }

        protected override Guid GetUniqueInentifier() {
            return Guid;
        }
    }
}