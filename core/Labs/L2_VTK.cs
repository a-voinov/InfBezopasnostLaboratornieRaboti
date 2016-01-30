using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.VTK
{
    public class VTK
    {
        public const string ALPHABET_RU = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        public char keyLetter { get; private set; }

        public VTK(char _keyLetter)
        {
            keyLetter = _keyLetter;
        }

        public string Encrypt(string text)
        {
            string result = "";
            char tempKeyLetter = keyLetter;
            foreach (char letter in text)
            {
                if (letter == ' ') { result += ' '; continue; }
                                             
                char coded = (char)(((letter + tempKeyLetter) % ALPHABET_RU.Length) + ALPHABET_RU[0]); 
                result += coded;
                tempKeyLetter = coded;
            }

            return result;
        }

        public string Decrypt(string text)
        {
            string result = "";
            char tempKeyLetter = keyLetter;
            int keyCounter = 0; 
            foreach (char letter in text)
            {
                if (letter == ' ') { result += ' '; continue; }

                if (keyCounter > 0)
                {                  
                    if (text[keyCounter - 1] == ' ')
                    {
                        tempKeyLetter = text[keyCounter];
                        keyCounter++;
                    }
                    else
                        tempKeyLetter = text[keyCounter - 1]; 
                }
                char coded = (char)(mod(letter - tempKeyLetter, ALPHABET_RU.Length) + ALPHABET_RU[0]);
                result += coded;
                keyCounter++;
            }

            return result;
        }

        int mod(int x, int m)
        {
            return (x % m + m) % m;
        }

    }
}