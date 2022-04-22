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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;                  //do wpisywania rzeczy do bazy danych
using System.Configuration;

namespace PlannerApp
{
    /// <summary>
    /// Logika interakcji dla klasy NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {
        public NewTaskWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Dodawanie nowego Taska:
            // TODO: Walidacja wprowadzanych danych i zablokowanie zatwierdzenia
            DateTime enteredDate = DateTime.Parse(NewTaskText2.Text);
            TimeSpan enteredTimeStart = TimeSpan.Parse(NewTaskText3.Text);
            TimeSpan enteredTimeEnd = TimeSpan.Parse(NewTaskText4.Text);
            using (var context = new PlanerEntities())
            {
                var std = new Task()
                {
                    profile_id = 3,                     //Tu trzeba bedzie zmienic w zaleznosci od profilu
                    task_title = NewTaskText1.Text,
                    task_date = enteredDate,
                    task_time_start = enteredTimeStart,
                    task_time_end = enteredTimeEnd,
                    task_description = NewTaskText5.Text
                };
                context.Tasks.Add(std);

                context.SaveChanges();
            }
            //this.Close();
            

            MainWindow newWindow = new MainWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            this.Close();
        }
    }
}
