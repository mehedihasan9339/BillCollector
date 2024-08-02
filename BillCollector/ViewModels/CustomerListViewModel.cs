using BillCollector.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.ApplicationModel; // Ensure you have this namespace

namespace BillCollector.ViewModels
{
    public class CustomerListViewModel : BaseViewModel
    {
        public ObservableCollection<Customer> Customers { get; }
        public ICommand CallCommand { get; }

        public CustomerListViewModel()
        {
            Customers = new ObservableCollection<Customer>();
            LoadCustomers();
            CallCommand = new Command<string>(OnCall);
        }

        private async void LoadCustomers()
        {
            var customers = await App.Database.GetCustomersAsync();
            foreach (var customer in customers)
            {
                Customers.Add(customer);
            }
        }

        private async void OnCall(string phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                try
                {
                    // Launch the phone dialer with the given phone number
                    await Launcher.OpenAsync(new Uri($"tel:{phoneNumber}"));
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., if dialer is not available)
                    // You can use a dialog to show the error or log it
                    await Application.Current.MainPage.DisplayAlert("Error", $"Unable to make a call: {ex.Message}", "OK");
                }
            }
        }
    }
}
