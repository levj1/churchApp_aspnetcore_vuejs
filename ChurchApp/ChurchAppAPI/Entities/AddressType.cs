using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChurchAppAPI.Entities
{
    public class AddressType
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }
    }
}