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


using System.Numerics;
using core.KP;

namespace WpfView.LabTabs
{
    /// <summary>
    /// Interaction logic for KP_Asym.xaml
    /// </summary>
    public partial class KP_Asym : TabItem
    {
        public KP_Asym()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DiffieHellman difHel = new DiffieHellman();

            pBox.Text = difHel.p.ToString();
            gBox.Text = difHel.g.ToString();
            xBox.Text = difHel.x.ToString();
            yBox.Text = difHel.y.ToString();
            kABox.Text = difHel.kA.ToString();
            kBBox.Text = difHel.kB.ToString();
            kSABox.Text = difHel.kSA.ToString();
            kSBBox.Text = difHel.kSB.ToString();
        }
    }
}
