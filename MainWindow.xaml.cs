using System;
using System.Collections.Generic;
using System.Data;
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

using Lab4_DS.MakdoknekDataSetTableAdapters;

namespace Lab4_DS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClientTableAdapter client = new ClientTableAdapter();
        MenuTableAdapter menu = new MenuTableAdapter();
        BookingTableAdapter booking = new BookingTableAdapter();

        bool ClientTableIsEnabled = false;
        bool MenuTableIsEnabled = false;
        bool BookingTableIsEnabled = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // поиск
        {
            if (ClientTableIsEnabled == true)
                ClientDgr.ItemsSource = client.SearchClientByName(SearchTxt.Text);
            if (MenuTableIsEnabled == true)
                ClientDgr.ItemsSource = menu.SearchMenuByName(SearchTxt.Text);
            if (BookingTableIsEnabled == true)
                ClientDgr.ItemsSource = booking.SearchBookingByDate(SearchTxt.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // клиенты
        {
            ClientDgr.ItemsSource = client.GetData();

            ClientTableIsEnabled = true;
            MenuTableIsEnabled = false;
            BookingTableIsEnabled = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // меню
        {
            ClientDgr.ItemsSource = menu.GetData();

            ClientTableIsEnabled = false;
            MenuTableIsEnabled = true;
            BookingTableIsEnabled = false;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) // заказы
        {
            ClientDgr.ItemsSource = booking.GetData();

            ClientCbx.ItemsSource = client.GetData();
            ClientCbx.DisplayMemberPath = "Client_sName";

            ClientTableIsEnabled = false;
            MenuTableIsEnabled = false;
            BookingTableIsEnabled = true;
        }

        private void ClientCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientCbx.SelectedItem != null)
            {
                var id = (int)(ClientCbx.SelectedItem as DataRowView).Row[0];
                ClientDgr.ItemsSource = booking.FilterBookingByName(id);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) // очистка
        {
            ClientDgr.ItemsSource = booking.GetData();
        }
    }
}