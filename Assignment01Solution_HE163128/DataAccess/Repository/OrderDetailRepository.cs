using BusinessObjects.Models;
using DataAccess.DAO;
using DataAccess.Dto;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public OrderDetail FindOrderDetailById(int orderId) => OrderDetailDAO.FindOrderDetailById(orderId);

        public void UpdateOrderDetail(OrderDetailUpdateRequestDto p) => OrderDetailDAO.UpdateOrderDetail(p);
    }
}
