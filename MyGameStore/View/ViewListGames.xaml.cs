using MyGameStore.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
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
    /// Логика взаимодействия для ViewListGames.xaml
    /// </summary>
    public partial class ViewListGames : UserControl
    {
        private string connectionString = "Server=localhost;Database=game_store;User=root;Password=qazmlpasd-270";
        public List<ViewGameOnList> GamesList { get; set; }
        public int Price = -1;
        public decimal Discount = -1;
        public List<Genre> Genres = null;
        public List<Tage> Tags = null;
        public DateTime MinimumDate = new DateTime(1980, 1, 1);
        public DateTime MaximumDate = DateTime.Now;
        public List<Language> Languages = null;
        public string type { get; set; }
        public Account gameAccount { get; set; }

        public ViewListGames(Account account, string type)
        {
            InitializeComponent();
            DataContext = this;

            this.type = type;
            gameAccount = account;
            GamesList = SetViewGameList();
        }

        internal List<Game> SetGameListFilters(int price, decimal discount, List<Genre> genres, List<Tage> tags,
            DateTime minimumDate, DateTime maximumDate, List<Language> languages)
        {

            // Проверка условий фильтрации
            var listGame = GetGameList()
                .Where(game => minimumDate == new DateTime(1980, 1, 1) || game.releaseDateGame >= minimumDate)
                .Where(game => maximumDate == DateTime.Now || game.releaseDateGame <= maximumDate)
                .Where(game => price == -1 || game.priceAnnouncement <= price)
                .Where(game => discount == -1 || game.discountAnnouncement >= discount)
                .Where(game => genres == null || genres.Any(genre => genre.nameGenre == game.nameGenre))
                .Where(game => tags == null || tags.Any(tag => tag.nameTage == game.nameTage))
                .Where(game => languages == null || languages.Any(language => language.nameLanguage == game.nameLanguage))
                .ToList();

            var resultGameList = listGame
                .OrderByDescending(game => game.releaseDateGame)
                .ToList();

            return resultGameList;
        }

        internal List<Game> SetGameListForAccount(Account account)
        {

            var listGame = GetGameList(account);

            var resultGameList = listGame
                .OrderByDescending(game => game.releaseDateGame)
                .ToList();

            return resultGameList;
        }

        internal List<ViewGameOnList> SetViewGameList()
        {
            var listGame = new List<Game>();
            var gamesList = new List<ViewGameOnList>();

            if (type == "filter")
            {
                listGame = SetGameListFilters(Price, Discount, Genres, Tags, MinimumDate, MaximumDate, Languages);
            }

            if (type == "accountGame")
            {
                listGame = SetGameListForAccount(gameAccount);
            }

            foreach (var game in listGame)
            {
                var newViewGameOnList = new ViewGameOnList()
                {
                    ImageGame = game.skinGame,
                    NameGame = game.nameGame,
                    Price = $"{game.priceAnnouncement * (1 - game.discountAnnouncement):F2} РУБ",
                    DiscountedPrice = $"{game.priceAnnouncement:F2} РУБ",
                    Discounted = $"{game.discountAnnouncement * 100:F0} %",
                    account = gameAccount
                };
                gamesList.Add(newViewGameOnList);
            }
            return gamesList;
        }

        internal List<Game> GetGameList()
        {
            var gamesList = new List<Game>();

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
                                gamesList.Add(game);
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

            return gamesList;
        }

        internal List<Game> GetGameList(Account account)
        {
            var gamesList = new List<Game>();
            var gameIds = new List<int>();

            // Первый запрос: Получение idGame по idAccount
            string sqlGetGameIds = @"SELECT gameGamesForUsers FROM GamesForUsers WHERE accountGamesForUsers = @accountId";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(sqlGetGameIds, connection))
                    {
                        command.Parameters.AddWithValue("@accountId", account.idAccount);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                gameIds.Add(reader.GetInt32("gameGamesForUsers"));
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при получении ID игр: {ex.Message}");
                return gamesList; // Возвращаем пустой список, если произошла ошибка
            }

            if (gameIds.Count == 0)
            {
                return gamesList; // Возвращаем пустой список, если нет игр для данного аккаунта
            }

            // Второй запрос: Получение данных о играх по списку idGame
            string sqlGetGames = @"
                SELECT idGame, nameGame, releaseDateGame, sizeGame, skinGame, descriptionGame, nameCampaign, 
               nameTage, nameGenre, nameLanguage, priceAnnouncement, discountAnnouncement 
                FROM Game 
                LEFT JOIN Campaign ON Game.campaignGame = Campaign.idCampaign 
                LEFT JOIN Tage ON Game.tageGame = Tage.idTage 
                LEFT JOIN Genre ON Game.genreGame = Genre.idGenre 
                LEFT JOIN Language ON Game.languageGame = Language.idLanguage 
                LEFT JOIN Announcement ON Game.idGame = Announcement.gameAnnouncement 
                WHERE idGame IN (@gameIds)";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(sqlGetGames, connection))
                    {
                        // Параметризация списка idGame
                        var gameIdsParameter = string.Join(",", gameIds);
                        command.Parameters.AddWithValue("@gameIds", gameIdsParameter);

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
                                gamesList.Add(game);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при получении данных игр: {ex.Message}");
            }

            return gamesList;
        }

        private void FlatButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            var gamesList = new List<ViewGameOnList>();
            string search = ZTextBoxSearch.Text;

            var listGame = SetGameListFilters(Price, Discount, Genres, Tags, MinimumDate, MaximumDate, Languages)
                .Where(game => game.nameGame.ToLower() == search.ToLower())
                .ToList();

            foreach (var game in listGame)
            {
                var newViewGameOnList = new ViewGameOnList()
                {
                    ImageGame = game.skinGame,
                    NameGame = game.nameGame,
                    Price = $"{game.priceAnnouncement * (1 - game.discountAnnouncement):F2} РУБ",
                    DiscountedPrice = $"{game.priceAnnouncement:F2} РУБ",
                    Discounted = $"{game.discountAnnouncement * 100:F0} %"
                };
                gamesList.Add(newViewGameOnList);
            }
            GamesList = gamesList;
        }

        
    }
}
