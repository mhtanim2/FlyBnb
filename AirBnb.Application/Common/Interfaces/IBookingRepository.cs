using AirBnb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Application.Common.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        void Update(Booking entity);
        //Paymet status
        void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId);
        void UpdateStatus(int bookingId, string orderStatus, int villaNumber);
    }
}
