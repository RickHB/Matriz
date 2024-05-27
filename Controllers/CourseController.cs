using Metricos.DTO;
using Metricos.IRepository;
using Metricos.Models;
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


        [HttpGet("AllCourses")]
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
        [HttpPost("CreateCourse")]
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

        [HttpGet("GetSpecificCourse")]
        public async Task<IActionResult> GetResumen(string course)
        {
            try
            {
                var collection = await _courseRepository.GetSpecificCourse(course); // Asegúrate de pasar el nombre correcto del curso
                return Ok(collection);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetSpecificStatus")]
        public async Task<IActionResult> GetEstatus(string course, string estatus)
        {
            try
            {
                var collection = await _courseRepository.GetSpecificEstatus(course, estatus); // Asegúrate de pasar el nombre correcto del curso
                return Ok(collection);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
