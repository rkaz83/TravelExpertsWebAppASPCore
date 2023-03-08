using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertsData.Models
{
    public partial class Package
    {
        public Package()
        {
            Bookings = new HashSet<Booking>();
            PackagesProductsSuppliers = new HashSet<PackagesProductsSupplier>();
        }

        [Key]
        [Display(Name = "Package ID")]
        public int PackageId { get; set; }
        [StringLength(50)]
        [Display(Name = "Name")]
        public string PkgName { get; set; } = null!;
        [Column(TypeName = "datetime")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? PkgStartDate { get; set; }
        [Column(TypeName = "datetime")]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? PkgEndDate { get; set; }
        [StringLength(50)]
        [Display(Name = "Description")]
        public string? PkgDesc { get; set; }
        [Column(TypeName = "money")]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal PkgBasePrice { get; set; }
        [Column(TypeName = "money")]
        [Display(Name = "Commission")]
        [DataType(DataType.Currency)]
        public decimal? PkgAgencyCommission { get; set; }

        [InverseProperty("Package")]
        public virtual ICollection<Booking> Bookings { get; set; }
        [InverseProperty("Package")]
        public virtual ICollection<PackagesProductsSupplier> PackagesProductsSuppliers { get; set; }
    }
}
