using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefactoringChallenge.API.Controllers.Base;
using RefactoringChallenge.Business.DTO;
using RefactoringChallenge.Business.Exceptions;
using RefactoringChallenge.Business.Services;
using System.Collections.Generic;

namespace RefactoringChallenge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : APIController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get order paging result
        /// </summary>
        /// <param name="skip">The number of records in previous page</param>
        /// <param name="take">The page size</param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(int? skip = null, int? take = null)
        {
            var result = _orderService.GetPagingResult(skip, take);
            return Ok(Make(result));
        }

        ///<summary>
        /// Get order details by order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("{orderId}")]
        public IActionResult GetById([FromRoute] int orderId)
        {
            try
            {
                var result = _orderService.GetById(orderId);
                return Ok(Make(result));
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create an order
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult Create(CreateOrderRequest request)
        {
            var result = _orderService.Create(request);
            return Ok(Make(result));
        }

        /// <summary>
        /// Add product to an order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderDetails"></param>
        /// <returns></returns>
        [HttpPost("{orderId}/products")]
        public IActionResult AddProductsToOrder([FromRoute] int orderId, IEnumerable<OrderDetailRequest> orderDetails)
        {
            try
            {
                var result = _orderService.AddProductsToOrder(orderId, orderDetails);
                return Ok(Make(result));
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete an order by order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpDelete("{orderId}")]
        public IActionResult Delete([FromRoute] int orderId)
        {
            try
            {
                var result = _orderService.Delete(orderId);
                return Ok(Make(result));
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
