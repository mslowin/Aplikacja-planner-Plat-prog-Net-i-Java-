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
using Newtonsoft.Json;
using System.Net;

namespace PlannerApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string APIKey = "1bf41ab026650fafe68a87a0db054a80";
        public List<Task> MyTasks { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            WyswietlNazweProfilu();
            getWeather("Wroclaw");

            List<TaskMenager> TasksList = new List<TaskMenager>();
            PlanerEntities tasks_db = new PlanerEntities();

            var tasks = from d in tasks_db.Tasks
                        select d;

            foreach (var item in tasks)
            {
                string time = item.task_time_start.ToString().Remove(5,3) + " - " + item.task_time_end.ToString().Remove(5,3);
                TasksList.Add(new TaskMenager() { TaskTitle = item.task_title, TaskDate = item.task_date.ToString().Remove(10,9), TaskTime = time, TaskID = item.task_id });
            }

            UserList.ItemsSource = TasksList;

            //Wyświetlenie danych na datagrid:
            //using (PlanerEntities _context = new PlanerEntities())
            //{
            //    MyTasks = _context.Tasks.ToList();
            //}
            //DataGridTasks.ItemsSource = MyTasks;
        }

        void getWeather(string miasto)
        {
            using (WebClient web = new WebClient())
            {
                string City = miasto;
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", City, APIKey);
                var json = web.DownloadString(url);

                // zdjęcie
                WeatherInformation.root Info = JsonConvert.DeserializeObject<WeatherInformation.root>(json);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("https://openweathermap.org/img/w/" + Info.weather[0].icon + ".png");
                bitmap.EndInit();
                picIcon.Stretch = Stretch.Fill;
                picIcon.Source = bitmap;

                // temp
                //double tempCelc = (int)temp.Text - 273.15;
                //string input = (Info.main.temp - 275.15).ToString();
                //int index = input.LastIndexOf("/");
                //if (index >= 0)
                //    input = input.Substring(0, index); // or index + 1 to keep slash

                temp.Text = ((int)(Info.main.temp-275.15)).ToString() + " °C";
                opis.Text = Info.weather[0].description.ToString();
                cisnienie.Text = Info.main.pressure.ToString() + " hPa";
            }
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text == "Wybierz miasto...")
                txtBox.Text = string.Empty;
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

        // Usuwanie tasków
        private void removeTask_Click(object sender, RoutedEventArgs e)
        {

            //-------------------------------------------------------------------pobranie bazy danych do list klas
            List<TaskMenager> TasksList = new List<TaskMenager>();
            PlanerEntities tasks_db = new PlanerEntities();

            var tasks = from d in tasks_db.Tasks
                        select d;

            foreach (var item in tasks)
            {
                string time = item.task_time_start.ToString().Remove(5, 3) + " - " + item.task_time_end.ToString().Remove(5, 3);
                TasksList.Add(new TaskMenager() { TaskTitle = item.task_title, TaskDate = item.task_date.ToString().Remove(10, 9), TaskTime = time, TaskID = item.task_id });
            }
            //------------------------------------------------------------------------------------------------------

            using (var context = new PlanerEntities())
            {
                var std = new Task()
                {
                    task_id = TasksList[0].TaskID
                };
                context.Tasks.Attach(std);
                context.Tasks.Remove(std);

                context.SaveChanges();
            }
            
        }

        private void WyswietlNazweProfilu()
        {
            PlanerEntities profiles_db = new PlanerEntities();

            var profiles = from d in profiles_db.Profiles
                           select d;

            foreach (var item in profiles)
            {
                if (item.profile_id.ToString() == MyVariables.SelectedProfile_ID)
                {
                    WyswietlanaNazwaProfilu.Text = item.profile_name;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            getWeather(miasto.Text);
        }
    }

    public class TaskMenager
    {
        public string TaskTitle { get; set; }
        public string TaskTime { get; set; }
        public string TaskDate { get; set; }
        public int TaskID { get; set; }
    }
}
