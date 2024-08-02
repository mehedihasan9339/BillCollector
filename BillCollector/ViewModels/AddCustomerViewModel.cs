using BillCollector.Models;
using System.Windows.Input;
using System.Threading.Tasks;

namespace BillCollector.ViewModels
{
    public class AddCustomerViewModel : BaseViewModel
    {
        private string _name;
        private string _phone;
        private string _email;
        private string _customerCode;
        private string _wifiName;
        private string _wifiKey;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string CustomerCode
        {
            get => _customerCode;
            set
            {
                _customerCode = value;
                OnPropertyChanged();
            }
        }

        public string WifiName
        {
            get => _wifiName;
            set
            {
                _wifiName = value;
                OnPropertyChanged();
            }
        }

        public string WifiKey
        {
            get => _wifiKey;
            set
            {
                _wifiKey = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        public AddCustomerViewModel()
        {
            SaveCommand = new Command(async () => await OnSave());
        }

        private async Task OnSave()
        {
            try
            {
                var newCustomer = new Customer
                {
                    Id = DateTime.Now.Ticks.ToString(),
                    Name = Name,
                    Phone = Phone,
                    Email = Email,
                    CustomerCode = CustomerCode,
                    WifiName = WifiName,
                    WifiKey = WifiKey
                };

                await App.Database.SaveCustomerAsync(newCustomer);

                // Clear fields after saving
                Name = string.Empty;
                Phone = string.Empty;
                Email = string.Empty;
                CustomerCode = string.Empty;
                WifiName = string.Empty;
                WifiKey = string.Empty;

                // Optionally navigate back or show a confirmation message
            }
            catch (Exception ex)
            {
                // Handle error (you can log it or display a message to the user)
            }
        }
    }
}
