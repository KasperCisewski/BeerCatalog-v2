using System;
using System.Collections.Generic;
using System.Text;

namespace KatalogPiw.Models
{
    public class CsvModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Provider { get; set; }
        public int Quantity { get; set; }
        public double NetPrice { get; set; }
        public double PurchasePrice { get; set; }
        public string Description { get; set; }
        public string UnitWeight { get; set; }
        public string PackageWeight { get; set; }
        public string SupplierIndex { get; set; }
        public string FilePickturePath { get; set; }

        public CsvModel()
        {

        }
        public CsvModel(string[] tab)
        {
            this.ID = Convert.ToInt32(tab[0]);
            this.Code = tab[1];
            this.Name = tab[2];
            this.Group = tab[3];
            this.Provider = tab[4];
            int _quantity = 0;
            bool result = Int32.TryParse(tab[5], out _quantity);
            this.Quantity = _quantity;
            double _netPrice = 0;
            tab[6] = tab[6].Replace(",", ".").Replace("-", " ");
            result = Double.TryParse(tab[6], out _netPrice);
            this.NetPrice = _netPrice;
            double _purchasePrice = 0;
            tab[7] = tab[7].Replace(",", ".").Replace("-", " ");
            result = Double.TryParse(tab[7], out _purchasePrice);
            this.PurchasePrice = _purchasePrice;
            this.Description = tab[8];
            this.UnitWeight = tab[9];
            this.PackageWeight = tab[10];
            this.SupplierIndex = tab[11];
            this.FilePickturePath = tab[12];
        }
    }
}
