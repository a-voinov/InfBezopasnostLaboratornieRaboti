using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Kardano
{
    public class Kardano
    {
        private const int BITS = 16;

        public uint key;
        
        public Kardano(uint _k)
        {
            //key = ByteOperations.GetBytes(_k);
            key = _k;
        }

        /// <summary>
        /// Шифрование Кардано
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int[] Encrypt(string text)
        {
            //переводим строку в массив байтов
            byte[] textInBytes = ByteOperations.GetBytes(text);

            //массив с закодированными символами
            int[] codedText = new int[textInBytes.Length];

            //шифруем первый символ побитовым сложением
            codedText[0] = (int)key ^ textInBytes[0];

            int counter = 0;

            uint tempKey = key;
            //Для шифрования последующих символов исходного текста 
            foreach (byte byt in textInBytes)
            {
                counter++;

                if (byt == 0 || byt == 4 || counter == 1)
                {
                    continue;
                }

                //модифицирование ключа
                try
                {
                    tempKey = ByteOperations.shiftLeft(tempKey, 1, BITS);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                int num = (int)textInBytes[counter - 1];

                //шифрование символа
                codedText[counter - 1] = (int)tempKey ^ num;

                //если 0
                if (codedText[counter - 1] == 0) codedText[counter - 1] = -1;
            }                   

            return codedText;
        }

        public string Decrypt(int[] encrypted)
        {
            //массив с закодированными символами
            int[] encodedText = new int[encrypted.Length];

            int counter = 0;

            uint tempKey = key;
            //Для расшифорвки последующих символов исходного текста 
            foreach (int num in encrypted)
            {
                if (num == 0)
                {
                    continue;
                }

                //расшифровка символа
                encodedText[counter] = (int)tempKey ^ num;

                //модифицирование ключа
                try
                {
                    tempKey = ByteOperations.shiftLeft(tempKey, 1, BITS);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
              

                counter++;
            }

            byte[] bytes = ByteOperations.GetBytes(encodedText);

            string byteString = ByteOperations.GetString(bytes);

            return byteString.Replace("\0", string.Empty);
        }

    }
}
