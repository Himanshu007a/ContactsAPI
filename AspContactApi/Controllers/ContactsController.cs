using System;
using System.Threading.Tasks;
using AspContactApi.Data;
using AspContactApi.Models;
using AspContactApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspContactApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactDbContext _context;

        public ContactsController(ContactDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _context.Contacts.ToListAsync();

            var contactdto = new List<ContactDto>();
            foreach(var contact in contacts)
            {
                contactdto.Add(new ContactDto()
                {
                    Id = contact.Id,
                    FullName = contact.FullName,
                    Phone = contact.Phone,
                    Email = contact.Email,
                    Address = contact.Address

                });
            }

            return Ok(contactdto);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetContact(Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }
            var contactdto = new ContactDto()
            {

                Id = contact.Id,
                FullName = contact.FullName,
                Phone = contact.Phone,
                Email = contact.Email,
                Address = contact.Address

            };


            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddPostRequestDto addPostRequestDto)
        {
            var contact = new Contact()
            {
                FullName = addPostRequestDto.FullName,
                Email = addPostRequestDto.Email,
                Phone = addPostRequestDto.Phone,
                Address = addPostRequestDto.Address
            };

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            var contactdto = new ContactDto()
            {
                Id = contact.Id,
                FullName = contact.FullName,
                Email = contact.Email,
                Phone = contact.Phone,
                Address = contact.Address
            };

            return CreatedAtAction(nameof(GetContact), new { id = contactdto.Id }, contactdto);

        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateContact(Guid id, UpdateContactRequestDto updateContactRequestDto)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }

            contact.FullName = updateContactRequestDto.FullName;
            contact.Email = updateContactRequestDto.Email;
            contact.Phone = updateContactRequestDto.Phone;
            contact.Address = updateContactRequestDto.Address;

            await _context.SaveChangesAsync();

            var contactdto = new ContactDto()
            {
                Id = contact.Id,
                FullName = contact.FullName,
                Email = contact.Email,
                Phone = contact.Phone,
                Address = contact.Address
            };

            return Ok(contactdto);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            var contactDto = new ContactDto
            {
                Id = contact.Id,
               
            };

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return Ok(contactDto);
        }

    }
}

