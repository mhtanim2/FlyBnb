using AirBnb.Application.Common.Interfaces;
using AirBnb.Application.Common.Utility;
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
        private readonly IUnitOfWork _unitOfWork;

        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<VillaNumber> villaNumbers= _unitOfWork.VillaNumberRepo.GetAll(includeProperties:SD.Villa).ToList();
            return View(villaNumbers);
        }

        public IActionResult Create() {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.VillaRepo.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Create(VillaNumberVM obj) {
            bool isRoomNoExist = _unitOfWork.VillaNumberRepo.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);
                
            if (ModelState.IsValid && !isRoomNoExist)
            {
                _unitOfWork.VillaNumberRepo.Add(obj.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "Villa Number Added Successfully";
                return RedirectToAction(nameof(Index));
            }
            if (isRoomNoExist)
            {
                TempData["warning"] = "Villa number already exist";
                /*return RedirectToAction("Index");*/
            }

            obj.VillaList = _unitOfWork.VillaRepo.GetAll().Select(u=>new SelectListItem { 
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }
        
        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.VillaRepo.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber=_unitOfWork.VillaNumberRepo.Get(x=>x.Villa_Number == villaNumberId)
            };

            if (villaNumberVM == null)
            {
                return RedirectToAction("Error","Home");
            }
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Update(VillaNumberVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.VillaNumberRepo.Update(obj.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "The Villa Number Has been Updated Successfully";
                return RedirectToAction(nameof(Index));
            }

            obj.VillaList = _unitOfWork.VillaRepo.GetAll().Select(u => new SelectListItem
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
                VillaList = _unitOfWork.VillaRepo.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _unitOfWork.VillaNumberRepo.Get(x => x.Villa_Number == villaNumberId)
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
            VillaNumber? objVilla = _unitOfWork.VillaNumberRepo.Get(x => x.Villa_Number ==obj.VillaNumber.Villa_Number);
            if (objVilla is not null)
            {
                _unitOfWork.VillaNumberRepo.Delete(objVilla);
                _unitOfWork.Save();
                TempData["success"] = "Villa Deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["warning"] = "can't delete the villa no";
            return View();
        }
    }
}
