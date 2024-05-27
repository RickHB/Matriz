using Metricos.IRepository;
using Metricos.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Metricos.Controllers
{
    [ApiController]
    public class AsignarController : ControllerBase
    {
        private readonly IAsignarRepository asignarRepository;
        public AsignarController(IAsignarRepository _asignarRepository)
        {
            asignarRepository = _asignarRepository;
        }

        [HttpPost("/Asignar")]
        public async Task<IActionResult> Index(int nomina, string curso, string type_course)
        {
            try
            {
                var result = await asignarRepository.AsignCourse(nomina, curso, type_course);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/EditStatus")]
        public async Task<IActionResult> Asign(int nomina, string curso, string estatus, string phaseCourse)
        {
            try
            {
                var result = await asignarRepository.UpdateSpecificEstatus(nomina, curso, estatus, phaseCourse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
