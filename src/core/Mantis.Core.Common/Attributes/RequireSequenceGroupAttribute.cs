﻿namespace Mantis.Core.Common.Attributes
{
    /// <summary>
    /// Indicates that the current type requires a defined sequence group.
    /// This will provider a compile time error if the Mantis source analyzer
    /// is installed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public abstract class RequireSequenceGroupAttribute : Attribute
    {
        public readonly Type SequenceGroupType;

        internal RequireSequenceGroupAttribute(Type sequenceGroupType)
        {
            this.SequenceGroupType = sequenceGroupType;
        }
    }

    /// <summary>
    /// Indicates that the current type requires a defined sequence group.
    /// This will provider a compile time error if the Mantis source analyzer
    /// is installed.
    /// </summary>
    public sealed class RequireSequenceGroupAttribute<TSequence> : RequireSequenceGroupAttribute
        where TSequence : unmanaged, Enum
    {
        public RequireSequenceGroupAttribute() : base(typeof(TSequence))
        {

        }
    }
}