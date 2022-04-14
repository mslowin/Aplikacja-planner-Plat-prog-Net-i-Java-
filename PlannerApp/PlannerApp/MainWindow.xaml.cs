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
using System.Data;
using System.Data.SqlClient;                  //do wpisywania rzeczy do bazy danych
using System.Configuration;

namespace PlannerApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //bindDataGridProfiles();
            bindDataGridTasks();

        }

        private void bindDataGridTasks()
        {
            SqlConnection con = new SqlConnection();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["PlanerEntities"].ConnectionString;
            //con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from [Tasks]";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Tasks");
            //da.Fill(dt);
            DataGridTasks.ItemsSource = dt.DefaultView;
        }

        //private void bindDataGridProfiles()
        //{
        //    SqlConnection con = new SqlConnection();
        //    con.ConnectionString = ConfigurationManager.ConnectionStrings["PlanerEntities"].ConnectionString;
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = "select * from [Profiles]";
        //    cmd.Connection = con;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable("Profiles");
        //    da.Fill(dt);
        //    DataGridProfiles.ItemsSource = dt.DefaultView;
        //}

        private void DodajZadanie_Click(object sender, RoutedEventArgs e)
        {

            NewTaskWindow window2 = new NewTaskWindow();
            window2.Show();



            //var profiles = from d in profiles_db.Profiles
            //               select d;

            //foreach (var item in profiles)
            //{
            //    Console.WriteLine(item.profile_id);
            //    Console.WriteLine(item.profile_name);
            //    Console.WriteLine(item.profile_surname);
            //    Console.WriteLine(item.profile_date_of_birth);
            //}
        }
    }
}
