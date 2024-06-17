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
    /// Логика взаимодействия для ViewTagsFilter.xaml
    /// </summary>
    public partial class ViewTagsFilter : Window
    {
        private string connectionString = "Server=localhost;Database=game_store;User=root;Password=qazmlpasd-270";
        public event EventHandler FlatButtonApplyClicked;
        public List<Tage> tags { get; set; } = new List<Tage>();

        public ViewTagsFilter()
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
            tags = GetListTags();
            this.Close();
        }

        private void FlatButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private List<Tage> GetListTags()
        {
            List<string> tagsNames = GetTags();
            List<Tage> tagsList = new List<Tage>();

            string tageFilter = string.Join(",", tagsNames.Select(t => $"'{t}'"));

            string sql = $@"SELECT idTage, nameTage FROM Tage WHERE nameTage IN ({tageFilter});";

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
                                var tage = new Tage()
                                {
                                    idTage = reader.GetInt32("idLanguage"),
                                    nameTage = reader.GetString("nameLanguage")
                                };
                                tagsList.Add(tage);
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

            return tagsList;
        }

        private List<string> GetTags()
        {
            List<string> SelectedTag = new List<string>();

            SelectedTag.Clear();

            if (ZCheckBoxAdventure.IsChecked == true)
            {
                SelectedTag.Add("Приключение");
            }
            if (ZCheckBoxAction.IsChecked == true)
            {
                SelectedTag.Add("Экшен");
            }
            if (ZCheckBoxIndie.IsChecked == true)
            {
                SelectedTag.Add("Инди");
            }
            if (ZCheckBoxFantasy.IsChecked == true)
            {
                SelectedTag.Add("Фэнтези");
            }
            if (ZCheckBoxRichPlot.IsChecked == true)
            {
                SelectedTag.Add("Насыщенный сюжет");
            }
            if (ZCheckBoxAtmospheric.IsChecked == true)
            {
                SelectedTag.Add("Атмосферные");
            }
            if (ZCheckBox2D.IsChecked == true)
            {
                SelectedTag.Add("2D");
            }
            if (ZCheckBoxScienceFiction.IsChecked == true)
            {
                SelectedTag.Add("Научная фантастика");
            }

            return SelectedTag;
        }

        private void ZCheckBoxScienceFiction_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxScienceFiction.IsChecked == true)
            {
                UncheckOtherTags("Научная фантастика");
            }
        }

        private void ZCheckBox2D_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBox2D.IsChecked == true)
            {
                UncheckOtherTags("2D");
            }
        }

        private void ZCheckBoxAtmospheric_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxAtmospheric.IsChecked == true)
            {
                UncheckOtherTags("Атмосферные");
            }
        }

        private void ZCheckBoxRichPlot_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxRichPlot.IsChecked == true)
            {
                UncheckOtherTags("Насыщенный сюжет");
            }
        }

        private void ZCheckBoxFantasy_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxFantasy.IsChecked == true)
            {
                UncheckOtherTags("Фэнтези");
            }
        }

        private void ZCheckBoxIndie_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxIndie.IsChecked == true)
            {
                UncheckOtherTags("Инди");
            }
        }

        private void ZCheckBoxAction_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxAction.IsChecked == true)
            {
                UncheckOtherTags("Экшен");
            }
        }

        private void ZCheckBoxAdventure_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxAdventure.IsChecked == true)
            {
                UncheckOtherTags("Приключение");
            }
        }

        private void UncheckOtherTags(string genre)
        {
            if (genre != "Приключение") ZCheckBoxAdventure.IsChecked = false;
            if (genre != "Экшен") ZCheckBoxAction.IsChecked = false;
            if (genre != "Инди") ZCheckBoxIndie.IsChecked = false;
            if (genre != "Фэнтези") ZCheckBoxFantasy.IsChecked = false;
            if (genre != "Насыщенный сюжет") ZCheckBoxRichPlot.IsChecked = false;
            if (genre != "Атмосферные") ZCheckBoxAtmospheric.IsChecked = false;
            if (genre != "2D") ZCheckBox2D.IsChecked = false;
            if (genre != "Научная фантастика") ZCheckBoxScienceFiction.IsChecked = false;
        }
    }
}
