using KatalogPiw.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace KatalogPiw.ViewModels
{
    public class FileOpenViewModel
    {
        public FileOpenViewModel()
        {
        }

        public void LoadBeerListFile(string fileName)
        {
            List<string> lines = new List<string>();
            List<CsvModel> ModelsList = new List<CsvModel>();
            List<Beer> BeerList = new List<Beer>();
            using (StreamReader sr = new StreamReader((fileName), Encoding.GetEncoding("utf-8")))
            {
                foreach (var lin in File.ReadLines(fileName))
                {
                    lines.Add(lin);
                }
            }
            
            ModelsList = LinesToData(lines);
            BeerList = ConvertToBeer(ModelsList);
        }

        private List<CsvModel> LinesToData(List<string> lines )
        {
            lines = GetRecords(lines);
            List<CsvModel> ModelsList = new List<CsvModel>();
            for (int i = 0; i < lines.Count; i++)
            {

                string[] tabToAddModel = new string[13];
                GenerateTab(lines[i], ref tabToAddModel);
                CsvModel cSVModel = new CsvModel(tabToAddModel);
                ModelsList.Add(cSVModel);
            }
            return ModelsList;
        }

        List<string> GetRecords(List<string> lines)
        {
            List<string> fileContent = new List<string>();
            for (int i = 2; i < lines.Count; i = i + 2)
                fileContent.Add(lines[i]);

            return fileContent;
        }

        void GenerateTab(string line, ref string[] tabToAddModel)
        {
            tabToAddModel = line.Split('\t');
        }

        List<Beer> ConvertToBeer(List<CsvModel> CsvModelList)
        {
            List<Beer> _beerList = new List<Beer>();
            for (int i = 0; i < CsvModelList.Count; i++)
            {
                CsvModel csvModel = CsvModelList[i];
                if (csvModel.Quantity > 0)
                {


                    Brewery brewery = new Brewery();
                    Models.Type type = new Models.Type();
                    brewery.BreweryName = csvModel.Provider;
                    type.TypeName = csvModel.Group;
                    type.FoodParing = csvModel.Provider;//docelowo foodparing

                    Beer beer = new Beer(csvModel.Name, brewery, csvModel.NetPrice, csvModel.PurchasePrice, type, csvModel.UnitWeight, csvModel.Description);
                    beer.BrewerName = brewery.BreweryName;
                    beer.TypeName = type.TypeName;

                    UpdateDataBases(ref brewery, ref type);
                    beer.BreweryID = brewery.BreweryID;
                    beer.TypeID = type.TypeID;
                    beer.PriceListA = CountGrossPrice(csvModel.NetPrice);
                    beer.Quantity = csvModel.Quantity;
                    beer.EanCode = csvModel.Code;
                    //beer.PhotoPath = csvModel.FilePickturePath;
                    beer.PhotoPath = ConvertPath(csvModel.FilePickturePath);
                    beer.Plato = csvModel.PackageWeight;

                    _beerList.Add(beer);
                    KatalogPiw.App.Database.SaveBeer(beer);
                }
            }
            return _beerList;
        }

        private string ConvertPath(string path)
        {
            if (String.IsNullOrEmpty(path))
            {
                return " ";
            }
            string[] tab = path.Split('\\');

            string pathPhoto = "Photos" + "\\" + tab[tab.Length - 2] + "\\" + tab[tab.Length - 1];
            return pathPhoto;



        }

        private void UpdateDataBases(ref Brewery brewery, ref Models.Type type)
        {
            if (IsBrewerInDB(ref brewery) == false)
            {
                App.Database.SaveBrewery(brewery);
            }
            if (IsTypeInDB(ref type) == false)
            {
                App.Database.SaveType(type);
            }
        }

        private bool IsBrewerInDB(ref Brewery brewery)
        {
            List<Brewery> breweries = App.Database.GetBreweries();
            for (int i = 0; i < breweries.Count; i++)
            {
                if (breweries[i].BreweryName.ToLower() == brewery.BreweryName.ToLower())
                {
                    brewery.BreweryID = breweries[i].BreweryID;
                    return true;
                }
            }
            return false;

        }

        private bool IsTypeInDB(ref Models.Type type)
        {
            List<Models.Type> types = App.Database.GetTypes();
            for (int i = 0; i < types.Count; i++)
            {
                if (types[i].TypeName.ToLower() == type.TypeName.ToLower())
                {
                    type.TypeID = types[i].TypeID;
                    return true;
                }
            }
            return false;

        }

        private double CountGrossPrice(double netPrice)
        {
            netPrice = UptoTwoDecimalPoints(netPrice);
            netPrice = netPrice * 1.23;
            netPrice = netPrice / (0.8);
            netPrice = UptoTwoDecimalPoints(netPrice);
            netPrice = System.Math.Round(netPrice, 2);
            string s = netPrice.ToString();
            char[] tab = String.Format("{0:0.00}", netPrice).ToCharArray();
            String.Format("{0:0.00}", s);
            netPrice = RoundingPrice(tab, netPrice);
            netPrice = UptoTwoDecimalPoints(netPrice);
            return netPrice;
        }

        private double RoundingPrice(char[] digitInTab, double netPrice)
        {
            string priceInString = new string(digitInTab);
            priceInString = " " + priceInString;
            char[] tab = priceInString.ToCharArray();
            int lastDigit = (int)Char.GetNumericValue(tab[priceInString.Length - 1]);

            if (lastDigit >= 5)
            {
                lastDigit = '9';
                tab[priceInString.Length - 1] = (char)lastDigit;
                string net = new string(tab);
                netPrice = Convert.ToDouble(net);
                return netPrice;
            }
            else
            {
                lastDigit = '9';
                tab[priceInString.Length - 1] = (char)lastDigit;
                if (tab[priceInString.Length - 2] == '0')
                {
                    int nineDigit = '9';
                    tab[priceInString.Length - 2] = (char)nineDigit;
                    if (tab[priceInString.Length - 4] == '0')
                    {
                        tab[priceInString.Length - 4] = (char)nineDigit;
                        if (tab[priceInString.Length - 5] == '1')
                        {
                            tab[priceInString.Length - 5] = ' ';

                            string net = new string(tab);
                            netPrice = Convert.ToDouble(net);
                            return netPrice;
                        }
                        else
                        {
                            int firstValue = (int)Char.GetNumericValue(tab[priceInString.Length - 5]);
                            firstValue = firstValue - 1;
                            string s = firstValue.ToString();
                            tab[priceInString.Length - 5] = s[0];
                            string net = new string(tab);
                            netPrice = Convert.ToDouble(net);
                            return netPrice;
                        }
                    }
                    else
                    {
                        int value = (int)Char.GetNumericValue(tab[priceInString.Length - 4]);
                        value = value - 1;
                        string s = value.ToString();
                        tab[priceInString.Length - 4] = s[0];
                        string net = new string(tab);
                        netPrice = Convert.ToDouble(net);
                        return netPrice;
                    }
                }
                else
                {
                    int value = (int)Char.GetNumericValue(tab[priceInString.Length - 2]);
                    value = value - 1;
                    string s = value.ToString();
                    tab[priceInString.Length - 2] = s[0];
                    string net = new string(tab);
                    netPrice = Convert.ToDouble(net);
                    return netPrice;
                }
            }
        }

        public static double UptoTwoDecimalPoints(double num)
        {
            var totalCost = Convert.ToDouble(String.Format("{0:0.00}", num));
            return totalCost;
        }

    }
}
