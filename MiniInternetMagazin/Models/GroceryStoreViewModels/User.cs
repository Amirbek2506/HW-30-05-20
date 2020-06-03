using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniInternetMagazin.Models.GroceryStoreViewModels
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Roll { get; set; }
        [Required]
        public int Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
