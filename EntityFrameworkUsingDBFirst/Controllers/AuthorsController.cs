using EntityFrameworkUsingDBFirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkUsingDBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /*
 Attributes
The Microsoft.AspNetCore.Mvc namespace provides attributes that can be used to configure the behavior of web API controllers and action methods. 

Here are some more examples of attributes that are available.

Attribute	Notes
[Route]	Specifies URL pattern for a controller or action.
[Bind]	Specifies prefix and properties to include for model binding.
[HttpGet]	Identifies an action that supports the HTTP GET action verb.

ApiController attribute
The [ApiController] attribute can be applied to a controller class to enable the following opinionated, API-specific behaviors:

Attribute routing requirement
Automatic HTTP 400 responses
Binding source parameter inference
Multipart/form-data request inference
Problem details for error status codes
     */

    /*
     * ControllerBase class
A controller-based web API consists of one or more controller classes that derive from ControllerBase. The web API project template provides a starter controller:
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
A base class for an MVC controller without view support.
Web API controllers should typically derive from ControllerBase rather from Controller. Controller derives from ControllerBase and adds support for views, so it's for handling web pages, not web API requests. If the same controller must support views and web APIs, derive from Controller.
The ControllerBase class provides many properties and methods that are useful for handling HTTP requests.
    */

    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreContext _context;
        public AuthorsController(BookStoreContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get() 
        {
           var authors = _context.Authors;
            return Ok(authors);
        }

        [HttpGet("(id)" , Name ="GetAuthor")]
        public IActionResult Get(long id)
        {
            var author = _context.Authors.Find(id);
            return author == null ? NotFound() : Ok(author);
        }

        // POST: api/Authors
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Author author)
        {
            if (author is null)
            {
                return BadRequest("Author is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Add(author);
            await _context.SaveChangesAsync(); // Use async version of SaveChanges

            // After the author is added, return a 201 Created response with the newly created author's ID.
            return CreatedAtRoute("GetAuthor", new { Id = author.Id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Author updatedAuthor)
        {
            if (updatedAuthor == null)
            {
                return BadRequest("Author data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAuthor = await _context.Authors.FindAsync(id);

            if (existingAuthor == null)
            {
                return NotFound("Author not found.");
            }

            // Update the existing author's properties with the new data
            existingAuthor.Name = updatedAuthor.Name;
          

            _context.Authors.Update(existingAuthor);
            await _context.SaveChangesAsync();

            return NoContent(); // Return a 204 No Content response to indicate success with no response body.
        }



    }
}
