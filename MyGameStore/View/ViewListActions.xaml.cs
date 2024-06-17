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
    /// Логика взаимодействия для ViewListActions.xaml
    /// </summary>
    public partial class ViewListActions : UserControl
    {

        public event EventHandler ButtonAccountClicked;
        public event EventHandler ButtonHomeClicked;
        public event EventHandler ButtonAllGamesClicked;
        public event EventHandler ButtonLibraryClicked;

        public ViewListActions()
        {
            InitializeComponent();
        }

        private void ButtonAccount_Click(object sender, RoutedEventArgs e)
        {
            ButtonAccountClicked?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            ButtonHomeClicked?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonAllGames_Click(object sender, RoutedEventArgs e)
        {
            ButtonAllGamesClicked?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonLibrary_Click(object sender, RoutedEventArgs e)
        {
            ButtonLibraryClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
