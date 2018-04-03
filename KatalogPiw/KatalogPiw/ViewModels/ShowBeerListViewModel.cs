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
using System.Windows.Input;
using System.Diagnostics;

namespace KatalogPiw.ViewModels
{
    public class ShowBeerListViewModel:INotifyPropertyChanged,INotifyCollectionChanged
    {
        
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
                    notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, _beers));

                }

            }
        }

        public ShowBeerListViewModel()
        {
            _beers = new ObservableCollection<Beer>();

            for (int i = 0; i < App.Database.GetBeers().Count; i++)
            {
                _beers.Add(KatalogPiw.App.Database.GetBeer(i));
                _beers[i].Breweries = App.Database.GetBreweries();
                _beers[i].Types = App.Database.GetTypes();
            }
        }
        
        public void SelectAllBeers(string searchBarText)
        {
            List<Beer> beers = new List<Beer>();
            
           
                beers = Beers.Where(i => (i.BrewerName.ToLower().Contains(searchBarText.ToLower())
                    || (i.TypeName.ToLower().Contains(searchBarText.ToLower())))).ToList();

            for(int i=0;i<beers.Count;i++)
            {
                beers[i].IsSelect = true;
            }

            for(int i=0;i<beers.Count;i++)
            {
                for(int j=0;j<_beers.Count;j++)
                {
                    if(beers[i].ID==_beers[j].ID)
                    {
                        _beers[j].IsSelect = true;
                        notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, _beers[j]));
                        break;
                    }
                }
            }
          


        }

        public void CreatePriceListA()
        {
            PdfDocument doc = new PdfDocument();

            PdfPage page = doc.Pages.Add();

            RectangleF bounds = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);

            PdfPageTemplateElement header = new PdfPageTemplateElement(bounds);

            Stream imageStream = File.OpenRead("chmielove.png");
            PdfBitmap image = new PdfBitmap(imageStream);
            PdfGraphics graphics = page.Graphics;

            graphics.DrawImage(image, 0, 0); // format 60x50

            this.AddHeader(page, doc, "cos");



            PdfGrid pdfGrid = new PdfGrid();
            pdfGrid.Columns.Add(10);
            PdfStringFormat format = new PdfStringFormat();

            format.Alignment = PdfTextAlignment.Left;
            format.LineAlignment = PdfVerticalAlignment.Top;

            for (int i = 0; i < 10; i++)
            {
                pdfGrid.Columns[i].Format = format;
            }
            pdfGrid.Columns[0].Width = 48;
            pdfGrid.Columns[1].Width = 95;
            pdfGrid.Columns[2].Width = 50;
            pdfGrid.Columns[3].Width = 30;
            pdfGrid.Columns[4].Width = 170;
            pdfGrid.Columns[5].Width = 20;
            pdfGrid.Columns[6].Width = 15;
            pdfGrid.Columns[7].Width = 25;
            pdfGrid.Columns[8].Width = 35;
            pdfGrid.Headers.Add(1);

            PdfGridRow pdfGridHeader = pdfGrid.Headers[0];

            pdfGridHeader.Cells[0].Value = "Kod";
            pdfGridHeader.Cells[1].Value = "Nazwa";
            pdfGridHeader.Cells[2].Value = "Browar";
            pdfGridHeader.Cells[3].Value = "Gatunek";
            pdfGridHeader.Cells[4].Value = "Opis 2";
            pdfGridHeader.Cells[5].Value = "Alkohol";
            pdfGridHeader.Cells[6].Value = "Plato";
            pdfGridHeader.Cells[7].Value = "Cena Netto";
            pdfGridHeader.Cells[8].Value = "Cena sugerowana";
            // pdfGridHeader.Cells[9].Value = "Zdjecie";
            pdfGridHeader.Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 6, PdfFontStyle.Bold);

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
                pdfGridRow.Cells[0].Value = OutBeerList[i].EanCode;
                pdfGridRow.Cells[1].Value = OutBeerList[i].BeerName;
                pdfGridRow.Cells[2].Value = OutBeerList[i].BrewerName;    //OutBeerList[i].   Browary[OutBeerList[i].BrowarID - 1].NazwaBrowaru;
                pdfGridRow.Cells[3].Value = OutBeerList[i].TypeName;
                pdfGridRow.Cells[4].Value = OutBeerList[i].Description;
                pdfGridRow.Cells[5].Value = OutBeerList[i].Parameters; // DodawanieGatunkuViewModel.ListaWszystkichGatunkow[OutBeerList[i].GatunekID - 1].NazwaGatunku;
                pdfGridRow.Cells[6].Value = OutBeerList[i].FoodParing;
                double outputValue = OutBeerList[i].NetPriceWithoutDiscout;
                outputValue = FileOpenViewModel.UptoTwoDecimalPoints(outputValue);
                pdfGridRow.Cells[7].Value = outputValue.ToString();
                pdfGridRow.Cells[8].Value = OutBeerList[i].PriceListA.ToString();
                //pdfGridRow.Cells[9].Value = OutBeerList[i].Image;
                pdfGridRow.Style.Font= new PdfStandardFont(PdfFontFamily.Helvetica, 6, PdfFontStyle.Bold);
            }


            pdfGrid.Draw(page, 0, 80);

            //Save and close
            MemoryStream stream = new MemoryStream();

            doc.Save(stream);



            Xamarin.Forms.DependencyService.Get<Services.ISave>().SaveTextAsync("CennikA.pdf", "application/pdf", stream);

            doc.Close(true);
        }

        public void CreatePriceListB()
        {
            PdfDocument doc = new PdfDocument();

            PdfPage page = doc.Pages.Add();

            RectangleF bounds = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);

            PdfPageTemplateElement header = new PdfPageTemplateElement(bounds);

            Stream imageStream = File.OpenRead("chmielove.png");
            PdfBitmap image = new PdfBitmap(imageStream);
            PdfGraphics graphics = page.Graphics;

            graphics.DrawImage(image, 0, 0); // format 60x50

            this.AddHeader(page, doc, "cos");



            PdfGrid pdfGrid = new PdfGrid();
            pdfGrid.Columns.Add(10);
            PdfStringFormat format = new PdfStringFormat();

            format.Alignment = PdfTextAlignment.Left;
            format.LineAlignment = PdfVerticalAlignment.Top;

            for (int i = 0; i < 10; i++)
            {
                pdfGrid.Columns[i].Format = format;
            }
            pdfGrid.Columns[0].Width = 48;
            pdfGrid.Columns[1].Width = 95;
            pdfGrid.Columns[2].Width = 50;
            pdfGrid.Columns[3].Width = 30;
            pdfGrid.Columns[4].Width = 170;
            pdfGrid.Columns[5].Width = 20;
            pdfGrid.Columns[6].Width = 15;
            pdfGrid.Columns[7].Width = 25;
            pdfGrid.Columns[8].Width = 35;
            pdfGrid.Headers.Add(1);

            PdfGridRow pdfGridHeader = pdfGrid.Headers[0];

            pdfGridHeader.Cells[0].Value = "Kod";
            pdfGridHeader.Cells[1].Value = "Nazwa";
            pdfGridHeader.Cells[2].Value = "Browar";
            pdfGridHeader.Cells[3].Value = "Gatunek";
            pdfGridHeader.Cells[4].Value = "Opis 2";
            pdfGridHeader.Cells[5].Value = "Alkohol";
            pdfGridHeader.Cells[6].Value = "Plato";
            pdfGridHeader.Cells[7].Value = "Cena Netto";
            pdfGridHeader.Cells[8].Value = "Cena sugerowana";
            // pdfGridHeader.Cells[9].Value = "Zdjecie";
            pdfGridHeader.Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 6, PdfFontStyle.Bold);

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
                pdfGridRow.Cells[0].Value = OutBeerList[i].EanCode;
                pdfGridRow.Cells[1].Value = OutBeerList[i].BeerName;
                pdfGridRow.Cells[2].Value = OutBeerList[i].BrewerName;    //OutBeerList[i].   Browary[OutBeerList[i].BrowarID - 1].NazwaBrowaru;
                pdfGridRow.Cells[3].Value = OutBeerList[i].TypeName;
                pdfGridRow.Cells[4].Value = OutBeerList[i].Description;
                pdfGridRow.Cells[5].Value = OutBeerList[i].Parameters; // DodawanieGatunkuViewModel.ListaWszystkichGatunkow[OutBeerList[i].GatunekID - 1].NazwaGatunku;
                pdfGridRow.Cells[6].Value = OutBeerList[i].FoodParing;
                double outputValue = OutBeerList[i].NetPriceWithoutDiscout*0.95;
                outputValue = FileOpenViewModel.UptoTwoDecimalPoints(outputValue);
                pdfGridRow.Cells[7].Value = outputValue.ToString();
                pdfGridRow.Cells[8].Value = OutBeerList[i].PriceListB.ToString();
                //pdfGridRow.Cells[9].Value = OutBeerList[i].Image;
                pdfGridRow.Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 6, PdfFontStyle.Bold);

            }


            pdfGrid.Draw(page, 0, 80);

            //Save and close
            MemoryStream stream = new MemoryStream();

            doc.Save(stream);



            Xamarin.Forms.DependencyService.Get<Services.ISave>().SaveTextAsync("CennikB.pdf", "application/pdf", stream);

            doc.Close(true);
        }

        public void CreatePriceListC()
        {
            PdfDocument doc = new PdfDocument();

            PdfPage page = doc.Pages.Add();

            RectangleF bounds = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);

            PdfPageTemplateElement header = new PdfPageTemplateElement(bounds);

            Stream imageStream = File.OpenRead("chmielove.png");
            PdfBitmap image = new PdfBitmap(imageStream);
            PdfGraphics graphics = page.Graphics;

            graphics.DrawImage(image, 0, 0); // format 60x50

            this.AddHeader(page,doc,"cos");


            PdfGrid pdfGrid = new PdfGrid();
            pdfGrid.Columns.Add(10);
            PdfStringFormat format = new PdfStringFormat();

            format.Alignment = PdfTextAlignment.Center;
            format.LineAlignment = PdfVerticalAlignment.Middle;

            for (int i = 0; i < 10; i++)
            {
                pdfGrid.Columns[i].Format = format;
            }
            pdfGrid.Columns[0].Width = 48;
            pdfGrid.Columns[1].Width = 95;
            pdfGrid.Columns[2].Width = 50;
            pdfGrid.Columns[3].Width = 30;
            pdfGrid.Columns[4].Width = 170;
            pdfGrid.Columns[5].Width = 20;
            pdfGrid.Columns[6].Width = 15;
            pdfGrid.Columns[7].Width = 25;
            pdfGrid.Columns[8].Width = 35;
            pdfGrid.Headers.Add(1);

            PdfGridRow pdfGridHeader = pdfGrid.Headers[0];

            pdfGridHeader.Cells[0].Value = "Kod";
            pdfGridHeader.Cells[1].Value = "Nazwa";
            pdfGridHeader.Cells[2].Value = "Browar";
            pdfGridHeader.Cells[3].Value = "Gatunek";
            pdfGridHeader.Cells[4].Value = "Opis 2";
            pdfGridHeader.Cells[5].Value = "Alkohol";
            pdfGridHeader.Cells[6].Value = "Plato";
            pdfGridHeader.Cells[7].Value = "Cena Netto";
            pdfGridHeader.Cells[8].Value = "Cena sugerowana";
            // pdfGridHeader.Cells[9].Value = "Zdjecie";
            pdfGridHeader.Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 5, PdfFontStyle.Bold);

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
                pdfGridRow.Cells[0].Value = OutBeerList[i].EanCode;
                pdfGridRow.Cells[1].Value = OutBeerList[i].BeerName;
                pdfGridRow.Cells[2].Value = OutBeerList[i].BrewerName;    
                pdfGridRow.Cells[3].Value = OutBeerList[i].TypeName;
                pdfGridRow.Cells[4].Value = OutBeerList[i].Description;
                pdfGridRow.Cells[5].Value = OutBeerList[i].Parameters; 
                pdfGridRow.Cells[6].Value = OutBeerList[i].FoodParing;
                double outputValue = OutBeerList[i].NetPriceWithoutDiscout*0.90;
                outputValue = FileOpenViewModel.UptoTwoDecimalPoints(outputValue);
                pdfGridRow.Cells[7].Value = outputValue.ToString();
                pdfGridRow.Cells[8].Value = OutBeerList[i].PriceListC.ToString();
                //pdfGridRow.Cells[9].Value = OutBeerList[i].Image;
                pdfGridRow.Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 6, PdfFontStyle.Bold);

            }


            pdfGrid.Draw(page, 0, 80);

            //Save and close
            MemoryStream stream = new MemoryStream();

            doc.Save(stream);

            Xamarin.Forms.DependencyService.Get<Services.ISave>().SaveTextAsync("CennikC.pdf", "application/pdf", stream);

            doc.Close(true);
        }

        private void AddHeader(PdfPage page,PdfDocument doc,string title)
        {
            RectangleF rect = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);
            PdfPageTemplateElement header = new PdfPageTemplateElement(rect);
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 24);
            float doubleHeight = font.Height * 2;
            Syncfusion.Drawing.Color activeColor = Syncfusion.Drawing.Color.FromArgb(44, 71, 120);


            //Draw the image in the Header.

            PdfSolidBrush brush = new PdfSolidBrush(activeColor);

            PdfPen pen = new PdfPen(Syncfusion.Drawing.Color.DarkBlue, 3f);
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 6, PdfFontStyle.Bold);

            //Set formattings for the text
            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Center;
            format.LineAlignment = PdfVerticalAlignment.Middle;

            //Draw title
            //Draw description
            header.Graphics.DrawString("                  mail: biuro@chmielove.eu", font, brush, new RectangleF(0, 0, header.Width, header.Height), format);
            header.Graphics.DrawString(" tel: 781 507 097", font, brush, new RectangleF(0, 0, header.Width, header.Height+11), format);
            header.Graphics.DrawString("       785 886 491", font, brush, new RectangleF(0, 0, header.Width, header.Height +21), format);
            brush = new PdfSolidBrush(Syncfusion.Drawing.Color.Gray);
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 6, PdfFontStyle.Bold);

            format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Left;
            format.LineAlignment = PdfVerticalAlignment.Bottom;

           


            //Draw some lines in the header
            pen = new PdfPen(Syncfusion.Drawing.Color.DarkBlue, 0.7f);
            header.Graphics.DrawLine(pen, 0, 0, header.Width, 0);
            pen = new PdfPen(Syncfusion.Drawing.Color.DarkBlue, 2f);
            header.Graphics.DrawLine(pen, 0, 03, header.Width + 3, 03);
            pen = new PdfPen(Syncfusion.Drawing.Color.DarkBlue, 2f);
            header.Graphics.DrawLine(pen, 0, header.Height - 3, header.Width, header.Height - 3);
            header.Graphics.DrawLine(pen, 0, header.Height, header.Width, header.Height);

            //Add header template at the top.
            doc.Template.Top = header;
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
