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
        public List<Task> MyTasks { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            WyswietlNazweProfilu();

            //Wyświetlenie danych na datagrid:
            using (PlanerEntities _context = new PlanerEntities())
            {
                MyTasks = _context.Tasks.ToList();
            }
            DataGridTasks.ItemsSource = MyTasks;
        }

        private void DodajZadanie_Click(object sender, RoutedEventArgs e)
        {
            NewTaskWindow window2 = new NewTaskWindow();
            window2.Owner = this;
            window2.Show();
        }

        private void ZmienProfil_Click(object sender, RoutedEventArgs e)
        {
            NewProfileWindow window3 = new NewProfileWindow();
            window3.Owner = this;
            window3.Show();
        }

        private void WyswietlNazweProfilu()
        {
            PlanerEntities nazwa_profilu = new PlanerEntities();
            WyswietlanaNazwaProfilu.Text = "HELLO!";
        }
    }
}
