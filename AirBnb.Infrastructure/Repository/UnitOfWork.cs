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
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public IVillaRepository VillaRepo { get; private set; }
        public IAmenityRepository AmenityRepo { get; private set; }
        public IVillaNumberRepository VillaNumberRepo {  get; private set; }
        public IBookingRepository BookingRepo { get; private set;}
        public IUserRepository UserRepo { get; private set; }
        
        // Create instance of repository calss object
        public UnitOfWork(ApplicationDbContext context)
        {
            _context= context;
            VillaRepo=new VillaRepository(_context);
            BookingRepo=new BookingRepository(_context);
            UserRepo=new UserRepository(_context);
            VillaNumberRepo=new VillaNumberRepository(_context);
            AmenityRepo=new AmenityRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
