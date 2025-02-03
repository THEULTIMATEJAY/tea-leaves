using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using tea_leaves.Models;
namespace tea_leaves.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController(MyDbContext context, IMapper mapper, ILogger<ContactController> logger) : ControllerBase
    {
        private readonly MyDbContext _context = context;
        private static List<Contact> _contact = new List<Contact>();
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<ContactController> _logger = logger;
        [HttpPost]
        [ProducesResponseType(typeof(ContactDTO), StatusCodes.Status200OK)]
        public IActionResult CreateContact(AddContactRequest contactRequest)
        {
            try
            {
                var newContact = new Contact
                {
                    name = contactRequest.Name.Trim(),
                    email = contactRequest.Email.Trim(),
                    number = contactRequest.Number.Trim(),
                    message = contactRequest.Message.Trim(),
                    location = contactRequest.Location.Trim()
                };

                _context.Contact.Add(newContact);
                _context.SaveChanges();

                // Retrieve the newly created product, including user info if necessary
                Contact? createdContact = _context.Contact
                    .FirstOrDefault(p => p.name == newContact.name);
                if (createdContact == null)
                {
                    return StatusCode(500, "Failed to retrieve the created contact.");
                }


                // Map to DTO for response
                ContactDTO contactDTO = _mapper.Map<ContactDTO>(createdContact);
                return Ok(contactDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when creating contact");
                return StatusCode(500, "An error occurred while creating the contact.");
            }

        }
    }
}

