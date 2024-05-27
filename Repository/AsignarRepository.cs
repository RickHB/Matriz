using Metricos.Context;
using Metricos.DTO;
using Metricos.IRepository;
using Metricos.Models;
using Microsoft.EntityFrameworkCore;

namespace Metricos.Repository
{
    public class AsignarRepository : IAsignarRepository
    {
        private readonly SqlDbContext _context;
        public AsignarRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<string> AsignCourse(int nomina, string course, string type_course = null)
        {
            try
            {
                // Obtener la instancia de Employee
                var employee = await _context.Employee.FirstOrDefaultAsync(e => e.NOMINA == nomina);
                if (employee == null)
                {
                    throw new BadHttpRequestException($"No se encontró al empleado con la nómina {nomina}");
                }

                // Obtener la instancia de Course
                var courseEntity = await _context.Course.FirstOrDefaultAsync(c => c.NAME_COURSE == course);
                if (courseEntity == null)
                {
                    throw new BadHttpRequestException($"No se encontró el curso '{course}'");
                }

                // Obtener la instancia de Phases_Course
                var phaseEntity = await _context.Phases_Course
                    .FirstOrDefaultAsync(p => string.IsNullOrEmpty(type_course) || p.TYPE_COURSE == type_course);

                // Verificar si ya existe un registro para el empleado y el curso
                var existingRecord = await _context.Details_Course
                    .FirstOrDefaultAsync(dc => dc.ID_NOMINA == employee.NOMINA && dc.ID_COURSE == courseEntity.ID);

                if (existingRecord != null)
                {
                    // Si el registro existente tiene un estatus de "Reprobado", eliminarlo
                    if (existingRecord.STATUS == "Reprobado")
                    {
                        _context.Details_Course.Remove(existingRecord);
                    }
                    else
                    {
                        throw new BadHttpRequestException($"Ya existe un registro para el empleado {employee.NOMINA} y el curso {course}");
                    }
                }

                // Crear una nueva instancia de Details_Course
                var detailsCourse = new Details_Course
                {
                    ID_NOMINA = employee.NOMINA,
                    ID_COURSE = courseEntity.ID,
                    ID_Phases_Course = phaseEntity?.ID, // Asignar el ID de phaseEntity si no es nulo, de lo contrario, asignar null
                    STATUS = "Pendiente",
                    TIMES_TAKEN = existingRecord != null ? existingRecord.TIMES_TAKEN + 1 : 1, // Sumar 1 al valor de TIMES_TAKEN si existe un registro previo
                    DATE_TAKEN = null,
                    DATE_VALIDATED = null
                };

                // Agregar la nueva instancia al contexto y guardar los cambios
                _context.Details_Course.Add(detailsCourse);
                await _context.SaveChangesAsync();

                return "OK";
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.Error.WriteLine($"Error: {ex}");
                throw new BadHttpRequestException($"Error al asignar el curso: {ex.Message}", ex);
            }
        }

        public async Task<SpecificCourseDTO> UpdateSpecificEstatus(int nomina, string course, string estatus, string phaseCourse)
        {
            try
            {
                var detailsCourse = await (from c in _context.Course
                                           join de in _context.Details_Course on c.ID equals de.ID_COURSE
                                           join em in _context.Employee on de.ID_NOMINA equals em.NOMINA
                                           join ph in _context.Phases_Course on de.ID_Phases_Course equals ph.ID into phJoin
                                           from ph in phJoin.DefaultIfEmpty()
                                           where c.NAME_COURSE == course
                                           where em.NOMINA == nomina
                                           select de).FirstOrDefaultAsync();

                if (detailsCourse == null)
                {
                    throw new BadHttpRequestException("Course not found");
                }

                detailsCourse.STATUS = estatus;

                if (phaseCourse != null)
                {
                    var phase = await _context.Phases_Course.FirstOrDefaultAsync(pc => pc.PHASE_COURSE == phaseCourse);
                    if (phase != null)
                    {
                        detailsCourse.ID_Phases_Course = phase.ID;
                    }
                }

                detailsCourse.DATE_TAKEN = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new SpecificCourseDTO
                {
                    ID = detailsCourse.ID,
                    Nomina = nomina,
                    Phase_Course = phaseCourse,
                    Status = estatus,
                    Times_Taken = detailsCourse.TIMES_TAKEN,
                    Date_Taken = detailsCourse.DATE_TAKEN,
                    Date_Validated = detailsCourse.DATE_VALIDATED,
                };
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.Error.WriteLine($"Error: {ex}");
                throw new BadHttpRequestException($"Error al actualizar los detalles del curso: {ex.Message}", ex);
            }
        }


    }
}
