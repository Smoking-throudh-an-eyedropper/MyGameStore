using MyGameStore.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Логика взаимодействия для ViewDescriptionGame.xaml
    /// </summary>
    public partial class ViewDescriptionGame : Window
    {
        private string connectionString = "Server=localhost;Database=game_store;User=root;Password=qazmlpasd-270";
        internal Game game { get; set; }
        internal bool buy { get; set; }
        public List<Image> GameImagesList { get; set; }
        public string GameTitle { get; set; }
        public string GamePrice { get; set; }
        public string GameName { get; set; }
        public string BuyDownloadButtonText { get; set; }
        public string DescriptionLeft { get; set; }
        public string DescriptionRight { get; set; }

        internal ViewDescriptionGame(Game game, bool buy)
        {
            InitializeComponent();
            this.buy = buy;
            this.game = game;

            GameImagesList = LoadGameImages(game.idGame);
            GameTitle = game.nameGame;
            GamePrice = $"{game.priceAnnouncement * (1 - game.discountAnnouncement):F2} РУБ"; // Исправлено для отображения цены с учетом скидки
            GameName = game.nameGame;
            BuyDownloadButtonText = buy ? "Скачать" : "Купить";
            DescriptionLeft = game.descriptionGame;
            DescriptionRight = $"Дата релиза: {game.releaseDateGame}" + Environment.NewLine +
                $"Размер игры: {game.sizeGame}" + Environment.NewLine + $"Теги: {game.nameTage}"
                + Environment.NewLine + $"Жанры: {game.nameGenre}" + Environment.NewLine + 
                $"Языки: {game.nameLanguage}";

            DataContext = this; // Установка контекста данных
        }

        private void FlatButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FlatButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        public List<Image> LoadGameImages(int gameId)
        {
            var images = new List<Image>();

            string sql = "SELECT imageGameListImage FROM GameListImage WHERE gameGameListImage = @gameId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@gameId", gameId);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string imagePath = reader.GetString("imageGameListImage");
                        Image image = new Image();
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                        bitmap.EndInit();
                        image.Source = bitmap;
                        images.Add(image);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading game images: " + ex.Message);
                }
            }
            return images;
        }

        private void FlatButtonBuyDownload_Click(object sender, RoutedEventArgs e)
        {
            if (buy)
            {
                System.Diagnostics.Process.Start("https://your_download_url.com"); // Укажите правильный URL для загрузки
            }
            else
            {
                System.Diagnostics.Process.Start("https://your_purchase_url.com"); // Укажите правильный URL для покупки
            }
        }
    }
}
