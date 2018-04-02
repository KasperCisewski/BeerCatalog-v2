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

        public void LoadFile(string fileName)
        {
            List<string> lines = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    foreach (var lin in File.ReadLines(fileName))
                    {
                        lines.Add(lin);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            lines = GetRecords(lines);
            List<CsvModel> ModelsList = new List<CsvModel>();
            for (int i = 0; i < lines.Count; i++)
            {

                string[] tabToAddModel = new string[13];
                // tabToAddModel = GenerateEmptyTab(tabToAddModel);
                GenerateTab(lines[i], ref tabToAddModel);
                CsvModel cSVModel = new CsvModel(tabToAddModel);
                ModelsList.Add(cSVModel);
            }
            List<Beer> BeerList = new List<Beer>();
            BeerList = ConvertToBeer(ModelsList);
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
                Brewery brewery = new Brewery();
                Models.Type type = new Models.Type();
                brewery.BreweryName = csvModel.Provider;
                type.TypeName = csvModel.Group;

                Beer beer = new Beer(csvModel.Name, brewery, csvModel.NetPrice, csvModel.PurchasePrice, type, csvModel.UnitWeight, csvModel.Description, csvModel.Quantity.ToString());
                beer.BrewerName = brewery.BreweryName;
                beer.TypeName = type.TypeName;
                _beerList.Add(beer);
                KatalogPiw.App.Database.SaveBeer(beer);
            }
            return _beerList;
        }
    }
}
