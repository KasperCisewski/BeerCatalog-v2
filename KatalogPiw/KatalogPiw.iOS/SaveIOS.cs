using System;
using UIKit;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.IO;

using QuickLook;
using KatalogPiw.iOS;
using KatalogPiw.Services;

[assembly: Dependency(typeof(SaveIOS))]
namespace KatalogPiw.iOS
{
    public class SaveIOS:ISave
    {
        public async Task SaveTextAsync(string filename, string contentType, MemoryStream s)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(path, filename);
            try
            {
                FileStream fileStream = File.Open(filePath, FileMode.Create);
                s.Position = 0;
                s.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Close();
            }
            catch (Exception e)
            {

            }

            UIViewController currentController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            while (currentController.PresentedViewController != null)
                currentController = currentController.PresentedViewController;
            UIView currentView = currentController.View;

            QLPreviewController qlPreview = new QLPreviewController();
            QLPreviewItem item = new QLPreviewItemBundle(filename, filePath);
            qlPreview.DataSource = new PreviewControllerDS(item);

            currentController.PresentViewController(qlPreview, true, null);


        }
    }
}