using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using core;
using core.VTK;

namespace WpfView.LabTabs
{
    /// <summary>
    /// Interaction logic for LabTab2.xaml
    /// </summary>
    public partial class LabTab2 : TabItem
    {
        public LabTab2()
        {
            InitializeComponent();
        }

        string forbiddenChars = "!@#$%^&*()_+1234567890~/?|';:.<>,abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string smallChars = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                makeVTK();
            }
        }

        private void makeVTK()
        {
            try
            {
                VTK vtk = new VTK(keyLetterBox.Text[0]);

                codedTextBox.Text = vtk.Encrypt(textBox.Text);
                decodedTextBox.Text = vtk.Decrypt(codedTextBox.Text).ToString();
                entropyBox.Text = Entropy.Calculate(textBox.Text).ToString();              
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkTextBox(textBox);
        }

        private void keyLetterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (keyLetterBox.Text.Length > 1)
            {
                keyLetterBox.Clear();              
            }
            else
            checkTextBox(keyLetterBox);           
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
    }
}
