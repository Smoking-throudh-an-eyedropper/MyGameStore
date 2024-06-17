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
    /// Логика взаимодействия для ViewMyProfile.xaml
    /// </summary>
    public partial class ViewMyProfile : Window
    {
        private string connectionString = "Server=localhost;Database=game_store;User=root;Password=qazmlpasd-270";
        public Account account { get; set; }

        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Photo { get; set; }

        public ViewMyProfile(Account account)
        {
            InitializeComponent();
            this.account = account;

            Email = account.emailAccount;
            Nickname = account.nameAccount;
            Photo = account.imageAccount;

            DataContext = this;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void FlatButtonApply_Click(object sender, RoutedEventArgs e)
        {
            UpdateProfileInDatabase();
            this.Close();
        }

        private void FlatButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateProfileInDatabase()
        {
            int accountId = account.idAccount;

            string sql = "UPDATE Account SET emailAccount = @Email, nameAccount = @Name, imageAccount = @Image WHERE idAccount = @AccountId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Name", Nickname); // Assuming Nickname is nameAccount
                command.Parameters.AddWithValue("@Image", Photo); // Assuming Photo is imageAccount
                command.Parameters.AddWithValue("@AccountId", accountId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Профиль был успешно обновлен!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("При обновлении профиля произошла ошибка");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
            }
        }

    }

}
