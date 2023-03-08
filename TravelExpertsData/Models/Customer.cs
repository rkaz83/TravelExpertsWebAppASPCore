using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertsData.Models
{
    [Index("AgentId", Name = "EmployeesCustomers")]
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
            CreditCards = new HashSet<CreditCard>();
            CustomersRewards = new HashSet<CustomersReward>();
        }

        [Key]
        public int CustomerId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [StringLength(25)]
        public string CustFirstName { get; set; } = null!;
        [StringLength(25)]
        [Display(Name = "Last Name")]
        public string CustLastName { get; set; } = null!;
        [StringLength(75)]
        [Display(Name = "Address")]
        public string CustAddress { get; set; } = null!;
        [StringLength(50)]
        [Display(Name = "City")]
        public string CustCity { get; set; } = null!;
        [StringLength(2)]
        [Display(Name = "Province")]
        public string CustProv { get; set; } = null!;
        [StringLength(7)]
        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Postal Code is Required")]
        [RegularExpression(@"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$", ErrorMessage = "Invalid Postal Code [T6R 3E2]")]
        public string CustPostal { get; set; } = null!;
        [StringLength(25)]
        [Display(Name = "Country")]
        public string? CustCountry { get; set; }
        [StringLength(20)]
        [Display(Name = "Home Phone")]
        [Required(ErrorMessage = "Telephone Number Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.[000-000-0000]")]
        public string? CustHomePhone { get; set; }
        [StringLength(20)]
        [Display(Name = "Business Phone")]
        [Required(ErrorMessage = "Business phone Number Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.[000-000-0000]")]
        public string? CustBusPhone { get; set; } = null!;
        [StringLength(50)]
        [Display(Name = "Email Address")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string? CustEmail { get; set; } = null!;
        public int? AgentId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [Required(ErrorMessage = "Username is Required")]
        public string? Username { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required")]
        //[Compare("Password", ErrorMessage = "Password must be same as Confirm Password.")]

        public string? Password { get; set; }

        //[Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password did not match.")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [ForeignKey("AgentId")]
        [InverseProperty("Customers")]
        public virtual Agent? Agent { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<Booking> Bookings { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<CustomersReward> CustomersRewards { get; set; }
    }
}
