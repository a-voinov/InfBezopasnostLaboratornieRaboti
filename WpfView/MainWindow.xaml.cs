using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections;

using core;
using core.Kardano;

namespace WpfView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const uint KARDANO_DEFAULT_KEY = 2;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && textBox.Text != "")
            {
                try
                {
                    makeKardano();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Ошибка в работе программы [" + ex.Message + "]. Попробуйте ввести английскими буквами и без пробелов", "Ошибка!");
                }                                            
            }
        }
       
        uint kardanoKey = KARDANO_DEFAULT_KEY;        

        void makeKardano()
        {
            //код исходного текста
            byte[] srcBytes = ByteOperations.GetBytes(textBox.Text);
            printToBox(textBox3, srcBytes);

            //побитовое представление текста
            var bitsSrc = new BitArray(srcBytes);
            printBitArray(textBox4, bitsSrc);

            //шифр
            Kardano kardano = new Kardano(kardanoKey);
            int[] encrypted = kardano.Encrypt(textBox.Text);
            printToBox(textBox1, encrypted);

            //зашифрованные символы
            byte[] encryptedString = ByteOperations.GetBytes(encrypted);
            textBox6.Clear();
            textBox6.Text = ByteOperations.GetString(encryptedString).Replace("\0", string.Empty);

            //побитовое представление шифра
            var bitsCrypted = new BitArray(encryptedString);
            printBitArray(textBox5, bitsCrypted);

            //обратный перевод
            textBox2.Clear();
            textBox2.Text = kardano.Decrypt(encrypted);
        }

        void printBitArray(TextBox box, BitArray bits)
        {
            StringBuilder sb = new StringBuilder();

            int counter = 0;
            int zerosCounter = 0;
            string temp = "";
            foreach (bool b in bits)
            {
                counter++;

                if (b)
                {
                    temp += "1";
                }
                else
                {
                    temp += "0";
                    zerosCounter++;
                }


                if (counter % 8 == 0)
                {
                    if (zerosCounter == 8)
                    {
                        temp = "";
                        zerosCounter = 0;
                        continue;
                    }

                    sb.Append(temp);
                    sb.Append(" ");
                    temp = "";
                    zerosCounter = 0;
                }
            }

            box.Text = sb.ToString();
        }

        void printToBox(TextBox box, byte[] array)
        {
            box.Clear();
            foreach (byte num in array)
            {
                if (num == 0 || num == 4) continue;
                box.Text += num + " ";
            }
        }

        void printToBox(TextBox box, int[] array)
        {
            box.Clear();
            foreach (byte num in array)
            {
                //if (num == 0) continue;
                box.Text += num + " ";
            }
        }



        private void textBox6_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = textBox7.Text;
            int convertedText = 0;
            int.TryParse(text, out convertedText);

            if (convertedText > 0 && convertedText <= 16)
            {
                textBox7.Text = text;
                kardanoKey = (uint)convertedText;
            }
            else
            {
                textBox7.Text = "";
            }
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBox7.Text == "")
            {
                MessageBox.Show("Введите ключ (1-16)");
                textBox7.Focus();
            }
        }
    }
}
