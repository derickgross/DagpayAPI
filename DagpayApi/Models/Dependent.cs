using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DagpayApi.Models
{
    public class Dependent
    {
        [Required]
        public int DependentId { get; set; }

        [Required]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public int DiscountFactor { get; set; }
    }
}
