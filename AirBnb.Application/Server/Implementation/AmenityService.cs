using AirBnb.Application.Common.Interfaces;
using AirBnb.Application.Common.Utility;
using AirBnb.Application.Server.Interface;
using AirBnb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Application.Server.Implementation
{
    public class AmenityService:IAmenityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AmenityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CheckAmenityExist(int amenityNumber)
        {
            return _unitOfWork.AmenityRepo.Any(u => u.Id == amenityNumber); ;
        }

        public void CreateAmenity(Amenity amenity)
        {
            _unitOfWork.AmenityRepo.Add(amenity);
            _unitOfWork.Save();
        }

        public bool DeleteAmenity(int id)
        {
            try
            {
                Amenity? obj = GetAmenityById(id);
                if (obj is not null)
                {
                    _unitOfWork.AmenityRepo.Delete(obj);
                    _unitOfWork.Save();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public IEnumerable<Amenity> GetAllAmenities()
        {
            return _unitOfWork.AmenityRepo.GetAll(includeProperties:SD.Villa);

        }

        public Amenity GetAmenityById(int id)
        {
            return _unitOfWork.AmenityRepo.Get(u=>u.Id==id,includeProperties: SD.Villa);
        }

        public void UpdateAmenity(Amenity amenity)
        {
            _unitOfWork.AmenityRepo.Update(amenity);
            _unitOfWork.Save();
        }
    }
}
