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
    public class KeyTests
    {
        [Fact]
        public void KeysWithDifferentNames_AreNotEqual()
        {
            Key<object> idOne = Key<object>.GetByName("First");
            Key<object> idTwo = Key<object>.GetByName("Second");

            Assert.NotEqual(idOne, idTwo);
        }

        [Fact]
        public void KeysWithMatchingNames_AreEqual()
        {
            Key<object> idOne = Key<object>.GetByName("First");
            Key<object> idTwo = Key<object>.GetByName("First");

            Assert.Equal(idOne, idTwo);
        }

        [Fact]
        public void KeysWithMatchingNamesButDifferentTypes_AreNotEqual()
        {
            Key<object> idOne = Key<object>.GetByName("First");
            Key<int> idTwo = Key<int>.GetByName("First");

            Assert.False(idOne.Equals(idTwo));
        }
    }
}