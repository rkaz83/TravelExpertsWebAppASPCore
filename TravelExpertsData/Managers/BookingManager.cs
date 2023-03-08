using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertsData.Models;

namespace TravelExpertsData.Managers
{
    /// <summary>
    /// Author: Aaron Li
    /// February 18, 2023
    /// </summary>
    public static class BookingManager
    {
        /// <summary>
        /// Retrieves a list of customer bookings from the database
        /// </summary>
        /// <param name="db">Travel Experts Database context</param>
        /// <param name="id">Customer ID</param>
        /// <returns>List of bookings for the customer</returns>
        public static List<Booking> GetCustomerBookings(TravelExpertsContext db, int id)
        {
            List<Booking> bookings = db.Bookings
                .Where(b => b.CustomerId == id)
                .Include(b => b.BookingDetails)
                .OrderByDescending(b => b.BookingDate)
                .ToList();
            return bookings;
        }
        /// <summary>
        /// Retrieves specific booking from the booking ID
        /// </summary>
        /// <param name="db">Travel Experts Databaase context</param>
        /// <param name="id">Booking ID</param>
        /// <returns>Booking object</returns>
        public static Booking GetBooking(TravelExpertsContext db, int id)
        {
            Booking booking = db.Bookings.Find(id);
            return booking;
        }
        /// <summary>
        /// Retreives booking details for the booking
        /// </summary>
        /// <param name="db">Travel Experts Database context</param>
        /// <param name="id">Booking ID</param>
        /// <returns>Booking details for the specific booking</returns>
        public static List<BookingDetail> GetBookingDetails(TravelExpertsContext db, int id)
        {
            List<BookingDetail> details = db.BookingDetails
                .Where(bd => bd.BookingId == id || bd.BookingDetailId == id)
                .ToList();
            return details;
        }

        /// <summary>
        /// Returns the booking detail ID given a booking ID
        /// </summary>
        /// <param name="db">Travel Experts Database context</param>
        /// <param name="id">Booking ID</param>
        /// <returns>Booking Detail ID</returns>
        public static int? GetBookingDetailID(TravelExpertsContext db, int id)
        {
            BookingDetail detail = db.BookingDetails.Find(id);
            int? detailID = detail.BookingDetailId;
            return detailID;
        }

        /// <summary>
        /// Adds booking to the database
        /// </summary>
        /// <param name="db">Travel Experts Database context</param>
        /// <param name="booking">The booking to be added to the database</param>
        /// <returns>Booking ID of the booking</returns>
        public static int AddBooking(TravelExpertsContext db, Booking booking)
        {
            db.Bookings.Add(booking);
            db.SaveChanges();
            return booking.BookingId;
        }

        /// <summary>
        /// Adds booking details to the database
        /// </summary>
        /// <param name="db">Travel Experts Database context</param>
        /// <param name="detail">The booking detail to be added to the database</param>
        public static void AddBookingDetails(TravelExpertsContext db, BookingDetail detail)
        {
            db.BookingDetails.Add(detail);
            db.SaveChanges();
        }

        /// <summary>
        /// Deletes booking from the database
        /// </summary>
        /// <param name="db">Travel Experts Database context</param>
        /// <param name="id">Booking ID of the booking to be deleted</param>
        public static void DeleteBooking(TravelExpertsContext db, int? id)
        {
            Booking? booking = db.Bookings.Find(id);
            if (booking != null)
            {
                db.Bookings.Remove(booking);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes booking details from the database
        /// </summary>
        /// <param name="db">Travel Experts Database connection</param>
        /// <param name="id">Booking ID of the booking details to be deleted</param>
        public static void DeleteBookingDetails(TravelExpertsContext db, int? id)
        {
            BookingDetail? detail = db.BookingDetails.Find(id);
            if(detail != null)
            {
                db.BookingDetails.Remove(detail);
                db.SaveChanges();
            }
        }
    }
}
