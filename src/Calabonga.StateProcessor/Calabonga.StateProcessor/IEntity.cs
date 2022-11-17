using System;

namespace Calabonga.StatusProcessor {

    /// <summary>
    /// Entity under state observation
    /// </summary>
    public interface IEntity {

        Guid ActiveState { get; set; }
    }
}