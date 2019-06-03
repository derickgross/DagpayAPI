using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace DagpayApi.Models
{
    public class User
    {
        [Required]
        [EmailAddress]
        public string Username {get; set;}

        [Required]
        public string Password { get; set; }
    }
}
