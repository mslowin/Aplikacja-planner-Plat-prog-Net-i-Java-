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
using System.Data;                  //do wpisywania rzeczy do bazy danych

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
            PlanerEntities tasks_db = new PlanerEntities();

            

            var tasks = from d in tasks_db.Tasks
                        select d;

            foreach (var item in tasks)
            {
                item.task_title = NewTaskText1.Text;
            }
        }
    }
}
