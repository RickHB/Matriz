using Metricos.Context;
using Metricos.DTO;
using Metricos.IRepository;
using Metricos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Phases_CourseRepository : IPhases_CourseRepository
{
    private readonly SqlDbContext _context;

    public Phases_CourseRepository(SqlDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CourseDetailsDto>> GetCourseDetailsAsync()
    {
        return await (from c in _context.Course
                      join de in _context.Details_Course on c.ID equals de.ID_COURSE
                      join em in _context.Employee on de.ID_NOMINA equals em.NOMINA
                      join ph in _context.Phases_Course on de.ID_Phases_Course equals ph.ID into phJoin
                      from ph in phJoin.DefaultIfEmpty()
                      select new CourseDetailsDto
                      {
                          ID = de.ID,
                          Name_Course = c.NAME_COURSE,
                          Phase_Course = ph.PHASE_COURSE,
                          Type_Course = ph.TYPE_COURSE,
                          Hours = c.HOURS,
                          Date_Created = c.DATE_CREATED,
                          Created_By = c.CREATED_BY,
                          CourseActive = c.ACTIVE,
                          Nomina = em.NOMINA,
                          Leader_ID = em.LEADER_ID,
                          Status = de.STATUS,
                          Times_Taken = de.TIMES_TAKEN,
                          Date_Maximum = c.DATE_MAXIMUM, // No changes needed here
                          Date_Taken = de.DATE_TAKEN,
                          Date_Validated = de.DATE_VALIDATED
                      }).ToListAsync();
    }

    public async Task<ICollection<ResumenDTO>> Resumen()
    {
        try
        {
            var query = from c in _context.Course
                        join de in _context.Details_Course on c.ID equals de.ID_COURSE
                        group new { c, de } by new { c.NAME_COURSE, c.TYPE_COURSE } into g
                        select new ResumenDTO
                        {
                            COURSE = g.Key.NAME_COURSE,
                            TYPE_COURSE = g.Key.TYPE_COURSE,
                            PENDIENTE = g.Count(x => x.de.STATUS == "Pendiente"),
                            PROGRESO = g.Count(x => x.de.STATUS == "En Progreso"),
                            COMPLETADO = g.Count(x => x.de.STATUS == "Completo"),
                            REPROBADO = g.Count(x => x.de.STATUS == "Reprobado")
                        };

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            // Log the exception details
            Console.Error.WriteLine($"Error: {ex}");
            throw new BadHttpRequestException($"Error al obtener el resumen del curso: {ex.Message}", ex);
        }
    }

}
