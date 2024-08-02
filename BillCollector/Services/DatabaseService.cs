using Firebase.Database;
using Firebase.Database.Query;
using BillCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillCollector.Services
{
    public class DatabaseService
    {
        private readonly FirebaseClient _firebaseClient;

        public DatabaseService(string firebaseUrl, string authSecret)
        {
            _firebaseClient = new FirebaseClient(
                firebaseUrl,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(authSecret)
                });
        }

        // Initialize Database (if needed, can be used to set up initial data)
        private async Task InitializeDatabaseAsync()
        {
            // You can add initial setup code here if needed
        }

        // Customers
        public async Task<List<Customer>> GetCustomersAsync()
        {
            var customers = await _firebaseClient
                .Child("Customers")
                .OnceAsync<Customer>();

            return customers.Select(item => item.Object).ToList();
        }

        public async Task SaveCustomerAsync(Customer customer)
        {
            await _firebaseClient
                   .Child("Customers")
                   .PostAsync(customer);
        }

        // Bills
        public async Task<List<Bill>> GetBillsByCustomerIdAsync(string customerId)
        {
            var bills = await _firebaseClient
            .Child("Bills")
            .OnceAsync<Bill>();

            return bills.Select(item => item.Object).ToList();
        }
        // Bills
        public List<Bill> GetBills(string customerId)
        {
            var bills = _firebaseClient
                .Child("Bills")
                .OnceAsync<Bill>()
                .Result;

            return bills.Where(b => b.Object.CustomerId == customerId)
                        .Select(b => new Bill
                        {
                            Id = b.Key,
                            CustomerId = b.Object.CustomerId,
                            BillDate = b.Object.BillDate,
                            Amount = b.Object.Amount
                        }).ToList();
        }

        public void SaveBill(Bill bill)
        {
            var result = _firebaseClient
                    .Child("Bills")
                    .PostAsync(bill)
                    .Result;
            bill.Id = result.Key;
        }
        public async Task SaveBillAsync(Bill bill)
        {
            if (string.IsNullOrEmpty(bill.Id.ToString())) // If bill doesn't have an ID, create a new one
            {
                var result = await _firebaseClient
                    .Child("Bills")
                    .PostAsync(bill);
                bill.Id = result.Key;
            }
            else // If bill has an ID, update it
            {
                await _firebaseClient
                    .Child("Bills")
                    .Child(bill.Id.ToString())
                    .PutAsync(bill);
            }
        }

        // Revenue
        public async Task<decimal> GetTotalRevenueAsync()
        {
            var bills = await _firebaseClient
                .Child("Bills")
                .OnceAsync<Bill>();

            return bills.Sum(item => item.Object.Amount);
        }

        public async Task<decimal> GetRevenueForMonthAsync(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddTicks(-1);

            var bills = await _firebaseClient
                .Child("Bills")
                .OnceAsync<Bill>();

            return bills
                .Where(item => item.Object.BillDate >= startDate && item.Object.BillDate <= endDate)
                .Sum(item => item.Object.Amount);
        }



    }
}
