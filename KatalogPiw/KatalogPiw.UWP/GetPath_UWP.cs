using KatalogPiw.Services;
using KatalogPiw.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(GetPath_UWP))]
namespace KatalogPiw.UWP
{
    public class GetPath_UWP:IGetPath
    {
        public string GetPath(string fileName)
        {
            var path = Path.Combine(ApplicationData.
         Current.LocalFolder.Path, fileName);
            return path;
        }
    }
}
