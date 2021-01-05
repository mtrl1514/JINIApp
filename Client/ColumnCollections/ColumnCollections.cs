using GridShared;
using System;
using JINIApp.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GridShared.Columns;
using JINIApp.Client.Pages.SalesOrders;

namespace JINIApp.Client.ColumnCollections
{
    public class ColumnCollections
    {
        public static Action<IGridColumnCollection<Item>> ItemColumns = c =>
        {
            /* Adding "ID" column: */
            c.Add(o => o.ID).SetPrimaryKey(true).Titled("ID").SetWidth(20);

            /* Adding "Supplier" column: */
            c.Add(o => o.Supplier).Titled("사입가게").SetWidth(150);

            /* Adding "Address" column: */
            c.Add(o => o.Address).Titled("주소").SetWidth(250);

            /* Adding "ContactNumber" column: */
            c.Add(o => o.ContactNumber).Titled("연락처").SetWidth(100);

            /* Adding "ItemName" column: */
            c.Add(o => o.ItemName).Titled("제품명").SetWidth(150);
        };

        public static Action<IGridColumnCollection<Customer>> CustomerColumns = c =>
        {
            /* Adding "ID" column: */
            c.Add(o => o.ID).SetPrimaryKey(true).Titled("ID").SetWidth(20);

            /* Adding "Supplier" column: */
            c.Add(o => o.Name).Titled("고객명").SetWidth(250);

            /* Adding "Address" column: */
            c.Add(o => o.Owner).Titled("대표자").SetWidth(250);            
        };

        //public static Action<IGridColumnCollection<SalesOrder>> SalesOrderColumns = c =>
        //{
        //    /* Adding "ID" column: */
        //    c.Add(o => o.ID).SetPrimaryKey(true).Titled("ID").SetWidth(20);

        //    /* Adding "Supplier" column: */
        //    c.Add(o => o.SalesOrderNo).Titled("주문번호").SetWidth(50);

        //    /* Adding "Address" column: */
        //    c.Add(o => o.SalesDate).Titled("주문일자").Format("{0:yyyy-MM-dd}");                                    

        //    /* Adding "ContactNumber" column: */
        //    c.Add(o => o.Customer.Name, true).Titled("고객").SetWidth(100);            

        //};

        public static Action<IGridColumnCollection<SalesOrder>, string, object> SalesOrderColumnsWithCustomer = (c, path, obj) =>
        {
            //c.Add().Titled("주문상세").Encoded(false).Sanitized(false).RenderComponentAs<ItemButtonCell>(obj).SetWidth(5);
            //c.Add().Titled("매출").Encoded(false).Sanitized(false).RenderComponentAs<RevenueButtonCell>(obj).SetWidth(5);

            /* Adding "ID" column: */
            c.Add(o => o.ID, true).SetPrimaryKey(true).Titled("ID");

            /* Adding "Supplier" column: */
            c.Add(o => o.SalesOrderNo).Titled("주문번호");

            /* Adding "Address" column: */
            c.Add(o => o.SalesDate).Titled("주문일자").Format("{0:yyyy-MM-dd}");

            /* Adding "ContactNumber" column: */
            c.Add(o => o.CustomerID, true).Titled("고객")
            .SetSelectField(true, o => o.Customer.ID + " - " + o.Customer.Name, path + $"api/CustomerExtends/GetAllCustomerForSelect");

            c.Add(o => o.Customer.Name).Titled("고객").SetCrudHidden(true);

            

            //c.Add().RenderValueAs(model => "SalesOrderItem" + model.ID)
            //.Encoded(false)
            //.Sanitized(false)
            //.RenderValueAs(o => $"<b><a class='modal_link' href='/salesOrderItems'>주문상세</a></b>")
            //.SetWidth(150);

            //c.Add().RenderValueAs(model => "Revenue" + model.ID)
            //.Encoded(false)
            //.Sanitized(false)
            //.RenderValueAs(o => $"<b><a class='modal_link' href='/revenues'>매출</a></b>")
            //.SetWidth(150);



        };

        public static Action<IGridColumnCollection<SalesOrderItem>, string, object> SalesOrderItemColumns = (c, path, obj) =>
        {
            c.Add(o => o.SalesOrderID, true)
            .SetCrudHidden(true)
            .Titled("주문");
            //.SetSelectField(true, o => o.SalesOrder.SalesOrderNo + "-" + o.SalesOrder.Customer.Name, path + $"api/SalesOrderExtends/GetAllSalesOrderForSelect")
            //.SetReadOnlyOnUpdate(true);          

            c.Add(o => o.ID, true).SetPrimaryKey(true).Titled("순번");

            c.Add(o => o.SalesDate).Titled("주문일자")
                .SetInputType(InputType.Date)
                .Format("{0:yyyy-MM-dd}");

            c.Add(o => o.ItemID, true).Titled("제품")
            .SetSelectField(true, o => o.Item.Supplier + " - " + o.Item.ItemName, path + $"api/ItemExtends/GetAllItemForSelect");

            c.Add(o => o.Item.Supplier).Titled("사업가게")
            .SetCrudHidden(true).SetReadOnlyOnUpdate(true);

            c.Add(o => o.Item.ItemName).Titled("품명")
            .SetCrudHidden(true).SetReadOnlyOnUpdate(true);

            /* Adding "ItemName" column: */
            c.Add(o => o.ItemNumber).Titled("품번");

            /* Adding "ItemName" column: */
            c.Add(o => o.ItemColor).Titled("색상");

            c.Add(o => o.UnitCost)
                .Titled("단가")
                .Format("{0:0.##}");

            c.Add(o => o.SalesQuantity)
                .Titled("주문수량")
                .SetWidth(20)                
                .Format("{0:0.##}");

            c.Add().RenderValueAs(o => (o.UnitCost * o.SalesQuantity).ToString())
                .Titled("단가총합")
                .SetCrudHidden(true)
                .Format("{0:0.##}"); ;


            /* Adding "ContactNumber" column: */
            c.Add(o => o.UnitProfit)
                .Titled("단위이익")
                .Format("{0:0.##}");

            c.Add().RenderValueAs(o => (o.UnitProfit * o.SalesQuantity).ToString())
                .Titled("총수입금")
                .SetCrudHidden(true)
                .Format("{0:0.##}"); ;




            /* Adding "ItemName" column: */
            c.Add(o => o.Comment).Titled("비고");




        };

        public static Action<IGridColumnCollection<Revenue>, string> RevenueColumns = (c, path) =>
        {
            c.Add(o => o.SalesOrderID, true).Titled("주문")
             .SetSelectField(true, o => o.SalesOrder.SalesOrderNo + "-" + o.SalesOrder.Customer.Name, path + $"api/SalesOrderExtends/GetAllSalesOrderForSelect")
             .SetReadOnlyOnUpdate(true);

            c.Add(o => o.SalesOrder.SalesOrderNo).Titled("주문번호")
            .SetWidth(100).SetCrudHidden(true).SetReadOnlyOnUpdate(true);            

            c.Add(o => o.SalesOrder.SalesDate).Titled("주문일자")
            .SetWidth(100).Format("{0:yyyy-MM-dd}").SetCrudHidden(true).SetReadOnlyOnUpdate(true);

            c.Add(o => o.SalesOrder.Customer.Name).Titled("고객")
            .SetWidth(100).SetCrudHidden(true).SetReadOnlyOnUpdate(true);

            c.Add(o => o.ID).SetPrimaryKey(true).Titled("매출순번").SetWidth(20);

            /* Adding "Supplier" column: */
            c.Add(o => o.RevenueDate).Titled("매출일자")
                .SetInputType(InputType.Date)
                .Format("{0:yyyy-MM-dd}")
                .SetWidth(120);

            /* Adding "Address" column: */
            c.Add(o => o.RevenueCost)
                .Titled("결제금액")
                .SetWidth(100)
                .Format("{0:0.##}");
        };
    }
}
