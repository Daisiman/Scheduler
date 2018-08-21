using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Data;
using Scheduler.Models;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scheduler.Controllers.API
{
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: api/<controller>
        [HttpGet]
        public JsonResult Get()
        {
            var chat = _context.LiveChat
                .OrderBy(x => x.DateCreated)
                .ToList();

            return Json(chat);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post(string msg)
        {
            var message = new LiveChat()
            {
                Sender = User.Identity.Name,
                Message = msg,
                DateCreated = DateTime.Now
            };

            try
            {
                _context.LiveChat.Add(message);
                _context.SaveChanges();

                return Ok();
            }
            catch {

                return BadRequest();
            }

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
