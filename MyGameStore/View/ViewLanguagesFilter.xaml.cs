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
    /// Логика взаимодействия для ViewLanguagesFilter.xaml
    /// </summary>
    public partial class ViewLanguagesFilter : Window
    {
        private string connectionString = "Server=localhost;Database=game_store;User=root;Password=qazmlpasd-270";
        public event EventHandler FlatButtonApplyClicked;
        public List<Language> languages { get; set; } = new List<Language>();

        public ViewLanguagesFilter()
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
            languages = GetListLanguages();
            this.Close();
        }

        private void FlatButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private List<Language> GetListLanguages()
        {
            List<string> languagesNames = GetLanguages();
            List<Language> languagesList = new List<Language>();

            string languageFilter = string.Join(",", languagesNames.Select(l => $"'{l}'"));

            string sql = $@"SELECT idLanguage, nameLanguage FROM Language WHERE nameLanguage IN ({languageFilter});";

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
                                var language = new Language()
                                {
                                    idLanguage = reader.GetInt32("idLanguage"),
                                    nameLanguage = reader.GetString("nameLanguage")
                                };
                                languagesList.Add(language);
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

            return languagesList;
        }

        private List<string> GetLanguages()
        {
            List<string> SelectedLanguage = new List<string>();

            SelectedLanguage.Clear();

            if (ZCheckBoxRussian.IsChecked == true)
            {
                SelectedLanguage.Add("Русский");
            }
            if (ZCheckBoxEnglish.IsChecked == true)
            {
                SelectedLanguage.Add("Английский");
            }
            if (ZCheckBoxChinese.IsChecked == true)
            {
                SelectedLanguage.Add("Китайский");
            }
            if (ZCheckBoxJapanese.IsChecked == true)
            {
                SelectedLanguage.Add("Японский");
            }

            return SelectedLanguage;
        }

        private void ZCheckBoxRussian_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxRussian.IsChecked == true)
            {
                UncheckOtherLanguages("Русский");
            }
        }

        private void ZCheckBoxEnglish_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxEnglish.IsChecked == true)
            {
                UncheckOtherLanguages("Английский");
            }
        }

        private void ZCheckBoxChinese_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxChinese.IsChecked == true)
            {
                UncheckOtherLanguages("Китайский");
            }
        }

        private void ZCheckBoxJapanese_Checked(object sender, RoutedEventArgs e)
        {
            if (ZCheckBoxJapanese.IsChecked == true)
            {
                UncheckOtherLanguages("Японский");
            }
        }

        private void UncheckOtherLanguages(string genre)
        {
            if (genre != "Русский") ZCheckBoxRussian.IsChecked = false;
            if (genre != "Английский") ZCheckBoxEnglish.IsChecked = false;
            if (genre != "Китайский") ZCheckBoxChinese.IsChecked = false;
            if (genre != "Японский") ZCheckBoxJapanese.IsChecked = false;
        }
    }
}
