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
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public DataGrid dataGrid1;
        public Add(DataGrid dataGrid)
        {
            InitializeComponent();
            this.dataGrid1 = dataGrid;
            categoryCOMBO.ItemsSource = PracticeNewEntities2.GetContext().Categories.ToList();
            userCOMBO.ItemsSource = PracticeNewEntities2.GetContext().Users.ToList();
            productCOMBO.ItemsSource = PracticeNewEntities2.GetContext().Products.ToList();
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            var s = new Order();
            s.category = Convert.ToInt32(((Category)categoryCOMBO.SelectedItem).Id_category);
            s.count = Convert.ToInt32(countTXT.Text);
            s.Id_product = Convert.ToInt32(((Product)productCOMBO.SelectedItem).Id);
            s.user_id = Convert.ToInt32(((User)userCOMBO.SelectedItem).id);
            s.price = Convert.ToInt32(costTXT.Text);
            s.sum = s.price * s.count;
            s.data = dataPicker.SelectedDate;

            var db = new PracticeNewEntities2();
            db.Orders.Add(s);
            db.SaveChanges();

            dataGrid1.ItemsSource = db.Orders.ToList();

            this.Close();
        }

        private void closeBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
