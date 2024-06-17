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
    /// Логика взаимодействия для ViewConfidentiality.xaml
    /// </summary>
    public partial class ViewConfidentiality : Window
    {
        private string connectionString = "Server=localhost;Database=game_store;User=root;Password=qazmlpasd-270";
        public Account account;

        public ViewConfidentiality(Account account)
        {
            InitializeComponent();
            this.account = account;
        }

        private void UpdatePasswordInDatabase(string newPassword)
        {
            int accountId = account.idAccount;

            string sql = "UPDATE Account SET passwordAccount = @newPassword WHERE idAccount = @accountId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@newPassword", newPassword);
                command.Parameters.AddWithValue("@accountId", accountId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Пароль был успешно обнавлен!");
                    }
                    else
                    {
                        MessageBox.Show("При обновлении пароля произошла ошибка");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void FlatButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FlatButtonApply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (account.passwordAccount == ZPasswordBoxOldPassword.Text)
                {
                    UpdatePasswordInDatabase(ZPasswordBoxOldPassword.Text);
                }
                else
                {
                    if (account.passwordAccount != ZPasswordBoxOldPassword.Text)
                    {
                        MessageBox.Show("Неверный пароль");
                    }
                }
            } 
            catch (Exception ex) 
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
