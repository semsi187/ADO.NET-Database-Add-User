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
using System.Data.SqlClient;
using System.Xml.Linq;

namespace ADO.NET_Database_Add_User
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Library;Integrated Security=True;";


        SqlDataReader reader = null;

        public MainWindow()
        {
            InitializeComponent();
            LoadDataToListbox();
        }

        private void LoadDataToListbox()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT Id, FirstName, FirstName FROM Authors";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int Id = reader.GetInt32(0);
                            string firstname = reader.GetString(1);
                            string lastname = reader.GetString(2);
                            Authors.Items.Add($"{Id} {firstname} {lastname}");
                        }
                    }
                }
            }
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            string Name1 = Firstname.Text;
            string LastName1 = Lastname.Text;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO Authors(FirstName, LastName) VALUES(@Name1, @LastName1)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", Name1);
                    command.Parameters.AddWithValue("@LastName", LastName1);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            Authors.Items.Add($"{Name1} {LastName1}");
        }

    }
}
