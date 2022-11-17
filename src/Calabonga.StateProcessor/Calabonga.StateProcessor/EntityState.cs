using System;

namespace Calabonga.StatesProcessor {

    /// <summary>
    /// Entity State base class
    /// </summary>
    public abstract class EntityState : IState {

        protected EntityState() {
            SetGuid();
            SetStateName();
        }

        public string DisplayName
        {
            get
            {
                return GetDisplayName();
            }
        }

        public string Name { get; private set; }

        /// <summary>
        /// Identifier for state
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Status name
        /// </summary>
        /// <returns></returns>
        protected abstract string StateName();

        /// <summary>
        /// UI friendly state name
        /// </summary>
        /// <returns></returns>
        public virtual string GetDisplayName() {
            return GetType().Name;
        }

        private void SetStateName() {
            Name = StateName();
        }

        private void SetGuid() {
            Id = GetUniqueIdentifier();

        }

        protected abstract Guid GetUniqueIdentifier();
    }
}