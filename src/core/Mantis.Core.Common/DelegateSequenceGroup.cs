using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using Mantis.Core.Common.Extensions.System;
using Mantis.Core.Common.Extensions.System.Collections.Generic;
using Mantis.Core.Common.Extensions.System.Reflection;
using Mantis.Core.Common.Extensions.Utilities;
using Mantis.Core.Common.Utilities;

namespace Mantis.Core.Common
{
    public class DelegateSequenceGroup<TSequenceGroup, TDelegate>
        where TSequenceGroup : unmanaged, Enum
        where TDelegate : Delegate
    {
        private readonly Type _delegate;
        private readonly List<DynamicDelegate<TDelegate>> _orphans;
        private readonly HashSet<DynamicDelegate<TDelegate>> _all;
        private readonly Dictionary<SequenceGroup<TSequenceGroup>, TDelegate?> _grouped;

        public TDelegate? Sequenced { get; private set; }
        public readonly ReadOnlyDictionary<SequenceGroup<TSequenceGroup>, TDelegate?> Grouped;
        public readonly ReadOnlyCollection<DynamicDelegate<TDelegate>> Orphans;

        public readonly bool Sequence;

        public DelegateSequenceGroup(Type @delegate, bool sequence)
        {
            this._delegate = @delegate;
            this._grouped = [];
            this._all = [];
            this._orphans = [];

            this.Grouped = new ReadOnlyDictionary<SequenceGroup<TSequenceGroup>, TDelegate?>(this._grouped);
            this.Orphans = new ReadOnlyCollection<DynamicDelegate<TDelegate>>(this._orphans);
            this.Sequence = sequence;
        }

        public DelegateSequenceGroup(bool sequence) : this(typeof(TDelegate), sequence)
        {
            //
        }

        public void Add(IEnumerable<DynamicDelegate<TDelegate>> delegators)
        {
            if (this.Sequence == true)
            { // When the sequence groups are ordered we need to rebuild and resort the entire dictionary when an item is added
                if (this._all.AddRange(delegators) == 0)
                { // No new items added...
                    return;
                }

                // Sequence then sort all items
                this.Sequenced = this._all.OrderBySequenceGroup<TDelegate, TSequenceGroup>(this.Sequence).Combine();

                this._orphans.Clear();
                this._orphans.AddRange(this._all.Where(x => x.Method.HasSequenceGroup<TSequenceGroup>(x.Target) == false));

                this._grouped.Clear();
                var groups = this._all.Except(this._orphans).GroupBy(x => x.Method.GetSequenceGroup<TSequenceGroup>(x.Target));
                foreach (var group in groups)
                {
                    this._grouped.Add(group.Key, group.OrderBy(x => x.Method.GetSequence<TSequenceGroup>(x.Target)).Combine());
                }

                return;
            }

            bool modified = false;

            foreach (DynamicDelegate<TDelegate> delegator in delegators)
            {
                if (this._all.Add(delegator) == false)
                {
                    continue;
                }

                if (delegator.Method.TryGetSequenceGroup<TSequenceGroup>(delegator.Target, false, out var sequenceGroup) == false)
                {
                    this._orphans.Add(delegator);
                    continue;
                }

                if (delegator.Method.HasSequence<TSequenceGroup>(delegator.Target) == true)
                {
                    throw new ArgumentException($"{typeof(DelegateSequenceGroup<TSequenceGroup>).GetFormattedName()}::{nameof(Add)} - Method {delegator.Method.Name} should not be ordered.", nameof(delegators));
                }

                ref TDelegate? group = ref CollectionsMarshal.GetValueRefOrAddDefault(this._grouped, sequenceGroup, out bool exists);
                group = (TDelegate)Delegate.Combine(group, delegator.Delegate);

                modified = true;
            }

            if (modified == false)
            {
                return;
            }

            this.Sequenced = this._all.OrderBySequenceGroup<TDelegate, TSequenceGroup>(this.Sequence).Combine();
        }

        public void Remove(IEnumerable<DynamicDelegate<TDelegate>> delegators)
        {
            foreach (DynamicDelegate<TDelegate> delegator in delegators)
            {
                if (this._all.Remove(delegator) == false)
                {
                    continue;
                }

                if (this._orphans.Remove(delegator) == true)
                {
                    continue;
                }

                if (delegator.Method.TryGetSequenceGroup<TSequenceGroup>(delegator.Target, false, out var sequenceGroup) == true)
                {
                    ref TDelegate? group = ref CollectionsMarshal.GetValueRefOrAddDefault(this._grouped, sequenceGroup, out _);
                    group = (TDelegate?)Delegate.Remove(group, delegator.Delegate);
                }
            }
        }

        public void Add(IEnumerable<TDelegate> delegates)
        {
            IEnumerable<DynamicDelegate<TDelegate>> delegators = delegates.Select(x => new DynamicDelegate<TDelegate>(x));
            this.Add(delegators);
        }

        public void Remove(IEnumerable<TDelegate> delegates)
        {
            IEnumerable<DynamicDelegate<TDelegate>> delegators = delegates.Select(x => new DynamicDelegate<TDelegate>(x));
            this.Remove(delegators);
        }

        public void Add(IEnumerable<object> instances)
        {
            IEnumerable<DynamicDelegate<TDelegate>> delegators = instances.SelectMany(x => x.GetMatchingDelegators<TDelegate>(this._delegate));
            this.Add(delegators);
        }

        public void Remove(IEnumerable<object> instances)
        {
            IEnumerable<DynamicDelegate<TDelegate>> delegators = instances.SelectMany(x => x.GetMatchingDelegators<TDelegate>(this._delegate));
            this.Remove(delegators);
        }

        /// <summary>
        /// Sequence then invoke all delegators.
        /// </summary>
        /// <param name="delegators"></param>
        /// <param name="sequence">When true, items within each sequence group will be ordered via <see cref="Attributes.RequireSequenceGroupAttribute{TSequenceGroup}"/></param>
        /// <param name="args"></param>
        public static void Invoke(IEnumerable<DynamicDelegate<TDelegate>> delegators, bool sequence, object[] args)
        {
            foreach (DynamicDelegate<TDelegate> del in delegators.OrderBySequenceGroup<TDelegate, TSequenceGroup>(sequence))
            {
                del.Delegate.DynamicInvoke(args);
            }
        }

        /// <summary>
        /// Sequence then invoke all delegates.
        /// </summary>
        /// <param name="delegates"></param>
        /// <param name="sequence">When true, items within each sequence group will be ordered via <see cref="Attributes.RequireSequenceGroupAttribute{TSequenceGroup}"/></param>
        /// <param name="args"></param>
        public static void Invoke(IEnumerable<TDelegate> delegates, bool sequence, object[] args)
        {
            IEnumerable<DynamicDelegate<TDelegate>> delegators = delegates.Select(x => new DynamicDelegate<TDelegate>(x));
            DelegateSequenceGroup<TSequenceGroup, TDelegate>.Invoke(delegators, sequence, args);
        }

        /// <summary>
        /// Sequence then invoke all matching delegates within a collection of instances.
        /// </summary>
        /// <param name="instances"></param>
        /// <param name="sequence">When true, items within each sequence group will be ordered via <see cref="Attributes.RequireSequenceGroupAttribute{TSequenceGroup}"/></param>
        /// <param name="args"></param>
        public static void Invoke(IEnumerable<object> instances, bool sequence, object[] args)
        {
            IEnumerable<DynamicDelegate<TDelegate>> delegators = instances.SelectMany(x => x.GetMatchingDelegators<TDelegate>());
            DelegateSequenceGroup<TSequenceGroup, TDelegate>.Invoke(delegators, sequence, args);
        }
    }

    public abstract class DelegateSequenceGroup<TSequenceGroup>
        where TSequenceGroup : unmanaged, Enum
    {
        /// <summary>
        /// Sequence then invoke all delegates.
        /// </summary>
        /// <param name="delegates"></param>
        /// <param name="sequenced">When true, items within each sequence group will be ordered via <see cref="Attributes.RequireSequenceGroupAttribute{TSequenceGroup}"/></param>
        /// <param name="args"></param>
        public static void Invoke(IEnumerable<Delegate> delegates, bool sequenced, object[] args)
        {
            DelegateSequenceGroup<TSequenceGroup, Delegate>.Invoke(delegates, sequenced, args);
        }

        /// <summary>
        /// Sequence then invoke all matching delegates within a collection of instances.
        /// </summary>
        /// <param name="instances"></param>
        /// <param name="delegateType"></param>
        /// <param name="sequenced">When true, items within each sequence group will be ordered via <see cref="Attributes.RequireSequenceGroupAttribute{TSequenceGroup}"/></param>
        /// <param name="args"></param>
        public static void Invoke(IEnumerable<object> instances, Type delegateType, bool sequenced, object[] args)
        {
            IEnumerable<DynamicDelegate<Delegate>> delegators = instances.SelectMany(x => x.GetMatchingDelegators<Delegate>(delegateType));
            DelegateSequenceGroup<TSequenceGroup, Delegate>.Invoke(delegators, sequenced, args);
        }
    }
}