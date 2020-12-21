using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JINIApp.Server.Data;
using JINIApp.Shared.Models;
using GridMvc.Server;
using JINIApp.Server.Models;
using GridShared;
using JINIApp.Client.ColumnCollections;

namespace JINIApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerExtendsController : Controller
    {
        private readonly JINIAppServerContext _context;

        public CustomerExtendsController(JINIAppServerContext context)
        {
            _context = context;
        }        
       
        [HttpGet("[action]")]
        public ActionResult GetAllCustomerForSelect()
        {
            var repository = new CustomersRepository(_context);
            return Ok(repository.GetAll()
                    .Select(r => new SelectItem(r.ID.ToString(), r.ID + " - " + r.Name))
                    .ToList());
        }
      
    }
}
