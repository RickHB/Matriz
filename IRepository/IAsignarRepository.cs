using Metricos.DTO;

namespace Metricos.IRepository
{
    public interface IAsignarRepository
    {        Task<SpecificCourseDTO> UpdateSpecificEstatus(int nomina, string course, string estatus, string phaseCourse);
             Task<string> AsignCourse(int nomina, string course, string type_course);


    }
}
