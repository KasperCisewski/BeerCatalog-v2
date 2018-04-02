using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace KatalogPiw.Services
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();

    }
}
