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
        private int shiftCount;
        private int[] shiftResults;

        private const int VIEW_RES_STEP = 64;
        private int viewResFrom = 0;
        private int viewResTo = VIEW_RES_STEP;

        public Lab3()
        {
            InitializeComponent();
        }

        private void ChangeLabel()
        {
            numLabel.Content = "Сдвиг чисел [" + viewResFrom.ToString() + "; " + viewResTo.ToString() + "]";
        }

        private void ResultsStepForward()
        {
            if (viewResTo <= shiftCount)
            {
                viewResFrom += VIEW_RES_STEP;
                viewResTo += VIEW_RES_STEP;
            }
        }

        private void ResultsStepBack()
        {
            if (viewResFrom > 0)
            {
                viewResFrom -= VIEW_RES_STEP;
                viewResTo -= VIEW_RES_STEP;
            }
        }

        private void CalculateShift(int _num)
        {
            int shifted = LFSR16.Shift(_num);
         
            for (int i = 1; i <= shiftCount; i++)
            {
                shiftResults[i] = shifted;
                shifted = LFSR16.Shift(shifted);
            }

        }

        private void ViewResults()
        {
            ChangeLabel();
            numBox.Clear();

            for (int i = viewResFrom; i < viewResTo; i++)
            {
                numBox.Text += shiftResults[i].ToString() + " ";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            randomBox.Clear();

            Generate();

            viewResFrom = 0;
            viewResTo = VIEW_RES_STEP;
            ViewResults();

            ShowButtons();
        }

        private void Generate()
        {          
            int randomNum = LFSR16.Random16();
            shiftCount = LFSR16.GetFullCycleShiftCount(randomNum);
            shiftResults = new int[shiftCount + 1];
            randomBox.Text = randomNum.ToString();
            shiftResults[0] = randomNum;
            CalculateShift(randomNum);

        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {                      
            ResultsStepForward();
            ViewResults();

        }

        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {            
            ResultsStepBack();
            ViewResults();

        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            viewResFrom = 0;
            viewResTo = VIEW_RES_STEP;
            ViewResults();

        }

        private void FinishBtn_Click(object sender, RoutedEventArgs e)
        {
            viewResFrom = shiftCount - VIEW_RES_STEP;
            viewResTo = shiftCount + 1;
            ViewResults();

        }

        private void ShowButtons()
        {
            StartBtn.Visibility = Visibility.Visible;
            NextBtn.Visibility = Visibility.Visible;
            PrevBtn.Visibility = Visibility.Visible;
            FinishBtn.Visibility = Visibility.Visible;

        }
    }
}
