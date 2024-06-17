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
    /// Логика взаимодействия для ViewDataOutputFilter.xaml
    /// </summary>
    public partial class ViewDataOutputFilter : Window
    {
        public DateTime StartDate;
        public DateTime EndDate;
        public event EventHandler FlatButtonApplyClicked;

        public ViewDataOutputFilter()
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
            GetDates();
            this.Close();
        }

        private void FlatButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void GetDates()
        {
            int intStartDate = Convert.ToInt32(ZComboBoxStartDate.Text);
            StartDate = new DateTime(intStartDate, 1, 1);

            if (ZComboBoxEndDate.Text == "2024")
            {
                EndDate = DateTime.Now;
            }
            else
            {
                int intEndDate = Convert.ToInt32(ZComboBoxEndDate.Text);
                EndDate = new DateTime(intEndDate, 12, 31);   
            }
        }
    }
}
