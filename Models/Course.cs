using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Metricos.Models
{
    public class Course
    {
        [Key]
        public int ID { get; set; }
        public string NAME_COURSE { get; set; }
        public decimal HOURS { get; set; }
        public DateTime DATE_CREATED { get; set; }
        public string CREATED_BY { get; set; }
        public string TYPE_COURSE { get; set; }
        public DateTime DATE_MAXIMUM { get; set; }

        public bool ACTIVE { get; set; }

    }
}
