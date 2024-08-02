using BillCollector.ViewModels;

namespace BillCollector
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new AppShellViewModel();
        }

        private async void OnFooterLabelTapped(object sender, EventArgs e)
        {
            await Launcher.Default.OpenAsync(new Uri("mailto:mehedihasan9339@gmail.com"));
        }
    }
}
