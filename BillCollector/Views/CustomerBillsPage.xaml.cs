using BillCollector.Models;
using BillCollector.ViewModels;

namespace BillCollector.Views
{
    public partial class CustomerBillsPage : ContentPage
    {
        public CustomerBillsPage(Customer customer)
        {
            InitializeComponent();
            // Set the BindingContext to the view model with the passed customer
            BindingContext = new CustomerBillsViewModel(customer);
        }
    }
}
