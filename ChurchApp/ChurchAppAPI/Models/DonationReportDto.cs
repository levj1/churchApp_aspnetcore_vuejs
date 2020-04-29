using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Models
{
    public class DonationReportDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int DonationTypeId { get; set; }
    }
}
