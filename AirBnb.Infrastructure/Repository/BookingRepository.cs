using AirBnb.Application.Common.Interfaces;
using AirBnb.Application.Common.Utility;
using AirBnb.Domain.Entities;
using AirBnb.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Infrastructure.Repository
{
  public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Booking entity)
        {
            _context.Bookings.Update(entity);
        }

        public void UpdateStatus(int bookingId, string orderStatus, int villaNumber = 0)
        {
            var orderFromDb = _context.Bookings.FirstOrDefault(u => u.Id == bookingId);
            if (orderFromDb != null)
            {
                orderFromDb.Status = orderStatus;
                if (orderStatus == SD.StatusCheckedIn && villaNumber > 0)
                {
                    orderFromDb.VillaNumber = villaNumber;
                    orderFromDb.ActualCheckInDate = DateTime.Now;
                }
                if (orderStatus == SD.StatusCompleted)
                {
                    orderFromDb.ActualCheckOutDate = DateTime.Now;
                }

            }

        }

        public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
        {
            var bookingFromDb = _context.Bookings.FirstOrDefault(u => u.Id == id);
            if (bookingFromDb!=null)
                if (!string.IsNullOrEmpty(sessionId))
                {
                    bookingFromDb.StripeSessionId = sessionId;
                }
                if (!string.IsNullOrEmpty(paymentIntentId))
                {
                    bookingFromDb.StripePaymentIntentId = paymentIntentId;
                    bookingFromDb.PaymentDate = DateTime.Now;
                    bookingFromDb.IsPaymentSuccessful = true;
                }
        }
    }
}
