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
using System.Data;                  //do wpisywania rzeczy do bazy danych

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

            PlanerEntities profiles_db = new PlanerEntities();

            var profiles = from d in profiles_db.Profiles
                           select d;

            foreach (var item in profiles)
            {
                Console.WriteLine(item.profile_id);
                Console.WriteLine(item.profile_name);
                Console.WriteLine(item.profile_surname);
                Console.WriteLine(item.profile_date_of_birth);
            }    
        }

        private void DodajZadanie_Click(object sender, RoutedEventArgs e)
        {
            Text1.Text = "Nowe zadanie";

            NewTaskWindow window2 = new NewTaskWindow();
            window2.Show();
        }
    }
}
