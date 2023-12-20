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
            categoryCOMBO.ItemsSource = PracticeNewEntities.GetContext().Categories.ToList();
            userCOMBO.ItemsSource = PracticeNewEntities.GetContext().Users.ToList();
            productCOMBO.ItemsSource = PracticeNewEntities.GetContext().Products.ToList();
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            bool logic = false;
            try
            {
                if (categoryCOMBO.SelectedItem == null)
                {
                    MessageBox.Show("не заполнена категория");
                    logic = true;
                }
                if (userCOMBO.SelectedItem == null)
                {
                    MessageBox.Show("не заполнен пользователь");
                    logic = true;
                }
                if (productCOMBO.SelectedItem == null)
                {
                    MessageBox.Show("не заполнен продукт");
                    logic = true;
                }
                if (dataPicker.SelectedDate == null)
                {
                    MessageBox.Show("не заполнен дата");
                    logic = true;
                }
                try
                {
                    Convert.ToInt32(countTXT.Text);
                    if (countTXT.Text.Length == 0)
                    {
                        MessageBox.Show("не заполнен количество");
                        logic = true;
                    }
                }
                catch
                {
                    MessageBox.Show("неправильно введено количество");
                    logic = true;
                }
                try
                {
                    Convert.ToDouble(costTXT.Text);
                    if (costTXT.Text.Length == 0)
                    {
                        MessageBox.Show("не заполнен стоимость");
                        logic = true;
                    }
                }
                catch
                {
                    MessageBox.Show("неправильно введена стоимость");
                    logic = true;
                }

                int count = 0;
                for (int i = 0; i < forwhatTXT.Text.Length; i++)
                {
                    if (Char.IsLetter(forwhatTXT.Text[i]))
                    {
                        count++;
                    }
                }
                if (count < 3)
                {
                    MessageBox.Show("назначение имеет слишком короткое название. Минимум - 3 буквы.");
                    logic = true;
                }
            }
            catch 
            { 

            }

            if (!logic)
            {
                var s = new Order();
                s.category = Convert.ToInt32(((Category)categoryCOMBO.SelectedItem).Id_category);
                s.count = Convert.ToInt32(countTXT.Text);
                s.Id_product = Convert.ToInt32(((Product)productCOMBO.SelectedItem).Id);
                s.user_id = Convert.ToInt32(((User)userCOMBO.SelectedItem).id);
                s.price = Convert.ToInt32(costTXT.Text);
                s.sum = s.price * s.count;
                s.data = dataPicker.SelectedDate;
                s.uniquename = s.Category1.category_name.Substring(0, 1) + s.ID.ToString() + s.data.ToString();
                s.forwhat = forwhatTXT.Text;

                var db = new PracticeNewEntities();
                db.Orders.Add(s);
                db.SaveChanges();

                dataGrid1.ItemsSource = db.Orders.ToList();

                this.Close();
            }

        }

        private void closeBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
