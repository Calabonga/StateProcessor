using System;
using Calabonga.StatusProcessor;

namespace Calabonga.StateManager.ConsoleTest.Entities {

    public class Accident : IEntity {

        public int Id { get; set; }

        public Guid EntityActiveStatus { get; set; }
    }
}
