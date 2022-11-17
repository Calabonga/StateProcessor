using System;
using System.ComponentModel.DataAnnotations;

namespace Calabonga.StatusProcessor {
    public abstract class EntityStatus : IEntityStatus {

        protected EntityStatus() {
            SetGuid();
            SetStatusName();
        }

        public string DisplayName
        {
            get
            {
                return GetDisplayName();
            }
        }

        public string Name { get; private set; }

        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Status name
        /// </summary>
        /// <returns></returns>
        protected abstract string StatusName();

        /// <summary>
        /// UI friendly status name
        /// </summary>
        /// <returns></returns>
        public virtual string GetDisplayName() {
            return GetType().Name;
        }

        private void SetStatusName() {
            Name = StatusName();
        }

        private void SetGuid() {
            Id = GetUniqueInentifier();

        }

        protected abstract Guid GetUniqueInentifier();
    }
}