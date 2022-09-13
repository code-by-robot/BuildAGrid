using Group3Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Group3Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantPropController : ControllerBase
    {
        continuousdbContext context = new  continuousdbContext();

        [HttpGet("GetProps/{id}")]
        public Plantprop GetProps(int id)
        {
            return context.Plantprops.Where(p => p.Id == id).FirstOrDefault();

        }

        [HttpGet("GetAllProps")]
        public List<Plantprop> GetAllProps()
        {
            return context.Plantprops.ToList();

        }
    }
}
