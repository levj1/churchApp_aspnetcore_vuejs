
using System.ComponentModel.DataAnnotations;

namespace ChurchAppAPI.Models
{
    public class PersonWithoutAddressDto
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "First Name Required.")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name Required.")]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                if(string.IsNullOrEmpty(MiddleName))
                    return $"{FirstName} {MiddleName} {LastName}";

                return $"{FirstName} {LastName}";
            }
        }
    }
}
