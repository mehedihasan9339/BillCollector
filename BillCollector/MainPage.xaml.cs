using System.Collections.ObjectModel;
using BillCollector.Models;
using Microsoft.Maui.Controls;

namespace BillCollector
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Customer> Customers { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Customers = new ObservableCollection<Customer>();
            CustomerListView.ItemsSource = Customers;
        }

        private void OnAddCustomerClicked(object sender, EventArgs e)
        {
            var customer = new Customer
            {
                Name = CustomerNameEntry.Text,
                Phone = CustomerPhoneEntry.Text,
                Email = CustomerEmailEntry.Text
            };

            Customers.Add(customer);

            // Clear the entries
            CustomerNameEntry.Text = string.Empty;
            CustomerPhoneEntry.Text = string.Empty;
            CustomerEmailEntry.Text = string.Empty;
        }
    }
}
