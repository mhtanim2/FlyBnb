using AirBnb.Application.Common.Interfaces;
using AirBnb.Application.Services.Interface;
using AirBnb.Domain.Entities;
using AirBnb.Infrastructure.Data;
using AirBnb.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        public VillaController(IVillaService villaService)
        {
            _villaService = villaService;
        }

        public IActionResult Index()
        {
            IEnumerable<Villa> villas = _villaService.GetAllVillas();
            return View(villas);
        }

        public IActionResult Create()
        {
            ViewBag.Action = "create";
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning"] = "Input field is not correct";
                ViewBag.Action = "create";
                return View(obj);
            }
            _villaService.CreateVilla(obj);
            TempData["success"] = "Villa Added Successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Update(int villaId)
        {
            Villa villa = _villaService.GetVillaById(villaId);
            ViewBag.Action = "update";
            if (villa == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villa);
        }
        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                _villaService.UpdateVilla(obj);
                TempData["success"] = "Villa Updated Successfully";
                ViewBag.Action = "update";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int villaId)
        {
            Villa villa = _villaService.GetVillaById(villaId);
            if (villa is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villa);
        }
        [HttpPost]
        public IActionResult Delete(Villa villa)
        {
            if (_villaService.DeleteVilla(villa.Id))
            {
                TempData["success"] = "Villa Deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa Delete unsuccessfull";
            return View(villa);
        }
    }
}