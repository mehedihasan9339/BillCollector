using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BillCollector.Models;
using BillCollector.Services;

namespace BillCollector.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private int _totalCustomers;
        private decimal _totalRevenue;
        private decimal _thisMonthRevenue;

        public int TotalCustomers
        {
            get => _totalCustomers;
            set
            {
                _totalCustomers = value;
                OnPropertyChanged();
            }
        }

        public decimal TotalRevenue
        {
            get => _totalRevenue;
            set
            {
                _totalRevenue = value;
                OnPropertyChanged();
            }
        }

        public decimal ThisMonthRevenue
        {
            get => _thisMonthRevenue;
            set
            {
                _thisMonthRevenue = value;
                OnPropertyChanged();
            }
        }

        public DashboardViewModel()
        {
            LoadData();
        }

        public async void LoadData()
        {
            await LoadCustomerCount();
            await LoadTotalRevenue();
            await LoadThisMonthRevenue();
        }

        public async Task LoadCustomerCount()
        {
            TotalCustomers = (await App.Database.GetCustomersAsync()).Count;
        }

        public async Task LoadTotalRevenue()
        {
            TotalRevenue = await App.Database.GetTotalRevenueAsync();
        }

        public async Task LoadThisMonthRevenue()
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endDate = startDate.AddMonths(1).AddTicks(-1);

            ThisMonthRevenue = await App.Database.GetRevenueForMonthAsync(DateTime.Now.Year, DateTime.Now.Month);
        }
    }
}
