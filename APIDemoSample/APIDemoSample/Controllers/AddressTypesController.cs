using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDemoSample;
using APIDemoSample.Reperesentations;

namespace APIDemoSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressTypesController : ControllerBase
    {
        private readonly AdventureWorks2017Context _context = new AdventureWorks2017Context();

        public AddressTypesController()
        {
            
        }

        // GET: api/AddressTypes
        [HttpGet]
        public IEnumerable<AddressTypeRepresentation> GetAddressType()
        {
            List<AddressTypeRepresentation> addressTypesRepresentation = new List<AddressTypeRepresentation>();

            foreach (AddressType addressType in _context.AddressType)
            {
                AddressTypeRepresentation temp = new AddressTypeRepresentation()
                {
                    AddressTypeId = addressType.AddressTypeId,
                    ModifiedDate = addressType.ModifiedDate,
                    Name = addressType.Name
                };
                addressTypesRepresentation.Add(temp);
            }
            return addressTypesRepresentation;
        }

        // GET: api/AddressTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressTypeById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addressType = await _context.AddressType.FindAsync(id);

            if (addressType == null)
            {
                return NotFound();
            }

            return Ok(addressType);
        }

        // PUT: api/AddressTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddressType([FromRoute] int id, [FromBody] AddressType addressType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != addressType.AddressTypeId)
            {
                return BadRequest();
            }

            _context.Entry(addressType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressTypeExists(id))
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

        // POST: api/AddressTypes
        [HttpPost]
        public async Task<IActionResult> PostAddressType([FromBody] AddressType addressType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AddressType.Add(addressType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddressType", new { id = addressType.AddressTypeId }, addressType);
        }

        // DELETE: api/AddressTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addressType = await _context.AddressType.FindAsync(id);
            if (addressType == null)
            {
                return NotFound();
            }

            _context.AddressType.Remove(addressType);
            await _context.SaveChangesAsync();

            return Ok(addressType);
        }

        private bool AddressTypeExists(int id)
        {
            return _context.AddressType.Any(e => e.AddressTypeId == id);
        }
    }
}