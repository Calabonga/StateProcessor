using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Calabonga.StatesProcessor
{
    /// <summary>
    /// Rule Processor base implementation
    /// </summary>
    /// <typeparam name="TEntity">ProcessorEntity which have the states</typeparam>
    /// <typeparam name="TState">Type of the states for entity</typeparam>
    public abstract class StateProcessor<TEntity, TState>
        where TEntity : IEntity, new()
        where TState : IState

    {
        protected StateProcessor(IEnumerable<IStateRule<TEntity, TState>> rules, IEnumerable<TState> states)
        {
            Rules = rules;
            States = states;
        }
        public TEntity Entity { get; private set; }

        public IState RequestedState { get; set; }

        public IEnumerable<IStateRule<TEntity, TState>> Rules { get; private set; }

        public IEnumerable<TState> States { get; private set; }

        public async Task<TEntity> CreateAsync()
        {
            var status = InitialStatus();
            var entity = new TEntity();
            await UpdateStatusAsync(entity, status);
            return entity;
        }

        public async Task<TEntity> CreateAsync(CancellationToken cancellationToken)
        {
            var status = InitialStatus();
            var entity = new TEntity();
            await UpdateStatusAsync(entity, status, cancellationToken);
            return entity;
        }

        public ConvertResult<T> ToEnum<T>(string statusName)
        {
            var status = States.First(x => x.Name.Contains(statusName));
            return ToEnum<T>(status);
        }

        public ConvertResult<T> ToEnum<T>(Guid guid)
        {
            var status = States.First(x => x.Id.Equals(guid));
            return ToEnum<T>(status);
        }

        public ConvertResult<T> ToEnum<T>(IState state)
        {
            if (typeof(T).IsEnum)
            {
                var result = Enum.Parse(typeof(T), state.Name);
                return new ConvertResult<T>((T)result);
            }
            return null;
        }

        /// <summary>
        /// Set up initial Entity ActiveState for future operations
        /// </summary>
        /// <returns></returns>
        protected abstract Guid InitialStatus();

        /// <summary>
        /// Updates entity state to new state with checking rules
        /// </summary>
        /// <param name="entity">state from processor list of states</param>
        /// <param name="statusId"></param>
        /// <param name="payload">payload</param>
        public async Task<ProcessorResult<IState>> UpdateStatusAsync(TEntity entity, Guid statusId, object payload = null)
        {
            CheckRulesExists();
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (statusId == null || statusId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(statusId));
            }

            if (entity.ActiveState == null)
            {
                throw new NullReferenceException(nameof(entity.ActiveState));
            }

            Entity = entity;

            RequestedState = States.SingleOrDefault(x => x.Id == statusId);
            if (RequestedState == null)
            {
                throw new InvalidOperationException($"Requested Status {statusId} not found in state collection. Make sure state registered in dependency injection container.");
            }

            ProcessorResult<IState> result;
            var guid = GetCurrentState(entity);
            var oldStatus = States.FirstOrDefault(x => x.Id.Equals(guid));
            if (guid == statusId)
            {
                result = new ProcessorResult<IState>
                {
                    Succeeded = false,
                    OldStatus = oldStatus,
                    NewState = RequestedState,
                    Errors = new List<string> { "The requested state and current state are equals." }
                };

                return result;
            }
            var canLeaveCurrentStatus = LeaveFromCurrentState(guid, payload);
            var canEnterRequestedStatus = await EnterToRequestedStateAsync(statusId, payload);
            if (canEnterRequestedStatus.IsOk && canLeaveCurrentStatus.IsOk)
            {
                entity.ActiveState = statusId;
                result = new ProcessorResult<IState>
                {
                    Succeeded = true,
                    OldStatus = oldStatus,
                    NewState = RequestedState
                };

                return result;
            }
            var list = new List<string>();
            list.AddRange(canEnterRequestedStatus.Errors.ToList());
            list.AddRange(canLeaveCurrentStatus.Errors.ToList());
            result = new ProcessorResult<IState>
            {
                Succeeded = false,
                OldStatus = oldStatus,
                NewState = RequestedState,
                Errors = list
            };

            return result;
        }

        private Task<RuleValidationResult> EnterToRequestedStateAsync(Guid requestedStatus, object payload)
        {
            var status = GetStatesFromProcessor(requestedStatus);
            if (status == null)
            {
                return Task.FromResult(new RuleValidationResult());
            }
            var rules = Rules.Where(x => x.ActiveState.Id.Equals(status.Id));
            var context = new RuleContext<TEntity, TState>(this, payload);
            var blocks = rules.Select(async rule => await rule.CanEnterAsync(context)).Select(x => x.Result).ToList();
            var isOk = !blocks.Select(x => x.IsOk).Contains(false);
            return isOk
                ? Task.FromResult(new RuleValidationResult())
                : Task.FromResult(new RuleValidationResult(blocks.SelectMany(s => s.Errors)));
        }

        private RuleValidationResult LeaveFromCurrentState(Guid currentStatus, object payload)
        {
            var status = GetStatesFromProcessor(currentStatus);
            if (status == null) return new RuleValidationResult();

            var blocks = Rules
            .Where(x => x.ActiveState.Id.Equals(status.Id))
            .Select(async rule => await rule.CanLeaveAsync(new RuleContext<TEntity, TState>(this, payload)))
            .Select(x=>x.Result)
            .ToList();

            var isOk = !blocks.Select(x => x.IsOk).Contains(false);
            return isOk
                ? new RuleValidationResult()
                : new RuleValidationResult(blocks.SelectMany(s => s.Errors));
        }

        private void CheckRulesExists()
        {
            if (Rules == null || !Rules.Any())
            {
                throw new InvalidOperationException($"{nameof(StateProcessor<TEntity, TState>)} does not contains any rules for processing");
            }
        }

        private IState GetStatesFromProcessor(Guid requestedStatus)
        {
            return States.SingleOrDefault(x => x.Id == requestedStatus);
        }

        private Guid GetCurrentState(TEntity entity)
        {
            return entity.ActiveState;
        }

        public abstract TState GetCurrentState();
    }
}