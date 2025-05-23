﻿using System.Reflection;
using Mantis.Core.Common.Extensions.System;

namespace Mantis.Core.Common.Exceptions
{
    public class SequenceGroupException : Exception
    {
        public readonly Type Sequence;
        public readonly MemberInfo? Member;

        public SequenceGroupException(Type sequence) : base($"Attempted SequenceGroup on null")
        {
            this.Sequence = sequence;
        }

        public SequenceGroupException(Type sequence, MemberInfo member) : base($"Missing SequenceGroup '{sequence}' on member {GetMemberString(member)}")
        {
            this.Sequence = sequence;
            this.Member = member;
        }

        private static string GetMemberString(MemberInfo member)
        {
            if (member is MethodInfo methodInfo)
            {
                return $"{methodInfo.DeclaringType?.GetFormattedName()}::{methodInfo.Name}";
            }

            return member.ToString() ?? member.Name;
        }
    }
}