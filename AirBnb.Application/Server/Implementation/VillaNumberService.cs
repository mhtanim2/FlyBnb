using AirBnb.Application.Common.Interfaces;
using AirBnb.Application.Common.Utility;
using AirBnb.Application.Services.Interface;
using AirBnb.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Application.Services.Implementation
{
    public class VillaNumberService : IVillaNumberService
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public VillaNumberService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<VillaNumber> GetAllVillaNumbers()
        {
            return _unitOfWork.VillaNumberRepo.GetAll(includeProperties: SD.Villa);
        }

        public VillaNumber GetVillaNumberById(int id)
        {
            return _unitOfWork.VillaNumberRepo.Get(u => u.Villa_Number == id, includeProperties: SD.Villa);
        }

        public void CreateVillaNumber(VillaNumber villaNumber)
        {
            _unitOfWork.VillaNumberRepo.Add(villaNumber);
            _unitOfWork.Save();
        }

        public void UpdateVillaNumber(VillaNumber villaNumber)
        {
            _unitOfWork.VillaNumberRepo.Update(villaNumber);
            _unitOfWork.Save();
        }

        public bool DeleteVillaNumber(int id)
        {
            try
            {
                VillaNumber? objVilla = GetVillaNumberById(id);
                if (objVilla is not null)
                {
                    _unitOfWork.VillaNumberRepo.Delete(objVilla);
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

        public bool CheckVillaNumberExist(int villaNumber)
        {
            return _unitOfWork.VillaNumberRepo.Any(u => u.Villa_Number == villaNumber); ;
        }
    }
}