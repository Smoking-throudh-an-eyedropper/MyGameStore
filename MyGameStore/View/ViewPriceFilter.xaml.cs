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

namespace MyGameStore.View
{
    /// <summary>
    /// Логика взаимодействия для ViewPriceFilter.xaml
    /// </summary>
    public partial class ViewPriceFilter : Window
    {
        public int price;
        public decimal discount;
        public event EventHandler FlatButtonApplyClicked;

        public ViewPriceFilter()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void FlatButtonApply_Click(object sender, RoutedEventArgs e)
        {
            FlatButtonApplyClicked?.Invoke(this, EventArgs.Empty);
            GetPrice();
            this.Close();
        }

        private void FlatButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void GetPrice() 
        {
            if (ZCheckBoxFree.IsChecked == true)
            {
                price = 0;
            }
            if (ZCheckBoxUpTo100.IsChecked == true)
            {
                price = 100;
            }
            if (ZCheckBoxUpTo250.IsChecked == true)
            {
                price = 250;
            }
            if (ZCheckBoxUpTo500.IsChecked == true)
            {
                price = 500;
            }
            if (ZCheckBoxUpTo1000.IsChecked == true)
            {
                price = 1000;
            }
            if (ZCheckBoxUpTo2500.IsChecked == true)
            {
                price = 2500;
            }
            if (ZCheckBoxMoreThan2500.IsChecked == true)
            {
                price = -1;
            }
            if (ZCheckBoxOnlyWithDiscount.IsChecked == true)
            {
                discount = 0;
            }
        }

        private void ZCheckBoxFree_Checked(object sender, RoutedEventArgs e)
        {
            if(ZCheckBoxFree.IsChecked == true)
            {
                ZCheckBoxUpTo100.IsChecked = false;
                ZCheckBoxUpTo250.IsChecked = false;
                ZCheckBoxUpTo500.IsChecked = false;
                ZCheckBoxUpTo1000.IsChecked = false;
                ZCheckBoxUpTo2500.IsChecked = false;
                ZCheckBoxMoreThan2500.IsChecked = false;
                ZCheckBoxOnlyWithDiscount.IsChecked = false;
            }
        }

        private void ZCheckBoxUpTo100_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxUpTo100.IsChecked == true)
            {
                ZCheckBoxFree.IsChecked = false;
                ZCheckBoxUpTo250.IsChecked = false;
                ZCheckBoxUpTo500.IsChecked = false;
                ZCheckBoxUpTo1000.IsChecked = false;
                ZCheckBoxUpTo2500.IsChecked = false;
                ZCheckBoxMoreThan2500.IsChecked = false;
                ZCheckBoxOnlyWithDiscount.IsChecked = false;
            }
        }

        private void ZCheckBoxUpTo250_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxUpTo250.IsChecked == true)
            {
                ZCheckBoxFree.IsChecked = false;
                ZCheckBoxUpTo100.IsChecked = false;
                ZCheckBoxUpTo500.IsChecked = false;
                ZCheckBoxUpTo1000.IsChecked = false;
                ZCheckBoxUpTo2500.IsChecked = false;
                ZCheckBoxMoreThan2500.IsChecked = false;
                ZCheckBoxOnlyWithDiscount.IsChecked = false;
            }
        }

        private void ZCheckBoxUpTo500_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxUpTo500.IsChecked == true)
            {
                ZCheckBoxFree.IsChecked = false;
                ZCheckBoxUpTo100.IsChecked = false;
                ZCheckBoxUpTo250.IsChecked = false;
                ZCheckBoxUpTo1000.IsChecked = false;
                ZCheckBoxUpTo2500.IsChecked = false;
                ZCheckBoxMoreThan2500.IsChecked = false;
                ZCheckBoxOnlyWithDiscount.IsChecked = false;
            }
        }

        private void ZCheckBoxUpTo1000_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxUpTo1000.IsChecked == true)
            {
                ZCheckBoxFree.IsChecked = false;
                ZCheckBoxUpTo100.IsChecked = false;
                ZCheckBoxUpTo250.IsChecked = false;
                ZCheckBoxUpTo500.IsChecked = false;
                ZCheckBoxUpTo2500.IsChecked = false;
                ZCheckBoxMoreThan2500.IsChecked = false;
                ZCheckBoxOnlyWithDiscount.IsChecked = false;
            }
        }

        private void ZCheckBoxUpTo2500_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxUpTo2500.IsChecked == true)
            {
                ZCheckBoxFree.IsChecked = false;
                ZCheckBoxUpTo100.IsChecked = false;
                ZCheckBoxUpTo250.IsChecked = false;
                ZCheckBoxUpTo500.IsChecked = false;
                ZCheckBoxUpTo1000.IsChecked = false;
                ZCheckBoxMoreThan2500.IsChecked = false;
                ZCheckBoxOnlyWithDiscount.IsChecked = false;
            }
        }

        private void ZCheckBoxMoreThan2500_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxMoreThan2500.IsChecked == true)
            {
                ZCheckBoxFree.IsChecked = false;
                ZCheckBoxUpTo100.IsChecked = false;
                ZCheckBoxUpTo250.IsChecked = false;
                ZCheckBoxUpTo500.IsChecked = false;
                ZCheckBoxUpTo1000.IsChecked = false;
                ZCheckBoxUpTo2500.IsChecked = false;
                ZCheckBoxOnlyWithDiscount.IsChecked = false;
            }
        }

        private void ZCheckBoxOnlyWithDiscount_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxOnlyWithDiscount.IsChecked == true)
            {
                ZCheckBoxFree.IsChecked = false;
                ZCheckBoxUpTo100.IsChecked = false;
                ZCheckBoxUpTo250.IsChecked = false;
                ZCheckBoxUpTo500.IsChecked = false;
                ZCheckBoxUpTo1000.IsChecked = false;
                ZCheckBoxUpTo2500.IsChecked = false;
                ZCheckBoxMoreThan2500.IsChecked = false;
            }
        }
    }
}
