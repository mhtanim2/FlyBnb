using AirBnb.Domain.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Application.Common.Interfaces
{
    public interface IDashboardRepository
    {
        Task<RadialBarChartVM> GetBookingsChartDataAsync();
        Task<RadialBarChartVM> GetRevenueChartDataAsync();
        Task<RadialBarChartVM> GetRegisteredUserChartDataAsync();
        Task<DashboardLineChartVM> GetMemberAndBookingChartDataAsync();
        Task<DashboardPieChartVM> GetBookingPieChartDataAsync();
    }
}
