using KatalogPiw.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace KatalogPiw.ViewModels
{
    public class EditorViewModel:INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value != this._name)
                {
                    this._name = value;
                    OnPropertyChanged(_name);

                }
            }
        }

        private object _object;

        public EditorViewModel(object sender)
        {
            var mi = ((MenuItem)sender);

            Models.Type typeTest = new Models.Type();
            if (mi.BindingContext.GetType() == typeTest.GetType())
            {
                Models.Type type = (Models.Type)mi.BindingContext;
                _name = type.TypeName;
                _object = type;
            }
            else
            {
                Brewery brewery = (Brewery)mi.BindingContext;
                _name = brewery.BreweryName;
                _object = brewery;
            }

        }

        public void UpdateObject(string newName)
        {
            Models.Type typeTest = new Models.Type();
            if (_object.GetType() == typeTest.GetType())
            {
                Models.Type type = (Models.Type)_object;
                Models.Type newType = type;
                newType.TypeName = newName;
                App.Database.UpdateType(newType);
                PropertyChanged(this, new PropertyChangedEventArgs(newType.TypeName));
            }
            else
            {
                Brewery brewery = (Brewery)_object;
                Brewery newBrewery = brewery;
                newBrewery.BreweryName = newName;
                App.Database.UpdateBrewery(newBrewery);
                PropertyChanged(this, new PropertyChangedEventArgs(newBrewery.BreweryName));
            }

        }

       

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
