using JINIApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JINIApp.Server.Data
{
    public class DbInitializer
    {
        public static void Initialize(JINIAppServerContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Items.
            if (context.Items.Any())
            {
                return;   // DB has been seeded
            }

            var items = new Item[]
            {
                new Item{Supplier="中大",Address="",ContactNumber="34207027",ItemName="국제천"},
                new Item{Supplier="中大",Address="",ContactNumber="22039139",ItemName="스웨이드"},
                new Item{Supplier="中大",Address="",ContactNumber="89025092",ItemName="D452"},
                new Item{Supplier="YT 354",Address="",ContactNumber="29070977",ItemName="9760"}
            };

            context.Items.AddRange(items);
            context.SaveChanges();

            var customers = new Customer[]
            {
                new Customer{Name="에이치",Owner="김현준"},
                new Customer{Name="MIN",Owner="이민우"}
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();

            var salesOrderCustomer = context.Customers.FirstOrDefault(o => o.ID == (int)1);

            var salesOrder = new SalesOrder[]
            {                
                new SalesOrder{
                    SalesOrderNo="20201209001"
                ,SalesDate=Convert.ToDateTime("2020-12-09")
                ,CustomerID = 1
                ,Customer=salesOrderCustomer}                
            };

            context.SalesOrders.AddRange(salesOrder);
            context.SaveChanges();

            var salesOrderItemSalesOrder = context.SalesOrders.FirstOrDefault(o => o.ID == (int)1);
            var salesOrderItemItem = context.Items.FirstOrDefault(o => o.ID == (int)1);

            var salesOrderItem = new SalesOrderItem[]
            {
                new SalesOrderItem{
                        SalesDate=Convert.ToDateTime("2020-12-09")
                    , UnitCost = (decimal)10.0
                    , UnitProfit = (decimal)2.0
                    , SalesQuantity = (decimal)20.0
                    , ItemNumber = "123"
                    , ItemColor = "Red"
                    , Comment = ""
                    , SalesOrderID = 1
                    , SalesOrder = salesOrderItemSalesOrder
                    , ItemID = 1
                    , Item = salesOrderItemItem
                }
            };

            context.SalesOrderItems.AddRange(salesOrderItem);
            context.SaveChanges();

            var revenue = new Revenue[]
            {
                new Revenue{
                        RevenueDate=Convert.ToDateTime("2020-12-09")
                    , RevenueCost = (decimal)200.0   
                    , SalesOrderID = 1
                    , SalesOrder = salesOrderItemSalesOrder                    
                }
            };

            context.Revenues.AddRange(revenue);
            context.SaveChanges();
        }
    }
}
