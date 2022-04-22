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

namespace PlannerApp
{
    /// <summary>
    /// Logika interakcji dla klasy EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        public EditTaskWindow()
        {
            InitializeComponent();
            WypiszDane();
        }

        private void WypiszDane()
        {
            List<TaskEditor> TasksList = new List<TaskEditor>();
            PlanerEntities tasks_db = new PlanerEntities();

            var tasks = from d in tasks_db.Tasks
                        select d;

            int i = 0;
            foreach (var item in tasks)
            {
                if (item.profile_id.ToString() == MyVariables.SelectedProfile_ID)
                {
                    TasksList.Add(new TaskEditor()
                    {
                        profileID = item.profile_id,
                        TaskTitle = item.task_title,
                        TaskDate = item.task_date,
                        TaskID = item.task_id,
                        TaskDescription = item.task_description,
                        TaskTimeEnd = (TimeSpan)item.task_time_end,
                        TaskTimeStart = (TimeSpan)item.task_time_start,
                        enumNumber = i
                    });
                    i++;
                }
            }

            EditTaskText1.Text = TasksList[MyVariables.TaskEditIndex].TaskTitle;
            EditTaskText2.Text = TasksList[MyVariables.TaskEditIndex].TaskDate.ToString();
            EditTaskText3.Text = TasksList[MyVariables.TaskEditIndex].TaskTimeStart.ToString();
            EditTaskText4.Text = TasksList[MyVariables.TaskEditIndex].TaskTimeEnd.ToString();
            EditTaskText5.Text = TasksList[MyVariables.TaskEditIndex].TaskDescription;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<TaskEditor> TasksList = new List<TaskEditor>();
            PlanerEntities tasks_db = new PlanerEntities();

            var tasks = from d in tasks_db.Tasks
                        select d;

            int i = 0;
            foreach (var item in tasks)
            {
                if (item.profile_id.ToString() == MyVariables.SelectedProfile_ID)
                {
                    TasksList.Add(new TaskEditor()
                    {
                        profileID = item.profile_id,
                        TaskTitle = item.task_title,
                        TaskDate = item.task_date,
                        TaskID = item.task_id,
                        TaskDescription = item.task_description,
                        TaskTimeEnd = (TimeSpan)item.task_time_end,
                        TaskTimeStart = (TimeSpan)item.task_time_start,
                        enumNumber = i
                    });
                    i++;
                }
            }

            Console.WriteLine(MyVariables.TaskEditIndex);

            string enteredTitle;
            DateTime enteredDate;
            TimeSpan enteredTimeStart;
            TimeSpan enteredTimeEnd;
            string enteredDescription;

            if(String.IsNullOrEmpty(EditTaskText1.Text))
                enteredTitle = TasksList[MyVariables.TaskEditIndex].TaskTitle;
            else
                enteredTitle = EditTaskText1.Text;


            if (String.IsNullOrEmpty(EditTaskText2.Text))
                enteredDate = TasksList[MyVariables.TaskEditIndex].TaskDate;
            else
                enteredDate = DateTime.Parse(EditTaskText2.Text);


            if (String.IsNullOrEmpty(EditTaskText3.Text))
                enteredTimeStart = TasksList[MyVariables.TaskEditIndex].TaskTimeStart;
            else
                enteredTimeStart = TimeSpan.Parse(EditTaskText3.Text);


            if (String.IsNullOrEmpty(EditTaskText4.Text))
                enteredTimeEnd = TasksList[MyVariables.TaskEditIndex].TaskTimeEnd;
            else
                enteredTimeEnd = TimeSpan.Parse(EditTaskText4.Text);


            if (String.IsNullOrEmpty(EditTaskText5.Text))
                enteredDescription = TasksList[MyVariables.TaskEditIndex].TaskDescription;
            else
                enteredDescription = EditTaskText5.Text;


            using (var context = new PlanerEntities())
            {
                var std = new Task()
                {
                    task_id = TasksList[MyVariables.TaskEditIndex].TaskID,
                    profile_id = TasksList[MyVariables.TaskEditIndex].profileID,     //Tu trzeba bedzie zmienic w zaleznosci od profilu
                    task_title = enteredTitle,
                    task_date = enteredDate,
                    task_time_start = enteredTimeStart,
                    task_time_end = enteredTimeEnd,
                    task_description = enteredDescription
                };
                context.Tasks.Attach(std);
                context.Tasks.Remove(std);
                context.SaveChanges();
                context.Tasks.Add(std);
                context.SaveChanges();
            }

            

            this.Close();
        }
    }
}
