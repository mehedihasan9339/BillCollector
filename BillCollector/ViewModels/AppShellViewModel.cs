using System;
using System.Windows.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace BillCollector.ViewModels
{
    public class AppShellViewModel
    {
        public ICommand OpenMailCommand { get; }

        public AppShellViewModel()
        {
            OpenMailCommand = new Command(async () =>
            {
                var email = new EmailMessage
                {
                    Subject = "Contact",
                    Body = "Please enter your message here",
                    To = new List<string> { "mehedihasan9339@gmail.com" }
                };

                await Email.ComposeAsync(email);
            });
        }
    }
}
