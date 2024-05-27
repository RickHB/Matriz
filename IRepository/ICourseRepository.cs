using Metricos.DTO;
using Metricos.Models;
using System.Collections;

namespace Metricos.IRepository
{
    public interface ICourseRepository
    {
        ICollection<Course> GetCourses();
        Task<ICollection<SpecificCourseDTO>> GetSpecificCourse(string course);
        Task<ICollection<SpecificCourseDTO>> GetSpecificEstatus(string course, string estatus);

        void CreateCourse(CreateCourseDTO newCouse);
        //void UpdateCourse(Course course);

        //void DeleteCourse(Course course);
    }
}
