using Microsoft.AspNetCore.Mvc;
using EDP_project.Models;

namespace EDP_project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassBookingController(MyDbContext context) : ControllerBase
    {
        private readonly MyDbContext _context = context;

        [HttpGet]
        public IActionResult GetAll()
        {
            IQueryable<ClassBooking> result = _context.ClassBookings;
            var list = result.OrderByDescending(x => x.ClassBookingID).ToList();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult AddClassBooking(ClassBooking classbooking)
        {
            var now = DateTime.Now;
            var myClassBooking = new ClassBooking()
            {
                ClassBookingID = classbooking.ClassBookingID,
                ForeignUserID = classbooking.ForeignUserID,
                Name = classbooking.Name.Trim(),
                MobileNumber = classbooking.MobileNumber.Trim(),
                Email = classbooking.Email.Trim(),
                ClassType = classbooking.ClassType.Trim(),
                Session = classbooking.Session.Trim(),
                BYOC = classbooking.BYOC,
                CreatedAt = now,
                UpdatedAt = now
            };
            _context.ClassBookings.Add(myClassBooking);
            _context.SaveChanges();
            return Ok(myClassBooking);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateClassBooking(int id, ClassBooking classbooking)
        {
            var myClassBooking = _context.ClassBookings.Find(id);
            if (myClassBooking == null)
            {
                return NotFound();
            }
            myClassBooking.Name = classbooking.Name;
            myClassBooking.MobileNumber = classbooking.MobileNumber;
            myClassBooking.Email = classbooking.Email;
            myClassBooking.ClassType = classbooking.ClassType;
            myClassBooking.Session = classbooking.Session;
            myClassBooking.BYOC = classbooking.BYOC;
            myClassBooking.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return Ok("Class Booking Updated.");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteClassBooking(int id)
        {
            var myClassBooking = _context.ClassBookings.Find(id);
            if (myClassBooking == null)
            {
                return NotFound();
            }
            _context.ClassBookings.Remove(myClassBooking);
            _context.SaveChanges();
            return Ok("Class Booking Deleted.");
        }
    }
}
