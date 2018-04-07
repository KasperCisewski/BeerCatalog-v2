using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using KatalogPiw.ViewModels;
using KatalogPiw.Models;
using System.Collections.ObjectModel;

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
            // BeerList.BeginRefresh();

            //if (string.IsNullOrWhiteSpace(e.NewTextValue))
            //    BeerList.ItemsSource = vm.Beers;
            //else
            //{
            //    BeerList.ItemsSource = vm.Beers.Where(i => (i.BrewerName.ToLower().Contains(e.NewTextValue.ToLower())
            //        || (i.TypeName.ToLower().Contains(e.NewTextValue.ToLower()))));               
            //}

            if (SearchBar.Text == null)
            {
                SearchBar.Text = "";
            }

            BeerList.ItemsSource = vm.FiltringBeers(SearchBar.Text, BreweryList.SelectedItem, TypeList.SelectedItem, Slider.Value);
            //BeerList.EndRefresh();

        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            // BeerList.BeginRefresh();

            if (SearchBar.Text == null)
            {
                SearchBar.Text = "";
            }


            BeerList.ItemsSource = vm.FiltringBeers(SearchBar.Text, BreweryList.SelectedItem, TypeList.SelectedItem, Slider.Value);


            //BeerList.ItemsSource = vm.Beers.Where(i => (i.Quantity>(int)e.NewValue));
          //  BeerList.EndRefresh();

        }

        private async void buttonSelectAllInList_Click(object sender,TextChangedEventArgs e)
        {
            if(SearchBar.Text==null)
            {
                SearchBar.Text = "";
            }

              vm.SelectAllBeers(SearchBar.Text,BreweryList.SelectedItem,TypeList.SelectedItem,Slider.Value);
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

        private async void ClearTypeContext(object Sender, EventArgs e)
        {

            TypeList.SelectedItem = null;
        }

        private async void ClearBreweryContext(object sender,EventArgs e)
        {
            BreweryList.SelectedItem = null;
        }

        async void SelectType_ItemSelect(object sender,SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {

                return;
            }

            //Models.Type type = (Models.Type)((ListView)sender).SelectedItem;
            //type.IsSelected = true;
            //vm.UpdateTypeList(type);


            //BeerList.ItemsSource = vm.Beers.Where(i => (i.TypeID == type.TypeID));

            //// BeerList.ItemsSource=vm.Beers.Any(i=>i.TypeID==i.Types)
            //// BeerList.ItemsSource = vm.Beers.Where(i => (i.TypeID == i.Types.Where(p => (p.TypeID == i.TypeID && p.IsSelected == true))));
            //// BeerList.ItemsSource = vm.Beers.Where(i => (i.Types.Where(t => (t.IsSelected == true))));
            //// BeerList.ItemsSource = vm.Beers.Where(i => (i.TypeName.ToList() == vm.TypeList.Where(t => (t.IsSelected == true))));
            ////BeerList.ItemsSource=vm.Beers.Where(i=>(i.TypeName.Contains(vm.TypeList.Where(t=>(t)))
            if (SearchBar.Text == null)
            {
                SearchBar.Text = "";
            }

            //if (BreweryList.SelectedItem == null && TypeList.SelectedItem == null)
            //{


            //    BeerList.ItemsSource = vm.Beers.Where(i => ((i.BrewerName.ToLower().Contains(SearchBar.Text.ToLower())
            //        || (i.TypeName.ToLower().Contains(SearchBar.Text.ToLower())))) && (i.Quantity > (int)Slider.Value)).ToList();
            //}
            //else if (BreweryList.SelectedItem == null)
            //{
            //    Models.Type type = (Models.Type)TypeList.SelectedItem;

            //    BeerList.ItemsSource = vm.Beers.Where(i => ((i.BrewerName.ToLower().Contains(SearchBar.Text.ToLower())
            //        || (i.TypeName.ToLower().Contains(SearchBar.Text.ToLower())))) && (i.Quantity > (int)Slider.Value) && (i.TypeID == type.TypeID)).ToList();
            //}
            //else if (TypeList.SelectedItem == null)
            //{
            //    Brewery brewery = (Brewery)BreweryList.SelectedItem;
            //    BeerList.ItemsSource = vm.Beers.Where(i => ((i.BrewerName.ToLower().Contains(SearchBar.Text.ToLower())
            //        || (i.TypeName.ToLower().Contains(SearchBar.Text.ToLower())))) && (i.Quantity > (int)Slider.Value) && (i.BreweryID == brewery.BreweryID)).ToList();
            //}
            //else
            //{

            //    Brewery brewery = (Brewery)BreweryList.SelectedItem;
            //    Models.Type type = (Models.Type)TypeList.SelectedItem;
            //    BeerList.ItemsSource = vm.Beers.Where(i => ((i.BrewerName.ToLower().Contains(SearchBar.Text.ToLower())
            //        || (i.TypeName.ToLower().Contains(SearchBar.Text.ToLower())))) && (i.Quantity > (int)Slider.Value) && (i.BreweryID == brewery.BreweryID) && (i.TypeID == type.TypeID)).ToList();
            //}

            BeerList.ItemsSource = vm.FiltringBeers(SearchBar.Text, BreweryList.SelectedItem, TypeList.SelectedItem, Slider.Value);


        }
        async void SelectBrewery_ItemSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            if (SearchBar.Text == null)
            {
                SearchBar.Text = "";
            }


            BeerList.ItemsSource = vm.FiltringBeers(SearchBar.Text, BreweryList.SelectedItem, TypeList.SelectedItem, Slider.Value);

            //Brewery brewery = (Brewery)((ListView)sender).SelectedItem;
            //brewery.IsSelected = true;
            ////vm.UpdateBreweryList(brewery);
            //BeerList.ItemsSource = (vm.Beers.Where(i => (i.BreweryID == brewery.BreweryID)));
        }
    }
}