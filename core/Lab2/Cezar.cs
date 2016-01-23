using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Cezar
{
    public enum Alphabet
    {
        English,
        Russian
    }

    public class Cezar
    {
        public const string ALPHABET_EN = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string ALPHABET_RU = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЧШЩЬЫЪЭЮЯ";

        public string keyWord { get; private set; }
        public byte key { get; private set; }

        private Alphabet language;

        private OrderedDictionary orderedAlphabet;
        private Dictionary<char, char> alphabet = new Dictionary<char, char>();
        public string alphabetString { get; private set; }

        public Cezar(string _keyWord, byte _key, Alphabet _language)
        {
            keyWord = _keyWord;
            key = _key;
            language = _language;

            CheckKeyWord();

            if (language == Alphabet.English)
            {
                FillOrderedAlphabet(ALPHABET_EN);
                alphabetToString();
                FillAlphabet(ALPHABET_EN);
            }

            if (language == Alphabet.Russian)
            {
                FillOrderedAlphabet(ALPHABET_RU);
                alphabetToString();
                FillAlphabet(ALPHABET_RU);
            }

        }

        private void CheckKeyWord()
        {
            keyWord = keyWord.Replace(" ", String.Empty);
            keyWord = new String(keyWord.ToCharArray().Distinct().ToArray());
        }

        private void alphabetToString()
        {
            foreach (char letter in orderedAlphabet.Values)
            {
                alphabetString += letter;
            }
        }

        private char[] GetAlphabetValues()
        {
            int k = 0;
            char[] values = new char[orderedAlphabet.Values.Count];
            foreach (var c in orderedAlphabet.Values)
            {
                values[k] = (char)c;
                k++;
            }

            return values;
        }

        public string Encrypt(string text)
        {
            string result = "";

            foreach (char letter in text)
            {
                if (letter == ' ') { result += ' '; continue; }
                result += alphabet[letter];                
            }

            return result;
        }

        public string Decrypt(string text)
        {
            string result = "";

            foreach (char letter in text)
            {
                if (letter == ' ') { result += ' '; continue; }
                result += alphabet.FirstOrDefault(x=> x.Value == letter).Key;
            }

            return result;
        }

        private void FillAlphabet(string _alphabet)
        {
            char[] values = GetAlphabetValues();
            //заполнить обычный словарь
            for (int i = 0; i < values.Length; i++)
            {
                alphabet.Add(_alphabet[i], values[i]);
            }
        }

        private void FillOrderedAlphabet(string _alphabet)
        {
            //создание словаря
            orderedAlphabet = new OrderedDictionary();

            char[] keys = _alphabet.ToCharArray();
            foreach (char key in keys)
            {
                orderedAlphabet.Add(key, ' ');
            }

            int cycleCounter = 0;
            int lastLetterPosition = 0;
            //ввод в словарь ключевого слова
            for (int i = 0; i < keyWord.Length; i++)
            {                
                if (i + key < _alphabet.Length)
                {
                    orderedAlphabet[i + key] = keyWord[i];
                }
                else //проверка выхода индекса за границу
                {                  
                    orderedAlphabet[cycleCounter] = keyWord[i];
                    cycleCounter++;
                }

                //запомнить индекс последнего сивола ключевого слова
                if (i == keyWord.Length - 1)
                {
                    if (cycleCounter == 0)
                    {
                        lastLetterPosition = i + key;
                    }
                    else
                        lastLetterPosition = cycleCounter - 1;
                }
            }

            int counter = lastLetterPosition + 1;
            char[] values = GetAlphabetValues();
            //заполнить алфавит шифрования
            for (int i = 0; i < orderedAlphabet.Count; i++)
            { 
                char letter = _alphabet[i];

                //исключение повторяющихся символов
                values = GetAlphabetValues();

                if (values.Contains(letter))
                {
                    continue;
                }

                if (counter == _alphabet.Length) counter = 0;

                orderedAlphabet[counter] = letter;
               
                counter++;
            }
        }

    }
}
