using MyGameStore.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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

namespace MyGameStore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = "Server=localhost;Database=game_store;User=root;Password=qazmlpasd-270";
        private MySqlConnection connection;
        private List<Account> accounts;

        public MainWindow()
        {
            InitializeComponent();
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

        private bool AccountExists(string email)
        {
            bool exists = false;
            string sql = "SELECT COUNT(*) FROM Account WHERE emailAccount = @Email";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    exists = Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }

            return exists;
        }

        private Account GetAccountByEmail(string email)
        {
            Account account = null;
            string sql = "SELECT idAccount, emailAccount, passwordAccount FROM Account WHERE emailAccount = @Email";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            account = new Account()
                            {
                                idAccount = reader.GetInt32("idAccount"),
                                emailAccount = reader.GetString("emailAccount"),
                                passwordAccount = reader.GetString("passwordAccount")
                            };
                        }
                    }
                }
            }

            return account;
        }

        public int Registration()
        {
            int result = 0;
            string inputEmail = ZTextBoxEmail.Text.Trim();
            string inputPassword = ZPasswordBoxPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(inputEmail) || string.IsNullOrWhiteSpace(inputPassword))
            {
                MessageBox.Show("Email и пароль не могут быть пустыми.");
                return result;
            }

            if (AccountExists(inputEmail))
            {
                MessageBox.Show("Аккаунт с таким email уже существует.");
                return result;
            }

            string sql = "INSERT INTO Account (emailAccount, passwordAccount) VALUES (@Email, @Password);";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Email", inputEmail);
                        command.Parameters.AddWithValue("@Password", inputPassword);
                        result = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }

            return result;
        }

        public bool Authorization(out Account account)
        {
            bool result = false;
            account = null;
            string inputEmail = ZTextBoxEmail.Text;
            string inputPassword = ZPasswordBoxPassword.Text;
            string sql = "SELECT idAccount, emailAccount, passwordAccount FROM Account WHERE emailAccount = @Email AND passwordAccount = @Password";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Email", inputEmail);
                        command.Parameters.AddWithValue("@Password", inputPassword);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                account = new Account()
                                {
                                    idAccount = reader.GetInt32("idAccount"),
                                    emailAccount = reader.GetString("emailAccount"),
                                    passwordAccount = reader.GetString("passwordAccount")
                                };
                                result = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }

            return result;
        }

        private void FlatButtonRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (Registration() > 0)
            {
                try
                {
                    MessageBox.Show("Пользователь зарегистрирован");
                    var account = GetAccountByEmail(ZTextBoxEmail.Text);
                    PrimaryWindow primaryWindow = new PrimaryWindow(account);
                    primaryWindow.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
            }
        }

        private void FlatButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            if (Authorization(out var account))
            {
                try
                {
                    PrimaryWindow primaryWindow = new PrimaryWindow(account);
                    primaryWindow.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Неверно введен логин или пароль");
            }
        }
    }
}
