using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniInternetMagazin.Models.GroceryStoreViewModels
{
    public class Basket
    {
        public int Id { get; set; }
        public string AdressDelivery { get; set; }
        public int NumbersPhone { get; set; }
        public int TimeDeliver { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}
