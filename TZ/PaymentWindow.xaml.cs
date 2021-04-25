using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        SqlConnection connect;
        SqlCommand command;
        int b;
        
        public PaymentWindow()
        {
            InitializeComponent();            
            connect = new SqlConnection(@"Data Source=DESKTOP-V04P536\SQLEXPRESS;Initial Catalog=DBDemo;Integrated Security=True");                       
            command = new SqlCommand();
            command.Connection = connect;            
            BindComboBox(PickOrder);
            PayComboBox(PickPayment);

        }


        private void PaymentSum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
            if (PaymentSum.Text == "")
            {
                if (e.Text == "0")
                {
                    e.Handled = true;
                }
            }
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {           
            string sql = "INSERT Platezh (Nomer_zakaza,Nomer_Prihod,Plat_Sum) VALUES (@Nomer_zakaza,@Nomer_Prihod,@Plat_Sum)";
            SqlCommand cmd_SQL = new SqlCommand(sql, connect);
            cmd_SQL.Parameters.AddWithValue("@Nomer_zakaza",PickOrder.SelectedIndex+1);
            cmd_SQL.Parameters.AddWithValue("@Nomer_Prihod", PickPayment.SelectedIndex+1);
            cmd_SQL.Parameters.AddWithValue("@Plat_Sum", PaymentSum.Text);
           /* int a = int.Parse(PaymentSum.Text);
            int c = int.Parse(b);
            if(a > c)
            {
                MessageBox.Show("Введенная сумма больше остатка", "Предупреждение", MessageBoxButton.OK);
            }
            else
            {
               
            }*/
            try
            {
                connect.Open();
                int n = cmd_SQL.ExecuteNonQuery();
            }
            finally
            {
                connect.Close();
            }
            MainWindow main = new MainWindow();
            main.Show();
            this.Hide();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Hide();
        }

        public void BindComboBox(ComboBox PickOrder)
        {
            connect.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select Nomer, concat(Data,' ','Сумма:',PaymentSum) as Заказ From Zakaz", connect);
            DataSet ds = new DataSet();
            da.Fill(ds, "Zakaz");
            PickOrder.ItemsSource = ds.Tables[0].DefaultView;
            PickOrder.DisplayMemberPath = ds.Tables[0].Columns["Заказ"].ToString();
            PickOrder.SelectedValuePath = ds.Tables[0].Columns["Nomer"].ToString();
            connect.Close();
        }
    
        public void PayComboBox(ComboBox PicPayment)
        {
            connect.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select Nomer_prihoda,Ostatok, concat(Data,' ','Остаток:',Ostatok) as Деньги From Prihod_deneg Where Ostatok > 0", connect);
            DataSet ds = new DataSet();
            da.Fill(ds, "Prihod_deneg");
            PicPayment.ItemsSource = ds.Tables[0].DefaultView;
            PicPayment.DisplayMemberPath = ds.Tables[0].Columns["Деньги"].ToString();
            PicPayment.SelectedValuePath = ds.Tables[0].Columns["Nomer_prihoda"].ToString();          
            connect.Close();
        }
    }
}
