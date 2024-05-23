using Metricos.Context;
using Metricos.DTO;
using Metricos.IRepository;
using Metricos.Models;

namespace Metricos.Repository
{
    public class CourserRepository : ICourseRepository
    {
        private readonly SqlDbContext _dbContext;
        public CourserRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<Course> GetCourses()
        {
            return _dbContext.Course.ToList();
            
        }

        public void CreateCourse(CreateCourseDTO newCourse)
        {
            var course = new Course
            {
                NAME_COURSE = newCourse.NAME_COURSE,
                HOURS = newCourse.HOURS,
                TYPE_COURSE = newCourse.TYPE_COURSE,
                DATE_CREATED = newCourse.DATE_CREATED,
                CREATED_BY = newCourse.CREATED_BY,  
                ACTIVE = true
            };

            _dbContext.Course.Add(course);
            _dbContext.SaveChanges();
        }
    }
}
