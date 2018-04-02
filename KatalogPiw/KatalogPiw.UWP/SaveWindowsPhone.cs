using KatalogPiw.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Windows.Storage;
using KatalogPiw.Services;

[assembly: Dependency(typeof(SaveWindowsPhone))]
namespace KatalogPiw.UWP
{
    public class SaveWindowsPhone:ISave
    {
        public async Task SaveTextAsync(string filename, string contentType, MemoryStream s)
        {
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile outFile = await local.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            using (Stream outStream = await outFile.OpenStreamForWriteAsync())
            {
                outStream.Write(s.ToArray(), 0, (int)s.Length);

            }
            await Windows.System.Launcher.LaunchFileAsync(outFile);
        }
    }
}
