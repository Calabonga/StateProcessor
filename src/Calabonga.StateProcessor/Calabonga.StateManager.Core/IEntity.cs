using System;

namespace Calabonga.StatusProcessor {

    /// <summary>
    /// Entity under status observation
    /// </summary>
    public interface IEntity {

        Guid EntityActiveStatus { get; set; }
    }
}