using System;

namespace Calabonga.StatesProcessor {
    /// <summary>
    /// Entity state (locked object)
    /// </summary>
    public interface IState {

        string DisplayName { get; }

        string Name { get; }

        Guid Id { get; }
    }
}