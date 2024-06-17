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
    /// Логика взаимодействия для ViewAllGames.xaml
    /// </summary>
    public partial class ViewAllGames : UserControl
    {
        ViewListFilters viewListFilters = new ViewListFilters();
        ViewListGames viewListGames { get; set; }

        public Account AccountForGames { get; set; }

        public ViewAllGames(Account account)
        {
            InitializeComponent();
            AccountForGames = account;
            GridColumnFilter.InvalidateArrange();
            GridColumnFilter.Children.Add(viewListFilters);
            viewListGames = new ViewListGames(AccountForGames, "filter");
            viewListGames.Price = viewListFilters.Price;
            viewListGames.Discount = viewListFilters.Discount;
            viewListGames.Genres = viewListFilters.Genres;
            viewListGames.Tags = viewListFilters.Tags;
            viewListGames.Language = viewListFilters.Language;
            viewListGames.MinimumDate = viewListFilters.MinimumDate;
            viewListGames.MaximumDate = viewListFilters.MaximumDate;
            GridColumnListGame.InvalidateArrange();
            GridColumnListGame.Children.Add(viewListGames);
        }
    }
}
