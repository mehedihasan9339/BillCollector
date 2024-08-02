using BillCollector.ViewModels;
using Microsoft.Maui.Controls;

namespace BillCollector.Views
{
    public partial class CollectBillPage : ContentPage
    {
        public CollectBillPage()
        {
            InitializeComponent();
        }

        private void OnAmountTextChanged(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(e.NewTextValue, out _))
            {
                // Valid number input
                // Clear warning message
                (BindingContext as CollectBillViewModel).AmountWarning = string.Empty;
                (BindingContext as CollectBillViewModel).IsWarningVisible = false;
            }
            else
            {
                // Invalid input, show a warning
                (BindingContext as CollectBillViewModel).AmountWarning = "Please enter a valid number.";
                (BindingContext as CollectBillViewModel).IsWarningVisible = true;
            }
        }
    }
}
