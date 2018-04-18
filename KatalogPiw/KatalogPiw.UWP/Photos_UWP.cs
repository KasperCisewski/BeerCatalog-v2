using KatalogPiw.Services;
using KatalogPiw.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(Photos_UWP))]
namespace KatalogPiw.UWP
{
    public class Photos_UWP:IPhotos
    {
        public string GetPath(string photoName)
        {
            var path = Path.Combine(ApplicationData.
              Current.LocalFolder.Path, photoName);
            return path;
        }
    }
}
