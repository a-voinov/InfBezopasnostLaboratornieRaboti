using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using core.Kardano;

namespace LabTest
{
    [TestClass]
    public class KardanoTest
    {
        public Kardano kardano = new Kardano(2);

        [TestMethod]
        public void KardanoEncrypt()
        {
            int[] encrypted;
            int[] expected = new int[] { 99, 102, 107 };
            encrypted = kardano.Encrypt("abc");
            
            CollectionAssert.AreEqual(expected, encrypted);
        }

        [TestMethod]
        public void KardanoDecrypt()
        {
            int[] encrypted = new int[] { 99, 102, 107 };
            string decrypted = kardano.Decrypt(encrypted);

            Assert.AreEqual("abc", decrypted);
        }
    }
}
