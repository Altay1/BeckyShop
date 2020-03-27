using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeckyShop.Models
{
    public interface IPieRepository
    {
        IEnumerable<Pie> AllPies();
        IEnumerable<Pie> PiesOfTheWeek();
        Pie PieGetById(int id);
    }
}
