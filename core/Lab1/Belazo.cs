using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Belazo
{
    public class Belazo
    {
        private const int BITS = 16;

        public uint key;

        public Belazo(uint _k)
        {
            key = _k;
        }

        public int[] Encrypt(string text)
        {
            //переводим строку в массив байтов
            byte[] textInBytes = ByteOperations.GetBytes(text);

            //массив с закодированными символами
            int[] codedText = new int[textInBytes.Length];

            //шифруем первый символ побитовым сложением
            codedText[0] = (int)key ^ textInBytes[0];

            int counter = 0;

            //ключем становится первый зашифрованный символ
            uint tempKey = (uint)codedText[0];

            //Для шифрования последующих символов исходного текста 
            foreach (byte byt in textInBytes)
            {
                counter++;

                if (counter == 1)
                {
                    continue;
                }

                int num = (int)textInBytes[counter - 1];

                //шифрование символа
                codedText[counter - 1] = (int)tempKey ^ num;

                //модифицирование ключа
                tempKey = (uint)codedText[counter - 1];
            }

            return codedText;
        }

        public string Decrypt(int[] encrypted)
        {
            //массив с закодированными символами
            int[] encodedText = new int[encrypted.Length];

            int counter = 0;

            //расщифровываем первый символ побитовым сложением
            encodedText[0] = (int)key ^ encrypted[0];

            int oldNum = encrypted[0];
            //Для расшифорвки последующих символов исходного текста 
            foreach (int num in encrypted)
            {
                counter++;

                if (counter == 1)
                {
                    continue;
                }

                //расшифровка символа
                encodedText[counter - 1] = num ^ oldNum;

                oldNum = num;
            }

            byte[] bytes = ByteOperations.GetBytes(encodedText);

            string byteString = ByteOperations.GetString(bytes);

            return byteString.Replace("\0", string.Empty);
        }
    }
}
