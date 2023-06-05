using BusinessObjects.Models;
using DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IOrderDetailRepository
    {
        OrderDetail FindOrderDetailById(int orderId);
        void UpdateOrderDetail(OrderDetailUpdateRequestDto p);
        List<OrderDetail> ReportOrder(DateTime? fromDate, DateTime? toDate);
    }
}
