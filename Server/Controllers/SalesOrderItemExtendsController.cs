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
    public class SalesOrderItemExtendsController : Controller
    {
        private readonly JINIAppServerContext _context;

        public SalesOrderItemExtendsController(JINIAppServerContext context)
        {
            _context = context;
        }        
        

        [HttpGet("[action]")]
        public ActionResult GetSalesOrderItemExtendbySalesOrderId(int salesOrderId)
        {
            var repository = new SalesOrderItemsRepository(_context).GetForSalesOrder(salesOrderId);
            var server = new GridServer<SalesOrderItem>(repository, Request.Query,
                true, "salesOrderItemsGrid" + salesOrderId.ToString(), c => ColumnCollections.SalesOrderItemColumns(c, null, null))
                    .WithPaging(10)
                    .Sortable()
                    .Filterable()
                    .WithMultipleFilters()
                    .WithGridItemsCount();            

            var salesOrderItems = server.ItemsToDisplay;
            return Ok(salesOrderItems);

        }


        
    }
}
