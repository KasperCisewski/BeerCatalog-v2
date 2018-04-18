using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KatalogPiw.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddTypeView : ContentPage
	{
        ViewModels.AddTypeViewModel vm;
        public AddTypeView()
        {
            vm = new ViewModels.AddTypeViewModel();
            BindingContext = vm;
            InitializeComponent();
        }

        private async void buttonSaveType_Click(object sender, EventArgs e)
        {
            if (TypeName.Text == "")
            {
                DisplayAlert("blad", "powinienes wpisac nazwe gatunku", "OK");
            }
            else
            {
                string name = TypeName.Text;
                string foodParing = FoodParing.Text;
                vm.AddTypeByName(name,foodParing);
            }
        }
        private async void OnEdit(object sender, EventArgs e)
        {
            TypeList.BeginRefresh();
            await Navigation.PushAsync(new EditorView(sender));
            Initalize();
            TypeList.EndRefresh();
        }
        private void Initalize()
        {
            vm = new ViewModels.AddTypeViewModel();
            BindingContext = vm;
            InitializeComponent();
        }
        private async void OnDelete(object Sender, EventArgs e)
        {
            vm.DeleteTypeByObject(Sender);

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