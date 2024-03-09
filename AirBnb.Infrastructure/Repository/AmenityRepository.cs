using AirBnb.Application.Common.Interfaces;
using AirBnb.Domain.Entities;
using AirBnb.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Infrastructure.Repository
{
  public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        private ApplicationDbContext _context;
        public AmenityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Amenity entity)
        {
            _context.Amenities.Update(entity);
        }
    }
}
