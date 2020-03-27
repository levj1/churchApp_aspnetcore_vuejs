using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Models
{
    public class DonationTypeCreateDto
    {
        [Required]
        [MaxLength(20)]
        public string Type { get; set; }

        public string Description { get; set; }
    }
}
