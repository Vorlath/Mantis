using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Core.Common;
using Mantis.Tests.Core.Configurations;
using Mantis.Tests.Core.Fixtures;

namespace Mantis.Tests.Core
{
    public class IdTests
    {
        [Fact]
        public void IdsWithDifferentNames_AreNotEqual()
        {
            Id<object> idOne = Id<object>.GetByName("First");
            Id<object> idTwo = Id<object>.GetByName("Second");

            Assert.NotEqual(idOne, idTwo);
        }

        [Fact]
        public void IdsWithMatchingNames_AreEqual()
        {
            Id<object> idOne = Id<object>.GetByName("First");
            Id<object> idTwo = Id<object>.GetByName("First");

            Assert.Equal(idOne, idTwo);
        }

        [Fact]
        public void IdsWithMatchingNamesButDifferentTypes_AreNotEqual()
        {
            Id<object> idOne = Id<object>.GetByName("First");
            Id<int> idTwo = Id<int>.GetByName("First");

            Assert.False(idOne.Equals(idTwo));
        }
    }
}