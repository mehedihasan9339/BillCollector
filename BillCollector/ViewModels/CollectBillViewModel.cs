using BillCollector.Models;
using BillCollector.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BillCollector.ViewModels
{
    public class CollectBillViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;
        private ObservableCollection<Customer> _customers;
        private ObservableCollection<Bill> _bills;
        private Customer _selectedCustomer;
        private DateTime _billDate;
        private string _amount;
        private string _amountWarning;
        private bool _isWarningVisible;

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set
            {
                if (_customers != value)
                {
                    _customers = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Bill> Bills
        {
            get => _bills;
            set
            {
                if (_bills != value)
                {
                    _bills = value;
                    OnPropertyChanged();
                }
            }
        }

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    OnPropertyChanged();
                    LoadBills();
                }
            }
        }

        public DateTime BillDate
        {
            get => _billDate;
            set
            {
                if (_billDate != value)
                {
                    _billDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AmountWarning
        {
            get => _amountWarning;
            set
            {
                if (_amountWarning != value)
                {
                    _amountWarning = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsWarningVisible
        {
            get => _isWarningVisible;
            set
            {
                if (_isWarningVisible != value)
                {
                    _isWarningVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public Command SaveCommand { get; }

        public CollectBillViewModel()
        {
            _databaseService = App.Database;
            LoadCustomers();
            BillDate = DateTime.Now;
            SaveCommand = new Command(OnSave);
        }

        public void LoadCustomers()
        {
            var customers = Task.Run(() => _databaseService.GetCustomersAsync()).Result;
            Customers = new ObservableCollection<Customer>(customers);
        }

        private void LoadBills()
        {
            if (SelectedCustomer != null)
            {
                var bills = Task.Run(() => _databaseService.GetBills(SelectedCustomer.Id)).Result;
                Bills = new ObservableCollection<Bill>(bills);
            }
        }

        public void OnSave()
        {
            if (SelectedCustomer == null || !decimal.TryParse(Amount, out decimal parsedAmount))
            {
                AmountWarning = "Please select a customer and enter a valid amount.";
                IsWarningVisible = true;
                return;
            }

            try
            {
                var newBill = new Bill
                {
                    CustomerId = SelectedCustomer.Id,
                    BillDate = BillDate,
                    Amount = parsedAmount
                };

                Task.Run(() => _databaseService.SaveBill(newBill)).Wait();

                // Optionally, clear the form
                BillDate = DateTime.Now;
                Amount = string.Empty;

                // Optionally, refresh the bills list
                LoadBills();
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                AmountWarning = $"Error saving bill: {ex.Message}";
                IsWarningVisible = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
