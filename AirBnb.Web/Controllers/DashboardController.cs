using AirBnb.Application.Common.Interfaces;
using AirBnb.Domain.SharedModels;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> GetTotalBookingsChartData()
        {
            RadialBarChartVM dashboardRadialBarChartVM = await _dashboardRepository.GetBookingsChartDataAsync();
            return Json(dashboardRadialBarChartVM);
        }
        public async Task<IActionResult> GetTotalRevenueChartData()
        {
            RadialBarChartVM dashboardRadialBarChartVM = await _dashboardRepository.GetRevenueChartDataAsync();
            return Json(dashboardRadialBarChartVM);
        }

        public async Task<IActionResult> GetRegisteredUserChartData()
        {
            RadialBarChartVM dashboardRadialBarChartVM = await _dashboardRepository.GetRegisteredUserChartDataAsync();
            return Json(dashboardRadialBarChartVM);
        }

        public async Task<IActionResult> GetMemberAndBookingChartData()
        {
            DashboardLineChartVM dashboardLineChartVM = await _dashboardRepository.GetMemberAndBookingChartDataAsync();

            return Json(dashboardLineChartVM);
        }

        public async Task<IActionResult> GetCustomerBookingsPieChartData()
        {
            DashboardPieChartVM dashboardPieChartVM = await _dashboardRepository.GetBookingPieChartDataAsync();

            return Json(dashboardPieChartVM);
        }
    }
}