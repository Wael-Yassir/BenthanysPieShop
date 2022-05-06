using BenthanysPieShop.Models;
using System.Collections.Generic;

namespace BenthanysPieShop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pie> PiesOfTheWeek { get; set; }
    }
}
