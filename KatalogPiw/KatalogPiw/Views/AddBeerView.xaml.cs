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
                vm.AddBeer(BeerName.Text, (Models.Brewery)BreweryPicker.SelectedItem, netPriceWD,netPriceD ,(Models.Type)TypePicker.SelectedItem, Parameters.Text, Description.Text);
                //DisplayAlert("Dodano piwo", $"piwo o nazwie {piwo.NazwaPiwa} wyprodukowane w browarze {piwo.Browar.NazwaBrowaru} gatunku {piwo.Gatunek.NazwaGatunku} ", "ok");
            }
        }
    }
}