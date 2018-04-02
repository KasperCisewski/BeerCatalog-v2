using KatalogPiw.ViewModels;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
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
	public partial class FileOpenView : ContentPage
	{
        FileOpenViewModel vm;
		public FileOpenView ()
		{
            vm = new FileOpenViewModel();
            BindingContext = vm;
			InitializeComponent ();
		}
        private async void Button_Click(object sender, EventArgs e)
        {
            try
            {
                FileData fileData = new FileData();

                fileData = await CrossFilePicker.Current.PickFile();

                byte[] data = fileData.DataArray;

                string name = fileData.FileName;
                vm.LoadFile(name);
                string readText = data.ToString();
                //string filePath = fileData.FilePath;

            }
            catch (Exception ex)
            {



            }
        }
    }
}