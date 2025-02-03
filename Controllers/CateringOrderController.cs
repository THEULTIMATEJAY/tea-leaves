using Microsoft.AspNetCore.Mvc;
using EDP_project.Models;
using Mysqlx;

namespace EDP_project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CateringOrderController(MyDbContext context) : ControllerBase
    {
        private readonly MyDbContext _context = context;

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IQueryable<CateringOrder> result = _context.CateringOrders;
                var list = result.OrderByDescending(x => x.CateringOrderID).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddCateringOrder(CateringOrder cateringorder)
        {
            //Console.Write("Post cateringorder func ran");
            var now = DateTime.Now;
            var myCateringOrder = new CateringOrder()
            {
                CateringOrderID = cateringorder.CateringOrderID,
                ForeignUserID = cateringorder.ForeignUserID,
                Name = cateringorder.Name.Trim(),
                MobileNumber = cateringorder.MobileNumber.Trim(),
                Email = cateringorder.Email.Trim(),
                CateringServiceType = cateringorder.CateringServiceType.Trim(),
                Location = cateringorder.Location.Trim(),
                CreatedAt = now,
                UpdatedAt = now
            };

            _context.CateringOrders.Add(myCateringOrder);
            _context.SaveChanges();
            return Ok(myCateringOrder);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCateringOrder(int id, CateringOrder cateringorder)
        {
            var myCateringOrder = _context.CateringOrders.Find(id);
            if (myCateringOrder == null)
            {
                return NotFound();
            }
            myCateringOrder.Name = cateringorder.Name;
            myCateringOrder.MobileNumber = cateringorder.MobileNumber;
            myCateringOrder.Email = cateringorder.Email;
            myCateringOrder.CateringServiceType = cateringorder.CateringServiceType;
            myCateringOrder.Location = cateringorder.Location;
            myCateringOrder.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return Ok("Catering Order Updated.");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCateringOrder(int id)
        {
            var myCateringOrder = _context.CateringOrders.Find(id);
            if (myCateringOrder == null)
            {
                return NotFound();
            }
            _context.CateringOrders.Remove(myCateringOrder);
            _context.SaveChanges();
            return Ok("Catering Order Deleted.");
        }
    }
}
