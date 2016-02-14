using System;
using System.Numerics;

namespace core.Rabin
{
    public enum Answer
    {
        Composite,
        ProbablyPrime
    }

    public class Rabin
    {

        public int PrimeNumber { get { return p; } }

        private int p;
        private int s;
        private int t;
        private int b;

        private BigInteger x0
        {
            get
            {
                return BigInteger.ModPow(b, s, p);
            }
        }

        private Answer answer
        {
            get
            {
                if (x0 == 1)
                {
                    return Answer.ProbablyPrime;
                }
                else
                {
                    return CreateZRing() ? Answer.ProbablyPrime : Answer.Composite;
                }
            }
        }

        private BigInteger[] x;
        private Random random = new Random();

        public void StartTests(int _testCount)
        {
            bool startOver = true;
            int counter = 0;

            do
            {
                if (startOver)
                {
                    GenerateRandomOddByte();
                    CalculateSAndT();
                    startOver = false;
                }

                GenerateB();

                if (CheckCompositeP())
                {
                    startOver = true;
                    counter = 0;
                    continue;
                }

                if (answer == Answer.ProbablyPrime)
                {
                    counter++;
                }
                else
                {
                    startOver = true;
                    counter = 0;
                }

            }
            while (counter != _testCount);
        }

        /// <summary>
        /// Генерация Z кольца
        /// </summary>
        /// <returns>
        /// возвращает false, если в последовательности элементов
        /// кольца Z встретилась единица
        ///</returns>
        private bool CreateZRing()
        {
            x = new BigInteger[t];

            x[0] = x0;

            for (int i = 1; i < t; i++)
            {
                x[i] = (x[i - 1] * x[i - 1]) % p;

                if (x[i] == 1)
                {
                    return false;
                }
            }

            return true;
        }

        private void GenerateRandomOddByte()
        {
            p = random.Next(2, Int16.MaxValue);

            bool isEven = p % 2 == 0;

            if (isEven)
            {
                p += 1;
            }
        }

        private void CalculateSAndT()
        {
            t = 0;
            s = p; 
            bool isOdd = false;

            do
            {
                s = (int)Math.Ceiling(((double)s - 1) / 2);
                isOdd = s % 2 == 0;

                t++;

            } while (isOdd);
        }

        private void GenerateB()
        {
            b = random.Next(1, p);
            b = b % p;
        }

        /// <summary>
        /// Проверка условия - является ли p составным числом
        /// </summary>
        private bool CheckCompositeP()
        {
            BigInteger xt = BigInteger.ModPow(b, p - 1, p) ;
            return xt != 1 ? true : false;
        }

    }
}
