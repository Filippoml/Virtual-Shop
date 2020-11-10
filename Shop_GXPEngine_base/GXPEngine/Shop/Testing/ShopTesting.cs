using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Shop.Testing
{
    [TestFixture]
    class ShopTesting
    {
        [TestCase]
        public void Test()
        {
            Assert.AreEqual(10, 10);
        }

        [TestCase]
        public void Test2()
        {
            Assert.AreEqual(10, 101);
        }
    }
}
