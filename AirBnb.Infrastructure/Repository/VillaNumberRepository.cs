using AirBnb.Application.Common.Interfaces;
using AirBnb.Domain.Entities;
using AirBnb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Infrastructure.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>,IVillaNumberRepository
    {
        private readonly ApplicationDbContext _context;

        public VillaNumberRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(VillaNumber entity)
        {
            _context.VillaNumbers.Update(entity);
        }
    }
}
