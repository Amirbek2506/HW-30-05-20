using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniInternetMagazin.Models.GroceryStoreViewModels
{
public class Category
    {
        public int CategoryId{get; set;}
        public string CategoryName {get; set;}
        public virtual ICollection<Product> Products { get; set; }

    }
}
