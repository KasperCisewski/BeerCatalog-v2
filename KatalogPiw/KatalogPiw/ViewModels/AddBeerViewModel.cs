using KatalogPiw.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace KatalogPiw.ViewModels
{
    public class AddBeerViewModel:INotifyCollectionChanged
    {
        public List<Brewery> Breweries { get; set; }
        public List<Models.Type> Types { get; set; }

        private readonly ObservableCollection<Beer> _beerList;
        public ObservableCollection<Beer> BeerList
        {
            get { return _beerList; }
        }

        public  AddBeerViewModel()
        {
            _beerList = new ObservableCollection<Beer>();
            Breweries = new List<Models.Brewery>();
            Breweries = App.Database.GetBreweries();

            Types = new List<Models.Type>();
            Types = App.Database.GetTypes();

            for (int i = 0; i < App.Database.GetBeers().Count; i++)
            {
                _beerList.Add(App.Database.GetBeer(i));
                _beerList[i].Breweries = App.Database.GetBreweries();
                _beerList[i].Types = App.Database.GetTypes();
            }
        }

        public void AddBeer(string beerName, Models.Brewery brewery, double netPriceWD, double netPriceD, Models.Type type, string parameters, string description)
        {
            Models.Beer beer = new Models.Beer(beerName, brewery, netPriceWD, netPriceD, type, parameters, description);
            beer.Breweries = App.Database.GetBreweries();
            beer.Types = App.Database.GetTypes();
            beer.BrewerName = brewery.BreweryName;
            beer.TypeName = type.TypeName;
            _beerList.Add(beer);
            KatalogPiw.App.Database.SaveBeer(beer);
            notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, beer));
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void notifyCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, args);
            }
        }
    }
}
