using KatalogPiw.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;

namespace KatalogPiw.ViewModels
{
    public class AddBreweryViewModel:INotifyCollectionChanged
    {
        private readonly ObservableCollection<Brewery> _breweryList;
        public ObservableCollection<Brewery> BreweryList
        {
            get { return _breweryList; }

        }
        public AddBreweryViewModel()
        {
            _breweryList = new ObservableCollection<Brewery>();
            for (int i = 0; i < App.Database.GetBreweries().Count; i++)
            {
                _breweryList.Add(KatalogPiw.App.Database.GetBrewery(i));
            }

        }

        public void AddBrewery(string BreweryName)
        {
            Brewery brewery = new Brewery();
            brewery.BreweryName = BreweryName;
            if(App.Database.SaveBrewery(brewery)!=0)
            {
                _breweryList.Add(brewery);
            }
            notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, brewery));
        }

        public void DeleteBrewery(object Sender)
        {
            var mi = ((MenuItem)Sender);

            var brow = (Brewery)mi.BindingContext;

            int ID = brow.BreweryID;

            for (int i = 0; i < _breweryList.Count; i++)
            {
                if (ID == _breweryList[i].BreweryID)
                {
                    Brewery Brewery = _breweryList[i];
                    _breweryList.RemoveAt(i);
                    KatalogPiw.App.Database.DeleteBrewery(Brewery);
                    notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    break;
                }
            }

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
