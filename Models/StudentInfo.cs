using System.ComponentModel.DataAnnotations;

namespace ModelBinding.Models
{
    public class StudentInfo
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Contact { get; set; }

    }
}
