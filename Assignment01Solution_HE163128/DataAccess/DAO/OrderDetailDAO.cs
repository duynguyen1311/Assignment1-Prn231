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
    public class OrderDetailDAO
    {
        public static OrderDetail FindOrderDetailById(int orderId)
        {
            OrderDetail p = new OrderDetail();
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    p = context.OrderDetails.Include(i => i.Product).ThenInclude(i => i.Category).AsNoTracking().FirstOrDefault(x => x.OrderId == orderId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;
        }

        public static void UpdateOrderDetail(OrderDetailUpdateRequestDto p)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var od2 = context.OrderDetails.FirstOrDefault(i => i.OrderId == p.OrderId);
                    if (od2 == null)
                    {
                        throw new Exception("OrderDetail not found");
                    }
                    od2.UnitPrice = p.UnitPrice;
                    od2.Quantity = p.Quantity;
                    od2.Discount = p.Discount;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<OrderDetail> ReportOrder(DateTime? fromDate, DateTime? toDate)
        {
            List<OrderDetail>? listOrderDetail = null;
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var query = context.OrderDetails.AsQueryable();
                    if (fromDate != null && toDate != null)
                    {
                        query = query.Where(i => i.Order.OrderDate >= fromDate && i.Order.OrderDate <= toDate);
                    }
                    listOrderDetail = query.Include(i => i.Order).ThenInclude(i => i.Member)
                        .Include(i => i.Product).OrderByDescending(i => i.OrderId).AsNoTracking().ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listOrderDetail;
        }
    }
}
