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
    public class SalesOrdersController : Controller
    {
        private readonly JINIAppServerContext _context;

        public SalesOrdersController(JINIAppServerContext context)
        {
            _context = context;
        }        


        [HttpGet]
        public ActionResult GetAllSalesOrder()
        {           
            var repository = new SalesOrdersRepository(_context);
            IGridServer<SalesOrder> server = new GridServer<SalesOrder>(repository.GetAll(), Request.Query,
                true, "salesOrdersGrid", c => ColumnCollections.SalesOrderColumnsWithCustomer(c, null, null))
                    .WithPaging(10)
                    .Sortable()
                    .Filterable()
                    .WithMultipleFilters()
                    .WithGridItemsCount();

            var salesOrders = server.ItemsToDisplay;
            return Ok(salesOrders);

        }       

        // GET: api/SalesOrders
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<SalesOrder>>> GetSalesOrders()
        //{
        //    return await _context.SalesOrders.ToListAsync();
        //}

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                if (salesOrder == null)
                {
                    return BadRequest();
                }

                var repository = new SalesOrdersRepository(_context);
                try
                {
                    await repository.Insert(salesOrder);
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

        // POST: api/SalesOrders
        //[HttpPost]
        //public async Task<ActionResult<SalesOrder>> PostSalesOrder(SalesOrder salesOrder)
        //{
        //    _context.SalesOrders.Add(salesOrder);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetSalesOrder", new { id = salesOrder.ID }, salesOrder);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrder(int id)
        {
            var repository = new SalesOrdersRepository(_context);
            SalesOrder salesOrder = await repository.GetById(id);
            if (salesOrder == null)
            {
                return NotFound();
            }
            return Ok(salesOrder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, [FromBody] SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                if (salesOrder == null || salesOrder.ID != id)
                {
                    return BadRequest();
                }

                var repository = new SalesOrdersRepository(_context);
                try
                {
                    await repository.Update(salesOrder);
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
            var repository = new SalesOrdersRepository(_context);
            SalesOrder salesOrder = await repository.GetById(id);

            if (salesOrder == null)
            {
                return NotFound();
            }

            try
            {
                repository.Delete(salesOrder);
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
