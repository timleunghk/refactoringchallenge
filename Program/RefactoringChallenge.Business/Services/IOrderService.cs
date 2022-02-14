using RefactoringChallenge.Business.DTO;
using RefactoringChallenge.Business.Paging;
using System.Collections.Generic;

namespace RefactoringChallenge.Business.Services
{
    public interface IOrderService
    {
        PagingResult<OrderResponse> GetPagingResult(int? skip = null, int? take = null);
        OrderResponse GetById(int orderId);
        OrderResponse Create(CreateOrderRequest request);
        IEnumerable<OrderDetailResponse> AddProductsToOrder(int orderId, IEnumerable<OrderDetailRequest> orderDetails);
        int Delete(int orderId);
    }
}
