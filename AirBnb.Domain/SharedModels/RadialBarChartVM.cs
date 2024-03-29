﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Domain.SharedModels
{
    public class RadialBarChartVM
    {
        public decimal TotalCount { get; set; }
        public decimal IncreaseDecreaseRatio { get; set; }
        public decimal IncreaseDecreaseAmount { get; set; }
        public bool HasRatioIncreased { get; set; }
        public decimal[] Series { get; set; }
    }
}
