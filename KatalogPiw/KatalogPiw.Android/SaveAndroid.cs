using System;
using System.Collections.Generic;
using Android.Content;
using Xamarin.Forms;
using System.Threading.Tasks;
using Java.IO;
using System.IO;
using KatalogPiw.Droid;
using KatalogPiw.Services;

[assembly: Dependency(typeof(SaveAndroid))]
namespace KatalogPiw.Droid
{
    public class SaveAndroid
    {
        public async Task SaveTextAsync(string fileName, String contentType, MemoryStream s)
        {
            string root = null;
            if (Android.OS.Environment.IsExternalStorageEmulated)
            {
                root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            }
            else
                root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            Java.IO.File myDir = new Java.IO.File(root + "/Syncfusion");
            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, fileName);

            if (file.Exists()) file.Delete();

            try
            {
                FileOutputStream outs = new FileOutputStream(file);
                outs.Write(s.ToArray());

                outs.Flush();
                outs.Close();

            }
            catch (Exception e)
            {

            }
            if (file.Exists())
            {
                Android.Net.Uri path = Android.Net.Uri.FromFile(file);
                string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(path, mimeType);
                Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose App"));
            }
        }
        }
}