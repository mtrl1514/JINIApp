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
    public class ItemsController : Controller
    {
        private readonly JINIAppServerContext _context;

        public ItemsController(JINIAppServerContext context)
        {
            _context = context;
        }        


        [HttpGet]
        public ActionResult GetAllItem()
        {           
            var repository = new ItemsRepository(_context);
            IGridServer<Item> server = new GridServer<Item>(repository.GetAll(), Request.Query,
                true, "itemsGrid", ColumnCollections.ItemColumns)
                    .WithPaging(10)
                    .Sortable()
                    .Filterable()
                    .WithMultipleFilters()
                    .WithGridItemsCount();

            var items = server.ItemsToDisplay;
            return Ok(items);

        }

        // GET: api/Items
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        //{
        //    return await _context.Items.ToListAsync();
        //}

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Item item)
        {
            if (ModelState.IsValid)
            {
                if (item == null)
                {
                    return BadRequest();
                }

                var repository = new ItemsRepository(_context);
                try
                {
                    await repository.Insert(item);
                    repository.Save();

                    return NoContent();
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        message = e.Message.Replace('{', '(').Replace('}', ')')
                    });
                }
            }
            return BadRequest(new
            {
                message = "ModelState is not valid"
            });
        }

        // POST: api/Items
        //[HttpPost]
        //public async Task<ActionResult<Item>> PostItem(Item item)
        //{
        //    _context.Items.Add(item);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetItem", new { id = item.ID }, item);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrder(int id)
        {
            var repository = new ItemsRepository(_context);
            Item item = await repository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, [FromBody] Item item)
        {
            if (ModelState.IsValid)
            {
                if (item == null || item.ID != id)
                {
                    return BadRequest();
                }

                var repository = new ItemsRepository(_context);
                try
                {
                    await repository.Update(item);
                    repository.Save();

                    return NoContent();
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        message = e.Message.Replace('{', '(').Replace('}', ')')
                    });
                }
            }
            return BadRequest(new
            {
                message = "ModelState is not valid"
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var repository = new ItemsRepository(_context);
            Item item = await repository.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            try
            {
                repository.Delete(item);
                repository.Save();

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = e.Message.Replace('{', '(').Replace('}', ')')
                });
            }
        }
    }
}
