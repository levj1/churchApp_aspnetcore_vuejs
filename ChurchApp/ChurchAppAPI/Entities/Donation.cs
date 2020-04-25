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

        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        public decimal Amount { get; set; }

        public DateTime DonationDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DonationCreatedDate { get; set; }

        public int DonationTypeId { get; set; }

        public DonationType DonationType { get; set; }

        public string DonationTypeType
        {
            get
            {
                if (DonationType == null) return "";
                return DonationType.Type;
            }
        }

        public int PersonID { get; set; }

        public Person Person { get; set; }

        public bool IsCheck { get; set; }

        public bool IsCash { get; set; }


        public int MemberId { get; set; }
    }
}
