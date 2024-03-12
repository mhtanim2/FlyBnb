using AirBnb.Application.Common.Interfaces;
using AirBnb.Application.Common.Utility;
using AirBnb.Application.Services.Interface;
using AirBnb.Domain.Entities;
using AirBnb.Infrastructure.Data;
using AirBnb.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AirBnb.Web.Controllers
{
    [Authorize]
    public class VillaNumberController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IVillaNumberService _villaNumberService;

        public VillaNumberController(IVillaService villaService,
            IVillaNumberService villaNumberService)
        {
            _villaService = villaService;
            _villaNumberService = villaNumberService;
        }
        public IActionResult Index()
        {
            IEnumerable<VillaNumber> villaNumbers = _villaNumberService.GetAllVillaNumbers();
            return View(villaNumbers);
        }
        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _villaService.GetAllVillas().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {
            bool isRoomNoExist = _villaNumberService.CheckVillaNumberExist(obj.VillaNumber.Villa_Number);

            if (ModelState.IsValid && !isRoomNoExist)
            {
                _villaNumberService.CreateVillaNumber(obj.VillaNumber);

                TempData["success"] = "Villa Number Added Successfully";
                return RedirectToAction(nameof(Index));
            }
            if (isRoomNoExist)
            {
                TempData["warning"] = "Villa number already exist";
                /*return RedirectToAction("Index");*/
            }

            obj.VillaList = _villaService.GetAllVillas().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }

        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _villaService.GetAllVillas().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _villaNumberService.GetVillaNumberById(villaNumberId)
            };

            if (villaNumberVM == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Update(VillaNumberVM obj)
        {
            if (ModelState.IsValid)
            {
                _villaNumberService.UpdateVillaNumber(obj.VillaNumber);
                TempData["success"] = "The Villa Number Has been Updated Successfully";
                return RedirectToAction(nameof(Index));
            }

            obj.VillaList = _villaService.GetAllVillas().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }

        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _villaService.GetAllVillas().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _villaNumberService.GetVillaNumberById(villaNumberId)
            };

            if (villaNumberVM == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Delete(VillaNumberVM obj)
        {
            VillaNumber? objVilla = _villaNumberService.GetVillaNumberById(obj.VillaNumber.Villa_Number);
            if (objVilla is not null)
            {
                _villaNumberService.DeleteVillaNumber(objVilla.Villa_Number);

                TempData["success"] = "Villa Deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["warning"] = "can't delete the villa no";
            return View();
        }
    }
}