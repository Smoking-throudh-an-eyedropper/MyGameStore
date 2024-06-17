using MyGameStore.Entities;
using MySql.Data.MySqlClient;
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
    /// Логика взаимодействия для ViewGenresFilter.xaml
    /// </summary>
    public partial class ViewGenresFilter : Window
    {
        private string connectionString = "Server=localhost;Database=game_store;User=root;Password=qazmlpasd-270";
        public event EventHandler FlatButtonApplyClicked;
        public List<Genre> genres { get; set; } = new List<Genre>();

        public ViewGenresFilter()
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
            genres = GetListGenres();
            this.Close();
        }

        private void FlatButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private List<Genre> GetListGenres()
        {
            List<string> genreNames = GetGenres();
            List<Genre> genresList = new List<Genre>();

            string genreFilter = string.Join(",", genreNames.Select(g => $"'{g}'"));

            string sql = $@"SELECT idGenre, nameGenre FROM Genre WHERE nameGenre IN ({genreFilter});";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var genre = new Genre()
                                {
                                    idGenre = reader.GetInt32("idGenre"),
                                    nameGenre = reader.GetString("nameGenre")
                                };
                                genresList.Add(genre);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }

            return genresList;
        }

        private List<string> GetGenres()
        {
            List<string> SelectedGenres = new List<string>();

            SelectedGenres.Clear();

            if (ZCheckBoxRaces.IsChecked == true)
            {
                SelectedGenres.Add("Гонки");
            }
            if (ZCheckBoxAdventures.IsChecked == true)
            {
                SelectedGenres.Add("Приключения");
            }
            if (ZCheckBoxRolePlayingGames.IsChecked == true)
            {
                SelectedGenres.Add("Ролевые игры");
            }
            if (ZCheckBoxSimulator.IsChecked == true)
            {
                SelectedGenres.Add("Симулятор");
            }
            if (ZCheckBoxSport.IsChecked == true)
            {
                SelectedGenres.Add("Спорт");
            }
            if (ZCheckBoxStrategy.IsChecked == true)
            {
                SelectedGenres.Add("Стратегия");
            }
            if (ZCheckBoxShooter.IsChecked == true)
            {
                SelectedGenres.Add("Шутер");
            }
            if (ZCheckBoxAction.IsChecked == true)
            {
                SelectedGenres.Add("Экшен");
            }

            return SelectedGenres;
        }

        private void ZCheckBoxRaces_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxRaces.IsChecked == true)
            {
                UncheckOtherGenres("Гонки");
            }
        }

        private void ZCheckBoxAdventures_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxAdventures.IsChecked == true)
            {
                UncheckOtherGenres("Приключения");
            }
        }

        private void ZCheckBoxRolePlayingGames_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxRolePlayingGames.IsChecked == true)
            {
                UncheckOtherGenres("Ролевые игры");
            }
        }

        private void ZCheckBoxSimulator_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxSimulator.IsChecked == true)
            {
                UncheckOtherGenres("Симулятор");
            }
        }

        private void ZCheckBoxSport_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxSport.IsChecked == true)
            {
                UncheckOtherGenres("Спорт");
            }
        }

        private void ZCheckBoxStrategy_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxStrategy.IsChecked == true)
            {
                UncheckOtherGenres("Стратегия");
            }
        }

        private void ZCheckBoxShooter_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxShooter.IsChecked == true)
            {
                UncheckOtherGenres("Шутер");
            }
        }

        private void ZCheckBoxAction_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxAction.IsChecked == true)
            {
                UncheckOtherGenres("Экшен");
            }
        }

        private void UncheckOtherGenres(string genre)
        {
            if (genre != "Гонки") ZCheckBoxRaces.IsChecked = false;
            if (genre != "Приключения") ZCheckBoxAdventures.IsChecked = false;
            if (genre != "Ролевые игры") ZCheckBoxRolePlayingGames.IsChecked = false;
            if (genre != "Симулятор") ZCheckBoxSimulator.IsChecked = false;
            if (genre != "Спорт") ZCheckBoxSport.IsChecked = false;
            if (genre != "Стратегия") ZCheckBoxStrategy.IsChecked = false;
            if (genre != "Шутер") ZCheckBoxShooter.IsChecked = false;
            if (genre != "Экшен") ZCheckBoxAction.IsChecked = false;
        }
    }
}
