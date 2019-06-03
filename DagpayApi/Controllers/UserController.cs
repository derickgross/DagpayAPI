using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DagpayApi.Models;

namespace DagpayApi.Controllers
{
    [Authorize]
    [ApiController]
    //[Route("[controller]")];
    public class UserController : ControllerBase
    {
        private readonly AzureDatabaseContext _context;

        public UserController(AzureDatabaseContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(User user)
        {
            return Ok();
        }
    }
}
