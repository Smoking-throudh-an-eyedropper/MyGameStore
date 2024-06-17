using MyGameStore.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для ViewListFilters.xaml
    /// </summary>
    public partial class ViewListFilters : UserControl
    {
        public int Price = -1;
        public decimal Discount = -1;
        public List<Genre> Genres = null;
        public List<Tage> Tags = null;
        public DateTime MinimumDate = new DateTime(1980, 1, 1);
        public DateTime MaximumDate = DateTime.Now;
        public List<Language> Languages = null;
        public ViewPriceFilter priceFilter = new ViewPriceFilter();
        public ViewGenresFilter genresFilter = new ViewGenresFilter();
        public ViewTagsFilter tagsFilter = new ViewTagsFilter();
        public ViewDataOutputFilter dataOutputFilter = new ViewDataOutputFilter();
        public ViewLanguagesFilter languagesFilter = new ViewLanguagesFilter();

        public ViewListFilters()
        {
            InitializeComponent();

            priceFilter.FlatButtonApplyClicked += ViewPriceFilter_FlatButtonApplyClicked;
            genresFilter.FlatButtonApplyClicked += ViewGenresFilter_FlatButtonApplyClicked;
            tagsFilter.FlatButtonApplyClicked += ViewTagsFilter_FlatButtonApplyClicked;
            dataOutputFilter.FlatButtonApplyClicked += ViewDataOutputFilter_FlatButtonApplyClicked;
            languagesFilter.FlatButtonApplyClicked += ViewLanguagesFilter_FlatButtonApplyClicked;

        }

        private void ViewPriceFilter_FlatButtonApplyClicked(object sender, EventArgs e)
        {
            Price = priceFilter.price;
            Discount = priceFilter.discount;
        }

        private void ViewGenresFilter_FlatButtonApplyClicked(object sender, EventArgs e)
        {
            Genres = genresFilter.genres;
        }

        private void ViewTagsFilter_FlatButtonApplyClicked(object sender, EventArgs e)
        {
            Tags = tagsFilter.tags;
        }

        private void ViewDataOutputFilter_FlatButtonApplyClicked(object sender, EventArgs e)
        {
            MinimumDate = dataOutputFilter.StartDate;
            MaximumDate = dataOutputFilter.EndDate;
        }

        private void ViewLanguagesFilter_FlatButtonApplyClicked(object sender, EventArgs e)
        {
            Languages = languagesFilter.languages;
        }

        private void ButtonPrice_Click(object sender, RoutedEventArgs e)
        {
            priceFilter.Show();
        }

        private void ButtonGenre_Click(object sender, RoutedEventArgs e)
        {
            genresFilter.Show();
        }

        private void ButtonTage_Click(object sender, RoutedEventArgs e)
        {
            tagsFilter.Show();
        }

        private void ButtonReleaseDate_Click(object sender, RoutedEventArgs e)
        {
            dataOutputFilter.Show();
        }

        private void ButtonLanguage_Click(object sender, RoutedEventArgs e)
        {
            languagesFilter.Show();
        }
    }
}
