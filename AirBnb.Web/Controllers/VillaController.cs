using AirBnb.Application.Common.Interfaces;
using AirBnb.Domain.Entities;
using AirBnb.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public readonly IWebHostEnvironment _webHostEnvironment;
        

        public VillaController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Villa> villas= _unitOfWork.VillaRepo.GetAll();
            return View(villas);
        }
       
        public IActionResult Create() {
            ViewBag.Action = "create";
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Villa obj) {
            if (ModelState.IsValid)
            {
                if (obj.Image != null)
                {
                    string fileExtension = Path.GetExtension(obj.Image.FileName).ToLower();
                    if (fileExtension == ".jpg" || fileExtension == ".png") {
                        string fileName = Guid.NewGuid().ToString() + fileExtension;
                        string productPath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\products");// set the file path you want to updload the image

                        if (!string.IsNullOrEmpty(obj.ImageUrl))
                        {
                            //delete the old image
                            var oldImagePath =
                                Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));

                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                        {
                            obj.Image.CopyTo(fileStream);
                        }

                        obj.ImageUrl = @"\images\products\" + fileName;
                    }

                }
                else
                {
                    obj.ImageUrl = "https://placehold.co/600x400";
                }
                _unitOfWork.VillaRepo.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Villa Added Successfully";
                return RedirectToAction("Index");
            }
            ViewBag.Action = "create";
            return View(obj);
        }

        public IActionResult Update(int villaId)
        {
            Villa villa = _unitOfWork.VillaRepo.Get(x => x.Id == villaId);
            ViewBag.Action = "update";
            if (villa == null)
            {
                return RedirectToAction("Error","Home");
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                if (obj.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    string productPath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\products");

                    if (!string.IsNullOrEmpty(obj.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath =
                            Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        obj.Image.CopyTo(fileStream);
                    }

                    obj.ImageUrl = @"\images\products\" + fileName;
                }
                _unitOfWork.VillaRepo.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Villa Updated Successfully";
                ViewBag.Action = "update";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int villaId)
        {
            Villa villa = _unitOfWork.VillaRepo.Get(x => x.Id == villaId);
            if (villa is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villa);
        }
        [HttpPost]
        public IActionResult Delete(Villa villa)
        {
            Villa? objVilla = _unitOfWork.VillaRepo.Get(x => x.Id == villa.Id);
            if (objVilla is not null)
            {
                if (!string.IsNullOrEmpty(objVilla.ImageUrl))
                {
                    var oldImagePath =
                           Path.Combine(_webHostEnvironment.WebRootPath, objVilla.ImageUrl.TrimStart('\\'));
                    FileInfo file = new(oldImagePath);

                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                _unitOfWork.VillaRepo.Delete(objVilla);
                _unitOfWork.Save();
                TempData["error"] = "Villa Deleted successfully";
                return RedirectToAction("Index");
            }
            return View(villa);
        }
    }
}
