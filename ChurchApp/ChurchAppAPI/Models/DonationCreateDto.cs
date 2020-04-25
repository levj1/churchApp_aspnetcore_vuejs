using System;
using System.ComponentModel.DataAnnotations;

namespace ChurchAppAPI.Models
{
    public class DonationCreateDto
    {

        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        public decimal Amount { get; set; }

        public DateTime DonationDate { get; set; }

        public DateTime DateCreated { get; set; }

        public int DonationTypeId { get; set; }

        public int PersonID { get; set; }


        public bool IsCheck { get; set; }

        public bool IsCash { get; set; }

        public int MemberId { get; set; }
        
    }
}
