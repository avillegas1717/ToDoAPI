//Step 06) Scaffold an API controller using Resource as the Model and ResourcesContext as the Data Context.
//When testing Resources in th browser, we saw that the Category object returns null. Below we will modify our controller code to include the Category when looking up a Resource

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceAPI.Models;
using Microsoft.AspNetCore.Cors;//Added for Cors functionality

namespace ResourceAPI.Controllers
{
    //Step 09c) Add the EnableCors statement below AND the using statement above for Cors
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly ResourcesContext _context;

        public ResourcesController(ResourcesContext context)
        {
            _context = context;
        }

        // GET: api/Resources
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resource>>> GetResources()
        {
            if (_context.Resources == null)
            {
                return NotFound();
            }
            //Step 07) Modify the GET functionality to include the Category
            var resources = await _context.Resources.Include("Category").Select(x => new Resource()
            {
                //Assign each resource in our data set to a new Resource object for this application.
                ResourceId = x.ResourceId,
                Name = x.Name,
                Description = x.Description,
                Url = x.Url,
                LinkText = x.LinkText,
                CategoryId = x.CategoryId,
                Category = x.Category != null ? new Category()
                {
                    CategoryId = x.Category.CategoryId,
                    CategoryName = x.Category.CategoryName,
                    CategoryDescription = x.Category.CategoryDescription,
                } : null
            }).ToListAsync();


            return Ok(resources);
        }

        // GET: api/Resources/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Resource>> GetResource(int id)
        {
            if (_context.Resources == null)
            {
                return NotFound();
            }
            //Step 08) Modify the code below to include the Category
            var resource =
                await _context.Resources.Where(x => x.ResourceId == id).Select(x => new Resource()
                {
                    //Assign each resource in our data set to a new Resource object for this application.
                    ResourceId = x.ResourceId,
                    Name = x.Name,
                    Description = x.Description,
                    Url = x.Url,
                    LinkText = x.LinkText,
                    CategoryId = x.CategoryId,
                    Category = x.Category != null ? new Category()
                    {
                        CategoryId = x.Category.CategoryId,
                        CategoryName = x.Category.CategoryName,
                        CategoryDescription = x.Category.CategoryDescription,
                    } : null
                }).FirstOrDefaultAsync();

            if (resource == null)
            {
                return NotFound();
            }

            return resource;
        }

        // PUT: api/Resources/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResource(int id, Resource resource)
        {
            if (id != resource.ResourceId)
            {
                return BadRequest();
            }

            _context.Entry(resource).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Resources
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Resource>> PostResource(Resource resource)
        {
            if (_context.Resources == null)
            {
                return Problem("Entity set 'ResourcesContext.Resources'  is null.");
            }
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResource", new { id = resource.ResourceId }, resource);
        }

        // DELETE: api/Resources/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            if (_context.Resources == null)
            {
                return NotFound();
            }
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null)
            {
                return NotFound();
            }

            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResourceExists(int id)
        {
            return (_context.Resources?.Any(e => e.ResourceId == id)).GetValueOrDefault();
        }
    }
}
