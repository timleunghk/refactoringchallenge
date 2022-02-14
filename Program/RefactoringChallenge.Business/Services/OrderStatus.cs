using AutoMapper;
using RefactoringChallenge.Business.DTO;
using RefactoringChallenge.Business.Exceptions;
using RefactoringChallenge.Business.Extensions;
using RefactoringChallenge.Business.Paging;
using RefactoringChallenge.Data.Enities;
using RefactoringChallenge.Data.Contexts;
using System.Collections.Generic;
using System.Linq;
using System;

namespace RefactoringChallenge.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly NorthwindDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderService(NorthwindDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<OrderDetailResponse> AddProductsToOrder(int orderId, IEnumerable<OrderDetailRequest> orderDetails)
        {
            var order = GetOrderEntity(orderId);
            var newOrderDetails = new List<OrderDetail>();
            foreach (var orderDetail in orderDetails)
            {
                newOrderDetails.Add(new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = orderDetail.ProductId,
                    Discount = orderDetail.Discount,
                    Quantity = orderDetail.Quantity,
                    UnitPrice = orderDetail.UnitPrice,
                });
            }

            _dbContext.OrderDetails.AddRange(newOrderDetails);
            _dbContext.SaveChanges();

            return newOrderDetails.Select(od => _mapper.Map<OrderDetailResponse>(od));
        }

        public OrderResponse Create(CreateOrderRequest request)
        {
            var newOrderDetails = new List<OrderDetail>();
            foreach (var orderDetail in request.OrderDetails)
            {
                newOrderDetails.Add(new OrderDetail
                {
                    ProductId = orderDetail.ProductId,
                    Discount = orderDetail.Discount,
                    Quantity = orderDetail.Quantity,
                    UnitPrice = orderDetail.UnitPrice,
                });
            }

            var newOrder = new Order
            {
                CustomerId = request.CustomerId,
                EmployeeId = request.EmployeeId,
                OrderDate = DateTime.Now,
                RequiredDate = request.RequiredDate,
                ShipVia = request.ShipVia,
                Freight = request.Freight,
                ShipName = request.ShipName,
                ShipAddress = request.ShipAddress,
                ShipCity = request.ShipCity,
                ShipRegion = request.ShipRegion,
                ShipPostalCode = request.ShipPostalCode,
                ShipCountry = request.ShipCountry,
                OrderDetails = newOrderDetails,
            };
            _dbContext.Orders.Add(newOrder);
            _dbContext.SaveChanges();

            return _mapper.Map<OrderResponse>(newOrder);
        }

        public int Delete(int orderId)
        {
            var order = GetOrderEntity(orderId);
            var orderDetails = _dbContext.OrderDetails.Where(od => od.OrderId == orderId);

            _dbContext.OrderDetails.RemoveRange(orderDetails);
            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();

            return orderId;
        }

        public OrderResponse GetById(int orderId)
        {
            var order = GetOrderEntity(orderId);
            return _mapper.Map<OrderResponse>(order);
        }

        public PagingResult<OrderResponse> GetPagingResult(int? skip = null, int? take = null)
        {
            var paging = _dbContext.Orders.ToPagingResult(skip, take);
            return paging.MapTo(data => _mapper.Map<OrderResponse>(data));
        }

        private Order GetOrderEntity(int orderId)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                throw new EntityNotFoundExeption("Order");
            return order;
        }
    }
}
