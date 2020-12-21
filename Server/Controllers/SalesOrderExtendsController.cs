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
    public class SalesOrderExtendsController : Controller
    {
        private readonly JINIAppServerContext _context;

        public SalesOrderExtendsController(JINIAppServerContext context)
        {
            _context = context;
        }


        [HttpGet("[action]")]
        public ActionResult GetAllSalesOrderWithCustomer()
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

        [HttpGet("[action]")]
        public ActionResult GetAllSalesOrderForSelect()
        {
            var repository = new SalesOrdersRepository(_context);
            return Ok(repository.GetAll()
                    .Select(r => new SelectItem(r.ID.ToString(), r.SalesOrderNo + " - " + r.Customer.Name))
                    .ToList());
        }


    }
}
