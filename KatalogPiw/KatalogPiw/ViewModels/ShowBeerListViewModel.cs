using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

using Syncfusion.Drawing;
using Syncfusion.Pdf;
using System.IO;
using Syncfusion.Pdf.Security;
using Syncfusion.Pdf.Graphics;
using System.Reflection;
using Syncfusion.Pdf.Grid;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.Pdf.Parsing;
using System.Collections.Generic;
using KatalogPiw.Models;
using System.ComponentModel;
using SQLitePCL;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace KatalogPiw.ViewModels
{
    public class ShowBeerListViewModel:INotifyPropertyChanged,INotifyCollectionChanged
    {
        private string _filter;

        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    OnPropertyChanged("Filter");
                    this.Filter_beers();
                }
            }
        }


        private async void Filter_beers()
        {
            List<Beer> _newBeers = new List<Beer>(_beers);  //czy za kazdym razem trzeba tworzyc nowa liste?
            if (_beers != null)
            {
                if (String.IsNullOrEmpty(_filter))
                {
                    _beers = new ObservableCollection<Beer>();
                   // _beers = _beers.ToList();

                    for (int i = 0; i < App.Database.GetBeers().Count; i++)
                    {
                        _beers.Add(KatalogPiw.App.Database.GetBeer(i));
                        _beers[i].Breweries = App.Database.GetBreweries();
                        _beers[i].Types = App.Database.GetTypes();
                        notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, _beers));

                    }
                  //  notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, beer));

                }
                else
                {
                    _newBeers = _beers.Where(x => x.BrewerName.ToLower().Contains(_filter.ToLower())
                       || x.TypeName.ToLower().Contains(_filter.ToLower())).ToList();
                    notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, _beers));

                    int counter = _beers.Count - 1;
                    for (int i = counter; i >= 0; i--)
                    {
                        _beers.RemoveAt(i);
                    }
                    for (int i = 0; i < _newBeers.Count; i++)
                    {
                        _beers.Add(_newBeers[i]);
                        notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, _beers[i]));
                       
                    }
                    string filterCheck = _filter;
                    
                    await RefreshList(filterCheck);
                    

                    //if (filterCheck != Filter)
                    //{
                    //    _beers = new ObservableCollection<Beer>();
                    //    //  _beers = new List<Beer>();

                    //    for (int j = 0; j < App.Database.GetBeers().Count; j++)
                    //    {
                    //        _beers.Add(KatalogPiw.App.Database.GetBeer(j));
                    //        _beers[j].Breweries = App.Database.GetBreweries();
                    //        _beers[j].Types = App.Database.GetTypes();
                    //notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, _beers[j]));

                    //    }


                    //}
                }
            }
        }

        private async Task RefreshList(string filterCheck)
        {
           

                
                    _beers = new ObservableCollection<Beer>();
                    //  _beers = new List<Beer>();

                    for (int j = 0; j < App.Database.GetBeers().Count; j++)
                    {
                        _beers.Add(KatalogPiw.App.Database.GetBeer(j));
                        _beers[j].Breweries = App.Database.GetBreweries();
                        _beers[j].Types = App.Database.GetTypes();
                        notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, _beers[j]));

                    }
                    
                
                 
            
        }

        private ObservableCollection <Beer> _beers;
        public ObservableCollection<Beer> Beers
        {
            get
            {
                return _beers;
            }
            set
            {
                if (_beers != value)
                {
                    _beers = value;
                    OnPropertyChanged("Beers");
                }

            }
        }

        public ShowBeerListViewModel()
        {
            _beers = new ObservableCollection<Beer>();
           // _beers = new List<Beer>();
           // _beers = App.Database.GetBeers();

            for (int i = 0; i < App.Database.GetBeers().Count; i++)
            {
                _beers.Add(KatalogPiw.App.Database.GetBeer(i));
                _beers[i].Breweries = App.Database.GetBreweries();
                _beers[i].Types = App.Database.GetTypes();
            }
        }


        public void CreatePDF()
        {
            PdfDocument doc = new PdfDocument();

            PdfPage page = doc.Pages.Add();

            RectangleF bounds = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);

            PdfPageTemplateElement header = new PdfPageTemplateElement(bounds);

            Stream imageStream = File.OpenRead("chmielove.png");
            PdfBitmap image = new PdfBitmap(imageStream);
            PdfGraphics graphics = page.Graphics;

            graphics.DrawImage(image, 0, 0); // format 60x50




            PdfGrid pdfGrid = new PdfGrid();
            pdfGrid.Columns.Add(10);
            PdfStringFormat format = new PdfStringFormat();

            format.Alignment = PdfTextAlignment.Left;
            format.LineAlignment = PdfVerticalAlignment.Top;

            for (int i = 0; i < 10; i++)
            {
                pdfGrid.Columns[i].Format = format;
            }
            pdfGrid.Columns[0].Width = 15;
            pdfGrid.Columns[6].Width = 65;
            pdfGrid.Columns[7].Width = 200;
            pdfGrid.Headers.Add(1);

            PdfGridRow pdfGridHeader = pdfGrid.Headers[0];

            pdfGridHeader.Cells[0].Value = "Nr";
            pdfGridHeader.Cells[1].Value = "Nazwa";
            pdfGridHeader.Cells[2].Value = "Browar";
            pdfGridHeader.Cells[3].Value = "Cena netto bez rabatu";
            pdfGridHeader.Cells[4].Value = "Cena netto z rabatem";
            pdfGridHeader.Cells[5].Value = "Gatunek";
            pdfGridHeader.Cells[6].Value = "Parametry";
            pdfGridHeader.Cells[7].Value = "Opis";
            pdfGridHeader.Cells[8].Value = "Food Paring";
            // pdfGridHeader.Cells[9].Value = "Zdjecie";
            pdfGridHeader.Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 9, PdfFontStyle.Bold);

            //add rows
            List<Beer> OutBeerList = new List<Beer>();

            for (int i = 0; i < _beers.Count; i++)
            {
                if (_beers[i].IsSelect == true)
                {
                    OutBeerList.Add(_beers[i]);
                }
            }

            for (int i = 0; i < OutBeerList.Count; i++)
            {
                PdfGridRow pdfGridRow = pdfGrid.Rows.Add();
                pdfGridRow.Cells[0].Value = (i + 1).ToString();
                pdfGridRow.Cells[1].Value = OutBeerList[i].BeerName;
                pdfGridRow.Cells[2].Value = OutBeerList[i].BrewerName;    //OutBeerList[i].   Browary[OutBeerList[i].BrowarID - 1].NazwaBrowaru;
                pdfGridRow.Cells[3].Value = OutBeerList[i].NetPriceWithoutDiscout.ToString();
                pdfGridRow.Cells[4].Value = OutBeerList[i].NetPriceWithDiscout.ToString();
                pdfGridRow.Cells[5].Value = OutBeerList[i].TypeName; // DodawanieGatunkuViewModel.ListaWszystkichGatunkow[OutBeerList[i].GatunekID - 1].NazwaGatunku;
                pdfGridRow.Cells[6].Value = OutBeerList[i].Parameters;
                pdfGridRow.Cells[7].Value = OutBeerList[i].Description;
                pdfGridRow.Cells[8].Value = OutBeerList[i].FoodParing;
                //pdfGridRow.Cells[9].Value = OutBeerList[i].Image;
            }


            pdfGrid.Draw(page, 0, 80);

            //Save and close
            MemoryStream stream = new MemoryStream();

            doc.Save(stream);



            Xamarin.Forms.DependencyService.Get<Services.ISave>().SaveTextAsync("Output.pdf", "application/pdf", stream);

            doc.Close(true);
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
