using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniInternetMagazin.Models.GroceryStoreViewModels
{
    public class Product
    {
        public int ProductId{get; set;}
        public string ProductName {get; set;}
        public int CategoryId {get; set;}
        public decimal Price {get; set;}    
        
        public virtual Category Category { get; set; }
    }
}
