using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertsData.Models
{
    [Index("CustomerId", Name = "BookingsCustomerId")]
    [Index("CustomerId", Name = "CustomersBookings")]
    [Index("PackageId", Name = "PackageId")]
    [Index("PackageId", Name = "PackagesBookings")]
    [Index("TripTypeId", Name = "TripTypesBookings")]
    public partial class Booking
    {
        public Booking()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }

        [Key]
        [Display(Name = "Booking ID")]
        public int BookingId { get; set; }
        [Column(TypeName = "datetime")]
        [Display(Name = "Booking Date")]
        [DataType(DataType.Date)]
        public DateTime? BookingDate { get; set; }
        [StringLength(50)]
        [Display(Name = "Booking Number")]
        public string? BookingNo { get; set; }
        [Display(Name = "Traveler Count")]
        public double? TravelerCount { get; set; }
        [Display(Name = "Customer ID")]
        public int? CustomerId { get; set; }
        [StringLength(1)]

        public string? TripTypeId { get; set; }
        [Display(Name = "Package ID")]
        public int? PackageId { get; set; }

        [ForeignKey("CustomerId")]
        [InverseProperty("Bookings")]
        public virtual Customer? Customer { get; set; }
        [ForeignKey("PackageId")]
        [InverseProperty("Bookings")]
        public virtual Package? Package { get; set; }
        [ForeignKey("TripTypeId")]
        [InverseProperty("Bookings")]
        public virtual TripType? TripType { get; set; }
        [InverseProperty("Booking")]
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
