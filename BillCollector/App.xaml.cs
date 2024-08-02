using System;
using BillCollector.Services;

namespace BillCollector
{
    public partial class App : Application
    {
        static DatabaseService databaseService;

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        public static DatabaseService Database
        {
            get
            {
                if (databaseService == null)
                {
                    // Firebase configuration
                    string firebaseUrl = "https://billcollectordb-default-rtdb.firebaseio.com/"; // Replace with your Firebase Database URL
                    string authSecret = "a558a0f5cd9ef843b1072253b953d3ce4cd93950"; // Replace with your Firebase Database Secret

                    databaseService = new DatabaseService(firebaseUrl, authSecret);
                }
                return databaseService;
            }
        }
    }
}
