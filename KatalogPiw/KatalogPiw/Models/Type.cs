using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace KatalogPiw.Models
{
    public class Type
    {
        [PrimaryKey, AutoIncrement]
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public string FoodParing { get; set; }
        public bool IsSelected { get; set; }
        [OneToMany]
        public List<Beer> Beers { get; set; }
    }
}
