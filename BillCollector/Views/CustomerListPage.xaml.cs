using Microsoft.Maui.Controls;
using BillCollector.Models;

namespace BillCollector.Views
{
    public partial class CustomerListPage : ContentPage
    {
        public CustomerListPage()
        {
            InitializeComponent();
        }

        async void OnCustomerTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var customer = e.Item as Customer;
            await Navigation.PushAsync(new CustomerBillsPage(customer));

            // Deselect the item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
