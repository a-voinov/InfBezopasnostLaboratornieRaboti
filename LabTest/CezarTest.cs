using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using core.Cezar;

namespace LabTest
{
    [TestClass]
    public class CezarTest
    {
        public Cezar cezar = new Cezar("DIPLOMAT", 5, Alphabet.English);

        [TestMethod]
        public void CezarAlphabetTest()
        {
            string newAlphabet = "VWХYZDIPLOMATBCEFGHJKNQRSU";
            Assert.AreEqual(newAlphabet, cezar.alphabetString);
        }

        [TestMethod]
        public void TestCezar()
        {
            string tested = "SEND MORE MONEY";
            string expected = "HZBY TCGZ TCBZS";
            Assert.AreEqual(expected, cezar.Encrypt(tested));
            Assert.AreEqual(tested, cezar.Decrypt(expected));
        }
    }
}
