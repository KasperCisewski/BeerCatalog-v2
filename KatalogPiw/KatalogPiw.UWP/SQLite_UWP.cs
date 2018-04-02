using KatalogPiw.Services;
using KatalogPiw.UWP;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_UWP))]
namespace KatalogPiw.UWP
{
    public class SQLite_UWP : ISQLite
    {

  
        public SQLiteConnection GetConnection()
        {

            var dbName = "KatalogPiwDb.db3";
            var path = Path.Combine(ApplicationData.
              Current.LocalFolder.Path, dbName);
            return new SQLiteConnection(path);

        }
    }
}
