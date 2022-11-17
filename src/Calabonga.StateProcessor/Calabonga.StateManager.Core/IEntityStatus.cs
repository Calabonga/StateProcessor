using System;

namespace Calabonga.StatusProcessor {
    /// <summary>
    /// Entity status (locked object)
    /// </summary>
    public interface IEntityStatus {

        string DisplayName { get; }

        string Name { get; }

        Guid Id { get; }
    }
}