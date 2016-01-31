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

using core.LFSR;

namespace WpfView.LabTabs
{
    /// <summary>
    /// Interaction logic for Lab3.xaml
    /// </summary>
    public partial class Lab3 : TabItem
    {
        public Lab3()
        {
            InitializeComponent();
        }

        const int SHIFT_COUNT = 256;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            randomBox.Clear();
            bitBox.Clear();
            numBox.Clear();

            string random = LFSR.Random16();
            randomBox.Text = LFSR.getIntFrom16BitString(random).ToString();

            LFSR lsfr = new LFSR(random);

            for (int i = 0; i < SHIFT_COUNT; i++)
            {
                bitBox.Text += lsfr.RegistryReverse.ToString() + " ";
                numBox.Text += LFSR.getIntFrom16BitString(lsfr.Registry.ToString()) + " ";
                lsfr.Shift();
            }
        }
    }
}
