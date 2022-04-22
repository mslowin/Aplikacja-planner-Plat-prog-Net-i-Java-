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
    /// Logika interakcji dla klasy NewProfileWindow.xaml
    /// </summary>
    public partial class NewProfileWindow : Window
    {
        public List<Profile> MyProfiles { get; set; }

        public NewProfileWindow()
        {
            InitializeComponent();
            FillCombobox();

            //Wyświetlenie danych na datagrid:
            //using (PlanerEntities _context = new PlanerEntities())
            //{
            //    MyProfiles = _context.Profiles.ToList();
            //}
            //DataGridProfiles.ItemsSource = MyProfiles;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Dodawanie nowego profilu:
            // TODO: Walidacja wprowadzanych danych i zablokowanie zatwierdzenia
            DateTime enteredDate2 = DateTime.Parse(NewProfileText3.Text);
            using (var context2 = new PlanerEntities())
            {
                var std = new Profile()
                {
                    profile_name = NewProfileText1.Text,
                    profile_surname = NewProfileText2.Text,
                    profile_date_of_birth = enteredDate2
                };
                context2.Profiles.Add(std);
                context2.SaveChanges();

                //Wyzerowanie pól tekstowych
                NewProfileText1.Text = " ";
                NewProfileText2.Text = " ";
                NewProfileText3.Text = " ";

                MyVariables.SelectedProfile_ID = std.profile_id.ToString();
                
                this.Close();
            }
        }

        private void FillCombobox()
        {
            using (PlanerEntities c = new PlanerEntities())
            {
                ComboBoxProfiles.ItemsSource = c.Profiles.ToList();
                ComboBoxProfiles.SelectedValuePath = "profile_id";
                ComboBoxProfiles.DisplayMemberPath = "profile_name";
            }
        }

        public void ComboBoxProfiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id;
            //string id = ComboBoxProfiles.SelectedValue.ToString();
            bool result = int.TryParse(ComboBoxProfiles.SelectedValue.ToString(), out id);
            //Test.Text = "Id wybranego profilu to: " + id.ToString() + "\nTeraz trzeba według tego id przefiltrowac taski";

            MyVariables.SelectedProfile_ID = id.ToString();
        }

        private void Wybrany_Profil_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
