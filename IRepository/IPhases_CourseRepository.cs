using Metricos.DTO;

namespace Metricos.IRepository
{
    public interface IPhases_CourseRepository
    {
           Task<IEnumerable<CourseDetailsDto>> GetCourseDetailsAsync();
           Task<ICollection<ResumenDTO>> Resumen();
    }
}
