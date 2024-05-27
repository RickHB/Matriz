namespace Metricos.Models
{
    public class Details_Course
    {
        public int ID { get; set; }
        public int ID_COURSE { get; set; }
        public int ID_NOMINA { get; set; }
        public int? ID_Phases_Course { get; set; }
        public string? STATUS { get; set; }
        public int? TIMES_TAKEN { get; set; }
        public DateTime? DATE_TAKEN { get; set; }
        public DateTime? DATE_VALIDATED { get; set; }
    }
}
