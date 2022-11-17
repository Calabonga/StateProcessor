using System;
using Calabonga.StateProcessor;

namespace Calabonga.StatesProcessor.ConsoleTests.Entities
{
    /// <summary>
    /// Demo entity
    /// </summary>
    public class Accident : IEntity
    {
        public int Id { get; set; }

        public Guid ActiveState { get; set; }
        
        public string Name { get; set; }
    }
}
