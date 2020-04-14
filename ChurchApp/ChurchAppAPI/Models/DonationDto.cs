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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DonationDate { get; set; }

        public int DonationTypeId { get; set; }
        public DonationTypeDto DonationType { get; set; }

        public int PersonID { get; set; }

        public PersonWithoutAddressDto Person { get; set; }

        public bool IsCheck { get; set; }

        public bool IsCash { get; set; }


        public int MemberId { get; set; }
    }
}
