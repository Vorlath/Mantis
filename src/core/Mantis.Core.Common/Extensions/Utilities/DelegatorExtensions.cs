﻿using System.Diagnostics;
using Mantis.Core.Common.Extensions.System.Reflection;
using Mantis.Core.Common.Utilities;

namespace Mantis.Core.Common.Extensions.Utilities
{
    public static class DelegatorExtensions
    {
        /// <summary>
        /// Sequence a collection of delegators
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <typeparam name="TSequenceGroup"></typeparam>
        /// <param name="delegators"></param>
        /// <param name="sequenced">When true, the items within each sequence group will be ordered by their sequence. When false, it is assumed the order of items within each sequence group is irrelevant.</param>
        /// <returns></returns>
        /// <exception cref="UnreachableException"></exception>
        public static IEnumerable<DynamicDelegate<TDelegate>> OrderBySequenceGroup<TDelegate, TSequenceGroup>(
            this IEnumerable<DynamicDelegate<TDelegate>> delegators,
            bool sequenced
        )
            where TDelegate : Delegate
            where TSequenceGroup : unmanaged, Enum
        {
            IOrderedEnumerable<DynamicDelegate<TDelegate>> result = delegators.Where(x => x.Method.HasSequenceGroup<TSequenceGroup>(x.Target)).OrderBy(x =>
            {
                if (x.Method.TryGetSequenceGroup<TSequenceGroup>(x.Target, false, out var sequenceGroup))
                {
                    return sequenceGroup;
                }

                throw new UnreachableException();
            });

            if (sequenced == true)
            {
                result = result.ThenBy(x => x.Method.GetSequence<TSequenceGroup>(x.Target));
            }

            return result;
        }

        public static TDelegate? Combine<TDelegate>(this IEnumerable<DynamicDelegate<TDelegate>> delegators)
            where TDelegate : Delegate
        {
            TDelegate? result = null;

            foreach (var delegator in delegators)
            {
                result = (TDelegate)Delegate.Combine(result, delegator.Delegate);
            }

            return result;
        }
    }
}