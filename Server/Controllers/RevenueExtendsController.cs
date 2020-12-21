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
    public class RevenueExtendsController : Controller
    {
        private readonly JINIAppServerContext _context;

        public RevenueExtendsController(JINIAppServerContext context)
        {
            _context = context;
        }        


       

        [HttpGet("[action]")]
        public ActionResult GetRevenueExtendbySalesOrderId(int salesOrderId)
        {
            var repository = new RevenuesRepository(_context).GetForSalesOrder(salesOrderId);
            var server = new GridServer<Revenue>(repository, Request.Query,
                true, "revenuesGrid" + salesOrderId.ToString()
                , c => ColumnCollections.RevenueColumns(c, null))
                    .WithPaging(10)
                    .Sortable()
                    .Filterable()
                    .WithMultipleFilters()
                    .WithGridItemsCount();

            var revenues = server.ItemsToDisplay;
            return Ok(revenues);

        }

        
    }
}
