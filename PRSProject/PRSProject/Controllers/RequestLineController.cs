using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRSProject.Models;

namespace PRSProject.Controllers
{
    [Route("api/requestlines")]
    [ApiController]
    public class RequestLineController : ControllerBase
    {
        private readonly PRSDb _context;

        public RequestLineController(PRSDb context)
        {
            _context = context;
        }

        // GET: api/requestlines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestLine>>> GetRequestLines()
        {
            if (_context.RequestLines == null)
            {
                return NotFound();
            }
            return await _context.RequestLines.ToListAsync();
        }

        // GET: api/requestlines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestLine>> GetRequestLine(int id)
        {
            if (_context.RequestLines == null)
            {
                return NotFound();
            }
            var requestLine = await _context.RequestLines.FindAsync(id);

            if (requestLine == null)
            {
                return NotFound();
            }

            return requestLine;
        }

        // PUT: api/requestlines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestLine(int id, RequestLine requestLine)
        {
            if (id != requestLine.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestLineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            RecalculateRequestTotal(requestLine.RequestId);

            return NoContent();
        }

        // POST: api/requestlines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RequestLine>> PostRequestLine(RequestLine requestLine)
        {
          if (_context.RequestLines == null)
          {
              return Problem("Entity set 'PRSDb.RequestLines'  is null.");
          }
            _context.RequestLines.Add(requestLine);
            await _context.SaveChangesAsync();

            RecalculateRequestTotal(requestLine.RequestId);

            return CreatedAtAction("GetRequestLine", new { id = requestLine.Id }, requestLine);
        }

        // DELETE: api/requestlines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestLine(int id)
        {
            if (_context.RequestLines == null)
            {
                return NotFound();
            }
            var requestLine = await _context.RequestLines.FindAsync(id);
            if (requestLine == null)
            {
                return NotFound();
            }
            int requestid = requestLine.RequestId;
            _context.RequestLines.Remove(requestLine);
            await _context.SaveChangesAsync();
            
            RecalculateRequestTotal(requestid);

            return NoContent();
        }

        private void RecalculateRequestTotal(int requestId)
        {
         
         
           decimal total = _context.RequestLines.Include(r => r.Product)
                .Where(r => r.RequestId == requestId)
                .Sum(r => r.Product.Price * r.Quantity);

            //update request
            var request = _context.Requests.FirstOrDefault(r => r.Id == requestId);
            if (request != null)
            {
                request.Total = total;
            }
            _context.SaveChanges();
        }

        private bool RequestLineExists(int id)
        {
            return (_context.RequestLines?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
