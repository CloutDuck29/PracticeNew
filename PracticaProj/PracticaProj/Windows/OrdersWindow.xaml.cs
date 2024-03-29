﻿using Newtonsoft.Json;
using PracticaProj.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Xaml;

namespace PracticaProj
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        public OrdersWindow()
        {
            InitializeComponent();
            var db = new PracticeNewEntities();
            var items = db.Orders.ToList();
            myDataGrid.ItemsSource = items;
            mainCOMBO.ItemsSource = PracticeNewEntities.GetContext().Categories.ToList();

            adminBTN.Visibility = Visibility.Hidden;
            
            string json = File.ReadAllText(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "UserSession.json"));
            UserSession currentUser = JsonConvert.DeserializeObject<UserSession>(json);
            var user = db.Users.First(e => e.login == currentUser.UserName);

            if(user.admin == true)
            {
                adminBTN.Visibility = Visibility.Visible;
            }

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти из приложения?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                Authentication.session.CloseUserSession();
                Application.Current.Shutdown();
            }
            base.OnClosing(e);
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Add window = new Add(myDataGrid);
            window.Show();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var db = new PracticeNewEntities();
            if (myDataGrid.SelectedItems.Count == 1)
            {
                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить запись {((Order)myDataGrid.SelectedItem).uniquename}?", "Удалить", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    db.Orders.RemoveRange(db.Orders.Where(e1 => e1.ID == ((Order)myDataGrid.SelectedItem).ID));
                }
            }
            else if (myDataGrid.SelectedItems.Count > 1)
            {
                double? temp = 0;
                for(int i = 0; i < myDataGrid.SelectedItems.Count; i++)
                {
                    temp += ((Order)myDataGrid.SelectedItems[i]).sum;
                }
                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить {myDataGrid.SelectedItems.Count} записи(ей) со стоимостью {temp}?", "Удалить", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    for (int i = 0; i < myDataGrid.SelectedItems.Count; i++)
                    {
                        int temp1 = ((Order)myDataGrid.SelectedItems[i]).ID;
                        db.Orders.RemoveRange(db.Orders.Where(e1 => e1.ID == temp1));
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы ничего не выбрали", "ОШИБКА");
            }
            db.SaveChanges();
            myDataGrid.ItemsSource = db.Orders.ToList();
        }

        private void clearBTN_Click(object sender, RoutedEventArgs e)
        {
            var db = new PracticeNewEntities();
            myDataGrid.ItemsSource = db.Orders.ToList();
        }

        private void chooseBTN_Click(object sender, RoutedEventArgs e)
        {
            var filterValues = PracticeNewEntities.GetContext().Orders.ToList();

            var selectedCategory = mainCOMBO.SelectedItem;

            if (selectedCategory != null)
            {
                filterValues = filterValues.Where(x => x.category == ((Category)selectedCategory).Id_category).ToList();
            }

            if (datePickFrom.SelectedDate != null) 
            {
                filterValues = filterValues.Where(x => x.data > datePickFrom.SelectedDate).ToList();
            }

            if (datePickTo.SelectedDate != null)
            {
                filterValues = filterValues.Where(x => x.data < datePickTo.SelectedDate).ToList();
            }
            myDataGrid.ItemsSource = filterValues;

        }

        private void adminBTN_Click(object sender, RoutedEventArgs e)
        {
            Admin window = new Admin();
            window.Show();
        }
    }
}
