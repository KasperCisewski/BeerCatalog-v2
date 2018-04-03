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
	public partial class EditorView : ContentPage
	{
        ViewModels.EditorViewModel vm;
		public EditorView (object sender)
		{
            vm = new ViewModels.EditorViewModel(sender);
            BindingContext = vm;
			InitializeComponent ();
		}

        public void OnSaveClick(object sender, EventArgs e)
        {
            if (NewObjectName.Text == "Nowa nazwa")
            {
                DisplayAlert("Error", "Zmien nazwe", "OK");
            }
            else
            {
                string newName = NewObjectName.Text;
                vm.UpdateObject(newName);
                DisplayAlert("Uwaga", "Zmieniono nazwe, aby zobaczyc zmiany cofnij", "OK");
            }
        }
    }
}