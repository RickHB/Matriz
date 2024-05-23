using System.ComponentModel.DataAnnotations;

namespace Metricos.Models
{
    public class Employee
    {
        [Key]
        public int NOMINA { get; set; }
        public int LEADER_ID { get; set; }
        public bool ACTIVE{ get; set; }
    }
}
