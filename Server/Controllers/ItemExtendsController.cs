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
    public class ItemExtendsController : Controller
    {
        private readonly JINIAppServerContext _context;

        public ItemExtendsController(JINIAppServerContext context)
        {
            _context = context;
        }


        [HttpGet("[action]")]
        public ActionResult GetAllItemForSelect()
        {
            var repository = new ItemsRepository(_context);
            return Ok(repository.GetAll()
                    .Select(r => new SelectItem(r.ID.ToString(), r.Supplier + " - " + r.ItemName))
                    .ToList());
        }
    }
}
