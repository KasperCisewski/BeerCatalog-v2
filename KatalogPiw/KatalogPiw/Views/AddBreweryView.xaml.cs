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
	public partial class AddBreweryView : ContentPage
	{
        AddBreweryViewModel vm;
		public AddBreweryView ()
		{
            Init();
		}

        private void Init()
        {
            vm = new AddBreweryViewModel();
            BindingContext = vm;
            InitializeComponent();
        }
        private async void buttonSaveBrewery_Click(object sender, EventArgs e)
        {
            if (BreweryName.Text == "Nazwa Browaru")
            {
                DisplayAlert("blad", "powinienes wpisac nazwe browaru", "OK");
            }
            else
            {
                string Nazwa = BreweryName.Text;
                vm.AddBreweryByName(Nazwa);
               // Init();
            }
        }

        private async void OnEdit(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditorView(sender));
            Init();
        }


        private async void OnDelete(object Sender, EventArgs e)
        {
            vm.DeleteBreweryByObject(Sender);
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