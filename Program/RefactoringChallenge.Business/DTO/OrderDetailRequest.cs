﻿namespace RefactoringChallenge.Business.DTO
{
    public class OrderDetailRequest
    {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
    }
}