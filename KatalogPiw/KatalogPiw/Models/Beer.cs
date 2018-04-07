using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace KatalogPiw.Models
{
    public class Beer
    {
        [PrimaryKeyAttribute, AutoIncrement]
        public int ID { get; set; }
        public string EanCode { get; set; }
        public string BeerName { get; set; }
        [ForeignKey(typeof(Brewery))]
        public int BreweryID { get; set; }
        [ManyToOne]
        public List<Brewery> Breweries { get; set; }
        public string BrewerName { get; set; }
        public double NetPriceWithoutDiscout { get; set; }
        public double NetPriceWithDiscout { get; set; }
        public int Quantity { get; set; }
       // public double[] PriceListBeers = new double[3];
        public double PriceListA { get; set; }
        [ForeignKey(typeof(Type))]
        public int TypeID { get; set; }
        [ManyToOne]
        public List<Type> Types { get; set; }
        public string TypeName { get; set; }
        public string Parameters { get; set; }
        public string Plato { get; set; }
        public string Description { get; set; }
        public string FoodParing { get; set; }
        public string PhotoPath { get; set; }
        // ImageSource imageSource;
        public bool IsSelect { get; set; }

        public Beer()
        {

        }

        public Beer(string BeerName,Brewery Brew, double NetPriceWithoutDiscout, double NetPriceWithDiscout, Type Type, string Parameters, string Description, string FoodParing)
        {
            this.BeerName = BeerName;
            this.BreweryID=Brew.BreweryID;
            this.NetPriceWithoutDiscout = NetPriceWithoutDiscout;
            this.NetPriceWithDiscout=NetPriceWithDiscout;
            this.TypeID = Type.TypeID;
            this.Parameters = Parameters;
            this.Description = Description;
            this.FoodParing = FoodParing;
        }
    }
}
