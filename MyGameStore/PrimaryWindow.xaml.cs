using MyGameStore.Entities;
using MyGameStore.View;
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
using System.Windows.Shapes;

namespace MyGameStore
{
    /// <summary>
    /// Логика взаимодействия для PrimaryWindow.xaml
    /// </summary>
    public partial class PrimaryWindow : Window
    {
        ViewAccount viewAccount;
        ViewHome viewHome;
        ViewAllGames viewAllGames;
        ViewListGames viewListGames;
        Account primaryAccount;

        public PrimaryWindow(Account account)
        {
            InitializeComponent();
            primaryAccount = account;
            ViewListActions.ButtonAccountClicked += ViewListActions_ButtonAccountClicked;
            ViewListActions.ButtonHomeClicked += ViewListActions_ButtonHomeClicked;
            ViewListActions.ButtonAllGamesClicked += ViewListActions_ButtonAllGamesClicked;
            ViewListActions.ButtonLibraryClicked += ViewListActions_ButtonLibraryClicked;
        }

        public void ViewListActions_ButtonAccountClicked(object sender, EventArgs e)
        {
            viewAccount = new ViewAccount(primaryAccount);
            Grid.InvalidateArrange();
            Grid.Children.Add(viewAccount);
        }

        public void ViewListActions_ButtonHomeClicked(object sender, EventArgs e)
        {
            viewHome = new ViewHome();
            Grid.InvalidateArrange();
            Grid.Children.Add(viewHome);
        }

        public void ViewListActions_ButtonAllGamesClicked(object sender, EventArgs e)
        {
            viewAllGames = new ViewAllGames(primaryAccount);
            Grid.InvalidateArrange();
            Grid.Children.Add(viewAllGames);

        }

        public void ViewListActions_ButtonLibraryClicked(object sender, EventArgs e)
        {
            viewListGames = new ViewListGames(primaryAccount, "accountGame");
            Grid.InvalidateArrange();
            Grid.Children.Add(viewListGames);

        }
    }
}
