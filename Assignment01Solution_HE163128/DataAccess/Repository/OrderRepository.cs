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
    public class OrderRepository : IOrderRepository
    {
        public void DeleteOrder(Order p) => OrderDAO.DeleteOrder(p);
        public void SaveOrder(OrderRequestDto p) => OrderDAO.SaveOrder(p);
        public void UpdateOrder(OrderUpdateRequestDto p) => OrderDAO.UpdateOrder(p);
        public List<Category> GetCategories() => CategoryDAO.GetCategories();
        public List<Order>? GetOrders(string? keyword) => OrderDAO.GetOrders(keyword);
        public Order GetOrderById(int id) => OrderDAO.FindOrderById(id);
    }
}
