using Metricos.DTO;
using Metricos.Models;

namespace Metricos.IRepository
{
    public interface ICourseRepository
    {
        ICollection<Course> GetCourses();
        void CreateCourse(CreateCourseDTO newCouse);
        //void UpdateCourse(Course course);

        //void DeleteCourse(Course course);
    }
}
