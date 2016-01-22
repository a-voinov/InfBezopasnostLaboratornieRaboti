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

using core.Cezar;

namespace WpfView.LabTabs
{
    /// <summary>
    /// Interaction logic for Lab2.xaml
    /// </summary>
    public partial class Lab2 : TabItem
    {
        public Lab2()
        {
            InitializeComponent();
        }

        string forbiddenChars = "!@#$%^&*()_+1234567890~/?|';:.<>,";
        string smallChars = "abcdefghijklmnopqrstuvwxyz";

        void makeCezar()
        {
            if (textBox.Text != "" & keyBox.Text != "" && keyWordBox.Text != "")
            {
                try
                {
                    Cezar cezar = new Cezar(keyWordBox.Text, Convert.ToByte(keyBox.Text));
                    alphabetBox.Text = cezar.alphabetString;

                    codedTextBox.Text = cezar.Encrypt(textBox.Text);
                    decodedTextBox.Text = cezar.Decrypt(codedTextBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }               
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                makeCezar();
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkTextBox(textBox);
        }

        void checkTextBox(TextBox box)
        {
            if (box.Text.Intersect(forbiddenChars).Any())
            {
                box.Clear();
            }

            if (box.Text.Intersect(smallChars).Any())
            {
                box.Text = box.Text.ToUpper();
                box.SelectionStart = box.Text.Length;
                box.SelectionLength = 0;
            }
        }

        private void keyBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = keyBox.Text;
            int convertedText = 0;
            int.TryParse(text, out convertedText);

            if (convertedText > 0 && convertedText <= 25)
            {
                keyBox.Text = text;
            }
            else
            {
                keyBox.Text = "";
            }
        }

        private void keyWordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkTextBox(keyWordBox);
        }
    }
}
