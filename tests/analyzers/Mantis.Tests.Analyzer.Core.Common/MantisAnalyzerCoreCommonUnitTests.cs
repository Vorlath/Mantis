﻿using Mantis.Analyzer.Core.Common;
using Microsoft.CodeAnalysis;

//using Mantis.Core.Common.Attributes;
using VerifyCS = Mantis.Tests.Analyzer.Core.Common.CSharpCodeFixVerifier<
    Mantis.Analyzer.Core.Common.RequireSequenceGroupAnalyzer,
    Microsoft.CodeAnalysis.Testing.EmptyCodeFixProvider>;

namespace Mantis.Tests.Analyzer.Core.Common
{
    public class MantisAnalyzerCoreCommonUnitTest
    {
        public static readonly PortableExecutableReference MantisAssemblyLocation = MetadataReference.CreateFromFile("./../../../../../../src/core/Mantis.Core.Common/bin/Debug/net8.0/Mantis.Core.Common.dll");

        [Fact]
        public async Task RequireSequenceGroup_AttributeMissingOnInterfaceImplementation()
        {
            string test = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Mantis.Core.Common.Attributes;

namespace ConsoleApplication1
{
    enum TestSequenceGroup
    {
        Default
    }

    interface ITestInterface
    {
        [RequireSequenceGroup<TestSequenceGroup>]
        void SomeFunction();
    }

    class TestClass : ITestInterface
    {   
        public void SomeFunction()
        {
            throw new NotImplementedException();
        }
    }
}";

            var expected = VerifyCS.Diagnostic(RequireSequenceGroupAnalyzer.DiagnosticId).WithSpan(25, 21, 25, 33).WithArguments("SomeFunction", "TestClass", "ConsoleApplication1.TestSequenceGroup");
            await VerifyCS.VerifyCodeFixAsync(
                source: test,
                expected: [
                    expected
                ],
                fixedSource: null,
                additionalReferences: [
                    MantisAssemblyLocation,
                ]);
        }

        [Fact]
        public async Task RequireSequenceGroup_AttributeMissingOnAbstractImplementation()
        {
            string test = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Mantis.Core.Common.Attributes;

namespace ConsoleApplication1
{
    enum TestSequenceGroup
    {
        Default
    }

    interface ITestInterface
    {
        [RequireSequenceGroup<TestSequenceGroup>]
        void SomeFunction();
    }

    abstract class BaseTestClass : ITestInterface
    {
        public abstract void SomeFunction();
    }

    class TestClass : BaseTestClass
    {   
        public override void SomeFunction()
        {
            throw new NotImplementedException();
        }
    }
}";

            var expected = VerifyCS.Diagnostic(RequireSequenceGroupAnalyzer.DiagnosticId).WithSpan(30, 30, 30, 42).WithArguments("SomeFunction", "TestClass", "ConsoleApplication1.TestSequenceGroup");
            await VerifyCS.VerifyCodeFixAsync(
                source: test,
                expected: [
                    expected
                ],
                fixedSource: null,
                additionalReferences: [
                    MantisAssemblyLocation
                ]);
        }

        [Fact]
        public async Task RequireSequenceGroup_IRuntimeSequenceGroupImplementation()
        {
            string test = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Mantis.Core.Common.Attributes;
using Mantis.Core.Common.Interfaces;
using Mantis.Core.Common;

namespace ConsoleApplication1
{
    enum TestSequenceGroup
    {
        Default
    }

    interface ITestInterface
    {
        [RequireSequenceGroup<TestSequenceGroup>]
        void SomeFunction();
    }

    abstract class BaseTestClass : ITestInterface, IRuntimeSequenceGroup<TestSequenceGroup>
    {
        public SequenceGroup<TestSequenceGroup> Value { get; } = default!;

        public abstract void SomeFunction();
    }

    class TestClass : BaseTestClass
    {   
        public override void SomeFunction()
        {
            throw new NotImplementedException();
        }
    }
}";

            await VerifyCS.VerifyCodeFixAsync(
                source: test,
                expected: [],
                fixedSource: null,
                additionalReferences: [
                    MantisAssemblyLocation
                ]);
        }

        [Fact]
        public async Task RequireGenericSequenceGroup_AttributeMissingOnInterfaceImplementation()
        {
            string test = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Mantis.Core.Common.Attributes;

namespace ConsoleApplication1
{
    enum TestSequenceGroup
    {
        Default
    }

    interface ITestInterface<TSequenceGroup>
        where TSequenceGroup : unmanaged, Enum
    {
        [RequireGenericSequenceGroup(nameof(TSequenceGroup))]
        void SomeFunction();
    }

    class TestClass : ITestInterface<TestSequenceGroup>
    {   
        public void SomeFunction()
        {
            throw new NotImplementedException();
        }
    }
}";

            var expected = VerifyCS.Diagnostic(RequireSequenceGroupAnalyzer.DiagnosticId).WithSpan(26, 21, 26, 33).WithArguments("SomeFunction", "TestClass", "ConsoleApplication1.TestSequenceGroup");
            await VerifyCS.VerifyCodeFixAsync(
                source: test,
                expected: [
                    expected
                ],
                fixedSource: null,
                additionalReferences: [
                    MantisAssemblyLocation
                ]);
        }

        [Fact]
        public async Task RequireGenericSequenceGroup_AttributeMissingOnAbstractImplementation()
        {
            string test = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Mantis.Core.Common.Attributes;

namespace ConsoleApplication1
{
    enum TestSequenceGroup
    {
        Default
    }

    interface ITestInterface<TSequenceGroup>
        where TSequenceGroup : unmanaged, Enum
    {
        [RequireGenericSequenceGroup(nameof(TSequenceGroup))]
        void SomeFunction();
    }

    abstract class BaseTestClass : ITestInterface<TestSequenceGroup>
    {
        public abstract void SomeFunction();
    }

    class TestClass : BaseTestClass
    {   
        public override void SomeFunction()
        {
            throw new NotImplementedException();
        }
    }
}";

            var expected = VerifyCS.Diagnostic(RequireSequenceGroupAnalyzer.DiagnosticId).WithSpan(31, 30, 31, 42).WithArguments("SomeFunction", "TestClass", "ConsoleApplication1.TestSequenceGroup");
            await VerifyCS.VerifyCodeFixAsync(
                source: test,
                expected: [
                    expected
                ],
                fixedSource: null,
                additionalReferences: [
                    MantisAssemblyLocation
                ]);
        }
    }
}