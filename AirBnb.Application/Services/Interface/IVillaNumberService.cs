using AirBnb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Application.Services.Interface
{
    public interface IVillaNumberService
    {
        IEnumerable<VillaNumber> GetAllVillaNumbers();
        VillaNumber GetVillaNumberById(int id);
        void CreateVillaNumber(VillaNumber VillaNumber);
        void UpdateVillaNumber(VillaNumber VillaNumber);
        bool DeleteVillaNumber(int id);

        bool CheckVillaNumberExist(int villaNumber);
    }
}
