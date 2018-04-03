using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using KatalogPiw.ViewModels;
namespace KatalogPiw.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShowBeerListView : ContentPage
	{
        ShowBeerListViewModel vm;
		public ShowBeerListView ()
		{
            vm = new ShowBeerListViewModel();
            BindingContext = vm;
			InitializeComponent ();
		}

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            BeerList.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                BeerList.ItemsSource = vm.Beers;
            else
            {
                BeerList.ItemsSource = vm.Beers.Where(i => (i.BrewerName.ToLower().Contains(e.NewTextValue.ToLower())
                    || (i.TypeName.ToLower().Contains(e.NewTextValue.ToLower()))));               
            }
            BeerList.EndRefresh();
        }

        private async void buttonSelectAllInList_Click(object sender,TextChangedEventArgs e)
        {
            vm.SelectAllBeers(SearchBar.Text);
        }

        private async void buttonCreatePriceListA_Click(object sender, TextChangedEventArgs e)
        {
            vm.CreatePriceListA();
        }
        private async void buttonCreatePriceListB_Click(object sender, TextChangedEventArgs e)
        {
            vm.CreatePriceListB();
        }

        private async void buttonCreatePriceListC_Click(object sender, TextChangedEventArgs e)
        {
            vm.CreatePriceListC();
        }


        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }  
    }
}