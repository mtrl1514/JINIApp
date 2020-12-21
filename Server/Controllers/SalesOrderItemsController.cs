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
    public class SalesOrderItemsController : Controller
    {
        private readonly JINIAppServerContext _context;

        public SalesOrderItemsController(JINIAppServerContext context)
        {
            _context = context;
        }        


        [HttpGet]
        public ActionResult GetAllSalesOrderItem()
        {           
            var repository = new SalesOrderItemsRepository(_context);
            IGridServer<SalesOrderItem> server = new GridServer<SalesOrderItem>(repository.GetAll(), Request.Query,
                true, "salesOrderItemsGrid", c => ColumnCollections.SalesOrderItemColumns(c, null, null))
                    .WithPaging(10)
                    .Sortable()
                    .Filterable()
                    .WithMultipleFilters()
                    .WithGridItemsCount();

            var salesOrderItems = server.ItemsToDisplay;
            return Ok(salesOrderItems);

        }

        // GET: api/SalesOrderItems
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<SalesOrderItem>>> GetSalesOrderItems()
        //{
        //    return await _context.SalesOrderItems.ToListAsync();
        //}

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] SalesOrderItem salesOrderItem)
        {
            if (ModelState.IsValid)
            {
                if (salesOrderItem == null)
                {
                    return BadRequest();
                }

                var repository = new SalesOrderItemsRepository(_context);
                try
                {
                    await repository.Insert(salesOrderItem);
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

        // POST: api/SalesOrderItems
        //[HttpPost]
        //public async Task<ActionResult<SalesOrderItem>> PostSalesOrderItem(SalesOrderItem salesOrderItem)
        //{
        //    _context.SalesOrderItems.Add(salesOrderItem);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetSalesOrderItem", new { id = salesOrderItem.ID }, salesOrderItem);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrder(int id)
        {
            var repository = new SalesOrderItemsRepository(_context);
            SalesOrderItem salesOrderItem = await repository.GetById(id);
            if (salesOrderItem == null)
            {
                return NotFound();
            }
            return Ok(salesOrderItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, [FromBody] SalesOrderItem salesOrderItem)
        {
            if (ModelState.IsValid)
            {
                if (salesOrderItem == null || salesOrderItem.ID != id)
                {
                    return BadRequest();
                }

                var repository = new SalesOrderItemsRepository(_context);
                try
                {
                    await repository.Update(salesOrderItem);
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
            var repository = new SalesOrderItemsRepository(_context);
            SalesOrderItem salesOrderItem = await repository.GetById(id);

            if (salesOrderItem == null)
            {
                return NotFound();
            }

            try
            {
                repository.Delete(salesOrderItem);
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
