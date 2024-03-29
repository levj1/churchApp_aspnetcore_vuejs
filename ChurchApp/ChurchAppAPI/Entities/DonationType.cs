﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Entities
{
    public class DonationType
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Type { get; set; }

        public string Description { get; set; }
    }
}
