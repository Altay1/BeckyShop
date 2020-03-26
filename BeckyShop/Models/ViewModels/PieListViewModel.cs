using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeckyShop.Models.ViewModels
{
    public class PieListViewModel
    {
        public IEnumerable<Pie> ListOfPies { get; set; }
        public string CurrentCategory { get; set; }
    }
}
