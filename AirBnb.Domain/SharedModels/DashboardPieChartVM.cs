﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Domain.SharedModels
{
    public class DashboardPieChartVM
    {
        public decimal[] Series { get; set; }
        public string[] Labels { get; set; }
    }
}
