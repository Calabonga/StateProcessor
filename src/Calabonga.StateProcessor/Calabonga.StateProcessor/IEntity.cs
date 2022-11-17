using System;

namespace Calabonga.StatesProcessor {

    /// <summary>
    /// Entity under state observation
    /// </summary>
    public interface IEntity {

        Guid ActiveState { get; set; }
    }
}