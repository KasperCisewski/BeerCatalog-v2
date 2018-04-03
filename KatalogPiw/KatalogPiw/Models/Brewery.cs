using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace KatalogPiw.Models
{
    public class Brewery
    {
        [PrimaryKey, AutoIncrement]
        public int BreweryID { get; set; }
        public string BreweryName { get; set; }

        [OneToMany]
        public List<Beer> Beers { get; set; }
    }
}
