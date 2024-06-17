using MyGameStore.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для ViewHome.xaml
    /// </summary>
    public partial class ViewHome : UserControl
    {
        private string connectionString = "Server=localhost;Database=game_store;User=root;Password=qazmlpasd-270";
        public ObservableCollection<ViewGameOnList> GamesList { get; set; }

        public ViewHome()
        {
            InitializeComponent();
            DataContext = this;

            GamesList = SetGameList();
        }

        internal ObservableCollection<ViewGameOnList> SetGameList()
        {
            var gamesList = new ObservableCollection<ViewGameOnList>();

            // Получаем список игр
            var listGame = GetGameList().OrderByDescending(game => game.releaseDateGame);

            foreach (var game in listGame)
            {
                var newViewGameOnList = new ViewGameOnList()
                {
                    ImageGame = game.skinGame,
                    NameGame = game.nameGame,
                    Price = (game.priceAnnouncement * (1 - game.discountAnnouncement)) + " РУБ",
                    DiscountedPrice = game.priceAnnouncement + " РУБ",
                    Discounted = (100 * game.discountAnnouncement) + " %"
                };
                gamesList.Add(newViewGameOnList);
            }
            return gamesList;
        }

        internal List<Game> GetGameList()
        {
            var listGame = new List<Game>();

            string sql = @"SELECT idGame, nameGame, releaseDateGame, sizeGame, skinGame, descriptionGame, nameCampaign, 
                nameTage, nameGenre, nameLanguage, priceAnnouncement, discountAnnouncement FROM Game LEFT JOIN Campaign  
                ON Game.campaignGame = Campaign.idCampaign LEFT JOIN Tage ON Game.tageGame = Tage.idTage LEFT JOIN Genre
                ON Game.genreGame = Genre.idGenre LEFT JOIN Language ON Game.languageGame = Language.idLanguage LEFT JOIN
                Announcement ON Game.idGame = Announcement.gameAnnouncement;";

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
                                var game = new Game()
                                {
                                    idGame = reader.GetInt32("idGame"),
                                    nameGame = reader.GetString("nameGame"),
                                    releaseDateGame = reader.GetDateTime("releaseDateGame"),
                                    sizeGame = reader.GetDecimal("sizeGame"),
                                    skinGame = reader.GetString("skinGame"),
                                    descriptionGame = reader.GetString("descriptionGame"),
                                    nameCampaign = reader.GetString("nameCampaign"),
                                    nameTage = reader.GetString("nameTage"),
                                    nameGenre = reader.GetString("nameGenre"),
                                    nameLanguage = reader.GetString("nameLanguage"),
                                    priceAnnouncement = reader.GetDecimal("priceAnnouncement"),
                                    discountAnnouncement = reader.GetDecimal("discountAnnouncement")
                                };
                                listGame.Add(game);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }

            return listGame;
        }
    }
}
