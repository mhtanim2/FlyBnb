using AirBnb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Application.Common.Interfaces
{
    public interface IVillaNumberRepository:IRepository<VillaNumber>
    {
        void Update(VillaNumber entity);
    }
}
