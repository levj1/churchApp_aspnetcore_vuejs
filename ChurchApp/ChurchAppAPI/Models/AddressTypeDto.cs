using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Models
{
    public class AddressTypeDto
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Address Type is required.")]
        public string Name { get; set; }
    }
}
