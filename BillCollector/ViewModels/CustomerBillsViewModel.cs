using BillCollector.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BillCollector.ViewModels
{
    public class CustomerBillsViewModel : BaseViewModel
    {
        public ObservableCollection<Bill> Bills { get; }

        public CustomerBillsViewModel(Customer customer)
        {
            Bills = new ObservableCollection<Bill>();
            LoadBills(customer);
        }

        public async void LoadBills(Customer customer)
        {
            var bills = await App.Database.GetBillsByCustomerIdAsync(customer.Id);
            var sortedBills = bills.OrderBy(b => b.BillDate).ToList(); // Sort bills by BillDate
            foreach (var bill in sortedBills)
            {
                Bills.Add(bill);
            }
        }
    }
}
