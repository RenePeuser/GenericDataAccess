using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericDataAccess.Test
{
    [TestClass]
    public class UnitTest1 : SystemTestBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(Output, "Expected output");
        }

        protected override IEnumerable<string> CollectConsoleParameters()
        {
            yield return "Param1";
        }
    }
}
