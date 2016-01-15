using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using core;

namespace LabTest
{
    [TestClass]
    public class ByteOperationTest
    {
        [TestMethod]
        public void ShiftLeft()
        {
            uint expected = 360;
            uint actual = ByteOperations.shiftLeft(180, 1, 16);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShiftRight()
        {
            uint expected = 90;
            uint actual = ByteOperations.shiftRight(180, 1, 16);

            Assert.AreEqual(expected, actual);
        }
    }
}
