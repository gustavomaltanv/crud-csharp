using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MinhaApi.Context;
using MinhaApi.Entities;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly PhonebookContext _context;
        public ContactController(PhonebookContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Contact contact)
        {
            _context.Add(contact);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var consultedContact = _context.Contact.Find(id);
            if (consultedContact == null) return NotFound();
            return Ok(consultedContact);
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            var consultedContacts = _context.Contact.Where(e => e.Name.Contains(name));
            if (consultedContacts == null) return NotFound();
            return Ok(consultedContacts);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Contact contact)
        {
            var consultedContact = _context.Contact.Find(id);
            if (consultedContact == null) return NotFound();

            consultedContact.Name = contact.Name;
            consultedContact.Phone = contact.Phone;
            consultedContact.Communications = contact.Communications;

            _context.Contact.Update(consultedContact);
            _context.SaveChanges();

            return Ok(consultedContact);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var consultedContact = _context.Contact.Find(id);
            if (consultedContact == null) return NotFound();

            _context.Contact.Remove(consultedContact);
            _context.SaveChanges();
            return NoContent();
        }
    }
}