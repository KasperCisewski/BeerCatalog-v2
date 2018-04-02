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

        private async void buttonCreatePDF_Click(object sender, TextChangedEventArgs e)
        {
            vm.CreatePDF();


        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //    if (e.Item == null)
            //        return;

            //    await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //    //Deselect Item
            //    ((ListView)sender).SelectedItem = null;
        }

       
    }
}