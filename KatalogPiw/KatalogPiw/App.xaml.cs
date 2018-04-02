using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KatalogPiw.Views;
using KatalogPiw.Services;

using Xamarin.Forms;

namespace KatalogPiw
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        static BeerDatabase database;

        public static BeerDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new BeerDatabase();

                }
                return database;
            }
        }

        static BeerDatabase databaseToAllRecords;
    }
}
