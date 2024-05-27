using Metricos.IRepository;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Threading.Tasks;

namespace Metricos.Controllers
{
    [ApiController]
    public class IPhases_CourseController : ControllerBase
    {
        private readonly IPhases_CourseRepository _PhasesRepository;

        public IPhases_CourseController(IPhases_CourseRepository phasesRepository)
        {
            _PhasesRepository = phasesRepository;
        }

        [HttpGet("GetCourseDetails")]
        public async Task<IActionResult> GetCourseDetails()
        {
            var collecction = await _PhasesRepository.GetCourseDetailsAsync();
            return Ok(collecction);
        }
        [HttpGet("GetResumenOfStatus")]
        public async Task<IActionResult> GetResumen()
        {
            try
            {
                var resumen = await _PhasesRepository.Resumen();
                return Ok(resumen);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
