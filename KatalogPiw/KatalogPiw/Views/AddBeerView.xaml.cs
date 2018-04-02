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
	public partial class AddBeerView : ContentPage
	{

        AddBeerViewModel vm;
        public AddBeerView()
        {
            vm = new AddBeerViewModel();
            BindingContext = vm;
            InitializeComponent();
        }



        private async void buttonRefreshButton_Click(object sender, TextChangedEventArgs e)
        {
            vm = new AddBeerViewModel();
            BindingContext = vm;
            InitializeComponent();
        }


        private async void buttonSaveBeer_Click(object sender, TextChangedEventArgs e)
        {

            if (BeerName.Text==null)
            {
                DisplayAlert("blad", "powinienes wpisac nazwe piwa", "OK");
            }
            else
            {
                double netPriceWD = Convert.ToDouble(NetPriceWD.Text);
                double netPriceD = Convert.ToDouble(NetPriceD.Text);
                vm.AddBeer(BeerName.Text, (Models.Brewery)BreweryPicker.SelectedItem, netPriceWD,netPriceD ,(Models.Type)TypePicker.SelectedItem, Parameters.Text, Description.Text, FoodParing.Text);
                //DisplayAlert("Dodano piwo", $"piwo o nazwie {piwo.NazwaPiwa} wyprodukowane w browarze {piwo.Browar.NazwaBrowaru} gatunku {piwo.Gatunek.NazwaGatunku} ", "ok");
            }
        }
        private async void buttonShowList_Click(object sender, TextChangedEventArgs e)
        {

            if (App.Database.GetBeers().Count == 0)
            {
                DisplayAlert("Error", "Nie masz zadnych piw w liscie, dodaj piwo!", "OK");

            }
            else
            {
                await Navigation.PushAsync(new ShowBeerListView());
            }

        }
        private async void buttonLoadFile_Click(object sender, TextChangedEventArgs e)
        {
            await Navigation.PushAsync(new FileOpenView());
        }
    }
}