using BusinessObjects.Models;
using DataAccess.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDAO
    {
        public static List<Order>? GetOrders(string? keyword)
        {
            List<Order>? listOrders = null;
            try
            {
                using (var context = new PRN231_AS1Context())
                { 
                    if(keyword == null) listOrders = context.Orders.Include(i => i.Member).AsNoTracking().ToList();
                    else listOrders = context.Orders.Include(i => i.Member).Where(i => !string.IsNullOrEmpty(i.Member.CompanyName) && i.Member.CompanyName.ToLower().Contains(keyword.ToLower())).AsNoTracking().ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listOrders;
        }

        public static Order FindOrderById(int prodId)
        {
            Order p = new Order();
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    p = context.Orders.Include(i => i.Member).AsNoTracking().FirstOrDefault(x => x.OrderId == prodId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;
        }

        public static void SaveOrder(OrderRequestDto p)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var order = new Order()
                    {
                        MemberId = p.MemberId,
                        OrderDate = p.OrderDate,
                        RequireDate = p.RequireDate,
                        ShippedDate = p.ShippedDate,
                        Freight = p.Freight
                    };
                    context.Orders.Add(order);
                    context.SaveChanges();

                    var orderDetail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        ProductId = p.ProductId,
                        UnitPrice = p.UnitPrice,
                        Quantity = p.Quantity,
                        Discount = p.Discount
                    };
                    context.OrderDetails.Add(orderDetail);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateOrder(OrderUpdateRequestDto p)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var o = new Order()
                    {
                        OrderId = p.OrderId,
                        OrderDate = p.OrderDate,
                        RequireDate = p.RequireDate,
                        ShippedDate = p.ShippedDate,
                        Freight = p.Freight
                    };
                    context.Entry<Order>(o).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteOrder(Order p)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var p0 = context.OrderDetails.SingleOrDefault(
                                               c => c.OrderId == p.OrderId);
                    context.OrderDetails.Remove(p0);
                    context.SaveChanges();

                    var p1 = context.Orders.SingleOrDefault(
                        c => c.OrderId == p.OrderId);
                    context.Orders.Remove(p1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
