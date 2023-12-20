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

namespace PracticaProj
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
            var db = new PracticeNewEntities();
            var items = db.Users.ToList();
            adminDataGrid.ItemsSource = items;
        }

        private void searchBTN_Click(object sender, RoutedEventArgs e)
        {
            var filterValues = PracticeNewEntities.GetContext().Users.ToList();
            var searchedPeople = searchTXT.Text;
            filterValues = filterValues.Where(x => x.login == searchedPeople).ToList();
            adminDataGrid.ItemsSource = filterValues;
        }
    }
}
