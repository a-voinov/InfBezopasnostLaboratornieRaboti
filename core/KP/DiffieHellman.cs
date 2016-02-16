using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Numerics;

namespace core.KP
{
    using core.Rabin;

    public class DiffieHellman
    {
        /// <summary>
        /// сильно простое число
        /// </summary>
        public int p { get; private set; }
        /// <summary>
        /// генератор поля
        /// </summary>
        public int g { get; private set; }
        /// <summary>
        /// секретный ключ пользователя А
        /// </summary>
        public int x { get; private set; }
        /// <summary>
        /// секретный ключ пользователя Б
        /// </summary>
        public int y { get; private set; }
        /// <summary>
        /// открытый ключ пользователя А
        /// </summary>
        public BigInteger kA { get; private set; }
        /// <summary>
        /// открытый ключ пользователя Б
        /// </summary>
        public BigInteger kB { get; private set; }
        /// <summary>
        /// сеансовый ключ пользователя А
        /// </summary>
        public BigInteger kSA { get; private set; }
        /// <summary>
        /// сеансовый ключ пользователя Б
        /// </summary>
        public BigInteger kSB { get; private set; }

        private const int TEST_COUNT = 100;
        private const int PRIME_ROOT_BORDER = 10;

        public DiffieHellman()
        {
            GetStrongPrime();
            CalculateGenerator();

            x = GetRandomNumber();
            Thread.Sleep(1000);
            y = GetRandomNumber();

            kA = BigInteger.ModPow(g, x, p);
            kB = BigInteger.ModPow(g, y, p);

            kSA = BigInteger.ModPow(g, y, p);
            kSA = BigInteger.ModPow(kSA, x, p);
            kSB = BigInteger.ModPow(g, x, p);
            kSB = BigInteger.ModPow(kSB, y, p);

        }

        /// <summary>
        /// Получение сильно простого числа
        /// </summary>
        private void GetStrongPrime()
        {
            Rabin generator = new Rabin();

            bool isPrimeStrong = false;
            do
            {
                generator.StartTests(TEST_COUNT);
                p = generator.PrimeNumber;
                isPrimeStrong = generator.StartTests((generator.PrimeNumber - 1) / 2, TEST_COUNT);

            } while (!isPrimeStrong);

        }

        private int GetRandomNumber()
        {
            Random generator = new Random();
            return generator.Next(p);

        }

        #region вычисление генератора поля

        /// <summary>
        /// Получение генератора
        /// </summary>
        private void CalculateGenerator()
        {
            

            for (int i = 2; i <= PRIME_ROOT_BORDER; i++)
            {
                bool breakLoop = true;
                g = i;
                List<BigInteger> array = new List<BigInteger>(p - 1);

                int counter = 0;
                for (int k = 1; k < p - 1; k++)
                {
                    counter++;
                    BigInteger rowElement = BigInteger.ModPow(g, k, p);

                    if (!CheckRowElement(rowElement))
                        breakLoop = false;
                    array.Add(rowElement);
                }

                if (breakLoop)
                    break;
            }
        }

        /// <summary>
        /// Проверка находится ли элемент последовательности в пределах от 1 до p-1
        /// </summary>
        private bool CheckRowElement(BigInteger _rowElement)
        {
            return (_rowElement > 1 && _rowElement < p) ? true : false; 
        }

        #endregion
    }
}
