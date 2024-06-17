using MyGameStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для ViewGameOnList.xaml
    /// </summary>
    public partial class ViewGameOnList : UserControl
    {
        private string connectionString = "Server=localhost;Database=game_store;User=root;Password=qazmlpasd-270";
        ViewDescriptionGame descriptionGame {  get; set; }  
        Game game { get; set; }
        public Account account { get; set; }
        bool buy {  get; set; }

        public ViewGameOnList()
        {
            InitializeComponent();
            game = GetGameFromDatabase();
            buy = IsGameInUsersGames(game.idGame, account.idAccount);
            descriptionGame = new ViewDescriptionGame(game, buy);
        }

        public static readonly DependencyProperty ImageGameProperty =
            DependencyProperty.Register("ImageGame", typeof(string), typeof(ViewGameOnList));

        public string ImageGame
        {
            get { return (string)GetValue(ImageGameProperty); }
            set { SetValue(ImageGameProperty, value); }
        }

        public static readonly DependencyProperty NameGameProperty =
            DependencyProperty.Register("NameGame", typeof(string), typeof(ViewGameOnList));

        public string NameGame
        {
            get { return (string)GetValue(NameGameProperty); }
            set { SetValue(NameGameProperty, value); }
        }

        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(string), typeof(ViewGameOnList));

        public string Price
        {
            get { return (string)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        public static readonly DependencyProperty DiscountedPriceProperty =
            DependencyProperty.Register("DiscountedPrice", typeof(string), typeof(ViewGameOnList));

        public string DiscountedPrice
        {
            get { return (string)GetValue(DiscountedPriceProperty); }
            set { SetValue(DiscountedPriceProperty, value); }
        }

        public static readonly DependencyProperty DiscountedProperty =
            DependencyProperty.Register("Discounted", typeof(string), typeof(ViewGameOnList));

        public string Discounted
        {
            get { return (string)GetValue(DiscountedProperty); }
            set { SetValue(DiscountedProperty, value); }
        }

        internal Game GetGameFromDatabase()
        {
            Game game = null;

            string query = "SELECT g.idGame, g.nameGame, g.releaseDateGame, g.sizeGame, g.skinGame, g.descriptionGame, " +
                           "c.nameCampaign, t.nameTage, gr.nameGenre, l.nameLanguage, a.priceAnnouncement, a.discountAnnouncement " +
                           "FROM Game g " +
                           "JOIN Campaign c ON g.campaignGame = c.idCampaign " +
                           "JOIN Tage t ON g.tageGame = t.idTage " +
                           "JOIN Genre gr ON g.genreGame = gr.idGenre " +
                           "JOIN Language l ON g.languageGame = l.idLanguage " +
                           "JOIN Announcement a ON a.gameAnnouncement = g.idGame " +
                           "WHERE g.nameGame = @nameGame AND a.priceAnnouncement = @price";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nameGame", NameGame);
                command.Parameters.AddWithValue("@price", decimal.Parse(Price));

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            game = new Game
                            {
                                idGame = reader.GetInt32(0),
                                nameGame = reader.GetString(1),
                                releaseDateGame = reader.GetDateTime(2),
                                sizeGame = reader.GetDecimal(3),
                                skinGame = reader.GetString(4),
                                descriptionGame = reader.GetString(5),
                                nameCampaign = reader.GetString(6),
                                nameTage = reader.GetString(7),
                                nameGenre = reader.GetString(8),
                                nameLanguage = reader.GetString(9),
                                priceAnnouncement = reader.GetDecimal(10),
                                discountAnnouncement = reader.GetDecimal(11)
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                }
            }

            return game;
        }

        public bool IsGameInUsersGames(int gameId, int accountId)
        {
            bool exists = false;

            string query = "SELECT COUNT(*) FROM GamesForUsers WHERE accountGamesForUsers = @accountId AND gameGamesForUsers = @gameId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@accountId", accountId);
                command.Parameters.AddWithValue("@gameId", gameId);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    exists = count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                }
            }

            return exists;
        }

        public event EventHandler ButtonGameClicked;

        private void ButtonGame_Click(object sender, RoutedEventArgs e)
        {
            ButtonGameClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
