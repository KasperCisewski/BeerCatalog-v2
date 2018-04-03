using KatalogPiw.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace KatalogPiw.Services
{
    public class BeerDatabase
    {
        private SQLiteConnection database;

        static object locker = new object();

        public BeerDatabase()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Brewery>();
            database.CreateTable<Models.Type>();
            database.CreateTable<Beer>();
        }

        public int SaveBeer(Beer beer)
        {
            database.CreateTable<Beer>();

            lock (locker)
            {
                if (beer.ID != 0)
                {
                    return beer.ID;
                }
                else
                {
                    return database.Insert(beer);
                }
            }
        }

        public int SaveBrewery(Brewery Brewery)
        {
            database.CreateTable<Brewery>();

            lock (locker)
            {
                if (Brewery.BreweryID != 0)
                {
                    return Brewery.BreweryID;
                }
                else if(IsBrewerInList(Brewery.BreweryName)==false)
                {
                    return database.Insert(Brewery);
                }
                else
                {
                    return Brewery.BreweryID;
                }
            }
        }

        public bool IsBrewerInList(string brewerName)
        {
            List<Brewery> breweryList = GetBreweries();
            for(int i=0;i<breweryList.Count;i++)
            {
                if(breweryList[i].BreweryName.ToUpper()==brewerName.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        public void UpdateBrewery(Brewery nowyBrewery)
        {

            database.Update(nowyBrewery);
        }

        public int DeleteType(Models.Type Type)
        {
            return database.Delete(Type);
        }

        public int SaveType(Models.Type Type)
        {
            database.CreateTable<Models.Type>();

            lock (locker)
            {
                if (Type.TypeID != 0)
                {
                    return Type.TypeID;
                }
                else
                {
                    return database.Insert(Type);
                }
            }
        }

        public void UpdateType(Models.Type Type)
        {
            database.Update(Type);
        }

        public List<Models.Type> GetTypes()
        {
            lock (locker)
            {
                return (from c in database.Table<Models.Type>() select c).ToList();
            }
        }

        public Models.Type GetType(int i)
        {
            lock (locker)
            {
                List<Models.Type> Gatunki = GetTypes();
                return Gatunki[i];
            }
        }

        public List<Brewery> GetBreweries()
        {
            lock (locker)
            {
                return (from c in database.Table<Brewery>() select c).ToList();
            }
        }

        public Brewery GetBrewery(int i)
        {
            lock (locker)
            {
                List<Brewery> breweries = GetBreweries();
                return breweries[i];
            }
        }

        public int DeleteBrewery(Brewery brewery)
        {
            return database.Delete(brewery);
        }

        public List<Beer> GetBeers()
        {
            lock (locker)
            {
                return (from c in database.Table<Beer>() select c).ToList();
            }
        }

        public Beer GetBeer(int i)
        {
            lock (locker)
            {
                List<Beer> Beers = GetBeers();
                return Beers[i];
            }
        }
    }
}
