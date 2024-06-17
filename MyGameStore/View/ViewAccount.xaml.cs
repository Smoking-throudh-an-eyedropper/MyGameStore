using MyGameStore.Entities;
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
    /// Логика взаимодействия для ViewAccount.xaml
    /// </summary>
    public partial class ViewAccount : UserControl
    {
        string ImageAccount;
        Account account;

        public ViewAccount(Account account)
        {
            InitializeComponent();
            ImageAccount = account.imageAccount;
            TextBlockAccount.Text = account.nameAccount;
            this.account = account;
        }

        private void FlatButtonMyProfile_Click(object sender, RoutedEventArgs e)
        {
            ViewMyProfile viewMyProfile = new ViewMyProfile(account);
            viewMyProfile.Show();
        }

        private void FlatButtonConfidentiality_Click(object sender, RoutedEventArgs e)
        {
            ViewConfidentiality viewConfidentiality = new ViewConfidentiality(account);
            viewConfidentiality.Show();
        }
    }
}
