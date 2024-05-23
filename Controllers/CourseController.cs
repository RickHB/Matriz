using Metricos.DTO;
using Metricos.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Metricos.Controllers
{
    [ApiController]

    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        [HttpGet("Cursos")]
        public IActionResult Index()
        {
            try
            {
                var collectionCourses = _courseRepository.GetCourses();
                return Ok(collectionCourses);


            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CrearCursos")]
        public IActionResult CrearCurso(CreateCourseDTO createCourseDTO)
        {
            try
            {
                 _courseRepository.CreateCourse(createCourseDTO);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
