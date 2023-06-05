using BusinessObjects.Models;
using DataAccess.Dto;

namespace DataAccess.IRepository
{
    public interface IOrderRepository
    {
        void SaveOrder(OrderRequestDto p);
        Order GetOrderById(int id);
        void DeleteOrder(Order p);
        void UpdateOrder(OrderUpdateRequestDto p);
        List<Order>? GetOrders(string? keyword);
    }
}
