using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Data;

namespace TZ
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            

        }

        private void Payment_Click(object sender, RoutedEventArgs e)
        {
            PaymentWindow paymentWindow = new PaymentWindow();
            paymentWindow.Show();
            Hide();
        }

        private void Donation_Click(object sender, RoutedEventArgs e)
        {
            Donat donat = new Donat();
            donat.Show();
            Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-V04P536\SQLEXPRESS;Initial Catalog=DBDemo;Integrated Security=True";
            string commandText = "SELECT * FROM [Platezh]";
            string commandText2 = "SELECT * FROM [Prihod_deneg]";
            string commandText3 = "SELECT * FROM [Zakaz]";
            string BalanceCommand = "SELECT SUM(Ostatok) AS Всё From [Prihod_deneg]";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                using (SqlCommand cmd = new SqlCommand(commandText3, connection))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    Zakaz.ItemsSource = dt.AsDataView();

                }
                using (SqlCommand cmd = new SqlCommand(commandText2, connection))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    Donat.ItemsSource = dt.AsDataView();

                }
                using (SqlCommand cmd = new SqlCommand(commandText, connection))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    Pay.ItemsSource = dt.AsDataView();

                }
                using (SqlCommand cmd = new SqlCommand(BalanceCommand, connection))
                {
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Balance.Text = reader["Всё"].ToString();
                    }
                    cmd.Dispose();
                }
                connection.Close();
            }
        }
    }
}
