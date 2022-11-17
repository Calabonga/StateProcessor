using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Calabonga.StatusProcessor
{

    /// <summary>
    /// Rule Processor base implementation
    /// </summary>
    /// <typeparam name="TEntity">ProcessorEntity which have the statuses</typeparam>
    /// <typeparam name="TEntityStatus">Type of the statuses for entity</typeparam>
    public abstract class RuleProcessor<TEntity, TEntityStatus>
        where TEntity : IEntity, new()
        where TEntityStatus : IEntityStatus
    {

        protected RuleProcessor(IEnumerable<IStatusRule<TEntity, TEntityStatus>> rules, IEnumerable<TEntityStatus> statuses)
        {
            Rules = rules;
            Statuses = statuses;
        }

        public TEntity Entity { get; private set; }

        public IEntityStatus RequestedStatus { get; set; }

        public IEnumerable<IStatusRule<TEntity, TEntityStatus>> Rules { get; private set; }

        public IEnumerable<TEntityStatus> Statuses { get; private set; }

        public TEntity Create()
        {
            var status = InitialStatus();
            var entity = new TEntity();
            UpdateStatus(entity, status);
            return entity;
        }

        public ConvertResult<T> ToEnum<T>(string statusName)
        {
            var status = Statuses.First(x => x.Name.Contains(statusName));
            return ToEnum<T>(status);
        }

        public ConvertResult<T> ToEnum<T>(Guid guid)
        {
            var status = Statuses.First(x => x.Id.Equals(guid));
            return ToEnum<T>(status);
        }

        public ConvertResult<T> ToEnum<T>(IEntityStatus status)
        {
            if (typeof(T).IsEnum)
            {
                var result = Enum.Parse(typeof(T), status.Name);
                return new ConvertResult<T>((T)result);
            }
            return null;
        }

        /// <summary>
        /// Set up initial Entity EntityActiveStatus for future opertions
        /// </summary>
        /// <returns></returns>
        protected abstract Guid InitialStatus();

        /// <summary>
        /// Updates entity status to new status with checking rules
        /// </summary>
        /// <param name="entity">status from processor list of statuses</param>
        /// <param name="statusId"></param>
        /// <param name="payload">payload</param>
        public ProcessorResult<IEntityStatus> UpdateStatus(TEntity entity, Guid statusId, object payload = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (statusId == null || statusId == Guid.Empty) throw new ArgumentNullException(nameof(statusId));
            if (entity.EntityActiveStatus == null) throw new NullReferenceException(nameof(entity.EntityActiveStatus));
            Entity = entity;
            RequestedStatus = Statuses.SingleOrDefault(x=>x.Id== statusId);
            if (RequestedStatus == null)
            {
                throw new InvalidOperationException($"Requested Status {statusId} not found in status collection. Make sure status registered in dependentcy injection container.");
            }
            var guid = GetCurrentStatus(entity);
            var oldStatus = Statuses.FirstOrDefault(x => x.Id.Equals(guid));
            if (guid == statusId) return new ProcessorResult<IEntityStatus>
            {
                IsOk = false,
                OldStatus = oldStatus,
                NewStatus = RequestedStatus,
                Errors = new List<string> { "The requested status and current status are equals." }
            };
            var canLeaveCurrentStatus = LeaveFromCurrentStatus(guid, payload);
            var canEnterRequestedStatus = EnterToRequestedStatus(statusId, payload);
            if (canEnterRequestedStatus.IsOk && canLeaveCurrentStatus.IsOk)
            {
                entity.EntityActiveStatus = statusId;
                return new ProcessorResult<IEntityStatus>
                {
                    IsOk = true,
                    OldStatus = oldStatus,
                    NewStatus = RequestedStatus
                };
            }
            var list = new List<string>();
            list.AddRange(canEnterRequestedStatus.Errors.ToList());
            list.AddRange(canLeaveCurrentStatus.Errors.ToList());
            return new ProcessorResult<IEntityStatus>
            {
                IsOk = false,
                OldStatus = oldStatus,
                NewStatus = RequestedStatus,
                Errors = list
            };
        }

        private RuleValidationResult EnterToRequestedStatus(Guid requestedStatus, object payload)
        {
            var status = GetStatuseFromProcessor(requestedStatus);
            if (status == null) return new RuleValidationResult();
            var blocks = Rules.Where(x => x.EntityActiveStatus.Id.Equals(status.Id)).Select(rule => rule.CanEnter(new RuleContext<TEntity, TEntityStatus>(this, payload))).ToList();
            var isOk = !blocks.Select(x => x.IsOk).Contains(false);
            if (isOk)
            {
                return new RuleValidationResult();
            }
            return new RuleValidationResult(blocks.SelectMany(s => s.Errors));
        }

        private RuleValidationResult LeaveFromCurrentStatus(Guid currentStatus, object payload)
        {
            var status = GetStatuseFromProcessor(currentStatus);
            if (status == null) return new RuleValidationResult();

            var blocks = Rules
                .Where(x => x.EntityActiveStatus.Id.Equals(status.Id))
                .Select(rule => rule.CanLeave(new RuleContext<TEntity, TEntityStatus>(this, payload)))
                .ToList();
            var isOk = !blocks.Select(x => x.IsOk).Contains(false);
            if (isOk)
            {
                return new RuleValidationResult();
            }
            return new RuleValidationResult(blocks.SelectMany(s => s.Errors));
        }

        private IEntityStatus GetStatuseFromProcessor(Guid requestedStatus)
        {
            var status = Statuses.SingleOrDefault(x => x.Id == requestedStatus);
            return status;
        }

        private Guid GetCurrentStatus(TEntity entity)
        {
            return entity.EntityActiveStatus;
        }

    }
}