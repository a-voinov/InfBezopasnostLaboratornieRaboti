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

using core.Euclid;

namespace WpfView.LabTabs
{
    /// <summary>
    /// Логика взаимодействия для NodTab.xaml
    /// </summary>
    public partial class NodTab : TabItem
    {
        public NodTab()
        {
            InitializeComponent();
        }

        private void aBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                euclidBox.Text = "";

                int a = Convert.ToInt32(aBox.Text);
                int n = Convert.ToInt32(nBox.Text);

                nodBox.Text = Euclid.NOD(a, n).ToString();

                try
                {
                    euclidBox.Text = Euclid.Extended(a, n).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void aBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkNumBox(aBox);
        }

        void checkNumBox(TextBox box)
        {
            int convertedText = 0;
            if (!int.TryParse(box.Text, out convertedText))
            {
                box.Clear();
            }

        }

        private void nBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkNumBox(nBox);
        }
    }
}
