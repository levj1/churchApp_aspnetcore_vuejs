using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Entities
{
    public class Donation
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public decimal Amount { get; set; }

        public DateTime DonationDate { get; set; }

        public int PersonID { get; set; }

        public Person Person { get; set; }
    }
}
