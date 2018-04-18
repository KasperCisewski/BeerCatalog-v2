using System;
using System.Collections.Generic;
using System.Text;
using KatalogPiw.Models;
using KatalogPiw.Services;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using Xamarin.Forms;
namespace KatalogPiw.ViewModels
{
    public class AddTypeViewModel:INotifyCollectionChanged
    {
        private readonly ObservableCollection<Models.Type> _typeList;
        public ObservableCollection<Models.Type> TypeList
        {
            get { return _typeList; }
        }

        public AddTypeViewModel()
        {
            _typeList = new ObservableCollection<Models.Type>();
            for (int i = 0; i < App.Database.GetTypes().Count; i++)
            {
                _typeList.Add(KatalogPiw.App.Database.GetType(i));
            }
        }

        public void AddTypeByName(string typeName,string foodParing)
        {
            Models.Type type = new Models.Type();
            type.TypeName = typeName;
            type.FoodParing = foodParing;
            _typeList.Add(type);
            KatalogPiw.App.Database.SaveType(type);
            notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, type));
        }

        public void DeleteTypeByObject(Object Sender)
        {
            var mi = ((MenuItem)Sender);
            Models.Type type = (Models.Type)mi.BindingContext;
            int ID = type.TypeID;
            _typeList.Remove(type);
            KatalogPiw.App.Database.DeleteType(type);
            notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
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
