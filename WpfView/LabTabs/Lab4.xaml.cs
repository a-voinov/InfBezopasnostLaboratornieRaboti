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

using core.Rabin;

namespace WpfView.LabTabs
{
    /// <summary>
    /// Interaction logic for Lab4.xaml
    /// </summary>
    public partial class Lab4 : TabItem
    {
        private const int TESTS_COUNT = 100;

        public Lab4()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Rabin rabin = new Rabin();
            rabin.StartTests(TESTS_COUNT);
            primeBox.Text = rabin.PrimeNumber.ToString();

        }
    }
}
