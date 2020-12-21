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
    public class CustomersController : Controller
    {
        private readonly JINIAppServerContext _context;

        public CustomersController(JINIAppServerContext context)
        {
            _context = context;
        }        


        [HttpGet]
        public ActionResult GetAllCustomer()
        {           
            var repository = new CustomersRepository(_context);
            IGridServer<Customer> server = new GridServer<Customer>(repository.GetAll(), Request.Query,
                true, "customersGrid", ColumnCollections.CustomerColumns)
                    .WithPaging(10)
                    .Sortable()
                    .Filterable()
                    .WithMultipleFilters()
                    .WithGridItemsCount();

            var customers = server.ItemsToDisplay;
            return Ok(customers);

        }

        // GET: api/Customers
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        //{
        //    return await _context.Customers.ToListAsync();
        //}

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer == null)
                {
                    return BadRequest();
                }

                var repository = new CustomersRepository(_context);
                try
                {
                    await repository.Insert(customer);
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

        // POST: api/Customers
        //[HttpPost]
        //public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        //{
        //    _context.Customers.Add(customer);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCustomer", new { id = customer.ID }, customer);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrder(int id)
        {
            var repository = new CustomersRepository(_context);
            Customer customer = await repository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, [FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer == null || customer.ID != id)
                {
                    return BadRequest();
                }

                var repository = new CustomersRepository(_context);
                try
                {
                    await repository.Update(customer);
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
            var repository = new CustomersRepository(_context);
            Customer customer = await repository.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            try
            {
                repository.Delete(customer);
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
