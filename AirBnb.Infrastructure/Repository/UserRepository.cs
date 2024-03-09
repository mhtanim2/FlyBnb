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
  public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
