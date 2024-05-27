using Metricos.Context;
using Metricos.DTO;
using Metricos.IRepository;
using Metricos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Metricos.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SqlDbContext _context;
        public CourseRepository(SqlDbContext dbContext)
        {
            _context = dbContext;
        }

        public ICollection<Course> GetCourses()
        {
            return _context.Course.ToList();

        }

        public void CreateCourse(CreateCourseDTO newCourse)
        {
            var course = new Course
            {
                NAME_COURSE = newCourse.NAME_COURSE,
                HOURS = newCourse.HOURS,
                TYPE_COURSE = newCourse.TYPE_COURSE,
                DATE_CREATED = newCourse.DATE_CREATED,
                DATE_MAXIMUM = newCourse.DATE_MAXIMUM,
                CREATED_BY = newCourse.CREATED_BY,
                ACTIVE = true
            };

            _context.Course.Add(course);
            _context.SaveChanges();
        }
        public async Task<ICollection<SpecificCourseDTO>> GetSpecificCourse(string course)
        {
            try
            {
                return await (from c in _context.Course
                              join de in _context.Details_Course on c.ID equals de.ID_COURSE
                              join em in _context.Employee on de.ID_NOMINA equals em.NOMINA
                              join ph in _context.Phases_Course on de.ID_Phases_Course equals ph.ID into phJoin
                              from ph in phJoin.DefaultIfEmpty()
                              where c.NAME_COURSE == course
                              select new SpecificCourseDTO
                              {
                                  ID = de.ID,
                                  Nomina = em.NOMINA,
                                  Phase_Course = ph.PHASE_COURSE,
                                  Type_Course = ph.TYPE_COURSE,
                                  Status = de.STATUS,
                                  Times_Taken = de.TIMES_TAKEN,
                                  Date_Taken = de.DATE_TAKEN,
                                  Date_Validated = de.DATE_VALIDATED,
                                  DATE_MAXIMUM = c.DATE_MAXIMUM

                              }).ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.Error.WriteLine($"Error: {ex}");
                throw new BadHttpRequestException($"Error al obtener los detalles del curso: {ex.Message}", ex);
            }
        }

        public async Task<ICollection<SpecificCourseDTO>> GetSpecificEstatus(string course, string estatus)
        {
            try
            {
                return await(from c in _context.Course
                             join de in _context.Details_Course on c.ID equals de.ID_COURSE
                             join em in _context.Employee on de.ID_NOMINA equals em.NOMINA
                             join ph in _context.Phases_Course on de.ID_Phases_Course equals ph.ID into phJoin
                             from ph in phJoin.DefaultIfEmpty()
                             where c.NAME_COURSE == course
                             where de.STATUS == estatus

                             select new SpecificCourseDTO
                             {
                                 ID = de.ID,
                                 Nomina = em.NOMINA,
                                 Phase_Course = ph.PHASE_COURSE,
                                 Type_Course = ph.TYPE_COURSE,
                                 Status = de.STATUS,
                                 Times_Taken = de.TIMES_TAKEN,
                                 Date_Taken = de.DATE_TAKEN,
                                 Date_Validated = de.DATE_VALIDATED,
                                 DATE_MAXIMUM = c.DATE_MAXIMUM

                             }).ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.Error.WriteLine($"Error: {ex}");
                throw new BadHttpRequestException($"Error al obtener los detalles del curso: {ex.Message}", ex);
            }
        }
    }
}