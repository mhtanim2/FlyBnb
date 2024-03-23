using AirBnb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Application.Common.Utility
{
    public static class SD
    {
        public const string Role_Customer = "Customer";
        public const string Role_Admin = "Admin";

        public const string VillaAmenity = "VillaAmenity";
        public const string Villa = "Villa";
        public const string User = "User";

        // Status
        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusCheckedIn = "CheckedIn";
        public const string StatusCompleted = "Completed";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        public static char[] stringSplit = new char[] { ',' };

        public static int VillaRoomsAvailable_Count(int villaId,
            List<VillaNumber> villaNumberList, DateOnly checkInDate, int nights,
           List<Booking> bookings)
        {

            List<int> bookingInDate = new(); 
            int finalAvailableRoomForAllNights = int.MaxValue;

            int roomsInVilla = CountRoomsInVilla(villaId,villaNumberList);

            for (int i = 0; i < nights; i++)
            {
                var villasBooked = GetBookedVillasOnCheckIn(villaId,checkInDate,bookings,i);

                foreach (var booking in villasBooked)
                {
                    if (!bookingInDate.Contains(booking.Id))
                    {
                        bookingInDate.Add(booking.Id);
                    }
                }

                var totalAvailableRooms = roomsInVilla - bookingInDate.Count;
                if (totalAvailableRooms == 0)
                {
                    return 0;
                }
                else
                {
                    if (finalAvailableRoomForAllNights > totalAvailableRooms)
                    {
                        finalAvailableRoomForAllNights = totalAvailableRooms;
                    }
                }
            }

            return finalAvailableRoomForAllNights;
        }


        private static int CountRoomsInVilla(int villaId, List<VillaNumber> villaNumberList)
        {
            return villaNumberList.Count(x => x.VillaId == villaId);
        }

        private static IEnumerable<Booking> GetBookedVillasOnCheckIn(int villaId, DateOnly checkInDate, List<Booking> bookings,int i)
        {
            return bookings.Where(u => u.CheckInDate <= checkInDate.AddDays(i)
                && u.CheckOutDate > checkInDate.AddDays(i) && u.VillaId == villaId && u.Status!=StatusPending);
        }
    }
}