using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
                //код исходного текста
                byte[] srcBytes = ByteOperations.GetBytes(textBox.Text);
                printToBox(textBox3, srcBytes);

                //побитовое представление текста
                var bitsSrc = new BitArray(srcBytes);
                printBitArray(textBox4, bitsSrc);

                //шифр
                Kardano kardano = new Kardano(2);
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
                if (num == 0) continue;
                box.Text += num + " ";
            }
        }

        void printToBox(TextBox box, int[] array)
        {
            box.Clear();
            foreach (byte num in array)
            {
                if (num == 0) continue;
                box.Text += num + " ";
            }
        }



        private void textBox6_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
