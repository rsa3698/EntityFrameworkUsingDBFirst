using EntityFrameworkUsingDBFirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkUsingDBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly BookStoreContext _context;
        public PublishersController(BookStoreContext context) {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetPublishers()
        {
            return Ok(await _context.Publishers.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var publisher =await _context.Publishers.FindAsync(id);
            if(publisher == null)
            {
                return NotFound("The Publisher record could not be found");
            }
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return NoContent();
            
            

        }
    }
}
