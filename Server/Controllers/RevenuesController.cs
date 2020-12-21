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
    public class RevenuesController : Controller
    {
        private readonly JINIAppServerContext _context;

        public RevenuesController(JINIAppServerContext context)
        {
            _context = context;
        }        


        [HttpGet]
        public ActionResult GetAllRevenue()
        {           
            var repository = new RevenuesRepository(_context);
            IGridServer<Revenue> server = new GridServer<Revenue>(repository.GetAll(), Request.Query,
                true, "revenuesGrid", c => ColumnCollections.RevenueColumns(c, null))
                    .WithPaging(10)
                    .Sortable()
                    .Filterable()
                    .WithMultipleFilters()
                    .WithGridItemsCount();

            var revenues = server.ItemsToDisplay;
            return Ok(revenues);
        }

        [HttpGet("[action]")]
        public ActionResult GetRevenuebySalesOrderId(int salesOrderId)
        {
            var repository = new RevenuesRepository(_context).GetForSalesOrder(salesOrderId);
            var server = new GridServer<Revenue>(repository, Request.Query,
                true, "revenuesGrid" + salesOrderId.ToString(), c => ColumnCollections.RevenueColumns(c, null))
                    .WithPaging(10)
                    .Sortable()
                    .Filterable()
                    .WithMultipleFilters()
                    .WithGridItemsCount();

            var revenues = server.ItemsToDisplay;
            return Ok(revenues);

        }

        // GET: api/Revenues
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Revenue>>> GetRevenues()
        //{
        //    return await _context.Revenues.ToListAsync();
        //}

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Revenue revenue)
        {
            if (ModelState.IsValid)
            {
                if (revenue == null)
                {
                    return BadRequest();
                }

                var repository = new RevenuesRepository(_context);
                try
                {
                    await repository.Insert(revenue);
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

        // POST: api/Revenues
        //[HttpPost]
        //public async Task<ActionResult<Revenue>> PostRevenue(Revenue revenue)
        //{
        //    _context.Revenues.Add(revenue);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetRevenue", new { id = revenue.ID }, revenue);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrder(int id)
        {
            var repository = new RevenuesRepository(_context);
            Revenue revenue = await repository.GetById(id);
            if (revenue == null)
            {
                return NotFound();
            }
            return Ok(revenue);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, [FromBody] Revenue revenue)
        {
            if (ModelState.IsValid)
            {
                if (revenue == null || revenue.ID != id)
                {
                    return BadRequest();
                }

                var repository = new RevenuesRepository(_context);
                try
                {
                    await repository.Update(revenue);
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
            var repository = new RevenuesRepository(_context);
            Revenue revenue = await repository.GetById(id);

            if (revenue == null)
            {
                return NotFound();
            }

            try
            {
                repository.Delete(revenue);
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
