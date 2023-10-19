using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;
using Microsoft.AspNetCore.Cors;
using Humanizer.Localisation;

namespace ToDoAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDosController(ToDoContext context)
        {
            _context = context;
        }

        // GET: api/ToDos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetToDos()
        {
          if (_context.ToDos == null)
          {
              return NotFound();
          }
            var toDo = await _context.ToDos.Include("Category").Select(x => new ToDo()
            {
                ToDoId = x.ToDoId,
                Name = x.Name,
                Done = x.Done,
                CategoryId = x.CategoryId,
                Category = x.Category != null ? new Category()
                {
                    CategoryId = x.Category.CategoryId,
                    CatName = x.Category.CatName,
                    CatDesc = x.Category.CatDesc,
                } : null
            }).ToListAsync();

            return Ok(toDo);
        }

        // GET: api/ToDos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> GetToDo(int id)
        {
          if (_context.ToDos == null)
          {
              return NotFound();
          }
            var toDo = await _context.ToDos.Where(x => x.ToDoId == id).Select(x => new ToDo()
            {
                ToDoId = x.ToDoId,
                Name = x.Name,
                Done = x.Done,
                CategoryId = x.CategoryId,
                Category = x.Category != null ? new Category()
                {
                    CategoryId = x.Category.CategoryId,
                    CatName = x.Category.CatName,
                    CatDesc = x.Category.CatDesc,
                } : null
            }).FirstOrDefaultAsync();

            if (toDo == null)
            {
                return NotFound();
            }

            return toDo;
        }

        // PUT: api/ToDos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDo(int id, ToDo toDo)
        {
            if (id != toDo.ToDoId)
            {
                return BadRequest();
            }

            _context.Entry(toDo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoExists(id))
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

        // POST: api/ToDos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDo>> PostToDo(ToDo toDo)
        {
          if (_context.ToDos == null)
          {
              return Problem("Entity set 'ToDoContext.ToDos'  is null.");
          }
            _context.ToDos.Add(toDo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ToDoExists(toDo.ToDoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetToDo", new { id = toDo.ToDoId }, toDo);
        }

        // DELETE: api/ToDos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDo(int id)
        {
            if (_context.ToDos == null)
            {
                return NotFound();
            }
            var toDo = await _context.ToDos.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }

            _context.ToDos.Remove(toDo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoExists(int id)
        {
            return (_context.ToDos?.Any(e => e.ToDoId == id)).GetValueOrDefault();
        }
    }
}
