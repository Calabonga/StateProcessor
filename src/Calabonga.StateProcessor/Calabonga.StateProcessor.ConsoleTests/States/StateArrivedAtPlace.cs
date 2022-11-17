using Calabonga.StatusProcessor;
using System;

namespace Calabonga.StatesProcessor.ConsoleTests.States
{

    public class StateArrivedAtPlace : EntityState, IAccidentState
    {

        public static Guid Guid => Guid.Parse("C66AB26B-22A5-496D-982C-520473CB6DA2");

        public static string StateDisplayName => "Прибыл на место";

        protected override string StateName()
        {
            return AccidentStateTypes.ArrivedAtPlace.ToString();
        }

        /// <summary>
        /// UI friendly status name
        /// </summary>
        /// <returns></returns>
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