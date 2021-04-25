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
using System.Windows.Shapes;

namespace TZ
{
    /// <summary>
    /// Логика взаимодействия для Donat.xaml
    /// </summary>
    public partial class Donat : Window
    {
       
        public Donat()
        {
            InitializeComponent();

        }
        private void DonatSum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
            if (DonatSum.Text == "")
            {
                if (e.Text == "0")
                {
                    e.Handled = true;
                }
            }
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-V04P536\SQLEXPRESS;Initial Catalog=DBDemo;Integrated Security=True");
            string sql = "INSERT Prihod_deneg (Data,Sum,Ostatok) VALUES (@Data,@Sum,@Ostatok)";
            SqlCommand cmd_SQL = new SqlCommand(sql, connect);

            cmd_SQL.Parameters.AddWithValue("@Data", DateTime.UtcNow);
            cmd_SQL.Parameters.AddWithValue("@Sum", DonatSum.Text);
            cmd_SQL.Parameters.AddWithValue("@Ostatok", DonatSum.Text);
            try
            {
                connect.Open();
                int n = cmd_SQL.ExecuteNonQuery();
            }
            finally
            {
                connect.Close();
            }
            DonatSum.Clear();
            main.Show();
            this.Hide();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DonatSum.Clear();
            MainWindow main = new MainWindow();
            main.Show();
            this.Hide();
        }
    }
}
