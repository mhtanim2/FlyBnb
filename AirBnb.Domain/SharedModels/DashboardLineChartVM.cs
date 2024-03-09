using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Domain.SharedModels
{
    public class DashboardLineChartVM
    {
        public List<ChartData> Series { get; set; }
        public string[] Categories { get; set; }
    }
}
