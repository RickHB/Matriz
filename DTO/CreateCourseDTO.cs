using System.ComponentModel.DataAnnotations;

namespace Metricos.DTO
{
    public class CreateCourseDTO
    {
        [Required]
        public string NAME_COURSE { get; set; }

        [Required]
        public decimal HOURS { get; set; }

        [Required]
        public string CREATED_BY { get; set; }

        [Required]
        public DateTime DATE_CREATED { get; set; }

        [Required]
        public DateTime DATE_MAXIMUM { get; set; }
        [Required]
        public string TYPE_COURSE { get; set; }

    }
}
