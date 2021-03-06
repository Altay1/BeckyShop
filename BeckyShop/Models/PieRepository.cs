﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeckyShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext _appDbContext;

        public PieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Pie> AllPies()
        {
            return _appDbContext.Pies.Include(p => p.Category);
        }

        public IEnumerable<Pie> PiesOfTheWeek()
        {
            return _appDbContext.Pies.Include(p => p.Category).Where(p => p.IsPieOfTheWeek);
        }

        public Pie PieGetById(int id)
        {
            return _appDbContext.Pies.FirstOrDefault(p => p.Id == id);
        }
    }
}
