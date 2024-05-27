namespace Metricos.DTO
{
    public class SpecificCourseDTO
    {
        public int ID { get; set; }
        public int Nomina { get; set; }
        public string Phase_Course { get; set;}
        public string Type_Course { get; set; }
        public string Status { get; set; }
        public int? Times_Taken { get; set; }
        public DateTime? Date_Taken { get; set; }
        public DateTime? Date_Validated { get; set; }
        public DateTime DATE_MAXIMUM { get; set; }

    }
}
