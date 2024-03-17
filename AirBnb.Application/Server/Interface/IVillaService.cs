﻿using AirBnb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Application.Services.Interface
{
    public interface IVillaService
    {
        IEnumerable<Villa> GetAllVillas();
        Villa GetVillaById(int id);
        void CreateVilla(Villa villa);
        void UpdateVilla(Villa villa);
        bool DeleteVilla(int id);

        IEnumerable<Villa> GetVillaAvailabilityByDate(int nights, DateOnly checkInDate);
    }
}