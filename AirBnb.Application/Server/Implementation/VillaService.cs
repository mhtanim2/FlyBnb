using AirBnb.Application.Common.Interfaces;
using AirBnb.Application.Common.Utility;
using AirBnb.Application.Services.Interface;
using AirBnb.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Application.Services.Implementation
{
    public class VillaService : IVillaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public readonly IWebHostEnvironment _webHostEnvironment;


        public VillaService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IEnumerable<Villa> GetAllVillas()
        {
            return _unitOfWork.VillaRepo.GetAll(includeProperties: SD.VillaAmenity);
        }
        public void CreateVilla(Villa villa)
        {
            if (villa.Image != null)
            {
                HandleImage(villa);
            }
            else
            {
                villa.ImageUrl = "https://placehold.co/600x400";
            }
            _unitOfWork.VillaRepo.Add(villa);
            _unitOfWork.Save();
        }
        public bool DeleteVilla(int id)
        {
            Villa? objVilla = GetVillaById(id);
            if (objVilla is not null)
            {
                if (!string.IsNullOrEmpty(objVilla.ImageUrl))
                {
                    var oldImagePath =
                           Path.Combine(_webHostEnvironment.WebRootPath, objVilla.ImageUrl.TrimStart('\\'));
                    FileInfo file = new(oldImagePath);
                    if (file.Exists)
                        file.Delete();
                }
                _unitOfWork.VillaRepo.Delete(objVilla);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }
        public Villa GetVillaById(int id)
        {
            return _unitOfWork.VillaRepo.Get(x => x.Id == id, includeProperties: SD.VillaAmenity);
        }
        public void UpdateVilla(Villa villa)
        {
            HandleImage(villa);
            _unitOfWork.VillaRepo.Update(villa);
            _unitOfWork.Save();
        }

        //Private class
        private void HandleImage(Villa villa)
        {
            if (villa.Image != null && IsValidImageExtension(villa.Image.FileName))
            {
                if (!string.IsNullOrEmpty(villa.ImageUrl))
                {
                    DeleteOldImage(villa.ImageUrl);
                }
                string fileName = CreateUniqueFileName(villa.Image.FileName);
                string imagePath = UploadImage(villa.Image, fileName);

                villa.ImageUrl = imagePath;
            }

        }
        private void DeleteOldImage(string imageUrl)
        {
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, imageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
        }
        private string CreateUniqueFileName(string fileName)
        {
            return Guid.NewGuid().ToString() + Path.GetExtension(fileName).ToLower();
        }
        private bool IsValidImageExtension(string fileName)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            string fileExtension = Path.GetExtension(fileName).ToLower();
            return allowedExtensions.Contains(fileExtension);
        }
        private string UploadImage(IFormFile imageFile, string fileName)
        {
            string productPath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\products");
            string fullPath = Path.Combine(productPath, fileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }
            return @"\images\products\" + fileName;
        }

        public IEnumerable<Villa> GetVillaAvailabilityByDate(int nights, DateOnly checkInDate)
        {
            var villaList = _unitOfWork.VillaRepo.GetAll(includeProperties: SD.VillaAmenity).ToList();
            var villaNumbersList = _unitOfWork.VillaNumberRepo.GetAll().ToList();
            var bookedVillas = _unitOfWork.BookingRepo.GetAll(u => u.Status == SD.StatusApproved ||
            u.Status == SD.StatusCheckedIn).ToList();

            foreach (var villa in villaList)
            {
                int roomsAvailable = SD.VillaRoomsAvailable_Count(villa, villaNumbersList, checkInDate, nights, bookedVillas);
                villa.IsAvailable = roomsAvailable > 0 ? true : false;
            }
            return villaList;
        }
    }
}