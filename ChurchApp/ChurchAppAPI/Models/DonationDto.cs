using ChurchAppAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Models
{
    public class DonationDto
    {
        public int ID { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        public decimal Amount { get; set; }

        public DateTime DonationDate { get; set; }

        public int DonationTypeId { get; set; }
        public DonationTypeDto DonationType { get; set; }

        public int PersonID { get; set; }

        public PersonDto Person { get; set; }
    }
}
