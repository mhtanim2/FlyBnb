using AirBnb.Application.Common.Interfaces;
using AirBnb.Application.Common.Utility;
using AirBnb.Domain.Entities;
using AirBnb.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirBnb.Web.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Amenity> AmenityList = _unitOfWork.AmenityRepo.GetAll(includeProperties: SD.Villa).ToList();
            return View(AmenityList);
        }
        public IActionResult Create()
        {

            AmenityVM AmenityVM = new()
            {
                VillaList = _unitOfWork.VillaRepo.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(AmenityVM);
        }

        [HttpPost]
        public IActionResult Create(AmenityVM AmenityVM)
        {
            //Remove some validations
            ModelState.Remove("Amenity.Villa");


            if (ModelState.IsValid)
            {
                _unitOfWork.AmenityRepo.Add(AmenityVM.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "Amenity created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(AmenityVM);
        }

        public IActionResult Update(int amenityId)
        {
            AmenityVM AmenityVM = new()
            {
                VillaList = _unitOfWork.VillaRepo.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.AmenityRepo.Get(u => u.Id == amenityId)
            };
            if (AmenityVM.Amenity == null)
            {
                return RedirectToAction("error", "home");
            }
            return View(AmenityVM);
        }

        [HttpPost]
        public IActionResult Update(AmenityVM AmenityVM)
        {
            ModelState.Remove("Amenity.Villa");
            if (ModelState.IsValid)
            {
                _unitOfWork.AmenityRepo.Update(AmenityVM.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "Amenity updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(AmenityVM);
        }

        public IActionResult Delete(int amenityId)
        {
            AmenityVM AmenityVM = new()
            {
                VillaList = _unitOfWork.VillaRepo.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.AmenityRepo.Get(u => u.Id == amenityId)
            };
            if (AmenityVM.Amenity == null)
            {
                return RedirectToAction("error", "home");
            }
            return View(AmenityVM);
        }

        [HttpPost]
        public IActionResult Delete(AmenityVM AmenityVM)
        {
            Amenity? objFromDb = _unitOfWork.AmenityRepo.Get(x => x.Id == AmenityVM.Amenity.Id);
            if (objFromDb != null)
            {
                _unitOfWork.AmenityRepo.Delete(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Amenity Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(AmenityVM);
        }
    }
}
