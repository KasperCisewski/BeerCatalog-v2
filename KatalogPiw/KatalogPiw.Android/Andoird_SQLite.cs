using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KatalogPiw.Services;
using SQLite;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace KatalogPiw.Droid
{
    public class Andoird_SQLite:ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var dbName = "Members.sqlite";
            var dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var path = System.IO.Path.Combine(dbPath, dbName);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}