namespace Metricos.DTO
{
    public class CourseDetailsDto
    {
        public int ID { get; set; }
        public string Name_Course { get; set; }
        public string? Phase_Course { get; set; }
        public string? Type_Course { get; set; }
        public decimal Hours { get; set; }
        public DateTime Date_Created { get; set; }
        public string Created_By { get; set; }
        public bool CourseActive { get; set; }
        public int Nomina { get; set; }
        public int Leader_ID { get; set; }
        public string? Status { get; set; }
        public int? Times_Taken { get; set; }
        public DateTime? Date_Maximum { get; set; }
        public DateTime? Date_Taken { get; set; }
        public DateTime? Date_Validated { get; set; }
    }
}
